using System;
using System.Text;

public class ResponseDTO<T>
{
    public T Data { get; set; }
    [JsonIgnore]
    public int StatusCode { get; set; }
}
