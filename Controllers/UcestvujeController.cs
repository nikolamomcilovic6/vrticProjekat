using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
namespace WebProjekat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UcestvujeController : ControllerBase
    {
        public VrticContext Context { get; set; }
        public UcestvujeController(VrticContext context)
        {
            Context=context;            
        }

        [EnableCors("CORS")]
        [Route("DeteUcestvuje/{deteId}/{aktivnostId}")]
        [HttpPost]
        public async Task<ActionResult> DeteUcestvuje(int deteId, int aktivnostId)
        {
            if(deteId<=0)
            {
                return BadRequest("Pogresan ID deteta");
            }
            if(aktivnostId<=0)
            {
                return BadRequest("Pogresan ID aktivnosti");
            }
            try
            {
                var dete=await Context.Deca.FindAsync(deteId);
                if(dete==null)
                {
                    return BadRequest("Ne postoji dete sa zadatim ID-em");
                }
                var aktivnost=await Context.Aktivnosti.FindAsync(aktivnostId);
                if(aktivnost==null)
                {
                    return BadRequest("Ne postoji aktivnost sa zadatim ID-em");
                }
                Ucestvuje u=new Ucestvuje();
                u.Aktivnost=aktivnost;
                u.Dete=dete;
                Context.Ucestvuju.Add(u);
                await Context.SaveChangesAsync();
                return Ok("Uspesno ste detetu dodelili aktivnost");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }

         [EnableCors("CORS")]
        [Route("DeteUcestvujeIzbrisi/{deteId}/{aktivnostId}")]
        [HttpDelete]
        public async Task<ActionResult> DeteUcestvujeIzbrisi(int deteId, int aktivnostId)
        {
            if(deteId<=0)
            {
                return BadRequest("Pogresan ID deteta");
            }
            if(aktivnostId<=0)
            {
                return BadRequest("Pogresan ID aktivnosti");
            }
            try
            {
                var dete=await Context.Deca.FindAsync(deteId);
                if(dete==null)
                {
                    return BadRequest("Ne postoji dete sa zadatim ID-em");
                }
                var aktivnost=await Context.Aktivnosti.FindAsync(aktivnostId);
                if(aktivnost==null)
                {
                    return BadRequest("Ne postoji aktivnost sa zadatim ID-em");
                }
                var u=await Context.Ucestvuju.Where(p=>p.Aktivnost.ID==aktivnostId && p.Dete.ID==deteId).FirstOrDefaultAsync();
                Context.Ucestvuju.Remove(u);
                await Context.SaveChangesAsync();
                return Ok("Uspesno ste obrisali dete");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }

       /* [EnableCors("CORS")]
        [Route("PrikaziUcestvuje/{vrticId}/{aktivnostId}")]
        [HttpGet]
        public async Task<ActionResult> PrikaziVaspitace(int vrticId,int aktivnostId)
        {

           //var aktivnost= await Context.Aktivnosti.Where(p=>p.ID==aktivnostId).FirstOrDefaultAsync();
            var aktivnost=await Context.Odrzavaju
            .Include(p=>p.Aktivnost)
            .Where(p=>p.Aktivnost.ID==aktivnostId && p.Vrtic.ID==vrticId)
            .ToListAsync();

            

            try
            {
                return Ok(vaspitac);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }*/

    }
}