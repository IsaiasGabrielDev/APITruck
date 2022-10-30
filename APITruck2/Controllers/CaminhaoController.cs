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
    [Route("api/Caminhao")]
    public class CaminhaoController : ControllerBase
    {

        private ICaminhaoRepository _caminhaoRepository { get; set; }

        public CaminhaoController(ICaminhaoRepository caminhaoRepository)
        {
            _caminhaoRepository = caminhaoRepository;
        }

        /// <summary>
        /// Retorna o que corresponde ao Id
        /// </summary>
        /// <returns>Caminhao pelo Id</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCaminhaoId(int id)
        {
            if (id == 0)
            {
                throw new Exception($"Id não pode ser zero ou nulo.");
            }

            var caminhao = await _caminhaoRepository.BuscarId(id);

            if (caminhao == null)
            {
                throw new Exception("Caminhao não localizado");
            }

            return Ok(caminhao);
        }

        /// <summary>
        /// Retorna a lista completa
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetCaminhoes()
        {
            var caminhoes = await _caminhaoRepository.Listar();
            return Ok(caminhoes);
        }

        /// <summary>
        /// Insere
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] Caminhao caminhao)
        {
            caminhao.Id = 0;

            if (caminhao.AnoFabricacao == 0 || caminhao.AnoModelo == 0)
            {
                throw new Exception($"Ano de Fabricação e Ano do modelo não podem ser 0 ou inexistentes");
            }

            if (caminhao.AnoFabricacao != DateTime.Now.Year)
            {
                throw new Exception($"Ano de Fabricação não é o atual");
            }

            if (caminhao.AnoModelo > caminhao.AnoFabricacao + 1 || caminhao.AnoModelo < caminhao.AnoFabricacao)
            {
                throw new Exception($"Ano do modelo menor que o ano atual ou maior que o subsequente");
            }

            var caminhaodb = await _caminhaoRepository.Inserir(caminhao);
            return new ObjectResult(caminhaodb) { StatusCode = StatusCodes.Status201Created };
        }

        /// <summary>
        /// Atualizar o Caminhão através do Id
        /// </summary>        
        /// <param name="id"></param>
        /// <param name="caminhao"></param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar([FromRoute] int id, [FromBody] Caminhao caminhao)
        {
            if (id == 0)
            {
                throw new Exception($"Id não pode ser zero ou nulo.");
            }

            if (caminhao.AnoModelo > caminhao.AnoFabricacao + 1 || caminhao.AnoModelo < caminhao.AnoFabricacao)
            {
                throw new Exception($"Ano do modelo menor que o ano da Fabricação ou maior que um ano a mais dela.");
            }

            var caminhaodb = await _caminhaoRepository.Atualizar(id, caminhao);
            return new ObjectResult(caminhaodb) { StatusCode = StatusCodes.Status200OK };
        }

        /// <summary>
        /// Deleta o que corresponde ao Id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(id == 0)
            {
                throw new Exception($"Id não pode ser zero ou nulo.");
            }
            await _caminhaoRepository.Deletar(id);
            return Ok();
        }
    }
}
