using CommandLine;
using Newtonsoft.Json;

namespace ConsoleClient
{
    [JsonObject]
    public class DeleteInfo
    {
        
        [Option('p', "phonenumber", Required = true,
            HelpText = "Input user phone number.")]
        public string PhoneNumber { get; set; }  
        
        public string DeletedField { get; set; }
        public string DeletedValue { get; set; }
    }
}