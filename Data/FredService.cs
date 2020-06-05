using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Web.HttpUtility;
namespace stock_notifications.Data
{
    public class FredObservation {
      public string realtime_start {get; set;}
      public string realtime_end {get; set;}
      public string observation_start {get; set;}
      public string observation_end {get; set;}
      public string file_type {get; set;}
      public string order_by {get; set;}
      public string sort_order {get; set;}
      public double count {get; set;}
      public double offset {get; set;}
      public double limit {get; set;}
      public Observation[] observations {get; set;}
    }
    // data from fred
    public class Observation {
      public string realtime_start {get; set;}
      public string realtime_end {get; set;}
      public string date {get; set;}
      public string value {get; set;}
    }

    public class FredService
    {
        static readonly HttpClient client = new HttpClient();

        public async Task<FredObservation> GetObservationsForSeries(String series_id)
        {
            try
            {
              var builder = new UriBuilder("https://api.stlouisfed.org/fred/series/observations");
              var query = ParseQueryString(builder.Query);
              query["series_id"] = series_id;
              string fred_api_key = Environment.GetEnvironmentVariable("FRED_APIKEY");
              query["api_key"] = fred_api_key;
              query["file_type"] = "json";
              builder.Query = query.ToString();
              var observation_url = builder.ToString();
              HttpResponseMessage response = await client.GetAsync(observation_url);
              string responseBody = await response.Content.ReadAsStringAsync();
              var new_data = JsonSerializer.Deserialize<FredObservation>(
                responseBody,
                new JsonSerializerOptions {AllowTrailingCommas = true}
              );
              return new_data;
            }
            catch 
            {
              return new FredObservation();
            }
        }
    }
}
