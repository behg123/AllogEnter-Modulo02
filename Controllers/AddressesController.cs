using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Univali.Api.Controllers;
using Univali.Api.DbContexts;
using Univali.Api.Entities;
using Univali.Api.Features.Addresses.Commands.CreateAddress;
using Univali.Api.Features.Addresses.Commands.DeleteAddress;
using Univali.Api.Features.Addresses.Commands.UpdateAddress;
using Univali.Api.Features.Addresses.Queries.GetAddressDetail;
using Univali.Api.Features.Addresses.Queries.GetAddressesDetail;
using Univali.Api.Models;
using Univali.Api.Repositories;

namespace Univali.Api.Controller;

[Route("api/customers/{customerId}/addresses")]
[Authorize]
public class AddressesController : MainController
{
    private readonly Data _data;
    private readonly IMapper _mapper;
    private readonly CustomerContext _context;
    private readonly ICustomerRepository _customerRepository;
    private readonly IMediator _mediator;

    public AddressesController(Data data, IMapper mapper, CustomerContext context, ICustomerRepository customerRepository, IMediator mediator)
    {
        _data = data ?? throw new ArgumentNullException(nameof(data));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    }

///////////////////////////////////////////////////////////////////////////
// Create
///////////////////////////////////////////////////////////////////////////
    [HttpPost]
    public async Task<ActionResult<AddressDto>> CreateAddress(int customerId, CreateAddressCommand createAddressCommand)
    {
        createAddressCommand.CustomerId = customerId;
        var addressToReturn = await _mediator.Send(createAddressCommand);
        if(addressToReturn == null) return BadRequest();
        if(!addressToReturn.IsSuccess)
        {
            foreach(var error in addressToReturn.Erros)
            {
                string key = error.Key;
                string[] values = error.Value;

                foreach(var value in values)
                {
                    ModelState.AddModelError(key, value);
                }
            }
        }
        return CreatedAtRoute
        (
            "GetAddressById",
            new {customerId, addressId = addressToReturn.Address.Id},
            addressToReturn.Address
        );
    }

///////////////////////////////////////////////////////////////////////////
// Read
///////////////////////////////////////////////////////////////////////////
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AddressDto>>> GetAddresses(int customerId)
    {
        if(! await _customerRepository.CustomerExistsAsync(customerId)) return NotFound();

        var getAddressesDetailQuery = new GetAddressesDetailQuery {CustomerId = customerId};

        var addressesToReturn = await _mediator.Send(getAddressesDetailQuery);

        return Ok(addressesToReturn);
    }
    
    [HttpGet("{addressId}", Name = "GetAddressById")]
    public async Task<ActionResult<AddressDto>> GetAddressById(int customerId, int addressId)
    {
        if(!await _customerRepository.CustomerExistsAsync(customerId)) return NotFound();

        var getAddressDetailQuery = new GetAddressDetailQuery {Id = addressId, CustomerId = customerId};

        var addressToReturn = await _mediator.Send(getAddressDetailQuery);

        if (addressToReturn == null) return NotFound();

        return Ok(addressToReturn); 
    }


///////////////////////////////////////////////////////////////////////////
// Update
///////////////////////////////////////////////////////////////////////////
    [HttpPut("{addressId}")]
    public async Task<ActionResult> UpdateAddress(int customerId, int addressId, UpdateAddressCommand updateAddressCommand)
    {
        if(addressId != updateAddressCommand.Id) return BadRequest();

        updateAddressCommand.CustomerId = customerId;

        var addressToReturn = await _mediator.Send(updateAddressCommand);

        if(addressToReturn == null) return NotFound();

        if(!addressToReturn.IsSuccess)
        {
            foreach(var error in addressToReturn.Erros)
            {
                string key = error.Key;
                string[] values = error.Value;

                foreach(var value in values)
                {
                    ModelState.AddModelError(key, value);
                }
            }
        }
        return NoContent();
    }


///////////////////////////////////////////////////////////////////////////
// Delete
///////////////////////////////////////////////////////////////////////////
    [HttpDelete("{addressId}")]
    public async Task<ActionResult> DeleteAddress(int customerId, int addressId)
    {

        if(!await _mediator.Send(new DeleteAddressCommand { CustomerId = customerId, Id = addressId }))
            return NotFound();

        return NoContent();
    }

}