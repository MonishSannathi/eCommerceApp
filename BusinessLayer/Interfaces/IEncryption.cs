

namespace Ecommerce.BusinessLayer.Interfaces
{
    internal interface IEncryption
    {
        void Encrypt(string inputPath, string outputPath);

        void Decrypt(string inputPath, string outputPath);
    }
}
