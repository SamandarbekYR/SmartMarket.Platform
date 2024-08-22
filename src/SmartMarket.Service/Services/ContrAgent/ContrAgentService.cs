using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Interfaces;
using Et = SmartMarket.Domain.Entities.PartnersCompany;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.Common.Validators;
using SmartMarket.Service.DTOs.ContrAgent;
using SmartMarket.Service.Interfaces.ContrAgent;
using System.Net;

namespace SmartMarket.Service.Services.ContrAgent;

public class ContrAgentService(IUnitOfWork unitOfWork,
                             IMapper mapper,
                             IValidator<AddContrAgentDto> validator) : IContrAgentService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<AddContrAgentDto> _validator = validator;

    public async Task<bool> AddAsync(AddContrAgentDto dto)
    {
        var validationResult = _validator.Validate(dto);
        if (!validationResult.IsValid)
        {
            throw new ValidatorException(validationResult.Errors.First().ErrorMessage);
        }

        var contrAgent = _mapper.Map<Et.ContrAgent>(dto);
        return await _unitOfWork.ContrAgent.Add(contrAgent);
    }

    public async Task<bool> DeleteAsync(Guid Id)
    {
        var contrAgent = await _unitOfWork.ContrAgent.GetById(Id);
        if (contrAgent == null)
        {
            throw new StatusCodeException(HttpStatusCode.NotFound, "Counteragent not found.");
        }

        return await _unitOfWork.ContrAgent.Remove(contrAgent);
    }

    public async Task<List<ContrAgentDto>> GetAllAsync()
    {
        var contrAgents = await _unitOfWork.ContrAgent.GetAll().ToListAsync();
        return _mapper.Map<List<ContrAgentDto>>(contrAgents);
    }

    public async Task<bool> UpdateAsync(AddContrAgentDto dto, Guid Id)
    {
        var contrAgent = await _unitOfWork.ContrAgent.GetById(Id);
        if (contrAgent == null)
        {
            throw new StatusCodeException(HttpStatusCode.NotFound, "Counteragent not found.");
        }

        _mapper.Map(dto, contrAgent);

        return await _unitOfWork.ContrAgent.Update(contrAgent);
    }
}
