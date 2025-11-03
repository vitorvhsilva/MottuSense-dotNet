using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Motos.Application.Interfaces;
using Motos.Presentation.Dto.Ml;
using Motos.Presentation.Doc.Sample;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Motos.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/motos")]
    [ApiController]
    public class MLController: ControllerBase
    {
        private readonly IMLService _mlService;

        public MLController(IMLService mlService)
        {
            _mlService = mlService;
        }

        [HttpPost("prever-manutencao")]
        [SwaggerOperation(
                   Summary = "Prever necessidade de manutenção de uma moto",
                   Description = "Recebe informações da moto e retorna se a manutenção é recomendada com probabilidade"
               )]
        [SwaggerResponse(200, "Previsão gerada com sucesso", typeof(PreverManutencaoOutputDTO))]
        [SwaggerResponse(400, "Dados de entrada inválidos")]
        [SwaggerRequestExample(typeof(PreverManutencaoInputDTO), typeof(PreverManutencaoInputSample))]
        [SwaggerResponseExample(200, typeof(PreverManutencaoOutputSample))]
        public IActionResult PreverManutencao([FromBody] PreverManutencaoInputDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resultado = _mlService.Prever(dto);

            return Ok(new
            {
                data = resultado,
                links = new
                {
                    self = Url.Action(nameof(PreverManutencao), "ML", null, Request.Scheme),
                }
            });
        }
    }
}
