using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerPropertiesController : ControllerBase
    {
        private readonly CustomerContext _context;

        public CustomerPropertiesController(CustomerContext context)
        {
            _context = context;
        }

        // GET: api/CustomerProperties
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerProperties>>> GetCustomerItems()
        {
            return await _context.CustomerItems.ToListAsync();
        }

        // GET: api/CustomerProperties/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerProperties>> GetCustomerProperties(long id)
        {
            var customerProperties = await _context.CustomerItems.FindAsync(id);

            if (customerProperties == null)
            {
                return NotFound();
            }

            return customerProperties;
        }

        // PUT: api/CustomerProperties/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerProperties(long id, CustomerProperties customerProperties)
        {
            if (id != customerProperties.Id)
            {
                return BadRequest();
            }

            _context.Entry(customerProperties).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerPropertiesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CustomerProperties
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomerProperties>> PostCustomerProperties(CustomerProperties customerProperties)
        {
            _context.CustomerItems.Add(customerProperties);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomerProperties", new { id = customerProperties.Id }, customerProperties);
        }

        // DELETE: api/CustomerProperties/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerProperties(long id)
        {
            var customerProperties = await _context.CustomerItems.FindAsync(id);
            if (customerProperties == null)
            {
                return NotFound();
            }

            _context.CustomerItems.Remove(customerProperties);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerPropertiesExists(long id)
        {
            return _context.CustomerItems.Any(e => e.Id == id);
        }
    }
}
