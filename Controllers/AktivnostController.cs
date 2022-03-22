using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Projekat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AktivnostController : ControllerBase
    {
        public VrticContext Context { get; set; }
        public AktivnostController(VrticContext context)
        {
            Context=context;            
        }
        [EnableCors("CORS")]
        [Route("PreuzmiAktivnost/{vrticId}")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiAktivnost(int vrticId)
        {

           //var vrtic= await Context.Vrtici.FindAsync(vrticId);

            var aktivnost=await Context.Odrzavaju
            .Include(p=>p.Vrtic)
            .Include(P=>P.Aktivnost)
            .Where(p=>p.Vrtic.ID==vrticId)
            .Select(p=>new {
                p.Aktivnost.ID,
                p.Aktivnost.Naziv
            }).ToListAsync();

            try
            {

                return Ok(aktivnost);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }


       /* [EnableCors("CORS")]
        [Route("PreuzmiAktivnost")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiAktivnost()
        {
            try
            {
                return Ok(await Context.Aktivnosti.Select(p=> new{p.ID, p.Naziv}).ToListAsync());
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
*/

        [EnableCors("CORS")]
        [Route("DodajAktivnost/{naziv}")]
        [HttpPost]
        public async Task<ActionResult> DodajAktivnost(string naziv)
        {
            if(string.IsNullOrWhiteSpace(naziv) || naziv.Length>30)
            {
                return BadRequest("Pogresan naziv Aktivnosti");
            }
            try
            {
                var a=await Context.Aktivnosti.Where(p=>p.Naziv==naziv).FirstOrDefaultAsync();
                if(a!=null)
                {
                    return BadRequest("Vec postoji aktivnost sa zadatim nazivom");
                }
                var aktivnost=new Aktivnost();
                aktivnost.Naziv=naziv;
                Context.Aktivnosti.Add(aktivnost);
                await Context.SaveChangesAsync();
                return Ok("Uspesno ste dodali aktivnost");
                
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        
        [EnableCors("CORS")]
        [Route("ObrisatiAktivnost/{naziv}")]
        [HttpDelete]
        public async Task<ActionResult> ObrisiAktivnost(string naziv)
        {
            if(string.IsNullOrWhiteSpace(naziv) || naziv.Length>50)
            {
                return BadRequest("Pogresan naziv za aktivnost");
            }
            try
            {
                var aktivnost=Context.Aktivnosti.Where(p=>p.Naziv==naziv).FirstOrDefault();
                if(aktivnost==null)
                return BadRequest("Ne postoji aktivnost sa zadatim nazivom");
                string Naziv=aktivnost.Naziv;
                Context.Aktivnosti.Remove(aktivnost);
                await Context.SaveChangesAsync();
                return Ok($"Uspesno obrisana aktivnost sa nazivom:{Naziv}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

       

    }
}