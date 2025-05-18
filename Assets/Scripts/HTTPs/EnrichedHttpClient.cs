
using System.Collections.Generic;
using System.Threading;
using System;
using System.Threading.Tasks;
using Unity.VisualScripting;
using Assets.Scripts.MatchMessages.Responses;
using Newtonsoft.Json;
using System.Net.Http;
using UnityEditor.PackageManager;
using Cysharp.Threading.Tasks;
using System.Security.Cryptography;
namespace Assets.Scripts.HTTPs
{
    public class EnrichedHttpClient
    {
        private HttpClient client = null;

        public EnrichedHttpClient(string uri)
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(uri),
            };
        }

        public async UniTask PostHellmasterData(string method, object payloadObject, Action<HttpResponse<HellmasterDataResponse>> callback, IDictionary<string, string> queryString = null, CancellationToken cancellationToken = default)
        {
            HttpResponse<HellmasterDataResponse> response = new HttpResponse<HellmasterDataResponse>();
            HttpResponseMessage callResponse = null;
            try
            {
                string uri = $"{client.BaseAddress.AbsoluteUri}{method}";
                if (queryString != null)
                {
                    uri += $"?{queryString.ToQueryString()}";
                }

                string bodyString = string.Empty;

                if (payloadObject != null)
                {
                    bodyString = JsonConvert.SerializeObject(payloadObject);
                }
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(uri),
                    Headers = {
                        { "accept", "application/json, text/plain" }
                    },
                    Content = new StringContent(bodyString, System.Text.Encoding.UTF8, "application/json")
                };

                callResponse = await client.SendAsync(httpRequestMessage, cancellationToken);
                callResponse.EnsureSuccessStatusCode();
                await response.SetSuccessResponse(callResponse);

                callback?.Invoke(response);
            }
            catch (HttpRequestException e)
            {
                if (e.Message.Contains("504"))
                {
                    response.SetErrorResponse(System.Net.HttpStatusCode.GatewayTimeout, e.Message);

                    callback?.Invoke(response);
                }
                else
                {
                    callback?.Invoke(null);
                }
            }
        }
    }
}
