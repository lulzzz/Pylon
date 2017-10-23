using Aiursoft.Pylon.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Aiursoft.Pylon.Services
{
    public class HTTPService
    {
        public CookieContainer CC = new CookieContainer();
        public async Task<string> Post(AiurUrl Url, AiurUrl postDataStr)
        {
            var request = WebRequest.CreateHttp(Url.ToString());

            request.CookieContainer = CC;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            var myRequestStream = await request.GetRequestStreamAsync();
            var myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("utf-8"));
            await myStreamWriter.WriteAsync(postDataStr.ToString().Trim('?'));
            myStreamWriter.Dispose();
            myRequestStream.Dispose();

            var response = await request.GetResponseAsync();
            var myResponseStream = response.GetResponseStream();
            var myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = await myStreamReader.ReadToEndAsync();
            myStreamReader.Dispose();
            myResponseStream.Dispose();
            return retString;
        }

        public async Task<string> PostFile(AiurUrl Url, string filepath)
        {
            var file = new FileInfo(filepath);
            var fileStream = new FileStream(filepath, mode: FileMode.Open);
            var request = new HttpClient();
            var form = new MultipartFormDataContent();
            form.Add(new StreamContent(fileStream), "file", file.FullName);
            var response = await request.PostAsync(Url.ToString(), form);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> Get(AiurUrl Url)
        {
            var request = WebRequest.CreateHttp(Url.ToString());
            request.CookieContainer = CC;
            request.Method = "GET";
            request.ContentType = "text/html;charset=utf-8";
            var response = await request.GetResponseAsync();
            var myResponseStream = response.GetResponseStream();
            var myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = await myStreamReader.ReadToEndAsync();
            myStreamReader.Dispose();
            myResponseStream.Dispose();
            return retString;
        }
    }
}
