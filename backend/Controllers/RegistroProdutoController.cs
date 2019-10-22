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
    public class RegistroProdutoController: ControllerBase
    {
        XepaDigitalContext _contexto = new XepaDigitalContext(); 

        //GET: api/RegistroProduto
        [HttpGet]
        public async Task<ActionResult<List<RegistroProduto>>> Get(){
            var RegistroProdutos = await _contexto.RegistroProduto.ToListAsync();

            if(RegistroProdutos == null){
                return NotFound();
            }

            return RegistroProdutos;
        }

        //FAZENDO SELECT NO BANCO
        //GET: api/RegistroProduto/2
        [HttpGet("{id}")]
        public async Task<ActionResult<RegistroProduto>> Get(int id){
            var RegistroProduto = await _contexto.RegistroProduto.FindAsync(id);

            if(RegistroProduto == null){
                return NotFound();
            }

            return RegistroProduto;
        }  

        //FAZENDO ENVIO PARA O BANCO
        //POST api/RegistroProduto
        [HttpPost]
        public async Task<ActionResult<RegistroProduto>> Post(RegistroProduto RegistroProduto){
            try
            {
                //Tratamos contra ataques de SQL Injection
                await _contexto.AddAsync(RegistroProduto);
                //Salvamos efetivamente o nosso objeto no BD
                await _contexto.SaveChangesAsync();   
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;

            }

            return RegistroProduto;
        }


        //FAZENDO UPDATE NO BANCO
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, RegistroProduto RegistroProduto){
            //Se o Id do objeto não existir
            //ele retorna 400 
            if(id != RegistroProduto.IdRegistro){
                return BadRequest();
            }

            //Faz uma comparação do que foi mudado no Banco
            //Comparamos os atributos que foram modificados através do EF
            _contexto.Entry(RegistroProduto).State = EntityState.Modified;
            //UPDATE RegistroProduto SET titulo = "nt" where id =2

            try
            {
                await _contexto.SaveChangesAsync();    
            }
            catch (DbUpdateConcurrencyException)
            {
                //Verificamos se o objeto inserido realmente existe no banco
                var RegistroProduto_valido = await _contexto.RegistroProduto.FindAsync(id);
                if(RegistroProduto_valido == null){
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
        public async Task<ActionResult<RegistroProduto>> Delete(int id){
            var RegistroProduto = await _contexto.RegistroProduto.FindAsync(id);
            if(RegistroProduto == null){
                return NotFound();
            }
            
            //Selecionando o objeto a ser removido
            _contexto.RegistroProduto.Remove(RegistroProduto);
            //De fato deleta o arquivo
            await _contexto.SaveChangesAsync();
            return RegistroProduto;
        }

    }
}