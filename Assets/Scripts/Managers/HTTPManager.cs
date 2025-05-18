using Assets.Scripts.HTTPs;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System;
using System.Threading;
using UnityEngine;
using System.Collections;
using Assets.Scripts.MatchMessages.Responses;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Managers
{
    public class HTTPManager : MonoBehaviour
    {
        private const string PostHellmasterData = "PostHellmasterData";

        private EnrichedHttpClient http;

        private static HTTPManager _instance;

        public static HTTPManager Instance
        {
            get
            {
                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
            _instance = this;
        }

        public void HellmasterData<TRequest>(TRequest message, CancellationToken cancellationToken = default) where TRequest : struct, NetworkMessage
        {
            HellmasterDataRequest request = new HellmasterDataRequest()
            {
                // *** NOTE: Class definition removed on Demo ***
            };

            _ = http.PostHellmasterData(PostHellmasterData, request, null, cancellationToken: cancellationToken);
        }

        public IEnumerator HellmasterData<TRequest, TResponse>(TRequest message, bool isApiGateway, Action<TResponse> callback, CancellationToken cancellationToken = default) where TRequest : struct, NetworkMessage where TResponse : struct, NetworkMessage
        {
            HellmasterDataRequest request = new HellmasterDataRequest()
            {
                // *** NOTE: Class definition removed on Demo ***
            };

            HttpResponse<HellmasterDataResponse> result = null;
            yield return http.PostHellmasterData("HellmasterData", request, (response) =>
            {
                result = response;
            },
            cancellationToken: cancellationToken).ToCoroutine();

            if (result != null && result.Response != null && result.Response.result != null && result.Response.result != string.Empty)
            {
                var response = JsonConvert.DeserializeObject(result.Response.result, typeof(TResponse));

                callback?.Invoke((TResponse)response);
            }
            else
            {
                if (result == null)
                {
                    WaitForSecondsRealtime wait = new WaitForSecondsRealtime(2);
                    yield return wait;

                    yield return HellmasterData(message, isApiGateway, callback, cancellationToken);
                }
            }
        }
    }
}
