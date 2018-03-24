using ICSharpCode.SharpZipLib.Zip;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace MScResearchTool.Server.Web.Helpers
{
    public class CrackingInitializationHelper
    {
        public byte[] ProcessFormFileToArray(IFormFile formFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                formFile.CopyTo(memoryStream);

                return memoryStream.ToArray();
            }
        }

        public bool IsArchiveExtractable(byte[] arrayFile, string providedPassword) // TO DO (SharpZipLib test)
        {
            ZipFile zipFile = null;

            try
            {
                zipFile = new ZipFile(new MemoryStream(arrayFile));
                zipFile.Password = providedPassword;

                foreach(ZipEntry entry in zipFile)
                {
                    if(!entry.IsFile)
                    {
                        continue;
                    }

                    var entryName = entry.Name;
                    var buffer = new byte[4096];
                    var zipStream = zipFile.GetInputStream(entry);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
