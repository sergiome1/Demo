using System.Collections.Generic;
using System.Linq;
using UnityEngine.Networking;

namespace Assets.Scripts.HTTPs
{
    public static class DictionaryExtensions
    {
        public static string ToQueryString(this IDictionary<string, string> keyValues)
        {
            return string.Join("&", keyValues.Select(kvp => string.Format("{0}={1}", UnityWebRequest.EscapeURL(kvp.Key), UnityWebRequest.EscapeURL(kvp.Value))));
        }
    }
}
