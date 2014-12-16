using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Spring.Social.Twitter.Api;
using Spring.Social.Twitter.Api.Impl;
using TweetSharp;
using Nancy;

namespace TwitterRadar.Server
{
    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/locations", true] = async (ct, paramaters) =>
                {
                    ITwitter twitter = new TwitterTemplate(ConfigurationManager.AppSettings["TWITTER_CONSUMER_KEY"], ConfigurationManager.AppSettings["TWITTER_CONSUMER_SECRET"], ConfigurationManager.AppSettings["TWITTER_ACCESS_TOKEN"],ConfigurationManager.AppSettings["TWITTER_ACCESS_TOKEN_SECRET"]);
                    IList<Tweet> tweets = await twitter.TimelineOperations.GetUserTimelineAsync("rclarkson");
                    
                    tweets[0]
                    /*var tweets = service.ListTweetsOnUserTimeline(new ListTweetsOnUserTimelineOptions()
                        {
                            ContributorDetails = true,
                            Count = 100,
                            ExcludeReplies = false,
                            IncludeRts = true,
                            ScreenName = "jen20"
                        }).ToList();
                        
                    var tweetsWithLocations = tweets.Where(t => t.Location != null).ToList().OrderBy(t => t.CreatedDate);
                    *
                    return Response.AsJson(tweetsWithLocations.Select(t => new
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
                     * */
                    return 404;
                };

        }
    }
}