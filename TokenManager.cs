using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using System.IO;

namespace SHRD
{
    internal static class TokenManager
    {
        public static async Task Set(string token)
        {
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile f;
            if (File.Exists(Path.Combine(folder.Path, "SHRD-token.tkn")))
            {
                f = await folder.GetFileAsync("SHRD-token.tkn");
            }
            else
            {
                f = await folder.CreateFileAsync("SHRD-token.tkn", CreationCollisionOption.ReplaceExisting);
            }
            await FileIO.WriteTextAsync(f, token);
        }

        public static async Task<string> Get()
        {
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile f;
            if (!File.Exists(Path.Combine(folder.Path, "SHRD-token.tkn")))
            {
                throw new FileNotFoundException();
            }

            f = await folder.GetFileAsync("SHRD-token.tkn");
            return await FileIO.ReadTextAsync(f);
        }

        public static async Task Delete()
        {
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile f;
            if (!File.Exists(Path.Combine(folder.Path, "SHRD-token.tkn")))
            {
                throw new FileNotFoundException();
            }
            f = await folder.GetFileAsync("SHRD-token.tkn");
            await f.DeleteAsync();
        }
    }
}
