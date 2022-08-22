using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Infrastructure.Endpoints
{
    public static class AsynchronousEndpoint
    {
        public static class WithRequestType<TRequest>
        {
            public abstract class WithResponseType<TResponse> : BaseEndpoint
            {
                public abstract Task<ActionResult<TResponse>> HandleAsync(TRequest request);
            }

            public abstract class WithoutResponseBody : BaseEndpoint
            {
                public abstract Task<ActionResult> HandleAsync(TRequest request);
            }
        }

        public static class WithoutRequestBody
        {
            public abstract class WithResponseType<TResponse> : BaseEndpoint
            {
                public abstract Task<ActionResult<TResponse>> HandleAsync();
            }

            public abstract class WithoutResponseBody : BaseEndpoint
            {
                public abstract Task<ActionResult> HandleAsync();
            }
        }
    }

    [Authorize]
    [ApiController]
    public abstract class BaseEndpoint : ControllerBase
    {
    }
}