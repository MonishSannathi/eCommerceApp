using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Ecommerce.BusinessLayer.Interfaces
{
    internal interface ILocalFileOperations:IFileOperations
    {
        Boolean CheckFileName();
        Boolean CheckFileExtension(string contentType);
        Boolean CheckFileSize(int contentLength);

        void SetFileDetails(string path);

    }
}
