using Aiursoft.Pylon.Exceptions;
using Aiursoft.Pylon.Models;
using Aiursoft.Pylon.Models.MessageQueue.MessageAddressModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aiursoft.Pylon.Services.ToMessageQueueServer
{
    public class MessageService
    {
        public static async Task<AiurProtocal> PushMessageAsync(string AccessToken, int ChannelId, string MessageContent, bool noexception = false)
        {
            var httpContainer = new HTTPService();
            var url = new AiurUrl(Values.MessageQueueServerAddress, "Message", "PushMessage", new { });
            var form = new AiurUrl(string.Empty, new PushMessageAddressModel
            {
                AccessToken = AccessToken,
                ChannelId = ChannelId,
                MessageContent = MessageContent
            });
            var result = await httpContainer.Post(url, form);
            var jResult = JsonConvert.DeserializeObject<AiurProtocal>(result);
            if (!noexception && jResult.code != ErrorType.Success)
            {
                throw new AiurUnexceptedResponse(jResult);
            }
            return jResult;
        }
    }
}
