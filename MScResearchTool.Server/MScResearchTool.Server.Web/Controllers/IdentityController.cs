using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MScResearchTool.Server.Core.Businesses;
using MScResearchTool.Server.Core.Enums;
using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Web.Factories;
using MScResearchTool.Server.Web.Helpers;
using MScResearchTool.Server.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MScResearchTool.Server.Web.Controllers
{
    public class IdentityController : Controller
    {
        private ISession _session => HttpContext.Session;
        private IUsersBusiness _usersBusiness { get; set; }
        private HashHelper _hashHelper { get; set; }
        private UserFactory _userFactory { get; set; }
        private int _remainingLoginAttempts;

        public IdentityController(IUsersBusiness usersBusiness, HashHelper hashHelper, UserFactory userFactory)
        {
            _usersBusiness = usersBusiness;
            _hashHelper = hashHelper;
            _userFactory = userFactory;

            _remainingLoginAttempts = 3;
        }

        public IActionResult Index()
        {
            ViewData[EWebKeyValues.LoginFailed.ToString()] = null;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(UserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var attempts = 0;

            if (_session.GetInt32(EWebKeyValues.LoginAttempts.ToString()) != null)
            {
                attempts = _session.GetInt32(EWebKeyValues.LoginAttempts.ToString()).Value;

                if (attempts < 1)
                {
                    return Content("Access denied.");
                }
            }

            else attempts = _remainingLoginAttempts;

            var checkedUser = await _usersBusiness.ReadUserByNameOnlyAsync(userViewModel.Name);

            if (checkedUser == null)
            {
                var loginStatus = "Wrong username or password provided.";
                ViewData[EWebKeyValues.LoginFailed.ToString()] = loginStatus;

                _session.SetInt32(EWebKeyValues.LoginAttempts.ToString(), attempts - 1);

                return View();
            }

            userViewModel.Password = _hashHelper.HashSequence(userViewModel.Password, checkedUser.Salt);

            var user = await _usersBusiness.ReadUserByNameAndPasswordAsync(userViewModel.Name, userViewModel.Password);

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name, ClaimValueTypes.String),
                };

                var userIdentity = new ClaimsIdentity(claims, EWebKeyValues.UserData.ToString());
                var userPrincipal = new ClaimsPrincipal(userIdentity);

                await HttpContext.SignInAsync(userPrincipal,
                    new AuthenticationProperties
                    {
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(30),
                        IsPersistent = false,
                        AllowRefresh = true
                    });

                attempts = _remainingLoginAttempts;
                _session.Clear();

                return RedirectToAction("Index", "Reports");
            }

            else
            {
                var loginStatus = "Wrong username or password provided.";
                ViewData[EWebKeyValues.LoginFailed.ToString()] = loginStatus;

                _session.SetInt32(EWebKeyValues.LoginAttempts.ToString(), attempts - 1);

                return View();
            }
        }

        [Route("Api/CreateUser")]
        [HttpPost]
        public async Task<IActionResult> RemoteRegistration([FromBody] User user)
        {
            var areUsersAlready = true;

            areUsersAlready = await _usersBusiness.AreUsersInDatabaseAsync();

            if (areUsersAlready)
                return NotFound();

            var userToSave = _userFactory.Create(user.Name, user.Password);

            await _usersBusiness.CreateNewUserAsync(userToSave);

            return Ok();
        }

        [Authorize]
        public ActionResult Registration()
        {
            ViewData[EWebKeyValues.RegistrationResult.ToString()] = null;

            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(string username, string pass, string passwordVerification)
        {
            ViewData[EWebKeyValues.RegistrationResult.ToString()] = null;

            var regStatus = "";

            if (pass != passwordVerification)
            {
                regStatus = "Given passwords are not the same.";
                ViewData[EWebKeyValues.RegistrationResult.ToString()] = regStatus;

                return View();
            }

            var isExisting = await _usersBusiness.IsUsernameTakenAsync(username);

            if(isExisting)
            {
                regStatus = "Username already taken. Please try again.";
                ViewData[EWebKeyValues.RegistrationResult.ToString()] = regStatus;

                return View();
            }

            var user = _userFactory.Create(username, pass);

            await _usersBusiness.CreateNewUserAsync(user);

            regStatus = "User created succesfully.";
            ViewData[EWebKeyValues.RegistrationResult.ToString()] = regStatus;

            return View();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index");
        }
    }
}
