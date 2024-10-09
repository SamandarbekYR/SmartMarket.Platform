using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Interfaces;
using SmartMarket.Domain.Entities.PayDesks;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.DTOs.PayDesks;
using SmartMarket.Service.Interfaces.PayDesks;
using System.Net;
using Microsoft.Extensions.Logging; 

namespace SmartMarket.Service.Services.PayDesks
{
    public class PayDeskService(IUnitOfWork unitOfWork,
                                 IMapper mapper,
                                 IValidator<AddPayDesksDto> validator,
                                 ILogger<PayDeskService> logger) : IPayDeskService 
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<AddPayDesksDto> _validator = validator;
        private readonly ILogger<PayDeskService> _logger = logger;

        public async Task<bool> AddAsync(AddPayDesksDto dto)
        {
            try
            {
                var validationResult = _validator.Validate(dto);
                if (!validationResult.IsValid)
                {
                    throw new ValidatorException(validationResult.Errors.First().ErrorMessage);
                }

                var payDesk = _mapper.Map<PayDesk>(dto);
                return await _unitOfWork.PayDesk.Add(payDesk);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a pay desk.");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            try
            {
                var payDesk = await _unitOfWork.PayDesk.GetById(Id);
                if (payDesk == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Pay Desk not found.");
                }

                return await _unitOfWork.PayDesk.Remove(payDesk);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting a pay desk.");
                throw;
            }
        }

        public async Task<List<PayDesksDto>> GetAllAsync()
        {
            try
            {
                var payDesks = await _unitOfWork.PayDesk.GetAll().ToListAsync();
                return _mapper.Map<List<PayDesksDto>>(payDesks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all pay desks.");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(AddPayDesksDto dto, Guid Id)
        {
            try
            {
                var payDesk = await _unitOfWork.PayDesk.GetById(Id);
                if (payDesk == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Pay Desk not found.");
                }

                _mapper.Map(dto, payDesk);

                return await _unitOfWork.PayDesk.Update(payDesk);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating a pay desk.");
                throw;
            }
        }
    }
}