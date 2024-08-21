using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Interfaces;
using SmartMarket.Domain.Entities.PayDesks;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.DTOs.PayDesks;
using SmartMarket.Service.Interfaces.PayDesks;
using System.Net;

namespace SmartMarket.Service.Services.PayDesks
{
    public class PayDeskService(IUnitOfWork unitOfWork,
                                 IMapper mapper,
                                 IValidator<AddPayDesksDto> validator) : IPayDeskService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<AddPayDesksDto> _validator = validator;

        public async Task<bool> AddAsync(AddPayDesksDto dto)
        {
            var validationResult = _validator.Validate(dto);
            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult.Errors.First().ErrorMessage);
            }

            var payDesk = _mapper.Map<PayDesk>(dto);
            return await _unitOfWork.PayDesk.Add(payDesk);
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            var payDesk = await _unitOfWork.PayDesk.GetById(Id);
            if (payDesk == null)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "Pay Desk not found.");
            }

            return await _unitOfWork.PayDesk.Remove(payDesk);
        }

        public async Task<List<PayDesksDto>> GetAllAsync()
        {
            var payDesks = await _unitOfWork.PayDesk.GetAll().ToListAsync();
            return _mapper.Map<List<PayDesksDto>>(payDesks);
        }

        public async Task<bool> UpdateAsync(AddPayDesksDto dto, Guid Id)
        {
            var payDesk = await _unitOfWork.PayDesk.GetById(Id);
            if (payDesk == null)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "Pay Desk not found.");
            }

            _mapper.Map(dto, payDesk);

            return await _unitOfWork.PayDesk.Update(payDesk);
        }
    }
}
