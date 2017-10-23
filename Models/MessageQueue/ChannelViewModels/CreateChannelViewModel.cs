using System;
using System.Collections.Generic;
using System.Text;

namespace Aiursoft.Pylon.Models.MessageQueue.ChannelViewModels
{
    public class CreateChannelViewModel : AiurProtocal
    {
        public int ChannelId { get; set; }
        public string ConnectKey { get; set; }
    }
}
