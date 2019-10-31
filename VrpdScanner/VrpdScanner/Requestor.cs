using RestSharp;

namespace VrpdScanner
{
    public static class Requestor
    {
        private const string url = "https://localhost:44347/api/Guests";

        public static IRestResponse Send(object[] data)
        {
            RestClient client = new RestClient(url);

            IRestRequest req = new RestRequest(Method.POST).AddJsonBody(Serializer.ToByteArray(data));
            IRestResponse res = client.Execute(req);
            return res;
        }
    }
}
