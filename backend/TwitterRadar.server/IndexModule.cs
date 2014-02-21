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
            Get["/locations"] = (paramaters) =>
                {
                    var service = new TwitterService(ConfigurationManager.AppSettings["TWITTER_CONSUMER_KEY"], ConfigurationManager.AppSettings["TWITTER_CONSUMER_SECRET"]);
                    service.AuthenticateWith(ConfigurationManager.AppSettings["TWITTER_ACCESS_TOKEN"],ConfigurationManager.AppSettings["TWITTER_ACCESS_TOKEN_SECRET"]);

                    var tweets = service.ListTweetsOnUserTimeline(new ListTweetsOnUserTimelineOptions()
                        {
                            ContributorDetails = true,
                            Count = 30,
                            ExcludeReplies = false,
                            IncludeRts = true,
                            ScreenName = "jen20"
                        }).Where(t => t.Location != null).ToList().OrderBy(t => t.CreatedDate);

                    return Response.AsJson(tweets.Select(t => new
                            {
                                LastTweet = t.TextAsHtml,
                                Location = t.Location,
                                Date =
                                      t.CreatedDate.ToShortDateString() + " " +
                                      t.CreatedDate.ToShortTimeString()
                            })).WithHeaders(new []
                            {
                                new { Header = "Access-Control-Allow-Origin", Value="*" }
                            });
                };

        }
    }
}