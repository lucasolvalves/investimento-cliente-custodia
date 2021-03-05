using KissLog;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Investimento.Cliente.Custodia.Controllers.V2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/testes")]
    public class TestesController : MainController
    {
        public TestesController(ILogger logger) : base(logger) { }

        [HttpGet("dividebyzeroexception")]
        public int TesteException()
        {
            try
            {
                var i = 0;
                var result = 42 / i;

                return result;
            }
            catch (DivideByZeroException ex)
            {
                throw ex;
            }
        }

        [HttpGet("incluidologinfo")]
        public string Teste()
        {
            _logger.Info("FESTA DO PEÃO DE BARRETOS");
            return "SEGURA PEÃO!!!! sou a V2";
        }
    }
}
