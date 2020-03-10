using CommandLine;

namespace ConsoleClient.Options
{
    [Verb("select", HelpText = "select user with specific parameter from Database in format: field(-f) - value(-v).")]
    public class SelectOptions
    {
        [Option('f', "field", Required = true,
            HelpText = "Input field.")]
        public string SelectConditionField { get; set; }
        
        [Option('v', "value", Required = true,
            HelpText = "Input value.")]
        public string SelectConditionValue { get; set; }
    }
}