using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    //Definimos nossa rota do controller e dizemos que é um controller de API
    [Route("api/[Controller]")]
    [ApiController]
    public class EnderecoController: ControllerBase
    {
        XepaDigitalContext _contexto = new XepaDigitalContext(); 

        //GET: api/Endereco
        [HttpGet]
        public async Task<ActionResult<List<Endereco>>> Get(){
            var Enderecos = await _contexto.Endereco.ToListAsync();

            if(Enderecos == null){
                return NotFound();
            }

            return Enderecos;
        }

        //FAZENDO SELECT NO BANCO
        //GET: api/Endereco/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Endereco>> Get(int id){
            var Endereco = await _contexto.Endereco.FindAsync(id);

            if(Endereco == null){
                return NotFound();
            }

            return Endereco;
        }  

        //FAZENDO ENVIO PARA O BANCO
        //POST api/Endereco
        [HttpPost]
        public async Task<ActionResult<Endereco>> Post(Endereco Endereco){
            try
            {
                //Tratamos contra ataques de SQL Injection
                await _contexto.AddAsync(Endereco);
                //Salvamos efetivamente o nosso objeto no BD
                await _contexto.SaveChangesAsync();   
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;

            }

            return Endereco;
        }


        //FAZENDO UPDATE NO BANCO
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Endereco Endereco){
            //Se o Id do objeto não existir
            //ele retorna 400 
            if(id != Endereco.IdEndereco){
                return BadRequest();
            }

            //Faz uma comparação do que foi mudado no Banco
            //Comparamos os atributos que foram modificados através do EF
            _contexto.Entry(Endereco).State = EntityState.Modified;
            //UPDATE Endereco SET titulo = "nt" where id =2

            try
            {
                await _contexto.SaveChangesAsync();    
            }
            catch (DbUpdateConcurrencyException)
            {
                //Verificamos se o objeto inserido realmente existe no banco
                var Endereco_valido = await _contexto.Endereco.FindAsync(id);
                if(Endereco_valido == null){
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
        public async Task<ActionResult<Endereco>> Delete(int id){
            var Endereco = await _contexto.Endereco.FindAsync(id);
            if(Endereco == null){
                return NotFound();
            }
            
            //Selecionando o objeto a ser removido
            _contexto.Endereco.Remove(Endereco);
            //De fato deleta o arquivo
            await _contexto.SaveChangesAsync();
            return Endereco;
        }

    }
}