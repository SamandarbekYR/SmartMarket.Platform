using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Interfaces;
using Et = SmartMarket.Domain.Entities.Products;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.Common.Validators;
using System.Net;
using SmartMarket.Service.DTOs.Products.Debtors;
using SmartMarket.Service.Interfaces.Products.Debtor;

namespace SmartMarket.Service.Services.Products.Debtor
{
    public class DebtorsService(IUnitOfWork unitOfWork,
                             IMapper mapper,
                             IValidator<AddDebtorsDto> validator) : IDebtorsService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<AddDebtorsDto> _validator = validator;

        public async Task<bool> AddAsync(AddDebtorsDto dto)
        {
            var validationResult = _validator.Validate(dto);
            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult.Errors.First().ErrorMessage);
            }

            var partnerExists = await _unitOfWork.Partner.GetById(dto.PartnerId) != null;
            if (!partnerExists)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "Partner not found.");
            }

            var productExists = await _unitOfWork.Product.GetById(dto.ProductId) != null;
            if (!productExists)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "Product not found.");
            }

            var debtor = _mapper.Map<Et.Debtors>(dto);
            return await _unitOfWork.Debtors.Add(debtor);
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            var debtor = await _unitOfWork.Debtors.GetById(Id);
            if (debtor == null)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "Debtor not found.");
            }

            return await _unitOfWork.Debtors.Remove(debtor);
        }

        public async Task<List<DebtorsDto>> GetAllAsync()
        {
            var debtors = await _unitOfWork.Debtors.GetDebtorsFullInformationAsync();
            return _mapper.Map<List<DebtorsDto>>(debtors);
        }

        public async Task<bool> UpdateAsync(AddDebtorsDto dto, Guid Id)
        {
            var debtor = await _unitOfWork.Debtors.GetById(Id);
            if (debtor == null)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "Debtor not found.");
            }

            var partnerExists = await _unitOfWork.Partner.GetById(dto.PartnerId) != null;
            if (!partnerExists)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "Partner not found.");
            }

            var productExists = await _unitOfWork.Product.GetById(dto.ProductId) != null;
            if (!productExists)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "Product not found.");
            }

            _mapper.Map(dto, debtor);

            return await _unitOfWork.Debtors.Update(debtor);
        }
    }
}