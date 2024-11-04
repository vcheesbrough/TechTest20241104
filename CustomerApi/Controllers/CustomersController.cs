using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CustomerApi.Data;
using CustomerApi.Data.Models;
using CustomerApi.FluentValidation;
using FluentValidation;
using Shared.ApiModels;

namespace CustomerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController(CustomerContext context, IMapper mapper, IValidator<CustomerDto> validator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomers()
        {
            var customerDao = await context.Customers.ToListAsync();
            return Ok(mapper.Map<IEnumerable<CustomerDto>>(customerDao));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomer(int id)
        {
            var customer = await context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return mapper.Map<CustomerDto>(customer) ;
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> PostCustomer(CustomerDto customer)
        {
            var result = await validator.ValidateAsync(customer, strategy => strategy
                .IncludeRuleSets(
                    CustomerDtoValidator.DefaultRuleSet, 
                    CustomerDtoValidator.CreateRuleSet
                    ));
            if (!result.IsValid) 
            {
                return BadRequest(result.Errors);
            }
            var savedEntity = context.Customers.Add(mapper.Map<CustomerDao>(customer));
            await context.SaveChangesAsync();
            customer.Id = savedEntity.Entity.Id;

            var createdAtActionResult = CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
            return createdAtActionResult;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, CustomerDto customer)
        {
            var result = await validator.ValidateAsync(customer, strategy => strategy
                .IncludeRuleSets(
                    CustomerDtoValidator.DefaultRuleSet, 
                    CustomerDtoValidator.UpdateRuleSet
                ));
            if (!result.IsValid) 
            {
                return BadRequest(result.Errors);
            }

            if (id != customer.Id)
            {
                return BadRequest();
            }

            var customerDao = mapper.Map<CustomerDao>(customer);
            
            context.Entry(customerDao).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            context.Customers.Remove(customer);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return context.Customers.Any(e => e.Id == id);
        }
    }
}