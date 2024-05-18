using Microsoft.Extensions.Http.Resilience;
using Polly;

namespace ResilientHttpClient
{
    public class ResilientHttpClientFactory : IHttpClientFactory
    {
        private readonly int _connectionLifetime;
        private readonly int _maxRetryAttempts;

        public ResilientHttpClientFactory(int connectionLifetime = 15, int maxRetryAttempts = 3)
        {
            _connectionLifetime = connectionLifetime;
            _maxRetryAttempts = maxRetryAttempts;
        }

        public HttpClient CreateClient(string name)
        {
            var retryPipeline = new ResiliencePipelineBuilder<HttpResponseMessage>()
                .AddRetry(new HttpRetryStrategyOptions
                {
                    BackoffType = DelayBackoffType.Exponential,
                    MaxRetryAttempts = _maxRetryAttempts
                })
                .Build();

            var socketHandler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(_connectionLifetime)
            };

#pragma warning disable EXTEXP0001
            var resilienceHandler = new ResilienceHandler(retryPipeline)
#pragma warning restore EXTEXP0001
            {
                InnerHandler = socketHandler
            };

            return new HttpClient(resilienceHandler);
        }
    }
}