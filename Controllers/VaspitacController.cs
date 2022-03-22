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
    public class VaspitacController : ControllerBase
    {
        public VrticContext Context { get; set; }
        public VaspitacController(VrticContext context)
        {
            Context=context;            
        }
        [EnableCors("CORS")]
        [Route("PrikaziVaspitace/{vrticId}/{aktivnostId}")]
        [HttpGet]
        public async Task<ActionResult> PrikaziVaspitace(int vrticId,int aktivnostId)
        {

           var vaspitac=await Context.Nadgledaju
            .Include(p=>p.Aktivnost)
            .Include(p=>p.Vaspitac)
            .ThenInclude(p=>p.Vrtic)
            .Where(p=>p.Vaspitac.Vrtic.ID==vrticId && p.Aktivnost.ID==aktivnostId)
            .Select(p=>new{
                p.Vaspitac.ID,
                p.Vaspitac.JMBG,
                p.Vaspitac.Ime,
                p.Vaspitac.Prezime
            }).ToListAsync();
            try
            {
                return Ok(vaspitac);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [EnableCors("CORS")]
        [Route("DodajVaspitaca/{ime}/{prezime}/{jmbg}/{vrticId}")]
        [HttpPost]
        public async Task<ActionResult> DodajVaspitaca(string jmbg,string ime, string prezime, int vrticId)
        {
            if(jmbg.Length!=13)
            {
                return BadRequest("Pogresan jmbg!");
            }
            if(string.IsNullOrWhiteSpace(ime) || ime.Length>30)
            {
                return BadRequest("Pogresno ime vaspitaca");
            }
            if(string.IsNullOrWhiteSpace(prezime) || prezime.Length>50)
            {
                return BadRequest("Pogresno prezime vaspitaca");
            }
            try{
                var vrtic = await Context.Vrtici.FindAsync(vrticId);


                var vas=await Context.Vaspitaci
                .Where(p=>p.JMBG==jmbg).FirstOrDefaultAsync();

                if(vas!=null)
                {
                    return BadRequest("Vaspitac sa zadatim JMBG-om vec postoji");
                }

                if(vrtic==null)
                {
                    return BadRequest("Ne postoji vrtic sa zadatim id-em");
                }
                else
                {
                    Vaspitac vaspitac=new Vaspitac();
                    vaspitac.JMBG=jmbg;
                    vaspitac.Ime=ime;
                    vaspitac.Prezime=prezime;
                    vaspitac.Vrtic=vrtic;

                    Context.Vaspitaci.Add(vaspitac);
                    await Context.SaveChangesAsync();
                    return Ok("Vaspitac je uspesno dodat");
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [EnableCors("CORS")]
        [Route("IzmeniVaspitaca/{jmbg}/{nazivVrtica}")]
        [HttpPut]
        public async Task<ActionResult> PromeniVaspitaca(string jmbg,string nazivVrtica)
        {
                if(jmbg.Length!=13)
            {
                return BadRequest("Pogresan jmbg!");
            }
             if(string.IsNullOrWhiteSpace(nazivVrtica) || nazivVrtica.Length>50)
            {
                return BadRequest("Pogresan naziv vrtica");
            }
            try
            {                
                Vaspitac v=Context.Vaspitaci.Where(p=>p.JMBG==jmbg).FirstOrDefault();
                if(v==null)
                return BadRequest("Ne postoji vaspitac sa zadatim jmbg-om!");

                Vaspitac vas=new Vaspitac();
                vas.ID=v.ID;
                vas.Ime=v.Ime;
                vas.JMBG=v.JMBG;
                vas.Prezime=v.Prezime;
                await ObrisiVaspitaca(jmbg);
                var vrtic=Context.Vrtici.Where(p=>p.Naziv==nazivVrtica).FirstOrDefault();
                vas.Vrtic=vrtic;

                Context.Vaspitaci.Add(vas);
                await Context.SaveChangesAsync();
                return Ok("Uspesno izmenjen vaspitac");

            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }


       [EnableCors("CORS")]
       [Route("ObrisiVaspitaca/{jmbg}")]
       [HttpDelete]
       public async Task<ActionResult> ObrisiVaspitaca(string jmbg)
       {
           Vaspitac v=Context.Vaspitaci.Where(p=>p.JMBG==jmbg).FirstOrDefault();
          if(v==null)
          return BadRequest("Ne postoji vaspitac sa zadatim jmbg-om!");

          try{
                Context.Vaspitaci.Remove(v);
                await Context.SaveChangesAsync();
                return Ok("Vaspitac je uspesno obrisan!");

          }
          catch(Exception e)
          {
              return BadRequest(e.Message);
          }
       }

    

    }
}