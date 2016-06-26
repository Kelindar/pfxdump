using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PfxDump
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length < 1)
            {
                WriteUsage();
                return;
            }

            var input = args[0];
            var output = (args.Length >= 2) ? args[1] : "output.txt";
                
            var x509 = new X509Certificate2(input, "", X509KeyStorageFlags.Exportable);
            Console.WriteLine("{0}Subject: {1}{0}", Environment.NewLine, x509.Subject);
            Console.WriteLine("{0}Issuer: {1}{0}", Environment.NewLine, x509.Issuer);
            Console.WriteLine("{0}Version: {1}{0}", Environment.NewLine, x509.Version);
            Console.WriteLine("{0}Valid Date: {1}{0}", Environment.NewLine, x509.NotBefore);
            Console.WriteLine("{0}Expiry Date: {1}{0}", Environment.NewLine, x509.NotAfter);
            Console.WriteLine("{0}Thumbprint: {1}{0}", Environment.NewLine, x509.Thumbprint);
            Console.WriteLine("{0}Serial Number: {1}{0}", Environment.NewLine, x509.SerialNumber);
            Console.WriteLine("{0}Friendly Name: {1}{0}", Environment.NewLine, x509.PublicKey.Oid.FriendlyName);
            Console.WriteLine("{0}Public Key Format: {1}{0}", Environment.NewLine, x509.PublicKey.EncodedKeyValue.Format(true));
            Console.WriteLine("{0}Raw Data Length: {1}{0}", Environment.NewLine, x509.RawData.Length);
            Console.WriteLine("{0}Certificate to string: {1}{0}", Environment.NewLine, x509.ToString(true));


            // Encode and write out
            var encoded = Convert.ToBase64String(x509.Export(X509ContentType.Pfx));
            File.WriteAllText(output, encoded);
        }

        /// <summary>
        /// Writes the usage.
        /// </summary>
        static void WriteUsage()
        {
            Console.WriteLine("Usage: pfxdump [input*] [output]");
            Console.WriteLine("   [input*]: (required) The path to the pfx file that needs to be converted.");
            Console.WriteLine("   [output]: (optional) The path to the output that should be written.");
        }
    }
}
