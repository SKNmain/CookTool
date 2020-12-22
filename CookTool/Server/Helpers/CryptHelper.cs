using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CookTool.Server.Helpers
{
    public class CryptHelper
    {
        private static readonly byte[] entropy = Encoding.Unicode.GetBytes("PepperSalt");

        public static string Encrypt(string password)
        {
            var encryptedData = ProtectedData.Protect(
                Encoding.Unicode.GetBytes(password),
                entropy,
                DataProtectionScope.CurrentUser);

            return Convert.ToBase64String(encryptedData);
        }

        public static string Decrypt(string password)
        {
            var decryptedData = ProtectedData.Unprotect(
                    Convert.FromBase64String(password),
                    entropy,
                    DataProtectionScope.CurrentUser);

            return Encoding.Unicode.GetString(decryptedData);
        }
    }
}
