using RestSharp;

namespace BlueNetScanner
{
    public static class Requestor
    {
        private const string url = "https://bluenetweb.azurewebsites.net/api/Guests";

        public static IRestResponse Send(object[] data)
        {
            if (data == null)
                return null;
            RestClient client = new RestClient(url);

            IRestRequest req = new RestRequest(Method.POST).AddJsonBody(Serializer.ToByteArray(data));
            IRestResponse res = client.Execute(req);
            return res;
        }
    }
}
