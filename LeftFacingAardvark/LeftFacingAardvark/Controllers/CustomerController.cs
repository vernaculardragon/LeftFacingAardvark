using LeftFacingAardvark.EF;
using LeftFacingAardvark.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LeftFacingAardvark.Controllers
{
    [ApiController]
    [Route("{agentId}/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly AardvarkContext _db;
        private readonly ILogger<AgentController> _logger;

        public CustomerController(ILogger<AgentController> logger, AardvarkContext db)
        {
            _db = db;
            _logger = logger;
        }

        /// <summary>
        /// Gets a basic Summary of all Customers for an Agent
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Customer>), 200)]
        public async Task<IActionResult> Get(int agentId)
        {
            var customers = await _db.Customers.Where(x => x.AgentId == agentId).Select(Mappers.ToCustomerSummary).ToListAsync();
            return Ok(customers);
        }
        /// <summary>
        /// Gets details of a single Customer by ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(Customer), 200)]
        [Route("{id}")]
        public async Task<IActionResult> Get(int agentId, int id)
        {
            var customer = await _db.Customers
                .Include(x => x.CustomerTags)
                .ThenInclude(y => y.Tag)
                .Where(x => x.Id == id && x.AgentId == agentId)
                .Select(Mappers.ToCustomerDetails)
                .FirstOrDefaultAsync();
            if (customer != null)
            {
                return Ok(customer);
            }
            return NotFound("No customer with this ID found");
        }

        /// <summary>
        /// Creates a new Customer and returns the customer details once created
        /// </summary>
        /// <param name="agent"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Customer), 200)]
        public async Task<IActionResult> Post(int agentId, Customer customer)
        {
            if (agentId != customer.Agent_id)
            {
                return BadRequest("ID mismatch. Please ensure both agent IDs match");
            }
            if(!_db.Agents.Any(x=>x.Id == agentId))
            {
                return BadRequest("Missing Agent. The specified agent does not exist");
            }

            customer.Id = customer.Id == 0 ? null : customer.Id;

            if (customer.Id != null)
            {
                return BadRequest("Please Submit a new customer (no customer id)");
            }
            if (_db.Customers.Any(x => x.Guid == customer.Guid))
            {
                return BadRequest("Please Submit a new customer (Customer GUID already used)");
            }

            var dbCustomer = Mappers.ToDBCustomer(customer);

             var customerTags = _db.Tags.Where(x => customer.Tags.Contains(x.Value)).Select(x => new EF.Entities.CustomerTag { Tag = x }).ToList();

            if (customerTags.Any(x => x.Tag == null))
            {
                return BadRequest("Invalid Tag sets");
            }

            dbCustomer.CustomerTags = customerTags;

            _db.Customers.Add(dbCustomer);


            await _db.SaveChangesAsync();

            var returnAgent = await _db.Customers
                  .Include(x => x.CustomerTags)
                .ThenInclude(y => y.Tag)
                .Select(Mappers.ToCustomerDetails)

                .FirstAsync(x => x.Id == dbCustomer.Id);

            return Ok(returnAgent);
        }


        /// <summary>
        /// Updates a Customer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="agent"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(Customer), 200)]
        [Route("{id}")]
        public async Task<IActionResult> Put(int agentId, [FromRoute] int id, Customer customer)
        {
            if (agentId != customer.Agent_id || id != customer.Id)
            {
                return BadRequest("ID mismatch. Please ensure both route and data IDs match");
            }

            var existingCustomer = await _db.Customers.Include(x => x.CustomerTags).FirstOrDefaultAsync(x => x.Id == id && x.AgentId == agentId);

            if (customer.Id == null || existingCustomer == null)
            {
                return BadRequest("Please submit an Existing customer");
            }
            if (_db.Customers.Any(x => x.Guid == customer.Guid && x.Id != customer.Id))
            {
                return BadRequest("Sorry that Guid is Already In Use (Customer GUID already used)");
            }
            // this should probably be moved to a populate method, but it's also the only place it is used for now
            existingCustomer.Address = customer.Address;
            existingCustomer.Age = customer.Age;
            existingCustomer.Balance = customer.Balance;
            existingCustomer.Company = customer.Company;
            existingCustomer.Email = customer.Email;
            existingCustomer.EyeColor = customer.EyeColor;
            existingCustomer.FirstName = customer.Name.First;
            existingCustomer.LastName = customer.Name.Last;
            existingCustomer.Guid = customer.Guid;
            existingCustomer.IsActive = customer.IsActive;
            existingCustomer.Latitude = customer.Latitude;
            existingCustomer.Longitude = customer.Longitude;
            existingCustomer.Phone = customer.Phone;
            existingCustomer.Registered = customer.Registered;

            var customerTags = _db.Tags.Where(x => customer.Tags.Contains(x.Value)).Select(x => new EF.Entities.CustomerTag { Tag = x }).ToList();

            if (customerTags.Any(x => x.Tag == null))
            {
                return BadRequest("Invalid Tag sets");
            }
            existingCustomer.CustomerTags = customerTags;

            _db.Customers.Update(existingCustomer);

            await _db.SaveChangesAsync();

            var returnCustomer = await _db.Customers
                  .Include(x => x.CustomerTags)
                .ThenInclude(y => y.Tag)
                .Select(Mappers.ToCustomerDetails)
                .FirstAsync(x => x.Id == existingCustomer.Id);

            return Ok(returnCustomer);
        }
        /// <summary>
        /// Deletes an Customer Based on ID 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="agent"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(bool), 200)]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int agentId, [FromRoute] int id)
        {
            var existingCustomer = await _db.Customers.Include(x => x.CustomerTags).FirstOrDefaultAsync(x => x.Id == id && x.AgentId == agentId);

            if (existingCustomer != null)
            {

                _db.CustomerTags.RemoveRange(existingCustomer.CustomerTags);
                _db.Customers.Remove(existingCustomer);
                await _db.SaveChangesAsync();

                return Ok(true);
            }
            else
            {
                return BadRequest("No Customer Exists with that identifier");
            }


        }

    }
}
