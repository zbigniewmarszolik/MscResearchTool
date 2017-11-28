using System.Collections.Generic;
using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Core.Repositories;
using MScResearchTool.Server.Infrastructure.Connectors;
using System;
using NHibernate.Linq;
using System.Linq;

namespace MScResearchTool.Server.Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        public void CreateUser(User user)
        {
            try
            {
                using (var session = FluentNHibernateConnector.OpenSession())
                {
                    session.Save(user);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<User> ReadUsers()
        {
            IList<User> users;

            try
            {
                using (var session = FluentNHibernateConnector.OpenSession())
                {
                    users = session.Query<User>().ToList();
                }
            }
            catch (Exception ex)
            {
                users = null;
                throw ex;
            }

            return users;
        }
    }
}
