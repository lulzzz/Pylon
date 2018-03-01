﻿using Aiursoft.Pylon.Models;
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
            using (var memory = new MemoryStream())
            {
                using (var fileStream = File.OpenRead(path))
                {
                    await fileStream.CopyToAsync(memory);
                    fileStream.Close();
                }
                memory.Position = 0;
                if (download)
                {
                    return controller.File(memory, MIME.GetContentType(extension, download), filename);
                }
                else
                {
                    return controller.File(memory, MIME.GetContentType(extension, download));
                }
            }
#warning You did not use etag cache!
            //return controller.File(memory, MIME.GetContentType(extension, download), filename, fileInfo.LastWriteTime, new EntityTagHeaderValue("tag"));
        }
    }
}
