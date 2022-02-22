using ApiAuth.Models;
using ApiAuth.Repositories;
using ApiAuth.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiAuth.Controllers
{

    [ApiController]
    [Route("V1")]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] User model)
        {
            //Recupera o usuario
            var user = UserRepository.Get(model.Username, model.Password);

            //Verifica se o usuario existe
            if (user == null)
                return NotFound(new { message = "Usuario ou senha invalidos" });

            //Gera o token
            var token = TokenService.GenerateToken(user);

            //Oculta a senha
            user.Password = "";

            //Retorna os dados
            return new
            {
                user = user,
                token = token

            };
        }

    }
}
