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
        public async Task<ActionResult<IEnumerable<MunkalapDTO>>> GetMunkalap()
        {
            var result = await _context.munkalap.Include(m => m.hely).Include(m => m.szerelo)
                .Select(m => new MunkalapDTO(m.id, m.beadas_datum, m.javitas_datum, m.hely.telepules, m.hely.utca, m.szerelo.nev, m.munkaora, m.anyagar))
                .ToListAsync();

            return result;
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

        [HttpPost]
        public async Task<ActionResult<MunkalapKeresesDTO>> SearchMunkalap(int helyszinId, int szereloId)
        {
            return NotFound();
        }
    }
}
