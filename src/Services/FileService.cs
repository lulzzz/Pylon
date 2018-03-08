using Aiursoft.Pylon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
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
            byte[] file = null;
            await Task.Run(() =>
            {
                file = File.ReadAllBytes(path);
            });
            controller.Response.OnCompleted((_) =>
            {
                file = null;
                return Task.CompletedTask;
            }, null);
            var etag = ETagGenerator.GetETag(controller.Request.Path.ToString(), file);
            controller.Response.Headers.Add("ETag", etag);
            if (controller.Request.Headers.Keys.Contains("If-None-Match") && controller.Request.Headers["If-None-Match"].ToString() == etag)
            {
                return new StatusCodeResult(304);
            }
            controller.Response.Headers.Add("Content-Length", file.Length.ToString());
            if (download)
            {
                return controller.File(file, MIME.GetContentType(extension, download), filename);
            }
            else
            {
                return controller.File(file, MIME.GetContentType(extension, download));
            }
        }
    }
}
