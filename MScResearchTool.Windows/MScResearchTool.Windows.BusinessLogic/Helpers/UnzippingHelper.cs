using ICSharpCode.SharpZipLib.Zip;
using System;
using System.IO;

namespace MScResearchTool.Windows.BusinessLogic.Helpers
{
    public class UnzippingHelper
    {
        public bool IsArchiveExtractableWithPassword(byte[] archive, string passwordToValidate)
        {
            ZipFile zipFile = null;

            try
            {
                zipFile = new ZipFile(new MemoryStream(archive));
                zipFile.Password = passwordToValidate;

                foreach (ZipEntry entry in zipFile)
                {
                    if (!entry.IsFile)
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
