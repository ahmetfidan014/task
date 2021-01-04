using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Task.BLL.Tools
{
    public class CaptchaResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }
    }
    public class validateCaptcha
    {
        public static bool validateC(string response)
        {
            const string secret = "6LexUboUAAAAAKl2fgQDnY_V1GSY0qmTaPSGeRQP";

            var client = new WebClient();
            var reply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));

            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);
            return captchaResponse.Success;
        }
    }
}