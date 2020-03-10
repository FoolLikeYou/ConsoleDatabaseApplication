using Newtonsoft.Json;

namespace WebApplication.Models
{
    [JsonObject]
    public class SortInfo
    {
        public string SortConditionField { get; set; }
        public bool IsDescending { get; set; }
    }
}