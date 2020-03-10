using Newtonsoft.Json;

namespace WebApplication.Models
{
    public static class TableFields
    {
        public static string PhoneNumber => "phoneNumber";
        public static string FullName => "fullName";
        public static string Email => "email";
        public static string PositionHeld => "positionHeld";
        public static string AdditionalNumbers => "additionalNumbers";
    }

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
    }
}