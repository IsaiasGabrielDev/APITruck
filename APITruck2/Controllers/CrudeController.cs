using APITruck.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APITruck.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class CrudeController : ControllerBase
    {

        public CaminhaoRepository CaminhaoRepository { get; set; }

        public CrudeController(CaminhaoRepository caminhaoRepository)
        {
            CaminhaoRepository = caminhaoRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCaminhoes(int id)
        {
            var caminhao = await CaminhaoRepository.BuscarId(id);
            return Ok(caminhao);
        }

        [HttpGet("ListarCaminhoes")]
        public async Task<IActionResult> GetCaminhoes()
        {
            var caminhoes = await CaminhaoRepository.Listar();
            return Ok(caminhoes);
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] Caminhao caminhao)
        {
            var caminhaodb = await CaminhaoRepository.Inserir(caminhao);

            return new ObjectResult(caminhaodb) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Caminhao caminhao)
        {
            var caminhaodb = await CaminhaoRepository.Atualizar(id, caminhao);
            return new ObjectResult(caminhaodb) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await CaminhaoRepository.Deletar(id);
            return Ok();
        }
    }
}
