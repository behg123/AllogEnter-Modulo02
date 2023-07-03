using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Univali.Api.DbContexts;
using Univali.Api.Entities;
using Univali.Api.Features.Customers.Commands.CreateCustomer;
using Univali.Api.Features.Customers.Commands.CreateCustomerWithAddresses;
using Univali.Api.Features.Customers.Commands.DeleteCustomer;
using Univali.Api.Features.Customers.Commands.UpdateCustomer;
using Univali.Api.Features.Customers.Commands.UpdateCustomerWithAddresses;
using Univali.Api.Features.Customers.Queries.GetCustomerDetail;
using Univali.Api.Features.Customers.Queries.GetCustomersDetail;
using Univali.Api.Features.Customers.Queries.GetCustomersWithAddressesDetail;
using Univali.Api.Features.Customers.Queries.GetCustomerWithAddressesDetail;
using Univali.Api.Models;
using Univali.Api.Repositories;

namespace Univali.Api.Controllers;


[Route("api/customers")]
[Authorize]
public class CustomersController : MainController
{
    private readonly Data _data;
    private readonly IMapper _mapper;
    private readonly CustomerContext _context;
    private readonly IMediator _mediator;

    public CustomersController(Data data, IMapper mapper, CustomerContext context,
                                IMediator mediator)
    {
        _data = data ?? throw new ArgumentNullException(nameof(data));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

///////////////////////////////////////////////////////////////////////////
// Create
///////////////////////////////////////////////////////////////////////////
    [HttpPost]
    public async Task<ActionResult<CustomerDto>> CreateCustomer(
        CreateCustomerCommand createCustomerCommand
        )
    {
        var createCustomerCommandResponse = await _mediator.Send(createCustomerCommand);

        if(!createCustomerCommandResponse.IsSuccess)
        {
            foreach(var error in createCustomerCommandResponse.Errors)
            {
                string key = error.Key;
                string[] values = error.Value;
                
                foreach (var value in values)
                {
                    ModelState.AddModelError(key, value);
                }
            }
            return ValidationProblem(ModelState);
        }
        return CreatedAtRoute
        (
            "GetCustomerById",
            new { customerId = createCustomerCommandResponse.Customer.Id },
            createCustomerCommandResponse.Customer
        );
    }

    [HttpPost("with-addresses")]
    public async Task<ActionResult<CustomerWithAddressesDto>> CreateCustomerWithAddresses(
       CreateCustomerWithAddressesCommand createCustomerWithAddressesCommand)
    {

        var createcustomerCommandResponse = await _mediator.Send(createCustomerWithAddressesCommand);

        if (!createcustomerCommandResponse.IsSuccess)
        {
            foreach (var error in createcustomerCommandResponse.Errors)
            {
                string key = error.Key;
                string[] values = error.Value;

                foreach (var value in values)
                {
                    ModelState.AddModelError(key, value);
                }
            }

            return ValidationProblem(ModelState);
        }

        return CreatedAtRoute(
            "GetCustomerWithAddressesById",
            new { customerId = createcustomerCommandResponse.Customer.Id },
            createcustomerCommandResponse.Customer
        );
    }

///////////////////////////////////////////////////////////////////////////
// Read
///////////////////////////////////////////////////////////////////////////
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomers()
    {
        var getCustomerDetailQuery = new GetCustomersDetailQuery();

        var customerToReturn = await _mediator.Send(getCustomerDetailQuery);

        if (customerToReturn == null) return NotFound();

        return Ok(customerToReturn);
    }

    [HttpGet("{customerId}", Name = "GetCustomerById")]
    public async Task<ActionResult<CustomerDto>> GetCustomerById(int customerId)
    {
        var getCustomerDetailQuery = new GetCustomerDetailQuery {Id = customerId};

        var customerToReturn = await _mediator.Send(getCustomerDetailQuery);

        if (customerToReturn == null) return NotFound();

        return Ok(customerToReturn);
    }

    [HttpGet("cpf/{cpf}")]
    public ActionResult<CustomerDto> GetCustomerByCpf(string cpf)
    {
        var customerFromDatabase = _data.Customers
            .FirstOrDefault(c => c.Cpf == cpf);

        if (customerFromDatabase == null)
        {
            return NotFound();
        }

        CustomerDto customerToReturn = new CustomerDto
        {
            Id = customerFromDatabase.Id,
            Name = customerFromDatabase.Name,
            Cpf = customerFromDatabase.Cpf
        };
        return Ok(customerToReturn);
    }
    ///////////////////////////////////////////////////////////////////////////
    // With Addresses
    ///////////////////////////////////////////////////////////////////////////
    [HttpGet("with-addresses")]
    public async Task<ActionResult<IEnumerable<CustomerWithAddressesDto>>> GetCustomersWithAddresses()
    {
        var customersToReturn = await _mediator.Send(new GetCustomersWithAddressesDetailQuery());

        return Ok(customersToReturn);
    }

    [HttpGet("with-addresses/{customerId}", Name = "GetCustomerWithAddressesById")]
    public async Task<ActionResult<CustomerWithAddressesDto>> GetCustomerWithAddressesById(int customerId)
    {
        var customerToReturn = await _mediator.Send(new GetCustomerWithAddressesDetailQuery { Id = customerId });

        if (customerToReturn == null) return NotFound();

        return Ok(customerToReturn);
    }

///////////////////////////////////////////////////////////////////////////
// Update
///////////////////////////////////////////////////////////////////////////
    [HttpPut("{customerId}")]
    public async Task<ActionResult> UpdateCustomer(int customerId,
        UpdateCustomerCommand updateCustomerCommand)
    {
        if (customerId != updateCustomerCommand.Id) return BadRequest();

        var updateCustomerCommandResponse = await _mediator.Send(updateCustomerCommand);

        if (!updateCustomerCommandResponse.IsSuccess)
        {
            foreach (var error in updateCustomerCommandResponse.Errors)
            {
                string key = error.Key;
                string[] values = error.Value;

                foreach (var value in values)
                {
                    ModelState.AddModelError(key, value);
                }
            }

            return ValidationProblem(ModelState);
        }

        if(!updateCustomerCommandResponse.Exist) return NotFound();

        return NoContent();
    }

    [HttpPatch("{customerId}")]
    public ActionResult PartiallyUpdateCustomer(
        [FromBody] JsonPatchDocument<CustomerForPatchDto> patchDocument,
        [FromRoute] int customerId)
    {
        var customerFromDatabase = _data.Customers
            .FirstOrDefault(customer => customer.Id == customerId);

        if (customerFromDatabase == null) return NotFound();

        var customerToPatch = new CustomerForPatchDto
        {
            Name = customerFromDatabase.Name,
            Cpf = customerFromDatabase.Cpf
        };

        patchDocument.ApplyTo(customerToPatch, ModelState);

        if (!TryValidateModel(customerToPatch))
        {
            return ValidationProblem(ModelState);
        }

        customerFromDatabase.Name = customerToPatch.Name;
        customerFromDatabase.Cpf = customerToPatch.Cpf;

        return NoContent();
    }

    ///////////////////////////////////////////////////////////////////////////
    // With Addresses
    ///////////////////////////////////////////////////////////////////////////
    [HttpPut("with-addresses/{customerId}")]
    public async Task<ActionResult> UpdateCustomerWithAddresses(int customerId, UpdateCustomerWithAddressesCommand updateCustomerWithAddressesCommand)
    {
        if (customerId != updateCustomerWithAddressesCommand.Id) return BadRequest();

        var updateCustomerCommandResponse = await _mediator.Send(updateCustomerWithAddressesCommand);

        if (!updateCustomerCommandResponse.IsSuccess)
        {
            foreach (var error in updateCustomerCommandResponse.Errors)
            {
                string key = error.Key;
                string[] values = error.Value;

                foreach (var value in values)
                {
                    ModelState.AddModelError(key, value);
                }
            }

            return ValidationProblem(ModelState);
        }

        if(!updateCustomerCommandResponse.Exist) return NotFound();

        return NoContent();
    }
///////////////////////////////////////////////////////////////////////////
// Delete
///////////////////////////////////////////////////////////////////////////
    [HttpDelete("{customerId}")]
    public async Task<ActionResult> DeleteCustomer(int customerId)
    {
        var result = await _mediator.Send(new DeleteCustomerCommand { Id = customerId });

        if (!result) return NotFound();

        return NoContent();
    }

}