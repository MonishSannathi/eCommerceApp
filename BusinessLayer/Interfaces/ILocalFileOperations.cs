

namespace Ecommerce.BusinessLayer.Interfaces
{
    internal interface ILocalFileOperations:IFileOperations
    {
        void EncryptFile(string inputFilePath, string outputFilePath);

        void DecryptFile(string inputFilePath, string outputFilePath);

        string GetEncryptedFilePath();

        string GetDecryptedFilePath();
    }
}
