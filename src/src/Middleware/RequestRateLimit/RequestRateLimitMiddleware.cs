using System.Net;

namespace src.Middleware.RequestRateLimit
{
    public class RequestRateLimitMiddleware
    {
        private readonly RequestDelegate next;
        private readonly int _limit;
        private readonly int _intervalInSeconds;
        private readonly Dictionary<string, Queue<DateTime>> _requestHistory = new Dictionary<string, Queue<DateTime>>();
        public RequestRateLimitMiddleware(RequestDelegate next, int limit = 10, int intervalInSeconds = 60)
        {
            this.next = next;
            _limit = limit;
            _intervalInSeconds = intervalInSeconds;
        }

        public async Task Invoke(HttpContext context)
        {
            var ipAddress = context.Connection.RemoteIpAddress.ToString();
            var now = DateTime.UtcNow;

            lock (_requestHistory)
            {
                if (!_requestHistory.ContainsKey(ipAddress))
                {
                    _requestHistory[ipAddress] = new Queue<DateTime>();
                }

                while (_requestHistory[ipAddress].Count > 0 && (now - _requestHistory[ipAddress].Peek()).TotalSeconds > _intervalInSeconds)
                {
                    _requestHistory[ipAddress].Dequeue();
                }

                if (_requestHistory[ipAddress].Count > _limit)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                    return;
                }

                _requestHistory[ipAddress].Enqueue(now);
            }
            
            await next.Invoke(context);
        }
    }
}
