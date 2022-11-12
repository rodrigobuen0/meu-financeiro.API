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
    public class DespesasController : ControllerBase
    {
        private IDespesasService _despesasService;
        private IJwtUtils _jwtUtils;

        public DespesasController(IDespesasService despesasService, IJwtUtils jwtUtils)
        {
            _despesasService = despesasService;
            _jwtUtils = jwtUtils;

        }

        // GET: api/<DespesasController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var despesas = _despesasService.GetAll(Guid.Parse(Request.Headers["UserId"]));
            return Ok(despesas);
        }

        // GET: api/<ReceitasController>
        [HttpGet("DespesasMes")]
        public IActionResult GetAllValorMes()
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = _jwtUtils.ValidateJwtToken(token);
            if (userId == null)
                return BadRequest("Usuario não encontrado!");
            else
            {
                var receitas = _despesasService.GetAllMes((Guid)userId);
                return Ok(receitas);
            }
        }

        // GET api/<DespesasController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var despesa = _despesasService.GetById(id, Guid.Parse(Request.Headers["UserId"]));
            return Ok(despesa);
        }


        // POST api/<DespesasController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Despesas despesa)
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = _jwtUtils.ValidateJwtToken(token);
            if (userId == null)
                return BadRequest("Usuario não encontrado!");
            else
            {
                despesa.UserId = (Guid)userId;
                var response = await _despesasService.Post(despesa);
                return Ok(response);
            }

            //var response = await _despesasService.Post(despesa, Guid.Parse(Request.Headers["UserId"]));
            //return Ok(response);
        }

        // PUT api/<DespesasController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Despesas despesa)
        {
            var response = await _despesasService.Put(id, despesa, Guid.Parse(Request.Headers["UserId"]));
            return Ok(response);

        }

        // DELETE api/<DespesasController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _despesasService.Delete(id, Guid.Parse(Request.Headers["UserId"]));
            return Ok(response);
        }
    }
}
