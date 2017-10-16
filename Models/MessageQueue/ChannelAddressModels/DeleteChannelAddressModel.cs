using System;
using System.Collections.Generic;
using System.Text;

namespace AiursoftBase.Models.MessageQueue.ChannelAddressModels
{
    public class DeleteChannelAddressModel
    {
        public string AccessToken { get; set; }
        public int ChannelId { get; set; }
    }
}
