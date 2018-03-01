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
            var memory = new MemoryStream();
            var fileInfo = new FileInfo(path);
            var extension = fileInfo.Extension.ToLower().Trim('.');
            using (var fileStream = File.OpenRead(path))
            {
                await fileStream.CopyToAsync(memory);
                fileStream.Close();
            }
            memory.Position = 0;
            return controller.File(memory, MIME.GetContentType(extension, download), filename, fileInfo.LastWriteTime, new EntityTagHeaderValue("tag"));
        }
    }
}
