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
        public static async Task<IActionResult> AiurFile(this ControllerBase controller, string path, string filename, bool download = false)
        {
            var fileInfo = new FileInfo(path);
            var extension = filename.Substring(filename.LastIndexOf('.') + 1);
            var memory = new MemoryStream();
            using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                await fileStream.CopyToAsync(memory);
            }
            memory.Seek(0, SeekOrigin.Begin);
            controller.Response.OnCompleted((state) =>
            {
                (state as MemoryStream)?.Dispose();
                return Task.CompletedTask;
            }, memory);
            if (download)
            {
                return controller.File(memory, MIME.GetContentType(extension, download), filename);
            }
            else
            {
                return controller.File(memory, MIME.GetContentType(extension, download));
            }
        }
    }
}
