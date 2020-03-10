using Newtonsoft.Json;

namespace ConsoleClient
{
    [JsonObject]
    public class SortInfo
    {
        public string SortConditionField { get; set; }
        public bool IsDescending { get; set; }
    }
}