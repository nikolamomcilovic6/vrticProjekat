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
    public class DeteController : ControllerBase
    {
        public VrticContext Context { get; set; }
        public DeteController(VrticContext context)
        {
            Context=context;            
        }
        [EnableCors("CORS")]
        [Route("PrikaziDecu/{vrticId}/{aktivnostId}")]
        [HttpGet]
        public async Task<ActionResult> PrikaziDecu(int vrticId,int aktivnostId)
        {

            var deca=await Context.Ucestvuju
            .Include(p=>p.Aktivnost)
            .Include(p=>p.Dete)
            .ThenInclude(p=>p.Vrtic)
            .Where(p=>p.Aktivnost.ID==aktivnostId && p.Dete.Vrtic.ID==vrticId)
            .Select(p=>new{
                p.Dete.ID,
                p.Dete.Ime,
                p.Dete.Prezime,
                p.Dete.brojRoditelja
            }).ToListAsync();
            try
            {
                return Ok(deca);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [EnableCors("CORS")]
        [Route("VratiDete/{vrticId}/{jmbg}")]
        [HttpGet]
        public async Task<ActionResult> VratiDete(int vrticId,string jmbg)
        {
            try
            {
                return Ok(await Context.Deca.Where(p=>p.Vrtic.ID==vrticId && p.JMBG==jmbg)
                .Select(p=>new { p.ID, p.Ime, p.Prezime,p.brojRoditelja,p.JMBG}).FirstOrDefaultAsync());
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
         [EnableCors("CORS")]
        [Route("PreuzmiDecu")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiDete()
        {
            try
            {
                return Ok(await Context.Deca.Select(p=>new { p.ID, p.Ime, p.Prezime,p.brojRoditelja, p.JMBG}).ToListAsync());
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

         [EnableCors("CORS")]
        [Route("PrikaziDecu/{vrticId}")]
        [HttpGet]
        public async Task<ActionResult> PrikaziDecuVrtic(int vrticId)
        {

            var deca=await Context.Ucestvuju
            .Include(p=>p.Aktivnost)
            .Include(p=>p.Dete)
            .ThenInclude(p=>p.Vrtic)
            .Where(p=>p.Dete.Vrtic.ID==vrticId)
            .Select(p=>new{
                p.Dete.ID,
                p.Dete.Ime,
                p.Dete.Prezime,
                p.Dete.brojRoditelja,
                IdAktivnosti=p.Aktivnost.ID
            }).ToListAsync();
            try
            {
                return Ok(deca);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }




        [EnableCors("CORS")]
        [Route("DodatiDete/{ime}/{prezime}/{brojRoditelja}/{jmbg}/{vrticId}")]
        [HttpPost]
        public async Task<ActionResult> DodajDete (string ime, string prezime, string brojRoditelja,string jmbg,int vrticId)
        {
                if(string.IsNullOrWhiteSpace(ime) || ime.Length>30)
                {
                    return BadRequest("Pogresno ime deteta");
                }
                if(string.IsNullOrWhiteSpace(prezime) || prezime.Length>50)
                {
                    return BadRequest("Pogresno prezime deteta");
                }
                 if(string.IsNullOrWhiteSpace(jmbg) || jmbg.Length!=13)
                {
                    return BadRequest("JMBG mora imati 13 cifara");
                }
            var dete=await Context.Deca
            .Include(p=>p.Vrtic)
            .Where(p=>p.JMBG==jmbg && p.Vrtic.ID==vrticId)
            .FirstOrDefaultAsync();
            if(dete!=null)
            {
                return BadRequest("Vec postoji u bazi");
            }

            try
            {
               var vrtic= await Context.Vrtici.FindAsync(vrticId);

                if(vrtic==null)
                {
                    return BadRequest("Ne postoji vrtic u koji cete upisati dete");
                }
                else
                {
                Dete d=new Dete();
                d.Ime=ime;
                d.Prezime=prezime;
                d.brojRoditelja=brojRoditelja;
                d.JMBG=jmbg;
                d.Vrtic=vrtic;
                

                Context.Deca.Add(d);
                await Context.SaveChangesAsync();
                return Ok("Uspesno ste dodali dete");
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }


        }
        [EnableCors("CORS")]
       [Route("ObrisiDete/{ime}/{prezime}/{brojRoditelja}/{jmbg}")]
       [HttpDelete]
       public async Task<ActionResult> ObrisiDete(string ime, string prezime, string brojRoditelja,string jmbg)
       {
            if(string.IsNullOrWhiteSpace(ime) || ime.Length>30)
                {
                    return BadRequest("Pogresno ime deteta");
                }
                if(string.IsNullOrWhiteSpace(prezime) || prezime.Length>50)
                {
                    return BadRequest("Pogresno prezime deteta");
                }
                 if(string.IsNullOrWhiteSpace(jmbg) || jmbg.Length!=13)
                {
                    return BadRequest("JMBG mora imati 13 cifara");
                }
           var d=await Context.Deca.Where(p=>p.Ime==ime && p.Prezime==prezime && p.brojRoditelja==brojRoditelja && p.JMBG==jmbg)
           .FirstOrDefaultAsync();
          if(d==null)
          {
              return BadRequest("Ne postoji dete");
          }

          try{
                Context.Deca.Remove(d);
                await Context.SaveChangesAsync();
                return Ok("Uspesno ste obrisali dete!");

          }
          catch(Exception e)
          {
              return BadRequest(e.Message);
          }


       }
        [EnableCors("CORS")]
        [Route("IzmeniDete/{brojRoditelja}/{jmbg}")]
        [HttpPut]
        public async Task<ActionResult> IzmeniDete(string brojRoditelja,string jmbg)
        {
                if(jmbg.Length!=13)
            {
                return BadRequest("Pogresan jmbg!");
            }
            if(string.IsNullOrWhiteSpace(brojRoditelja))
            {
                return BadRequest("Pogresan broj");
            }
            try
            {                
                var v=await Context.Deca.Where(p=>p.JMBG==jmbg).FirstOrDefaultAsync();
                if(v==null)
                return BadRequest("Pogresan jmbg deteta");

                v.JMBG=jmbg;
                v.brojRoditelja=brojRoditelja;
                await Context.SaveChangesAsync();

                return Ok("Uspesno izmenjeno dete");

            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}