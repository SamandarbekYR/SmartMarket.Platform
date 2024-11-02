using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Interfaces;
using Et = SmartMarket.Domain.Entities.PartnersCompany;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.Common.Validators;
using System.Net;
using SmartMarket.Service.DTOs.PartnersCompany.ContrAgent;
using SmartMarket.Service.Interfaces.PartnersCompany.ContrAgent;
using SmartMarket.Service.Common.Utils;
using SmartMarket.Service.Common.Extentions;
using Microsoft.Extensions.Logging;
using SmartMarket.Service.Common.ServiceValidation; 

namespace SmartMarket.Service.Services.PartnersCompany.ContrAgent
{
    public class ContrAgentService(IUnitOfWork unitOfWork,
                                 IMapper mapper,
                                 IValidator<AddContrAgentDto> validator,
                                 ILogger<ContrAgentService> logger) : IContrAgentService 
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<AddContrAgentDto> _validator = validator;
        private readonly ILogger<ContrAgentService> _logger = logger; 

        public async Task<bool> AddAsync(AddContrAgentDto dto)
        {
            try
            {
                var validationResult = _validator.Validate(dto);
                if (!validationResult.IsValid)
                {
                    throw new ValidatorException(validationResult.Errors.First().ErrorMessage);
                }

                var companyExists = await _unitOfWork.PartnerCompany.GetById(dto.CompanyId) != null;
                if (!companyExists)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Company not found.");
                }

                if (!PhoneNumberValidators.IsValid(dto.PhoneNumber))
                {
                    throw new StatusCodeException(HttpStatusCode.BadRequest, "Invalid phone number format. Phone number should start with +998 and have 12 digits.");
                }

                var phoneNumberExists = await _unitOfWork.ContrAgent.GetAll()
                    .AnyAsync(ca => ca.PhoneNumber == dto.PhoneNumber);
                if (phoneNumberExists)
                {
                    throw new StatusCodeException(HttpStatusCode.BadRequest, "Phone number already exists.");
                }

                var contrAgent = _mapper.Map<Et.ContrAgent>(dto);
                return await _unitOfWork.ContrAgent.Add(contrAgent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a counteragent.");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            try
            {
                var contrAgent = await _unitOfWork.ContrAgent.GetById(Id);
                if (contrAgent == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Counteragent not found.");
                }

                return await _unitOfWork.ContrAgent.Remove(contrAgent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting a counteragent.");
                throw;
            }
        }

        public async Task<IEnumerable<ContrAgentDto>> GetAllAsync(PaginationParams @params)
        {
            try
            {
                var contrAgents = await _unitOfWork.ContrAgent.GetAll()
                                             .AsNoTracking()
                                             .ToPagedListAsync(@params);

                return contrAgents.Select(ca => _mapper.Map<ContrAgentDto>(ca)).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all counteragents.");
                throw;
            }
        }

        public async Task<ContrAgentDto> GetContrAgentByCompanyNameAsync(string companyName)
        {
            try
            {
                var contrAgents = await _unitOfWork.ContrAgent.GetContrAgentsFullInformationAsync();

                var contrAgent = contrAgents.FirstOrDefault(ca => ca.PartnerCompany.Name == companyName);

                if (contrAgent == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Counteragent not found.");
                }

                return _mapper.Map<ContrAgentDto>(contrAgent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting counteragent by company name.");
                throw;
            }
        }


        public async Task<IEnumerable<ContrAgentDto>> GetContrAgentByNameAsync(string name)
        {
            try
            {
                var contrAgent = await _unitOfWork.ContrAgent.GetAll()
                    .AsNoTracking()
                    .Where(ca => ca.FirstName.ToLower().Contains(name.ToLower()))
                    .ToListAsync();

                if (contrAgent == null || !contrAgent.Any())
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Counteragent not found.");
                }

                return _mapper.Map<IEnumerable<ContrAgentDto>>(contrAgent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting counteragent by name.");
                throw;
            }
        }

        public async Task<ContrAgentDto> GetContrAgentByNumberAsync(string number)
        {
            try
            {
                var contrAgent = await _unitOfWork.ContrAgent.GetAll()
                    .FirstOrDefaultAsync(ca => ca.PhoneNumber == number);

                if (contrAgent == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Counteragent not found.");
                }

                return _mapper.Map<ContrAgentDto>(contrAgent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting counteragent by phone number.");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(AddContrAgentDto dto, Guid Id)
        {
            try
            {
                var contrAgent = await _unitOfWork.ContrAgent.GetById(Id);
                if (contrAgent == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Counteragent not found.");
                }

                var companyExists = await _unitOfWork.PartnerCompany.GetById(dto.CompanyId) != null;
                if (!companyExists)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Company not found.");
                }

                if (!PhoneNumberValidators.IsValid(dto.PhoneNumber))
                {
                    throw new StatusCodeException(HttpStatusCode.BadRequest, "Invalid phone number format.");
                }

                var phoneNumberExists = await _unitOfWork.ContrAgent.GetAll()
                    .Where(ca => ca.Id != Id) 
                    .AnyAsync(ca => ca.PhoneNumber == dto.PhoneNumber);
                if (phoneNumberExists)
                {
                    throw new StatusCodeException(HttpStatusCode.BadRequest, "Phone number already exists.");
                }

                _mapper.Map(dto, contrAgent);

                return await _unitOfWork.ContrAgent.Update(contrAgent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating a counteragent.");
                throw;
            }
        }

    }
}