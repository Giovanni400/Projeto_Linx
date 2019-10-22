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
    public class ProdutoController: ControllerBase
    {
        XepaDigitalContext _contexto = new XepaDigitalContext(); 

        //GET: api/Produto
        [HttpGet]
        public async Task<ActionResult<List<Produto>>> Get(){
            var Produtos = await _contexto.Produto.ToListAsync();

            if(Produtos == null){
                return NotFound();
            }

            return Produtos;
        }

        //FAZENDO SELECT NO BANCO
        //GET: api/Produto/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> Get(int id){
            var Produto = await _contexto.Produto.FindAsync(id);

            if(Produto == null){
                return NotFound();
            }

            return Produto;
        }  

        //FAZENDO ENVIO PARA O BANCO
        //POST api/Produto
        [HttpPost]
        public async Task<ActionResult<Produto>> Post(Produto Produto){
            try
            {
                //Tratamos contra ataques de SQL Injection
                await _contexto.AddAsync(Produto);
                //Salvamos efetivamente o nosso objeto no BD
                await _contexto.SaveChangesAsync();   
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;

            }

            return Produto;
        }


        //FAZENDO UPDATE NO BANCO
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Produto Produto){
            //Se o Id do objeto não existir
            //ele retorna 400 
            if(id != Produto.IdProduto){
                return BadRequest();
            }

            //Faz uma comparação do que foi mudado no Banco
            //Comparamos os atributos que foram modificados através do EF
            _contexto.Entry(Produto).State = EntityState.Modified;
            //UPDATE Produto SET titulo = "nt" where id =2

            try
            {
                await _contexto.SaveChangesAsync();    
            }
            catch (DbUpdateConcurrencyException)
            {
                //Verificamos se o objeto inserido realmente existe no banco
                var Produto_valido = await _contexto.Produto.FindAsync(id);
                if(Produto_valido == null){
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
        public async Task<ActionResult<Produto>> Delete(int id){
            var Produto = await _contexto.Produto.FindAsync(id);
            if(Produto == null){
                return NotFound();
            }
            
            //Selecionando o objeto a ser removido
            _contexto.Produto.Remove(Produto);
            //De fato deleta o arquivo
            await _contexto.SaveChangesAsync();
            return Produto;
        }

    }
}