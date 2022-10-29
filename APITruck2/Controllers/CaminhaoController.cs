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
    public class CaminhaoController : ControllerBase
    {

        private ICaminhaoRepository _caminhaoRepository { get; set; }

        public CaminhaoController(ICaminhaoRepository caminhaoRepository)
        {
            _caminhaoRepository = caminhaoRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCaminhoes(int id)
        {
            var caminhao = await _caminhaoRepository.BuscarId(id);
            return Ok(caminhao);
        }

        [HttpGet("ListarCaminhoes")]
        public async Task<IActionResult> GetCaminhoes()
        {
            var caminhoes = await _caminhaoRepository.Listar();
            return Ok(caminhoes);
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] Caminhao caminhao)
        {
            var caminhaodb = await _caminhaoRepository.Inserir(caminhao);

            return new ObjectResult(caminhaodb) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Caminhao caminhao)
        {
            var caminhaodb = await _caminhaoRepository.Atualizar(id, caminhao);
            return new ObjectResult(caminhaodb) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _caminhaoRepository.Deletar(id);
            return Ok();
        }
    }
}
