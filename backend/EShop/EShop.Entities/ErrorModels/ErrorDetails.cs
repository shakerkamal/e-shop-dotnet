using Newtonsoft.Json;

namespace EShop.Entities.ErrorModels;

public class ErrorDetails
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public override string ToString() => JsonConvert.SerializeObject(this);
}
