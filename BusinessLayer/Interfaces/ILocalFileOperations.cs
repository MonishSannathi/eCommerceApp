﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Ecommerce.BusinessLayer.Interfaces
{
    internal interface ILocalFileOperations:IFileOperations
    {
        void SetFileDetails(string path);

        void EncryptFile(string inputFilePath, string outputFilePath);

        void DecryptFile(string inputFilePath, string outputFilePath);
    }
}
