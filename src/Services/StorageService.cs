﻿using System;
using System.Collections.Generic;
using System.Text;
using static System.IO.Path;
using static System.IO.Directory;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Aiursoft.Pylon.Services.ToOSSServer;
using Aiursoft.Pylon.Models;

namespace Aiursoft.Pylon.Services
{
    public static class StorageService
    {
        public static async Task<string> SaveLocally(IFormFile File, SaveFileOptions options = SaveFileOptions.RandomName, string name = "")
        {
            string directoryPath = GetCurrentDirectory() + DirectorySeparatorChar + $@"Storage" + DirectorySeparatorChar;
            if (Exists(directoryPath) == false)
            {
                CreateDirectory(directoryPath);
            }
            string localFilePath = string.Empty;
            if (options == SaveFileOptions.RandomName)
            {
                localFilePath = directoryPath + StringOperation.RandomString(10) + GetExtension(File.FileName);
            }
            else if (options == SaveFileOptions.SourceName)
            {
                localFilePath = directoryPath + File.FileName.Replace(" ", "_");
            }
            else
            {
                localFilePath = directoryPath + name;
            }
            var fileStream = new FileStream(localFilePath, FileMode.Create);
            await File.CopyToAsync(fileStream);
            fileStream.Close();
            return localFilePath;
        }
        public static async Task<string> SaveToOSS(IFormFile File, int BucketId, SaveFileOptions options = SaveFileOptions.RandomName, string AccessToken = null, string name = "")
        {
            string localFilePath = await SaveLocally(File, options, name);
            if (AccessToken == null)
            {
                AccessToken = await AppsContainer.AccessToken()();
            }
            var fileAddress = await ApiService.UploadFile(AccessToken, BucketId, localFilePath);
            return fileAddress.Path;
        }
    }
    public enum SaveFileOptions
    {
        RandomName,
        SourceName,
        TargetName
    }
}