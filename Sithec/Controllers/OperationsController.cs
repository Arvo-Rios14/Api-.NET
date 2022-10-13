using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Sithec.Models.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Sithec.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsController : ControllerBase
    {
        [HttpPost]
        public ActionResult<OperationResponse> PostOperation(double firstNumber, double secondNumber, Operations operation)
        {
            try
            {
                var result = AvailableOperations(firstNumber, secondNumber, operation);
                if (result.Error) return BadRequest("La división dió como resultado un valor infinito.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult<OperationResponse> GetOperation([FromHeader] double firstNumber, [FromHeader] double secondNumber, [FromHeader] Operations operation)
        {
            try
            {
                var result = AvailableOperations(firstNumber, secondNumber, operation);
                if (result.Error) return BadRequest("La división dió como resultado un valor infinito.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetHumanMockArray")]
        public ActionResult<Human[]> GetHumanMockArray()
        {
            Random random = new Random();
            List<Human> humans = new List<Human>();
            List<string> genreList = new List<string> { "Masculino", "Femenino", "Otro" };
            try
            {

                for (int i = 0; i < 5; i++)
                {
                    Human human = new Human();
                    human.Id = i;
                    human.Nombre = "Humano " + (i + 1).ToString();
                    human.Edad = random.Next(1, 50);
                    human.Sexo = genreList[random.Next(genreList.Count)];
                    human.Altura = random.Next(130, 215);
                    human.Peso = random.Next(50, 167);
                    humans.Add(human);
                }
                return Ok(humans.ToArray());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        private OperationResponse AvailableOperations(double x, double y, Operations op)
        {
            OperationResponse response = new OperationResponse();
            response.Operation = "La operación seleccionada es: " + Enum.GetName(typeof(Operations), op);
            response.Error = false;
            switch (op)
            {
                case Operations.Resta:
                    response.Result = x - y;
                    break;
                case Operations.Suma:
                    response.Result = x + y;
                    break;
                case Operations.Division:
                    response.Result = x / y;
                    if (double.IsInfinity(response.Result))
                    {
                        response.Error = true;
                    }
                    break;

                case Operations.Multiplicacion:
                    response.Result = x * y;
                    break;
                default:
                    response.Result = 0;
                    response.Operation = "No ha seleccionado ninguna operación.";
                    break;

            }
            return response;
        }
        public enum Operations
        {
            Suma,
            Resta,
            Multiplicacion,
            Division
        }

        public class OperationResponse
        {
            public string Operation { get; set; }
            public double Result { get; set; }
            [NotMapped]
            public bool Error { get; set; }
        }
    }
}
