using System;
using System.Collections.Generic;
using System.Text;
using static System.IO.Path;
using static System.IO.Directory;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using AiursoftBase.Services.ToOSSServer;
using AiursoftBase.Models;

namespace AiursoftBase.Services
{
    public static class StorageService
    {
        public static async Task<string> SaveLocally(IFormFile File, SaveFileOptions options = SaveFileOptions.RandomName)
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
            else
            {
                localFilePath = directoryPath + File.FileName.Replace(" ","_");
            }
            var fileStream = new FileStream(localFilePath, FileMode.Create);
            await File.CopyToAsync(fileStream);
            fileStream.Close();
            return localFilePath;
        }
        public static async Task<string> SaveToOSS(IFormFile File, int BucketId, SaveFileOptions options = SaveFileOptions.RandomName, string AccessToken = null)
        {
            string localFilePath = await SaveLocally(File, options);
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
        SourceName
    }
}
