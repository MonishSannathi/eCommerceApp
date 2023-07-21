using System.Web;

namespace Ecommerce.BusinessLayer.Interfaces
{
    internal interface IFileOperations
    {
        string GetFilePath();
        void SaveFile(string path, HttpPostedFileBase file);
        bool DeleteFile(string path);
    }
}
