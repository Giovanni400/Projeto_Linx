using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    //Definimos nossa rota do controller e dizemos que é um controller de API
    [Route("api/[Controller]")]
    [ApiController]
    public class UsuarioController: ControllerBase
    {
        XepaDigitalContext _contexto = new XepaDigitalContext(); 

        //GET: api/Usuario
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> Get(){
            var Usuarios = await _contexto.Usuario.Include("IdEnderecoNavigation").ToListAsync();

            if(Usuarios == null){
                return NotFound();
            }

            return Usuarios;
        }

        //FAZENDO SELECT NO BANCO
        //GET: api/Usuario/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> Get(int id){
            var Usuario = await _contexto.Usuario.Include("IdEnderecoNavigation").FirstOrDefaultAsync(e => e.IdUsuario == id);

            if(Usuario == null){
                return NotFound();
            }

            return Usuario;
        }  

        //FAZENDO ENVIO PARA O BANCO
        //POST api/Usuario
        [HttpPost]
        public async Task<ActionResult<Usuario>> Post(Usuario Usuario){
            try
            {
                //Tratamos contra ataques de SQL Injection
                await _contexto.AddAsync(Usuario);
                //Salvamos efetivamente o nosso objeto no BD
                await _contexto.SaveChangesAsync();   
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;

            }

            return Usuario;
        }


        //FAZENDO UPDATE NO BANCO
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Usuario Usuario){
            //Se o Id do objeto não existir
            //ele retorna 400 
            if(id != Usuario.IdUsuario){
                return BadRequest();
            }

            //Faz uma comparação do que foi mudado no Banco
            //Comparamos os atributos que foram modificados através do EF
            _contexto.Entry(Usuario).State = EntityState.Modified;
            //UPDATE Usuario SET titulo = "nt" where id =2

            try
            {
                await _contexto.SaveChangesAsync();    
            }
            catch (DbUpdateConcurrencyException)
            {
                //Verificamos se o objeto inserido realmente existe no banco
                var Usuario_valido = await _contexto.Usuario.FindAsync(id);
                if(Usuario_valido == null){
                    return NotFound();
                }else{
                    throw;
                }
            }

            //NoContent = Retorna 204, sem nada
            return NoContent();
        }

        //FAZENDO DELETE NO BANCO
        [HttpDelete("{id}")]
        public async Task<ActionResult<Usuario>> Delete(int id){
            var Usuario = await _contexto.Usuario.FindAsync(id);
            if(Usuario == null){
                return NotFound();
            }
            
            //Selecionando o objeto a ser removido
            _contexto.Usuario.Remove(Usuario);
            //De fato deleta o arquivo
            await _contexto.SaveChangesAsync();
            return Usuario;
        }

    }
}