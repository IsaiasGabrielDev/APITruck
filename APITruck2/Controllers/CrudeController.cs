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

        [HttpGet("Caminhoes")]
        public async Task<IActionResult> GetCaminhoes()
        {
            var caminhoes = await BaseContext.Caminhoes.AsTracking().ToListAsync();
            return Ok(caminhoes);
        }

        [HttpPost("Inserir")]
        public async Task<IActionResult> InserirCaminhao([FromBody] Caminhao caminhao)
        {           
            var modelo = await BaseContext.Modelos.AsTracking().FirstOrDefaultAsync(x => x.Nome.Contains(caminhao.NomeModelo.ToString()));

            caminhao.AnoFabricacao = DateTime.Now.Year;

            if (caminhao.AnoModelo > caminhao.AnoFabricacao + 1 || caminhao.AnoModelo < caminhao.AnoFabricacao)
            {
                return NotFound("Ano do Modelo não é compativel com o permitido.");
            }

            BaseContext.Caminhoes.Add(caminhao);

            await BaseContext.SaveChangesAsync();

            return new ObjectResult(caminhao) { StatusCode = StatusCodes.Status201Created };
        }

        //[HttpPost("teste")]
        //public async Task<IActionResult> ExcluirCaminhao([FromBody] Caminhao caminhao)
        //{
        //    ////var modelo = await BaseContext.Modelos.AsTracking().FirstOrDefaultAsync(x => x.Nome.Contains(caminhao.NomeModelo));

        //    //if (modelo == null)
        //    //{
        //    //    return NotFound();
        //    //}

        //    //caminhao.Modelo = modelo;
        //    //caminhao.AnoFabricacao = DateTime.Now.Year;

        //    //if (caminhao.AnoModelo > caminhao.AnoFabricacao + 1 || caminhao.AnoModelo < caminhao.AnoFabricacao)
        //    //{
        //    //    return NotFound("Ano do Modelo não é compátivel com o permitido.");
        //    //}

        //    //BaseContext.Caminhoes.Add(caminhao);

        //    //await BaseContext.SaveChangesAsync();

        //    //return new ObjectResult(caminhao) { StatusCode = StatusCodes.Status201Created };
        //}

        [HttpGet("Modelos")]
        public async Task<IActionResult> AddCaminhoes()
        {
            var modelos = await BaseContext.Modelos.AsTracking().ToListAsync();
            return Ok(modelos);
        }
    }
}
