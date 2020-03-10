using CommandLine;

namespace ConsoleClient.Options
{
    [Verb("sort", HelpText = "sort users by specific parameter in Database with choice decending mode in format: field(-f) - desending(-d).")]
    public class SortOptions
    {
        [Option('f', "field", Required = true,
            HelpText = "Input field.")]
        public string SortConditionField { get; set; }
        
        [Option('d', "descending", Required = true,
            HelpText = "Input value.")]
        public bool IsDescending { get; set; }
        
        
        
        
        
    }
}