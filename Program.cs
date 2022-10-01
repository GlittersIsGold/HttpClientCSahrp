// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using static System.Console;

var Url = "https://github.com/";

WriteLine($"Sending response to {Url}");

/*
Informational responses (100–199)
Successful responses (200–299)
Redirects (300–399)
Client errors (400–499)
Server errors (500–599)
 */

using var Client = new HttpClient();

var StatusRequest = await Client.GetAsync(Url);


WriteLine(StatusRequest.StatusCode);
WriteLine(StatusRequest.StatusCode.GetHashCode());

//WriteLine(Response.Headers);
//WriteLine(Response.Content);
//WriteLine(Response.TrailingHeaders);
//WriteLine(Response.RequestMessage);
//WriteLine(Response.Version);
//WriteLine(Response.IsSuccessStatusCode);

var GetRequest = await Client.GetStringAsync(Url);
WriteLine(GetRequest);

var HeadRequest = await Client.SendAsync(new HttpRequestMessage(HttpMethod.Head, Url));
WriteLine(HeadRequest);


/// <summary>
/// The User-Agent request header is a characteristic string that lets 
/// servers and network peers identify the application, operating system, 
/// vendor, and/or version of the requesting user agent.
/// </summary>

Client.DefaultRequestHeaders.Add("Cyberk1ra", "C# Programm");
var UserAgentRequest = await Client.GetStringAsync(Url);
WriteLine(UserAgentRequest);



using var Alpha = new HttpClient();
var RequestMessage = new HttpRequestMessage(HttpMethod.Get, Url);
RequestMessage.Headers.Add("Cyberk1ra", "C# Programm");
var HttpHeaderRequest = await Alpha.SendAsync(RequestMessage);
var HttpHeaderRequestContent = await HttpHeaderRequest.Content.ReadAsStringAsync();

/// <summary>
/// Query string is a part of the URL 
/// which is used to add some data to the request for the resource. 
/// It is often a sequence of key/value pairs. 
/// It follows the path and starts with the ? character.
/// </summary>
/// 

var WebResource = "http://webcode.me/qs.php";
using var Beta = new HttpClient();
//Beta.Timeout = TimeSpan.FromSeconds(30);
var builder = new UriBuilder(WebResource);
builder.Query = "name=Yves Tumor&album=The Asymptotical World";

var Link = builder.ToString();
var QueryRequest = await Beta.GetAsync(Link);

var QueryRequestContent = await QueryRequest.Content.ReadAsStringAsync();
WriteLine(QueryRequestContent);



///<summary>
/// Multiple async requests
/// </summary>

var Links = new string[]
{
    "http://webcode.me",
    "http://example.com",
    "http://httpbin.org",
    "https://ifconfig.me",
    "http://termbin.com",
    "https://github.com",
};

var Expression = new Regex(@"<title>\s*(.+?)\s*</title>", RegexOptions.Compiled);

using var Deltha = new HttpClient();

var Tasks = new List<Task<string>>();

foreach (var url in Links)
{
    Tasks.Add(Deltha.GetStringAsync(url));
}

Task.WaitAll(Tasks.ToArray());

var MultiAsyncReqData = new List<string>();

foreach (var RequestTask in Tasks) { MultiAsyncReqData.Add(await RequestTask); }

foreach (var RequestContent in MultiAsyncReqData)
{

    var comparison = Expression.Matches(RequestContent);

    foreach (var match in comparison)
    {
        WriteLine(match);
    }
}

///<summary>
/// Post request
/// </summary>
var Carol = new RecPerson("Carol", "Designer", 33);
var Json = JsonConvert.SerializeObject(Carol);
var PostData = new StringContent(Json, Encoding.UTF8, "application/json");


var PostRequestLink = "https://httpbin.org/post";
using var Gamma = new HttpClient();

var PostRequest = await Gamma.PostAsync(PostRequestLink, PostData);

var PostRequestResult = await PostRequest.Content.ReadAsStringAsync();
WriteLine(PostRequestResult);

/// <summary>
///  Json request
/// </summary>
using var Epsilon = new HttpClient();

Epsilon.BaseAddress = new Uri("https://api.github.com/");
Epsilon.DefaultRequestHeaders.Add("User-Agent", "C# cl programm");
Epsilon.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

