using Glossary.API.Models;
using Glossary.Application.Terms.Comands;
using Glossary.Application.Terms.Queries;
using Glossary.Domain.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Glossary.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TermController : ControllerBase
    {
        private IMediator _mediator;
        public TermController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("/createterm")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateTermModel request)
        {
            var id = await _mediator.Send(new CreateTermComand(request.Name, request.Definition));
            return Ok(id);
        }

        [HttpGet]
        public async Task<List<TermDto>> GetAllTerms()
        {
            var terms = await _mediator.Send(new GetTermsQuery());
            return terms.OrderBy(x=>x.Name).ToList();
        }

        [HttpPost("{id:guid}/publish")]
        public async Task<IActionResult> Publish([FromRoute] Guid id, CancellationToken ct)
        {
            await _mediator.Send(new PublishTermComand(id), ct);
            return NoContent();
        }

        [HttpPost("{id:guid}/archive")]
        public async Task<IActionResult> Archive([FromRoute] Guid id, CancellationToken ct)
        {
            await _mediator.Send(new ArchiveTermComand(id), ct);
            return NoContent();
        }

        [HttpDelete("{id:guid}/delete")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {           
            await _mediator.Send(new DeleteTermComand(id), ct);
            return NoContent(); 
        }
    }
}
