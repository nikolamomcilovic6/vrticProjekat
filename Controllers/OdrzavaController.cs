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
    public class OdrzavaController : ControllerBase
    {
        public VrticContext Context { get; set; }
        public OdrzavaController(VrticContext context)
        {
            Context=context;            
        }

        [EnableCors("CORS")]
        [Route("VrticOdrzava/{aktivnostNaziv}/{vrticId}")]
        [HttpPost]
        public async Task<ActionResult> VrticOdrzava(string aktivnostNaziv, int vrticId)
        {
            try
            {
                 var vrtic=Context.Vrtici.Where(p=>p.ID==vrticId).FirstOrDefault();

                if(vrtic==null)
                {
                    return BadRequest("Ne postoji vrtic koji ce da odrzava zadatu aktivnost");
                }

                var aktivnost=Context.Aktivnosti.Where(p=>p.Naziv==aktivnostNaziv).FirstOrDefault();
                if(aktivnost==null)
                {
                    return BadRequest("Ne postoji aktivnost koju ce da odrzava zadati vrtic");
                }
                var p=await Context.Odrzavaju.Where(p=>p.Vrtic==vrtic && p.Aktivnost==aktivnost).FirstOrDefaultAsync();
                if(p!=null)
                {
                    return BadRequest("Aktivnost postoji u vrticu");
                }

                Odrzava odrzava=new Odrzava();
                odrzava.Vrtic=vrtic;
                odrzava.Aktivnost=aktivnost;
                Context.Odrzavaju.Add(odrzava);
                await Context.SaveChangesAsync();
                return Ok("Uspesno ste dodali aktivnost za odrzavanje!");

            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        [EnableCors("CORS")]
        [Route("ObrisatiOdrzava/{id}")]
        [HttpDelete]
        public async Task<ActionResult> ObrisiOdrzava(int id)
        {
            
            try
            {
                var vrtic=Context.Odrzavaju.Where(p=>p.ID==id).FirstOrDefault();
                if(vrtic==null)
                return BadRequest("Ne postoji");
                Context.Odrzavaju.Remove(vrtic);
                await Context.SaveChangesAsync();
                return Ok("Uspesno obrisan");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}