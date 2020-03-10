using CommandLine;

namespace ConsoleClient.Options
{
    [Verb("add", HelpText = "Add user to Database, all fields are required.")]
    public class InsertOptions
    {
        [Option('p', "phonenumber", Required = true,
            HelpText = "Input user phone number.")]
        public string PhoneNumber { get; set; }    
        
        [Option('n', "name", Required = true,
            HelpText = "Input user full name.")]
        public string FullName { get; set; }
        
        [Option('e', "email", Required = true,
            HelpText = "Input user email.")]
        public string Email { get; set; }
        
        [Option( "position", Required = true,
            HelpText = "Input user Position Held.")]
        public string PositionHeld { get; set; }
        
        [Option('a', "additionalnumbers", Required = true,
            HelpText = "Input user additional numbers.")]
        public string AdditionalNumbers { get; set; }
        
    }
}