var JsonRequestLink = "repos/kubernetes/kubernetes/contributors";

HttpResponseMessage responseMessage = await Epsilon.GetAsync(JsonRequestLink);
responseMessage.EnsureSuccessStatusCode();

var JsonRequestResponse = await responseMessage.Content.ReadAsStringAsync();

var RequestHash = responseMessage.StatusCode.GetHashCode();

WriteLine(RequestHash);

if (RequestHash >= 200 && RequestHash < 300)
{

    if (JsonRequestResponse != null)
    {
        List<RecContributor> contributors = JsonConvert.DeserializeObject<List<RecContributor>>(JsonRequestResponse);

        if (contributors != null) { contributors.ForEach(WriteLine); }
        else { WriteLine("there is no contributors ≡(▔﹏▔)≡"); }
    }
    else
    {
        WriteLine("Response is null");
    }

}

else { WriteLine(RequestHash); }



/// <summary>
///  POST from data
/// </summary>
var PostFromDictionaryUrl = "https://httpbin.org/post";

using var Dzetha = new HttpClient();

var data = new Dictionary<string, string>
{
    {"name", "Jason Statham" },
    {"gender", "Man" },
    {"race","Caucasian" },
    {"born", "26 July 1967" },
    {"place", "Shirbrooke, Derbyshire, UK" }
};

var PostFromDictionaryResult = await Dzetha.PostAsync(PostFromDictionaryUrl, new FormUrlEncodedContent(data));

var PostFromDictionaryContent = await PostFromDictionaryResult.Content.ReadAsStringAsync();
WriteLine(PostFromDictionaryContent);



/// <summary>
///  HttpClient proxy
/// </summary>
var Port = 7497;
var Proxy = "51.222.146.133";
var ProxyUrl = "http://webcode.me";

var handler = new HttpClientHandler()
{
    Proxy = new WebProxy(new Uri($"socks5://{Proxy}:{Port}")),
    UseProxy = true
};

using var Ita = new HttpClient(handler);

try
{
    var ProxyResult = await Ita.GetAsync(ProxyUrl);
    var ProxyContent = await ProxyResult.Content.ReadAsStringAsync();

    WriteLine(ProxyContent);

}
catch (Exception ex)
{
    WriteLine(ex.Message);
    WriteLine("\n press any button to continue");
    ReadKey();
}



/// <summary>
///  HttpClient download image
/// </summary>
/// 

//using var Tetha = new HttpClient();
//var DownloadUrl = "https://www.figma.com/";
//byte[] IconBytes = await Tetha.GetByteArrayAsync(DownloadUrl);

//string IconSavePath = System.Environment.GetFolderPath(
//    System.Environment.SpecialFolder.Personal);

//string FileName = "favicon.svg";
//string LocalPath = Path.Combine(IconSavePath, FileName);

//WriteLine(LocalPath);
//File.WriteAllBytes(LocalPath, IconBytes);


/// <summary>
///  Streaming
/// </summary>
/// <returns>Will be updated</returns>

/*
 using var httpClient = new HttpClient();

var url = "https://cdn.netbsd.org/pub/NetBSD/NetBSD-9.2/images/NetBSD-9.2-amd64-install.img.gz";

var fname = Path.GetFileName(url);

var resp = await httpClient.GetAsync(url, 
    HttpCompletionOption.ResponseHeadersRead);
resp.EnsureSuccessStatusCode();

using Stream ms = await resp.Content.ReadAsStreamAsync();

using FileStream fs = File.Create(fname);
await ms.CopyToAsync(fs);

Console.WriteLine("file downloaded");
 */


///<summary>
/// Authentication
/// </summary>

var UserName = "Alyosha";
var UserPass = "ahsoylA";

var AuthUrl = "https://httpbin.org/basic-auth/Alyosha/ahsoylA";

using var Yota = new HttpClient();

var AuthToken = Encoding.ASCII.GetBytes($"{UserName}:{UserPass}");
Yota.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
    "Basic", Convert.ToBase64String(AuthToken));

var AuthResult = await Yota.GetAsync(AuthUrl);
var AuthContent = await AuthResult.Content.ReadAsStringAsync();
WriteLine(AuthContent);

ReadKey();