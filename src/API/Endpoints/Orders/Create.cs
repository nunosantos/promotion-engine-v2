using API.Endpoints.Items;
using Application.Interfaces;
using Application.Services;
using Infrastructure.Endpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace API.Endpoints.Orders
{
    public class Create : BaseAsyncEndpoint.WithRequest<CreateOrderCommand>.WithoutResponse
    {
        private readonly IRepository repository;

        public Create(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost("order")]
        [SwaggerOperation(
            Summary = "Creates an order",
            Description = "Creates an order",
            OperationId = "Order.Create",
            Tags = new[] { "CreateEndpoint" })
        ]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public override async Task<ActionResult> HandleAsync(CreateOrderCommand request)
        {
            try
            {
                if (request is null)
                {
                    BadRequest();
                }

                var order = RepositoryMapper.MapOrder(request);

                var orderCalculator = new OrderCalculatorService(repository);

                order.Total = orderCalculator.CalculateItemTotal(order);

                return Created("order", order);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}