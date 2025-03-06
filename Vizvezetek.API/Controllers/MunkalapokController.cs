using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vizvezetek.API.Context;
using Vizvezetek.API.DTO;
using Vizvezetek.API.DTOs;
using Vizvezetek.API.Models; // Ensure this namespace is imported

namespace Vizvezetek.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MunkalapokController : ControllerBase
    {
        private readonly vizvezetekContext _context;

        public MunkalapokController(vizvezetekContext context)
        {
            _context = context;
        }

        // GET: api/Munkalapok
        [HttpGet]
        [Route("")]
        [Route("ev/{ev:int?}")]
        public async Task<ActionResult<IEnumerable<MunkalapDTO>>> GetMunkalap(int? ev = null)
        {
            var query = _context.munkalap
                .Include(m => m.hely)
                .Include(m => m.szerelo).AsQueryable();

            if (ev.HasValue && ev.Value > 0)
            {
                query = query.Where(m => m.javitas_datum.Year == ev.Value);
            }

            var result = await query
                .Select(m => new MunkalapDTO(
                    m.id,
                    m.beadas_datum,
                    m.javitas_datum,
                    m.hely.telepules,
                    m.hely.utca,
                    m.szerelo.nev,
                    m.munkaora,
                    m.anyagar))
                .ToListAsync();

            if (!result.Any())
            {
                return NotFound();
            }

            return Ok(result);
        }


        // GET: api/Munkalapok/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MunkalapDTO>> GetMunkalap(int id)
        {
            var munkalap = await _context.munkalap
                .Where(m => m.id == id)
                .Select(m => new MunkalapDTO(m.id, m.beadas_datum, m.javitas_datum, m.hely.telepules, m.hely.utca, m.szerelo.nev, m.munkaora, m.anyagar))
                .FirstOrDefaultAsync();

            if (munkalap == null)
            {
                return NotFound();
            }

            return munkalap;
        }

        // POST: api/Munkalapok/Kereses
        [HttpPost("Kereses")]
        public async Task<ActionResult<IEnumerable<MunkalapDTO>>> SearchMunkalap(MunkalapKeresesDTO munkalapKeresesDTO)
        {
            var result = await _context.munkalap
                .Include(m => m.hely)
                .Include(m => m.szerelo)
                .Where(m => m.hely_id == munkalapKeresesDTO.HelyszinId &&
                            m.szerelo_id == munkalapKeresesDTO.SzereloId)
                .ToListAsync();

            var selectResult = result.Select(m => new MunkalapDTO(m.id, m.beadas_datum, m.javitas_datum, m.hely.telepules, m.hely.utca, m.szerelo.nev, m.munkaora, m.anyagar));

            if (selectResult == null)
            {
                return NotFound();
            }

            return Ok(selectResult);
        }

        //[HttpGet("ev/{ev}")]
        //public async Task<ActionResult<IEnumerable<MunkalapDTO>>> GetEvszam(int ev)
        //{
        //    var result = await _context.munkalap.Include(m => m.hely).Include(m => m.szerelo)
        //        .Where(m => m.javitas_datum.Year == ev)
        //        .Select(m => new MunkalapDTO(m.id, m.beadas_datum, m.javitas_datum, m.hely.telepules, m.hely.utca, m.szerelo.nev, m.munkaora, m.anyagar))
        //        .ToListAsync();

        //    if (result == null)
        //    {
        //        NotFound();
        //    }

        //    return Ok(result);
        //}
    }
}
