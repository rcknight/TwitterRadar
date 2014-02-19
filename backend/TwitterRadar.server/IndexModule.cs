using System.Configuration;
using System.Linq;
using TweetSharp;
using Nancy;

namespace TwitterRadar.Server
{
    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/location"] = (paramaters) =>
                {
                    var service = new TwitterService(ConfigurationManager.AppSettings["TWITTER_CONSUMER_KEY"], ConfigurationManager.AppSettings["TWITTER_CONSUMER_SECRET"]);
                    service.AuthenticateWith(ConfigurationManager.AppSettings["TWITTER_ACCESS_TOKEN"],ConfigurationManager.AppSettings["TWITTER_ACCESS_TOKEN_SECRET"]);

                    var tweets = service.ListTweetsOnUserTimeline(new ListTweetsOnUserTimelineOptions()
                        {
                            ContributorDetails = true,
                            Count = 1,
                            ExcludeReplies = false,
                            IncludeRts = true,
                            ScreenName = "jen20"
                        });

                    

                    return Response.AsJson(new
                        {
                            LastTweet = tweets.First().TextAsHtml,
                            Location = tweets.First().Location,
                            Date =
                                tweets.First().CreatedDate.ToShortDateString() + " " +
                                tweets.First().CreatedDate.ToShortTimeString()
                        }).WithHeaders(new []
                            {
                                new { Header = "Access-Control-Allow-Origin", Value="*" }
                            });
                };
        }
    }
}