using APITruck.Controllers;
using APITruck.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace APITruck.Tests.Controllers
{
    public class CrudeControllerTests
    {
        private CrudeController crudeController;

        public CrudeControllerTests()
        {
            crudeController = new CrudeController(new Mock<CaminhaoRepository>().Object);
        }

        [Fact]
        public void Insert_ObjetoAnoModeloValido()
        {
            Assert.True(true);
        }
    }
}
