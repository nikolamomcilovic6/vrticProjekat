using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
namespace Projekat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VrticController : ControllerBase
    {
        public VrticContext Context { get; set; }
        public VrticController(VrticContext context)
        {
            Context=context;            
        }

    

        [EnableCors("CORS")]
        [Route("PreuzmiVrtic")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiVrtic()
        {
            
            /*var vrtici=Context.Vrtici
            .Include(p=>p.Vaspitaci)
            .Include(p=>p.VrticAktivnost)
            .ThenInclude(p=>p.Aktivnost)
            .ThenInclude(p=>p.AktivnostDete)
            .Include(p=>p.Deca);
              return Ok(vrtici);*/

            try
            {
                return Ok(await Context.Vrtici.Select(p=> new{p.ID, p.Naziv}).ToListAsync());
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

          

        }

        
    
            


        [EnableCors("CORS")]
        [Route("DodatiVrtic")]
        [HttpPost]
        public async Task<ActionResult> DodajVrtic(String naziv)
        {
            if(string.IsNullOrWhiteSpace(naziv) || naziv.Length>50)
            {
                return BadRequest("Pogresan naziv za vrtic!");
            }

            try
            {
                var v=await Context.Vrtici.Where(p=>p.Naziv==naziv).FirstOrDefaultAsync();
                if(v!=null)
                return BadRequest("Vrtic vec postoji u bazi");
                Vrtic vrtic=new Vrtic();
                vrtic.Naziv=naziv;
                
                Context.Vrtici.Add(vrtic);
                await Context.SaveChangesAsync();
                return Ok("Vrtic je uspesno dodat!");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
       /* [EnableCors("CORS")]
        [Route("IzmeniVrtic")]
        [HttpPut]
        public async Task<ActionResult> PromeniVrtic(int id, string naziv)
        {
            if(id<=0)
            {
                return BadRequest("ID mora da bude veci od 0");
            }
            try
            {
                var vrtic= await Context.Vrtici.FindAsync(id);

                if(vrtic!=null)
                {
                    vrtic.Naziv=naziv;
                    
                        await Context.SaveChangesAsync();
                        return Ok("Uspesno promenjen vrtic");
                }
                else
                {
                    return BadRequest($"Vrtic sa id-em {id} nije pronadjen");
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }*/





        [EnableCors("CORS")]
        [Route("ObrisatiVrtic/{naziv}")]
        [HttpDelete]
        public async Task<ActionResult> ObrisiVrtic(string naziv)
        {
            if(string.IsNullOrWhiteSpace(naziv) || naziv.Length>50)
            {
                return BadRequest("Pogresan naziv za vrtic!");
            }
            try
            {
                var vrtic=Context.Vrtici.Where(p=>p.Naziv==naziv).FirstOrDefault();
                if(vrtic==null)
                return BadRequest("Ne postoji vrtic sa zadatim nazivom");
                string Naziv=vrtic.Naziv;
                Context.Vrtici.Remove(vrtic);
                await Context.SaveChangesAsync();
                return Ok($"Uspesno obrisan vrtic sa nazivom:{Naziv}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
