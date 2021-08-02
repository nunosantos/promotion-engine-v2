using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Infrastructure.Endpoints
{
    public static class BaseAsyncEndpoint
    {
        public static class WithRequest<TRequest>
        {
            public abstract class WithResponse<TResponse> : BaseEndpoint
            {
                public abstract Task<ActionResult<TResponse>> HandleAsync(TRequest request);
            }

            public abstract class WithoutResponse : BaseEndpoint
            {
                public abstract Task<ActionResult> HandleAsync(TRequest request);
            }
        }

        public static class WithoutRequest
        {
            public abstract class WithResponse<TResponse> : BaseEndpoint
            {
                public abstract Task<ActionResult<TResponse>> HandleAsync();
            }

            public abstract class WithoutResponse : BaseEndpoint
            {
                public abstract Task<ActionResult> HandleAsync();
            }
        }
    }

    [ApiController]
    public abstract class BaseEndpoint : ControllerBase
    {
    }
}