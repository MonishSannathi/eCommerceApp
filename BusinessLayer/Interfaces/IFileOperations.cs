﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.BusinessLayer.Interfaces
{
    internal interface IFileOperations
    {
        FileResult GetFile();
        string GetAbsoluteFilePath();

        string GetRelativeFilePath();
        void SaveFile(string path, HttpPostedFileBase file);

        string GetEncryptedFilePath();

        string GetDecryptedFilePath();

        string GetDecryptedLocalFilePath();

        bool DeleteFile(string path);
    }
}
