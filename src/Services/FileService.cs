﻿using Aiursoft.Pylon.Models;
using Microsoft.AspNetCore.Http.Extensions;
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
            await Task.Delay(0);
            var fileInfo = new FileInfo(path);
            var fileStream = File.OpenRead(path);
            var extension = filename.Substring(filename.LastIndexOf('.') + 1);
            long etagHash = fileInfo.LastWriteTime.ToUniversalTime().ToFileTime() ^ fileInfo.Length;
            var _etag = '\"' + Convert.ToString(etagHash, 16) + '\"';
            controller.Response.Headers.Add("ETag", _etag);
            if (controller.Request.Headers.Keys.Contains("If-None-Match") && controller.Request.Headers["If-None-Match"].ToString().Trim('"') == _etag)
            {
                return new StatusCodeResult(304);
            }
            controller.Response.Headers.Add("Content-Length", fileInfo.Length.ToString());
            //await StreamCopyOperation.CopyToAsync(fileStream, controller.Response.Body, fileInfo.Length, 64 * 1024, controller.HttpContext.RequestAborted);
            //controller.HttpContext.Abort();
            return controller.PhysicalFile(path, MIME.GetContentType(extension, download));
            //return controller.File(file, , filename);
        }
    }
}
