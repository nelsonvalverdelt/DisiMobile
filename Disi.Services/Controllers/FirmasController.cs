using Disi.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Disi.Services.Controllers
{
    public class FirmasController : ApiController
    {
        DisiConnection db = new DisiConnection();
        [Route("api/Firmas")]
        public async Task<IHttpActionResult> GetFirmas()
        {
            List<FirmaCsv> listaClientes = new List<FirmaCsv>();

            foreach (var item in db.Firma)
            {
                listaClientes.Add(new FirmaCsv
                {
                    Id = item.Id,
                    Dni = item.Dni,
                    FechaFirma = item.FechaFirma,
                    TiempoSegIniciofirma = item.TiempoSegIniciofirma,
                    TiempoSegFinFirma = item.TiempoSegFinFirma,
                    TiempoMiliSegInicioFirma = item.TiempoMiliSegInicioFirma,
                    TiempoMiliSegFinFirma = item.TiempoMiliSegFinFirma,
                    UrlImagen = item.UrlImagen
                });
            }
            return Ok(listaClientes);
        }
        [HttpPost]
        [Route("api/Firma")]
        public async Task<IHttpActionResult> PostFirmas(Firma firma)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Firma.Add(firma);
            await db.SaveChangesAsync();

            return Ok(firma);
        }
    }
}
