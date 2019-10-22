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
    public class ColaboradorController: ControllerBase
    {
        XepaDigitalContext _contexto = new XepaDigitalContext(); 

        //GET: api/Colaborador
        [HttpGet]
        public async Task<ActionResult<List<Colaborador>>> Get(){
            var Colaboradors = await _contexto.Colaborador.ToListAsync();

            if(Colaboradors == null){
                return NotFound();
            }

            return Colaboradors;
        }

        //FAZENDO SELECT NO BANCO
        //GET: api/Colaborador/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Colaborador>> Get(int id){
            var Colaborador = await _contexto.Colaborador.FindAsync(id);

            if(Colaborador == null){
                return NotFound();
            }

            return Colaborador;
        }  

        //FAZENDO ENVIO PARA O BANCO
        //POST api/Colaborador
        [HttpPost]
        public async Task<ActionResult<Colaborador>> Post(Colaborador Colaborador){
            try
            {
                //Tratamos contra ataques de SQL Injection
                await _contexto.AddAsync(Colaborador);
                //Salvamos efetivamente o nosso objeto no BD
                await _contexto.SaveChangesAsync();   
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;

            }

            return Colaborador;
        }


        //FAZENDO UPDATE NO BANCO
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Colaborador Colaborador){
            //Se o Id do objeto não existir
            //ele retorna 400 
            if(id != Colaborador.IdColaborador){
                return BadRequest();
            }

            //Faz uma comparação do que foi mudado no Banco
            //Comparamos os atributos que foram modificados através do EF
            _contexto.Entry(Colaborador).State = EntityState.Modified;
            //UPDATE Colaborador SET titulo = "nt" where id =2

            try
            {
                await _contexto.SaveChangesAsync();    
            }
            catch (DbUpdateConcurrencyException)
            {
                //Verificamos se o objeto inserido realmente existe no banco
                var Colaborador_valido = await _contexto.Colaborador.FindAsync(id);
                if(Colaborador_valido == null){
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
        public async Task<ActionResult<Colaborador>> Delete(int id){
            var Colaborador = await _contexto.Colaborador.FindAsync(id);
            if(Colaborador == null){
                return NotFound();
            }
            
            //Selecionando o objeto a ser removido
            _contexto.Colaborador.Remove(Colaborador);
            //De fato deleta o arquivo
            await _contexto.SaveChangesAsync();
            return Colaborador;
        }

    }
}