using CommandLine;

namespace ConsoleClient.Options
{
    
    [Verb("delete", HelpText = "delete user with a specific field in format: field(-f) - value(-v).")]
    public class DeleteOptions
    {
        [Option('f', "field", Required = true,
            HelpText = "Input c field.")]
        public string DeletedField { get; set; }
        
        [Option('v', "value", Required = true,
            HelpText = "Input user phone number.")]
        public string DeletedValue { get; set; }
    }
}