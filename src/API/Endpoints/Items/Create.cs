using Application.Interfaces;
using Infrastructure.Endpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace API.Endpoints.Items
{
    public class Create : AsynchronousEndpoint.WithRequestType<CreateItemCommand>.WithoutResponseBody
    {
        private readonly IRepository repository;

        public Create(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost("items")]
        [SwaggerOperation(
            Summary = "Create a set of items",
            Description = "Create a set of items",
            OperationId = "Item.Create",
            Tags = new[] { "ItemEndpoint" })
        ]

        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public override async Task<ActionResult> HandleAsync([FromBody] CreateItemCommand request)
        {
            try
            {
                if (request is null) BadRequest();

                var productItem = RepositoryMapper.MapProductItem(request);

                repository.Add(productItem);

                return Created("items", request);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}