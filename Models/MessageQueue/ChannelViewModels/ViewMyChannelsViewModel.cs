using System;
using System.Collections.Generic;
using System.Text;

namespace AiursoftBase.Models.MessageQueue.ChannelViewModels
{
    public class ViewMyChannelsViewModel : AiurProtocal
    {
        public string AppId { get; set; }
        public IEnumerable<Channel> Channel { get; set; }
    }
}
