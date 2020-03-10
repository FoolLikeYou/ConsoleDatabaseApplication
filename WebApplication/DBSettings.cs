using Newtonsoft.Json;

namespace WebApplication
{
    [JsonObject]
    public class DBSettings
    {
        public string DataSource { get; set; }

        public string DataBase { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}