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
    public class AdministratorController : ControllerBase
    {
        public VrticContext Context { get; set; }
        public AdministratorController(VrticContext context)
        {
            Context=context;            
        }
        [EnableCors("CORS")]
        [Route("DodatiAdministratora/{ime}/{jmbg}/{sifra}")]
        [HttpPost]
        public async Task<ActionResult> DodajAdministratora(string ime, string jmbg, string sifra)
        {
            if(string.IsNullOrWhiteSpace(ime) || ime.Length>50)
            {
                return BadRequest("Pogresno ime administratora");
            }
            if(string.IsNullOrWhiteSpace(jmbg) || jmbg.Length!=13)
            {
                return BadRequest("Pogresan jmbg administratora");
            }
            if(string.IsNullOrWhiteSpace(sifra) || sifra.Length>8 || sifra.Length<5)
            {
                return BadRequest("Sifra mora da ima izmedju 5 i 8 karaktera");
            }
            
            try
            {
                var v=await Context.Administratori.Where(p=>p.JMBG==jmbg).FirstOrDefaultAsync();
                if(v!=null)
                {
                    return BadRequest("Administrator vec postoji u bazi");
                }
                Administrator d=new Administrator();
                d.Ime=ime;
                d.JMBG=jmbg;
                d.Sifra=sifra;
                Context.Administratori.Add(d);
                await Context.SaveChangesAsync();
                return Ok("Uspenso ste upisali administratora");

            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }




        }
        [EnableCors("CORS")]
        [Route("PreuzmiAdministratora/{vrticId}")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiAdministratora(int vrticId)
        {
            
            var a= await Context.Vrtici.
            Include(p=>p.Administrator)
            .Where(p=>p.ID==vrticId)
            .Select(p=> new
            {
                p.Administrator.Ime,
                p.Administrator.Sifra,
                p.Administrator.JMBG
            }).FirstOrDefaultAsync();

            try
            {
                return Ok(a);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

          

        }
    
        



        
    }
}
