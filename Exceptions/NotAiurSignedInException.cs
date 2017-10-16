using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AiursoftBase.Exceptions
{
    public class NotAiurSignedInException : Exception
    {
        public Controller controller { get; }
        public string SignInRedirectPath { get; }
        public bool? JustTry { get; set; }
        public NotAiurSignedInException(Controller controller,string successfulRedirectPath,bool? justTry)
        {
            this.controller = controller;
            this.SignInRedirectPath = successfulRedirectPath;
            this.JustTry = justTry;
        }
    }
}
