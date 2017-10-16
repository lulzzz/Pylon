using AiursoftBase.Exceptions;
using AiursoftBase.Models;
using AiursoftBase.Models.MessageQueue.ChannelAddressModels;
using AiursoftBase.Models.MessageQueue.ChannelViewModels;
using AiursoftBase.Models.MessageQueue.ListenAddressModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AiursoftBase.Services.ToMessageQueueServer
{
    public class ChannelService
    {
        public static async Task<CreateChannelViewModel> CreateChannelAsync(string AccessToken, string Description)
        {
            var httpContainer = new HTTPService();
            var url = new AiurUrl(Values.MessageQueueServerAddress, "Channel", "CreateChannel", new { });
            var form = new AiurUrl(string.Empty, new CreateChannelAddressModel
            {
                AccessToken = AccessToken,
                Description = Description
            });
            var result = await httpContainer.Post(url, form);
            var jResult = JsonConvert.DeserializeObject<CreateChannelViewModel>(result);
            if (jResult.code != ErrorType.Success)
                throw new AiurUnexceptedResponse(jResult);
            return jResult;
        }

        public static async Task<AiurProtocal> ValidateChannelAsync(int Id, string Key)
        {
            var httpContainer = new HTTPService();
            var url = new AiurUrl(Values.MessageQueueServerAddress, "Channel", "ValidateChannel", new ChannelAddressModel
            {
                Id = Id,
                Key = Key
            });
            var result = await httpContainer.Get(url);
            var jResult = JsonConvert.DeserializeObject<AiurProtocal>(result);
            return jResult;
        }
    }
}
