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
    public class SobreProdutoController: ControllerBase
    {
        XepaDigitalContext _contexto = new XepaDigitalContext(); 

        //GET: api/SobreProduto
        [HttpGet]
        public async Task<ActionResult<List<SobreProduto>>> Get(){
            var SobreProdutos = await _contexto.SobreProduto.ToListAsync();

            if(SobreProdutos == null){
                return NotFound();
            }

            return SobreProdutos;
        }

        //FAZENDO SELECT NO BANCO
        //GET: api/SobreProduto/2
        [HttpGet("{id}")]
        public async Task<ActionResult<SobreProduto>> Get(int id){
            var SobreProduto = await _contexto.SobreProduto.FindAsync(id);

            if(SobreProduto == null){
                return NotFound();
            }

            return SobreProduto;
        }  

        //FAZENDO ENVIO PARA O BANCO
        //POST api/SobreProduto
        [HttpPost]
        public async Task<ActionResult<SobreProduto>> Post(SobreProduto SobreProduto){
            try
            {
                //Tratamos contra ataques de SQL Injection
                await _contexto.AddAsync(SobreProduto);
                //Salvamos efetivamente o nosso objeto no BD
                await _contexto.SaveChangesAsync();   
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;

            }

            return SobreProduto;
        }


        //FAZENDO UPDATE NO BANCO
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, SobreProduto SobreProduto){
            //Se o Id do objeto não existir
            //ele retorna 400 
            if(id != SobreProduto.IdSobreProduto){
                return BadRequest();
            }

            //Faz uma comparação do que foi mudado no Banco
            //Comparamos os atributos que foram modificados através do EF
            _contexto.Entry(SobreProduto).State = EntityState.Modified;
            //UPDATE SobreProduto SET titulo = "nt" where id =2

            try
            {
                await _contexto.SaveChangesAsync();    
            }
            catch (DbUpdateConcurrencyException)
            {
                //Verificamos se o objeto inserido realmente existe no banco
                var SobreProduto_valido = await _contexto.SobreProduto.FindAsync(id);
                if(SobreProduto_valido == null){
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
        public async Task<ActionResult<SobreProduto>> Delete(int id){
            var SobreProduto = await _contexto.SobreProduto.FindAsync(id);
            if(SobreProduto == null){
                return NotFound();
            }
            
            //Selecionando o objeto a ser removido
            _contexto.SobreProduto.Remove(SobreProduto);
            //De fato deleta o arquivo
            await _contexto.SaveChangesAsync();
            return SobreProduto;
        }

    }
}