using SharpCompress.Archives;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7ZipDecoder
{
    static class SevenZipOperator
    {
        public static bool DecryptFile(string password)
        {
            try
            {
                using (var archive = SharpCompress.Archives.SevenZip.SevenZipArchive.
                    Open(@"C:\Users\filip.zabnicki\source\repos\.NET_7Zzip_Decoder\FilipZ.7z",
                    new SharpCompress.Readers.ReaderOptions() { Password = password, LookForHeader = false }))
                {
                    foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
                    {
                        entry.WriteToDirectory("C:\\Decrypted_7Zip", new ExtractionOptions()
                        {
                            ExtractFullPath = true,
                            Overwrite = true
                        });
                    }
                }
                Console.WriteLine($"File unlocked with password: {password}");
                return true;
            }
            catch (InvalidFormatException ex)
            {
                ShowMessage("Something wrong", password);
            }
            catch (PasswordProtectedException ex)
            {
                ShowMessage("Password wrong", password);
            }
            catch (Exception ex)
            {
                ShowMessage("Password wrong", password);
            }
            return false;
        }
        [Conditional("DEBUG")]
        private static void ShowMessage(string message, string password)
        {
            Console.WriteLine($"{message}: {password}");
        }
    }
}
