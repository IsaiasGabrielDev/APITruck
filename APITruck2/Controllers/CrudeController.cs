using APITruck2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APITruck2.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class CrudeController : ControllerBase
    {

        public BaseContext BaseContext { get; set; }

        public CrudeController(BaseContext baseContext)
        {
            BaseContext = baseContext;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCaminhoes(int id)
        {
            var caminhao = await BaseContext.Caminhoes.AsTracking().FirstOrDefaultAsync(x => x.Id == id);
            return Ok(caminhao);
        }

        [HttpGet("ListarCaminhoes")]
        public async Task<IActionResult> GetCaminhoes()
        {
            var caminhoes = await BaseContext.Caminhoes.AsTracking().ToListAsync();
            return Ok(caminhoes);
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] Caminhao caminhao)
        {
            caminhao.AnoFabricacao = DateTime.Now.Year;

            if (caminhao.AnoModelo > caminhao.AnoFabricacao + 1 || caminhao.AnoModelo < caminhao.AnoFabricacao)
            {
                return NotFound("Ano do Modelo não é compativel com o permitido.");
            }

            BaseContext.Caminhoes.Add(caminhao);

            await BaseContext.SaveChangesAsync();

            return new ObjectResult(caminhao) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Caminhao caminhao)
        {
            if (caminhao.AnoModelo > caminhao.AnoFabricacao + 1 || caminhao.AnoModelo < caminhao.AnoFabricacao)
            {
                return NotFound("Ano do Modelo não é compativel com o permitido.");
            }

            var caminhaodb = await BaseContext.Caminhoes.FirstOrDefaultAsync(x => x.Id == id);

            caminhaodb.AnoFabricacao = caminhao.AnoFabricacao;
            caminhaodb.AnoModelo = caminhao.AnoModelo;
            caminhaodb.NomeModelo = caminhao.NomeModelo;

            await BaseContext.SaveChangesAsync();

            return new ObjectResult(caminhao) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var caminhao = await BaseContext.Caminhoes.FirstOrDefaultAsync(x => x.Id == id);
            BaseContext.Caminhoes.Remove(caminhao);
            await BaseContext.SaveChangesAsync();
            return Ok();
        }
    }
}
