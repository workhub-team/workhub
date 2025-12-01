public class DynamicResponse
{
    public string Message { get; set; }
    public int StatusCode { get; set; }
    public List<dynamic> Data { get; set; }
}