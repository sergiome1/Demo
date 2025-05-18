using Cysharp.Threading.Tasks;
using System.IO;
using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

namespace Assets.Scripts.HTTPs
{
    public class HttpResponse<T>
    {
        public T Response { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }
        public string ErrorMessage { get; private set; }
        public bool IsError { get => StatusCode != HttpStatusCode.OK; }
        public long? ContentLength { get; private set; } = 0L;

        public async UniTask SetSuccessResponse(HttpResponseMessage httpResponse, bool continueInCapturedContext = true)
        {
            await SetResponseObject(httpResponse, continueInCapturedContext);

            StatusCode = httpResponse.StatusCode;
        }

        public void SetErrorResponse(HttpStatusCode statusCode, string errorMessage)
        {
            Response = default;
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
        }

        private async UniTask SetResponseObject(HttpResponseMessage httpResponse, bool continueInCapturedContext = true)
        {
            if (httpResponse.Content != null)
            {
                ContentLength = httpResponse.Content.Headers.ContentLength;
            }

            if (ContentLength <= 0L)
            {
                Response = default;
                return;
            }

            Type type = typeof(T);

            if (type == typeof(byte[]))
            {
                object response = await httpResponse.Content.ReadAsByteArrayAsync().AsUniTask(continueInCapturedContext);
                Response = (T)response;
            }
            if (type == typeof(Stream))
            {
                object response = await httpResponse.Content.ReadAsStreamAsync().AsUniTask(continueInCapturedContext);
                Response = (T)response;
            }
            if (type == typeof(string))
            {
                string response = await httpResponse.Content.ReadAsStringAsync().AsUniTask(continueInCapturedContext);
                Response = (T)(object)response;
            }
            if (type.IsPrimitive)
            {
                string response = await httpResponse.Content.ReadAsStringAsync().AsUniTask(continueInCapturedContext);

                Response = (T)Convert.ChangeType(response, type);
            }
            else
            {
                string s = await httpResponse.Content.ReadAsStringAsync();
                Response = JsonConvert.DeserializeObject<T>(s);
            }
        }
    }
}
