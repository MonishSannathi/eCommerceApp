using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.BusinessLayer.Interfaces
{
    internal interface IEncryption
    {
        void Encrypt(string inputPath, string outputPath);

        void Decrypt(string inputPath, string outputPath);
    }
}
