using System;
using System.Linq;
using System.IO;
using System.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elasticsearch.Net;
namespace stock_notifications.Data
{
    public class SearchResult {
        public Hits hits {get; set;}
    }
    public class Hits {
        public Total total {get; set;}
        public Source[] hits {get; set;}
    }
    public class Source {
        public string _index {get; set;}
        public Post _source {get; set;}
    }

    public class Post {
        public string guid {get; set;}
        public string title {get; set;}
        public string description {get; set;}
    }
    public class Total {
        public int value {get; set;}
        public string relation {get; set;}
    }

    public class NewsService
    {
        // figure out how to add the es instance here as a public variable
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public Source[] GetNews()
        {
            string esInstance = Environment.GetEnvironmentVariable("ES_INSTANCE");
            if (esInstance == "" || esInstance == null) return null;
            var settings = new ConnectionConfiguration(new Uri(esInstance))
                .RequestTimeout(TimeSpan.FromMinutes(2));
            var lowlevelClient = new ElasticLowLevelClient(settings);
            var searchResponse = lowlevelClient.Search<StringResponse>("post", PostData.Serializable(new
            {
                size=1000,
                query = new
                {
                    match_all = new {}
                }
            }));
            var responseJson = searchResponse.Body;
            SearchResult searchResult = JsonSerializer.Deserialize<SearchResult>(responseJson);
            Source[] hits = searchResult.hits.hits;
            // foreach (Post post in hits) {
            //     Console.WriteLine(post);
            // }
            return hits;
        }
    }
}
