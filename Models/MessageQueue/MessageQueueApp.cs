using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AiursoftBase.Models.MessageQueue
{
    public class MessageQueueApp
    {
        [Key]
        public string Id { get; set; }

        [JsonIgnore]
        [InverseProperty(nameof(Channel.App))]
        public List<Channel> Channels { get; set; }
    }
}
