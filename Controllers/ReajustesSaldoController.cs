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
    public class ReajustesSaldoController : ControllerBase
    {
        private IReajusteSaldoService _reajusteSaldoService;
        private IJwtUtils _jwtUtils;

        public ReajustesSaldoController(IReajusteSaldoService reajustesSaldoService, IJwtUtils jwtUtils)
        {
            _reajusteSaldoService = reajustesSaldoService;
            _jwtUtils = jwtUtils;
        }

        // GET: api/<ReajustesSaldoController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = _jwtUtils.ValidateJwtToken(token);
            if (userId == null)
                return BadRequest("Usuario não encontrado!");
            else
            {
                var ReajustesSaldo = _reajusteSaldoService.GetAll((Guid)userId);
                return Ok(ReajustesSaldo);
            }
        }

        [HttpGet("ReajustesSaldoMes")]
        public IActionResult GetAllMes()
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = _jwtUtils.ValidateJwtToken(token);
            if (userId == null)
                return BadRequest("Usuario não encontrado!");
            else
            {
                var ReajustesSaldo = _reajusteSaldoService.GetAllMes((Guid)userId);
                return Ok(ReajustesSaldo);
            }
        }

        // GET api/<ReajustesSaldoController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var receita = _reajusteSaldoService.GetById(id, Guid.Parse(Request.Headers["UserId"]));
            return Ok(receita);
        }


        // POST api/<ReajustesSaldoController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ReajustesSaldoContas reajusteSaldoConta)
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = _jwtUtils.ValidateJwtToken(token);
            if (userId == null)
                return BadRequest("Usuario não encontrado!");
            else
            {
                reajusteSaldoConta.UserId = (Guid)userId;
                var response = await _reajusteSaldoService.Post(reajusteSaldoConta);
                return Ok(response);
            }
        }

        // PUT api/<ReajustesSaldoController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] ReajustesSaldoContas reajusteSaldoConta)
        {
            var response = await _reajusteSaldoService.Put(id, reajusteSaldoConta, Guid.Parse(Request.Headers["UserId"]));
            return Ok(response);

        }

        // DELETE api/<ReajustesSaldoController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _reajusteSaldoService.Delete(id, Guid.Parse(Request.Headers["UserId"]));
            return Ok(response);
        }
    }
}
