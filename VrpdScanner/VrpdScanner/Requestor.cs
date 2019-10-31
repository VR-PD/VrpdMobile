using RestSharp;

namespace VrpdScanner
{
    public static class Requestor
    {
        private const string url = "https://vrpd-webapp.azurewebsites.net/api/Guests";

        public static IRestResponse Send(object[] data)
        {
            RestClient client = new RestClient(url);

            IRestRequest req = new RestRequest(Method.POST).AddJsonBody(Serializer.ToByteArray(data));
            IRestResponse res = client.Execute(req);
            return res;
        }
    }
}
