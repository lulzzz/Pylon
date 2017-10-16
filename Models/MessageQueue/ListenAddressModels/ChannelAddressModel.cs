using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace AiursoftBase.Models.MessageQueue.ListenAddressModels
{
    public class ChannelAddressModel
    {
        public int Id { get; set; }
        public string Key { get; set; }
    }
}
