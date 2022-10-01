//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SendingHttpRequests
//{
//    internal class Contributor
//    {
//    }
//}

record RecContributor(
    string Login,
    string Id, 
    string Node_Id, 
    string Avatar_Url, 
    string Url, 
    string Html_url,
    string Followers_Url,
    string Following_Url,
    string Gists_Url,
    string Starred_Url,
    string Subscriptions_Url,
    string Organizations_Url,
    string Repos_Url,
    string Events_Url,
    string Received_Events_Url,
    string Type,
    string Site_Admin,
    short Contributions
    );