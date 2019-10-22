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
    public class ReceitaController: ControllerBase
    {
        XepaDigitalContext _contexto = new XepaDigitalContext(); 

        //GET: api/Receita
        [HttpGet]
        public async Task<ActionResult<List<Receita>>> Get(){
            var Receitas = await _contexto.Receita.ToListAsync();

            if(Receitas == null){
                return NotFound();
            }

            return Receitas;
        }

        //FAZENDO SELECT NO BANCO
        //GET: api/Receita/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Receita>> Get(int id){
            var Receita = await _contexto.Receita.FindAsync(id);

            if(Receita == null){
                return NotFound();
            }

            return Receita;
        }  

        //FAZENDO ENVIO PARA O BANCO
        //POST api/Receita
        [HttpPost]
        public async Task<ActionResult<Receita>> Post(Receita Receita){
            try
            {
                //Tratamos contra ataques de SQL Injection
                await _contexto.AddAsync(Receita);
                //Salvamos efetivamente o nosso objeto no BD
                await _contexto.SaveChangesAsync();   
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;

            }

            return Receita;
        }


        //FAZENDO UPDATE NO BANCO
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Receita Receita){
            //Se o Id do objeto não existir
            //ele retorna 400 
            if(id != Receita.IdReceita){
                return BadRequest();
            }

            //Faz uma comparação do que foi mudado no Banco
            //Comparamos os atributos que foram modificados através do EF
            _contexto.Entry(Receita).State = EntityState.Modified;
            //UPDATE Receita SET titulo = "nt" where id =2

            try
            {
                await _contexto.SaveChangesAsync();    
            }
            catch (DbUpdateConcurrencyException)
            {
                //Verificamos se o objeto inserido realmente existe no banco
                var Receita_valido = await _contexto.Receita.FindAsync(id);
                if(Receita_valido == null){
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
        public async Task<ActionResult<Receita>> Delete(int id){
            var Receita = await _contexto.Receita.FindAsync(id);
            if(Receita == null){
                return NotFound();
            }
            
            //Selecionando o objeto a ser removido
            _contexto.Receita.Remove(Receita);
            //De fato deleta o arquivo
            await _contexto.SaveChangesAsync();
            return Receita;
        }

    }
}