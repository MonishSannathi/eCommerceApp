using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.BusinessLayer.Interfaces
{
    internal interface IEncryption
    {
        byte[] EncryptFile(byte[] filedata);

        byte[] DecryptFile(byte[] filedata);
    }
}
