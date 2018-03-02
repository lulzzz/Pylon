using Aiursoft.Pylon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Aiursoft.Pylon.Services
{
    public static class FileService
    {
        public static async Task<FileStreamResult> AiurFile(this ControllerBase controller, string path, string filename, bool download = false)
        {
            var fileInfo = new FileInfo(path);
            var extension = filename.Substring(filename.LastIndexOf('.') + 1);
            var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 16 * 1024 * 1024);
            if (download)
            {
                return controller.File(fileStream, MIME.GetContentType(extension, download), filename);
            }
            else
            {
                return controller.File(fileStream, MIME.GetContentType(extension, download));
            }
        }
    }
}
