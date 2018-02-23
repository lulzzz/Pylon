using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Aiursoft.Pylon.Services
{
    public class AiurSMSSender
    {
        public static string SMSAccountIdentification { get; set; }
        public static string SMSAccountPassword { get; set; }
        public static string SMSAccountFrom { get; set; }
        public AiurSMSSender()
        {

        }

        public Task<MessageResource> SendAsync(string number, string message)
        {
            TwilioClient.Init(SMSAccountIdentification, SMSAccountPassword);
            return MessageResource.CreateAsync(
              to: new PhoneNumber(number),
              from: new PhoneNumber(SMSAccountFrom),
              body: message);
        }
    }
}
