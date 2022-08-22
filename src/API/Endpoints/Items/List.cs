using Application.Interfaces;
using Infrastructure.Endpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace API.Endpoints.Items
{
    public class List : AsynchronousEndpoint.WithoutRequestBody.WithResponseType<ListItemResult>
    {
        private readonly IRepository _repository;

        public List(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("items")]
        [SwaggerOperation(
            Summary = "List all items",
            Description = "List all items",
            OperationId = "items.List",
            Tags = new[] { "ItemEndpoint" })
        ]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public override async Task<ActionResult<ListItemResult>> HandleAsync()
        {
            try
            {
                var productItems = _repository.Get().ToArray();

                if (!productItems.Any()) return NoContent();

                return Ok(new ListItemResult() { Items = productItems });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}