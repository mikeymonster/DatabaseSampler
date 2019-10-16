using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DatabaseSampler.Application.Tests.TestHttpFactories
{
    public abstract class TestHttpClientFactory
    {
        protected HttpClient CreateClient(object response, string uri, string contentType = "application/json")
        {
            //var serialised = JsonConvert.SerializeObject(response);
            var serialized = JsonSerializer.Serialize(response);

            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(serialized)
            };
            httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);

            var fakeMessageHandler = new FakeHttpMessageHandler();
            fakeMessageHandler.AddFakeResponse(new Uri(uri),
                httpResponseMessage);

            var httpClient = new HttpClient(fakeMessageHandler);

            return httpClient;
        }
    }
}