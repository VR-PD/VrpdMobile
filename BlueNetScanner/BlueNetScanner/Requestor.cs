using RestSharp;

namespace BlueNetScanner
{
    public static class Requestor
    {
        private const string url = "https://bluenetweb.azurewebsites.net/api/Guests";

        /// <summary>
        /// Send object array of scanned data to webapi
        /// </summary>
        public static IRestResponse Send(object[] data)
        {
            // If theres no data to send dont bother sending a request and return null
            if (data == null)
                return null;

            // Make client to webapi
            RestClient client = new RestClient(url);

            // Serialize data to bytes and send them to client
            IRestRequest req = new RestRequest(Method.POST).AddJsonBody(Serializer.ToByteArray(data));
            IRestResponse res = client.Execute(req);
            // return response
            return res;
        }
    }
}
