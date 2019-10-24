using System.Linq;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System;
using Microsoft.AspNetCore.Authorization;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController: ControllerBase
    {
        //Chamamos nosso contexto da base de dados
        XepaDigitalContext _context = new XepaDigitalContext();

        // Definimos uma variável para percorrer nossos métodos com as configurações 
        // obtidas no appsettings.json
        private IConfiguration _config;

        //Definimos um método construtor para poder acessar essas configs ^
        public LoginController(IConfiguration config){
            _config = config;
        }

        //Chamamos nosso método para validar o usuário na aplicação
        private Usuario ValidarUsuario(Usuario login){
            var usuario = _context.Usuario.FirstOrDefault(
                u => u.EmailUsuario == login.EmailUsuario &&
                u.SenhaUsuario == login.SenhaUsuario
            );

            if(usuario != null){
                usuario = login;
            }

            return usuario;
        }

        //Geramos o Token
        private string GerarToken(Usuario userInfo){
            //Definimos a criptografia do nosso Token
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"])); 
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);  

            //Definimos nossas Claims (dados da sessão)
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.NameId, userInfo.NomeUsuario),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.EmailUsuario),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }; 

            //Configuramos nosso Token e seu tempo de vida
            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials : credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //Usamos essa anotação para ignorar a autenticação nesse método
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody]Usuario login){
            IActionResult response = Unauthorized();
            var user = ValidarUsuario(login);

            if(user != null){
                var tokenString = GerarToken(user);
                response = Ok( new { token  = tokenString } );
            }

            return response;
        }
        
    }
}