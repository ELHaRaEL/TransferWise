using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TransferWise
{
    internal class WiseRSA
    {
        private readonly RSA rSA = RSA.Create();
        public void SetRSAImportPrivateKey(string privateKeyPath)
        {
            try
            {
                string privateKey = File.ReadAllText(privateKeyPath);
                rSA.ImportFromPem(privateKey.ToCharArray());
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Could not find the private key file: " + privateKeyPath);
            }

        }

        public string SignData(string oneTimeTokenToSign)
        {
            byte[] signatureBytes = rSA.SignData(Encoding.UTF8.GetBytes(oneTimeTokenToSign), HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            return Convert.ToBase64String(signatureBytes);
        }

    }
}
