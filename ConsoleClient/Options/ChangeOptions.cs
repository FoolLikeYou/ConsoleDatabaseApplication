using CommandLine;

namespace ConsoleClient.Options
{
    [Verb("change", HelpText = "change user in Database happens by phonenumber, other fields are optional.")]
    public class ChangeOptions
    {
        [Option('p', "phonenumber", Required = false,
            HelpText = "Input user phone number.")]
        public string PhoneNumber { get; set; }

        [Option('n', "name", Required = false,
            HelpText = "Input user full name.")]
        public string FullName { get; set; }

        [Option('e', "email", Required = false,
            HelpText = "Input user email.")]
        public string Email { get; set; }

        [Option("position", Required = false,
            HelpText = "Input user Position Held.")]
        public string PositionHeld { get; set; }

        [Option('a', "additionalnumbers", Required = false,
            HelpText = "Input user additional numbers.")]
        public string AdditionalNumbers { get; set; }
            
        [Option('c', "userphonetochange", Required = true,
            HelpText = "Input field you want to change.")]
        public string UserPhoneToChange { get; set; }
    }
}