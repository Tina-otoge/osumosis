using System.Net.Http;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using osu.Framework.Logging;
using osu.Game.Scoring;

namespace osu.Game.Osmosis
{
    public class IgnorePropertiesResolver : DefaultContractResolver
    {
        private IEnumerable<string> _propsToIgnore;
        public IgnorePropertiesResolver(IEnumerable<string> propNamesToIgnore)
        {
            _propsToIgnore = propNamesToIgnore;
        }
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);
            property.ShouldSerialize = (x) => { return !_propsToIgnore.Contains(property.PropertyName); };
            return property;
        }
    }
    public class Submitter
    {
        private static readonly HttpClient client = new HttpClient();

        public static void Submit(ScoreInfo info) {
            Task.Run(async() => {
                Logger.Log("osmosis submission...");
                var json = JsonConvert.SerializeObject(info, Formatting.None,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        ContractResolver = new IgnorePropertiesResolver(new[] {
                                "Beatmaps",
                                "Metrics",
                                "Files",
                                "RankHistory",
                                "user_achievements"
                        }),
                    }
                );
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://osmosis.tina.moe/lazer", stringContent).ConfigureAwait(false);
                var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                Logger.Log(responseString);
            });
        }
    }
}
