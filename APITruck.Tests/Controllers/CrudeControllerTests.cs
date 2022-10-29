using APITruck.Controllers;
using APITruck.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace APITruck.Tests.Controllers
{
    public class CrudeControllerTests
    {
        private CaminhaoController _caminhaoController;

        public CrudeControllerTests()
        {
            _caminhaoController = new CaminhaoController(new Mock<ICaminhaoRepository>().Object);
        }


        public List<Caminhao> CriarRepository()
        {
            List<Caminhao> caminhoes = new List<Caminhao>() { new Caminhao
            {
                Id = 1,
                AnoFabricacao = 2022,
                AnoModelo = 2023,
                NomeModelo = ModeloNome.FM
            },
            new Caminhao
            {
                Id = 2,
                AnoFabricacao = 2022,
                AnoModelo = 2023,
                NomeModelo = ModeloNome.FH
            }};

            return caminhoes;
        }

        #region Gets
        [Fact]
        public void GetCaminhoes()
        {
            var resp = _caminhaoController.GetCaminhoes();
            var result = resp.Result as ObjectResult;

            if (result.StatusCode == 200)
            {
                Assert.True(true);
            }
            else
            {
                Assert.True(false);
            }
        }

        [Fact]
        public async Task GetCaminhaoId_IdNaoencontrado()
        {
            int id = 10000;

            var fakeRepository = CriarRepository();
            var mockRepository = new Mock<ICaminhaoRepository>();
            mockRepository.Setup(x => x.BuscarId(id)).Returns(Task.FromResult(fakeRepository.FirstOrDefault(u => u.Id == id)));
            _caminhaoController = new CaminhaoController(mockRepository.Object);

            await Assert.ThrowsAsync<Exception>(async () =>
            await _caminhaoController.GetCaminhaoId(id));
        }

        #endregion

        #region Insert

        [Fact]
        public void Insert_CaminhaoValido()
        {
            var resp = _caminhaoController.Insert(new Caminhao
            {
                Id = 0,
                AnoFabricacao = 2022,
                AnoModelo = 2023,
                NomeModelo = ModeloNome.FM
            });

            //Assert
            var result = resp.Result as ObjectResult;

            if (result.StatusCode == 201)
            {
                Assert.True(true);
            }
            else
            {
                Assert.True(false);
            }
        }

        [Fact]
        public async Task Insert_AnoModeloInvalido()
        {
            await Assert.ThrowsAsync<Exception>(async () =>
            await _caminhaoController.Insert(new Caminhao
            {
                Id = 0,
                AnoFabricacao = 2022,
                AnoModelo = 2024,
                NomeModelo = ModeloNome.FM
            }));
        }

        [Fact]
        public async Task Insert_AnoFabricacaoInvalido()
        {
            await Assert.ThrowsAsync<Exception>(async () =>
            await _caminhaoController.Insert(new Caminhao
            {
                Id = 0,
                AnoFabricacao = 1997,
                AnoModelo = 1998,
                NomeModelo = ModeloNome.FM
            }));
        }

        [Fact]
        public async Task Insert_AnoFabricacaoNulo()
        {
            await Assert.ThrowsAsync<Exception>(async () =>
            await _caminhaoController.Insert(new Caminhao
            {
                Id = 0,
                AnoFabricacao = 0,
                AnoModelo = 0,
                NomeModelo = ModeloNome.FM
            }));
        }

        #endregion

        #region Delete
        [Fact]
        public async Task Delete_IdZeroNulo()
        {
            await Assert.ThrowsAsync<Exception>(async () =>
            await _caminhaoController.Delete(0));
        }

        [Fact]
        public async Task Atualizar_IdZeroNulo()
        {
            await Assert.ThrowsAsync<Exception>(async () =>
            await _caminhaoController.Atualizar(0, new Caminhao
            {
                Id = 0,
                AnoFabricacao = 2022,
                AnoModelo = 2023,
                NomeModelo = ModeloNome.FM
            }));
        }
        #endregion


    }
}
