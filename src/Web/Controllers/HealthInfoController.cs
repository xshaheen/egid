using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EGID.Web.Data;
using EGID.Web.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EGID.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthInfoController : ControllerBase
    {
        private readonly EgidDbContext _context;

        public HealthInfoController(EgidDbContext context)
        {
            _context = context;
        }
         
        // GET: api/HealthInfo
        [HttpGet]
        public ActionResult<IEnumerable<HealthInfo>> GetHealthInfo()
        {
            return new List<HealthInfo>();
        }

        // GET: api/HealthInfo/1
        [HttpGet("{id}")]
        public async Task<ActionResult<HealthInfo>> GetHealthInfo(Guid id)
        {
            var healthInfo = await _context.HealthInformation.FindAsync(id);

            if (healthInfo == null) return NotFound();

            return healthInfo;
        }

        // PUT: api/HealthInfo/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutHealthInfo(Guid id, HealthInfo healthInfo)
        {
            if (id != healthInfo.Id) return BadRequest();

            _context.Entry(healthInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HealthInfoExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // POST: api/HealthInfo
        [HttpPost]
        public async Task<ActionResult<HealthInfo>> PostHealthInfo(HealthInfo healthInfo)
        {
            _context.HealthInformation.Add(healthInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHealthInfo", new { id = healthInfo.Id }, healthInfo);
        }

        // DELETE: api/HealthInfo/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HealthInfo>> DeleteHealthInfo(Guid id)
        {
            var healthInfo = await _context.HealthInformation.FindAsync(id);
            if (healthInfo == null) return NotFound();

            _context.HealthInformation.Remove(healthInfo);
            await _context.SaveChangesAsync();

            return healthInfo;
        }

        private bool HealthInfoExists(Guid id)
        {
            return _context.HealthInformation.Any(e => e.Id == id);
        }
    }
}
