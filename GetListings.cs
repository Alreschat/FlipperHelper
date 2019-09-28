using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Flipper_Extended
{
    class GetListings
    {
        int listingAmount = 10;
        private static readonly HttpClient client = new HttpClient();
        public List<Result> RequestListing(List<string> want, List<string> have, bool online = true, int minimum = 10)
        {
            string idRequestJson = RequestId(want, have, minimum, true);
            string idRespondJson = postWithJson(idRequestJson, "https://www.pathofexile.com/api/trade/exchange/Legion");
            IdResponse resp = JsonConvert.DeserializeObject<IdResponse>(idRespondJson);
            string result = String.Join(",", resp.result.Take(Math.Min(listingAmount, resp.total)));
            string listingJson = getListingsAsync(result, resp.id);
            RootObject listings = JsonConvert.DeserializeObject<RootObject>(listingJson);
            return listings.result;
        }
        public List<Result> RequestListing(string want, string have, bool online = true, int minimum = 10)
        {
            return RequestListing(new List<string> { want }, new List<string> { have }, online, minimum);
        }
        private string RequestId(List<string> want, List<string> have, int minimum, bool online)
        {
            IdRequest req = new IdRequest();
            req.exchange.want = want;
            req.exchange.have = have;
            req.exchange.minimum = minimum;
            req.exchange.status.option = online ? "online" : "any";
            string json = JsonConvert.SerializeObject(req, Formatting.Indented);
            return json;
        }
        private string postWithJson(string json, string URL)
        {
            string result;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            return result;
        }
        private string getListingsAsync(string result, string id)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://www.pathofexile.com/api/trade/fetch/{result}?query={id}&exchange");
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
