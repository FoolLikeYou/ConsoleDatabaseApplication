using Newtonsoft.Json;

namespace ConsoleClient
{
    [JsonObject]
    public class UsersInfo
    {
        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }
        
        [JsonProperty("fullName")]
        public string FullName { get; set; }
        
        [JsonProperty("email")]
        public string Email { get; set; }
        
        [JsonProperty("positionHeld")]
        public string PositionHeld { get; set; }
        
        [JsonProperty("additionalNumbers")]
        public string AdditionalNumbers { get; set; }
        
        [JsonProperty("userPhoneToChange")]
        public string UserPhoneToChange { get; set; }

        public override string ToString()
        {
            return $"{nameof(PhoneNumber)}: {PhoneNumber}, {nameof(FullName)}: {FullName}, " +
                   $"{nameof(Email)}: {Email}, {nameof(PositionHeld)}: {PositionHeld}, " +
                   $"{nameof(AdditionalNumbers)}: {AdditionalNumbers}";
        }
    }
}