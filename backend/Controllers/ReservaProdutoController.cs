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
    public class ReservaProdutoController: ControllerBase
    {
        XepaDigitalContext _contexto = new XepaDigitalContext(); 

        //GET: api/ReservaProduto
        [HttpGet]
        public async Task<ActionResult<List<ReservaProduto>>> Get(){
            var ReservaProdutos = await _contexto.ReservaProduto.ToListAsync();

            if(ReservaProdutos == null){
                return NotFound();
            }

            return ReservaProdutos;
        }

        //FAZENDO SELECT NO BANCO
        //GET: api/ReservaProduto/2
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservaProduto>> Get(int id){
            var ReservaProduto = await _contexto.ReservaProduto.FindAsync(id);

            if(ReservaProduto == null){
                return NotFound();
            }

            return ReservaProduto;
        }  

        //FAZENDO ENVIO PARA O BANCO
        //POST api/ReservaProduto
        [HttpPost]
        public async Task<ActionResult<ReservaProduto>> Post(ReservaProduto ReservaProduto){
            try
            {
                //Tratamos contra ataques de SQL Injection
                await _contexto.AddAsync(ReservaProduto);
                //Salvamos efetivamente o nosso objeto no BD
                await _contexto.SaveChangesAsync();   
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;

            }

            return ReservaProduto;
        }


        //FAZENDO UPDATE NO BANCO
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, ReservaProduto ReservaProduto){
            //Se o Id do objeto não existir
            //ele retorna 400 
            if(id != ReservaProduto.IdReserva){
                return BadRequest();
            }

            //Faz uma comparação do que foi mudado no Banco
            //Comparamos os atributos que foram modificados através do EF
            _contexto.Entry(ReservaProduto).State = EntityState.Modified;
            //UPDATE ReservaProduto SET titulo = "nt" where id =2

            try
            {
                await _contexto.SaveChangesAsync();    
            }
            catch (DbUpdateConcurrencyException)
            {
                //Verificamos se o objeto inserido realmente existe no banco
                var ReservaProduto_valido = await _contexto.ReservaProduto.FindAsync(id);
                if(ReservaProduto_valido == null){
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
        public async Task<ActionResult<ReservaProduto>> Delete(int id){
            var ReservaProduto = await _contexto.ReservaProduto.FindAsync(id);
            if(ReservaProduto == null){
                return NotFound();
            }
            
            //Selecionando o objeto a ser removido
            _contexto.ReservaProduto.Remove(ReservaProduto);
            //De fato deleta o arquivo
            await _contexto.SaveChangesAsync();
            return ReservaProduto;
        }

    }
}