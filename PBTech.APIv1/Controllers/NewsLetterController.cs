using Microsoft.AspNetCore.Mvc;
using PBTech.APIv1.Filters;
using PBTech.APIv1.ViewModel;
using PBTech.Domain.Interfaces;
using PBTech.Domain.Models;

namespace PBTech.APIv1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsLetterController : ControllerBase
    {
        INewsLetterRepository NewsRepo;
        public NewsLetterController(INewsLetterRepository NewsRepo)
        {
            this.NewsRepo = NewsRepo;
        }

        /// <summary>
        /// Lista todos os usuários cadastrados
        /// </summary>
        /// <returns>Retorna um array do tipo ReceiveNews[] </returns>
        /// <response code="200">Retorna o novo item criado</response>
        /// <response code="500">Caso aconteça alguma falha interna</response> 
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ReceiveNews>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorMessage))]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await NewsRepo.GetAll());
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }

        // GET api/newsletter/email
        /// <summary>
        /// Lista todos os usuários cadastrados
        /// </summary>
        /// <returns>Retorna um objeto do tipo ReceiveNews</returns>
        /// <response code="200">Retorna um objeto ReceiveNews a partir do e-mail cadastrado</response>
        /// <response code="404">Caso não encontre nenhum resultado</response> 
        /// <response code="500">Caso aconteça alguma falha interna</response> 
        [HttpGet("/api/[controller]/{Email}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReceiveNews))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorMessage))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorMessage))]
        public async Task<IActionResult> GetByEmail([FromRoute]string Email)
        {
            try
            {
                var receiveNews = await NewsRepo.GetByEmail(Email);

                if (receiveNews == null)
                    return NotFound("Register not found!");

                return Ok(receiveNews);
            }
            catch (Exception ex)
            {
                return StatusCode(500,new ErrorMessage { Message = ex.Message });
            }
        }

        // POST api/newsletter
        /// <summary>
        /// Inserir um novo registro ReceiveNews
        /// </summary>
        /// <returns>Cadastra um novo ReceiveNews</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/newsletter
        ///     {        
        ///       "fullName": "My Name",
        ///       "email": "my@email.com.br"     
        ///     }
        /// </remarks>
        /// <response code="201">Retorna o novo ReceiveNews criado</response>
        /// <response code="400">Caso o body não esteja com informações insuficientes</response>
        /// <response code="500">Caso aconteça alguma falha interna</response> 
        [HttpPost]
        [Produces("application/json")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ReceiveNews))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<PropertyError>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorMessage))]
        public async Task<IActionResult> Post([FromBody]ReceiveNews Receive)
        {
            try
            {
                var receiveNews = await NewsRepo.GetByEmail(Receive.Email);

                if (receiveNews != null)
                    return BadRequest("Email is use another register!");
                else
                    receiveNews = new ReceiveNews { FullName = Receive.FullName, Email = Receive.Email };
              
                if (!ModelState.IsValid)
                {
                    var x = ModelState;
                    return BadRequest(ModelState);
                }

                var result = await NewsRepo.Add(receiveNews);
                return CreatedAtAction(nameof(GetByEmail), new { Email = result.Email }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorMessage { Message = ex.Message });
            }
        }

        // PUT api/newsletter
        /// <summary>
        /// Altera um registro do tipo ReceiveNews
        /// </summary>
        /// Sample request:
        /// 
        ///     PUT api/newsletter
        ///     { 
        ///       "id":10,
        ///       "fullName": "My Name",
        ///       "email": "my@email.com.br"     
        ///     }
        /// </remarks>
        /// <returns>Cadastra um novo ReceiveNews</returns>
        /// <response code="201">Registro alterado com sucesso</response>
        /// <response code="400">Caso o body não esteja com informações insuficientes</response>
        /// <response code="500">Caso aconteça alguma falha interna</response> 
        [HttpPut("/api/[controller]/{Id}")]
        [Produces("application/json")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(ReceiveNews))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<PropertyError>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorMessage))]
        public async Task<IActionResult> Put(int Id,  [FromBody] ReceiveNews Receive)
        {
            try
            {

                if (Id != Receive.Id)
                {
                    return BadRequest();
                }

                var exists = await NewsRepo.GetById(Receive.Id);

                if(exists == null)
                {
                    return NotFound("Register not found!");
                }               

                await NewsRepo.Update(Receive);   

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorMessage { Message = ex.Message });
            }
        }

        // DELETE api/newsletter
        /// <summary>
        /// Adiciona um novo registro para receber novidades
        /// </summary>
        /// <returns>Retorna uma lista de codigos de demissões cadastrados</returns>
        /// <response code="200">Registro deletado com sucesso</response>
        /// <response code="404">Caso o registro não tenha sido encontrado</response> 
        /// <response code="500">Caso aconteça alguma falha interna</response> 
        [HttpDelete("/api/[controller]/{Email}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReceiveNews))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorMessage))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(List<ErrorMessage>))]
        public async Task<IActionResult> Delete(string Email)
        {
            try
            {
                var Receive = await NewsRepo.GetByEmail(Email);

                if (Receive == null)
                    return NotFound("Register not found!");

                await NewsRepo.Delete(Receive);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorMessage { Message = ex.Message });
            }
        }
    }
}
