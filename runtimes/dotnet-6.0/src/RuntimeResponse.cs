namespace DotNetRuntime;

using System.Text.Json;

public class RuntimeResponse
{
    public RuntimeOutput Send(string body, int statusCode = 200, Dictionary<string, string>? headers = new Dictionary<string,string>())
    {
        if (!headers.TryGetValue("content-type"))
        {
            headers.Add("content-type", "text/plain");
        }

        return new RuntimeOutput(
            body,
            statusCode,
            headers);
    }

    public RuntimeOutput Json(Dictionary<string, object?> json, int statusCode = 200, Dictionary<string, string>? headers = new Dictionary<string,string>())
    {
        if (!headers.TryGetValue("content-type"))
        {
            headers.Add("content-type", "application/json");
        }

        headers.Add("content-type", "application/json");
        return Send(JsonSerializer.Serialize(json), statusCode, headers);
    }

    public RuntimeOutput Empty()
    {
        return Send("", 204, new Dictionary<string, string>());
    }

    public RuntimeOutput Redirect(String url, int statusCode = 301, Dictionary<string, string>? headers = null)
    {
        if(headers == null) {
            headers = new Dictionary<string,string>();
        }

        headers.Add("location", url);

        return Send("", statusCode, headers);
    }
}