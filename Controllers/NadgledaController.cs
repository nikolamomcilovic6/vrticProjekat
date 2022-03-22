using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
namespace Projekat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NadgledaController : ControllerBase
    {
        public VrticContext Context { get; set; }
        public NadgledaController(VrticContext context)
        {
            Context=context;            
        }

        [EnableCors("CORS")]
        [Route("NadgledaAktivnost/{jmbgVaspitaca}/{aktivnostId}")]
        [HttpPost]
        public async Task<ActionResult> VaspitacNadgleda(string jmbgVaspitaca, int aktivnostId)
        {
            if(jmbgVaspitaca.Length!=13)
            {
                return BadRequest("Pogresan jmbg!");
            }
            try
            {
                var vaspitac=Context.Vaspitaci.Where(p=>p.JMBG==jmbgVaspitaca).FirstOrDefault();
                if(vaspitac==null)
                {
                    return BadRequest("Nema vaspitaca sa zadatim JMBG-om");
                }
                var aktivnost=Context.Aktivnosti.Where(p=>p.ID==aktivnostId).FirstOrDefault();
                if(aktivnost==null)
                {
                    return BadRequest("Nema aktivnosti sa zadatim id-em");
                }
                Nadgleda n=new Nadgleda();
                n.Vaspitac=vaspitac;
                n.Aktivnost=aktivnost;
                Context.Nadgledaju.Add(n);
                await Context.SaveChangesAsync();
                return Ok("Uspesno ste vaspitacu zadali aktivnost za nadgledanje");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }
         [EnableCors("CORS")]
        [Route("VaspitacNadgledaIzbrisi/{vaspitacId}/{aktivnostId}")]
        [HttpDelete]
        public async Task<ActionResult> DeteNadgledaIzbrisi(int vaspitacId, int aktivnostId)
        {
            if(vaspitacId<=0)
            {
                return BadRequest("Pogresan ID deteta");
            }
            if(aktivnostId<=0)
            {
                return BadRequest("Pogresan ID aktivnosti");
            }
            try
            {
                var vaspitac=await Context.Vaspitaci.FindAsync(vaspitacId);
                if(vaspitac==null)
                {
                    return BadRequest("Ne postoji dete sa zadatim ID-em");
                }
                var aktivnost=await Context.Aktivnosti.FindAsync(aktivnostId);
                if(aktivnost==null)
                {
                    return BadRequest("Ne postoji aktivnost sa zadatim ID-em");
                }
                var u=await Context.Nadgledaju.Where(p=>p.Aktivnost.ID==aktivnostId && p.Vaspitac.ID==vaspitacId).FirstOrDefaultAsync();
                Context.Nadgledaju.Remove(u);
                await Context.SaveChangesAsync();
                return Ok("Uspesno ste obrisali vaspitaca ");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [EnableCors("CORS")]
        [Route("IzmeniVaspitacNadgleda/{vaspitacId}/{aktivnostId}/{novaAktivnostId}")]
        [HttpPut]
        public async Task<ActionResult> PromeniVaspitacNadgleda(int vaspitacId,int aktivnostId,int novaAktivnostId)
        {
            try
            {                
                var v=await Context.Nadgledaju.Where(p=>p.Vaspitac.ID==vaspitacId && p.Aktivnost.ID==aktivnostId).FirstOrDefaultAsync();
                var a=await Context.Vaspitaci.FindAsync(vaspitacId);
                if(v==null)
               return BadRequest("Ne postoji vaspitac sa zadatim jmbg-om!");
                
                Context.Nadgledaju.Remove(v);
                await Context.SaveChangesAsync();
                await VaspitacNadgleda(a.JMBG,novaAktivnostId);
                
                
                return Ok("Uspesno izmenjen vaspitac");

            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}