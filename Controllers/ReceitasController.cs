using meu_financeiro.API.Authorization;
using meu_financeiro.API.Entities;
using meu_financeiro.API.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace meu_financeiro.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReceitasController : ControllerBase
    {
        private IReceitasService _receitasService;

        public ReceitasController(IReceitasService receitasService)
        {
            _receitasService = receitasService;
        }

        // GET: api/<ReceitasController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var receitas = _receitasService.GetAll(Guid.Parse(Request.Headers["UserId"]));
            return Ok(receitas);
        }

        // GET api/<ReceitasController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var receita = _receitasService.GetById(id);
            return Ok(receita);
        }


        // POST api/<ReceitasController>
        [HttpPost]
        public IActionResult Post([FromBody] Receitas receita)
        {
            var response = _receitasService.Post(receita);
            return Ok(response);
        }

        // PUT api/<ReceitasController>/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] Receitas receita)
        {
        }

        // DELETE api/<ReceitasController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
        }
    }
}
