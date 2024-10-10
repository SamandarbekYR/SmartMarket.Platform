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
using Microsoft.Extensions.Logging; 

namespace SmartMarket.Service.Services.Products.Debtor
{
    public class DebtorsService(IUnitOfWork unitOfWork,
                             IMapper mapper,
                             IValidator<AddDebtorsDto> validator,
                             ILogger<DebtorsService> logger) : IDebtorsService 
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<AddDebtorsDto> _validator = validator;
        private readonly ILogger<DebtorsService> _logger = logger;

        public async Task<bool> AddAsync(AddDebtorsDto dto)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a debtor.");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            try
            {
                var debtor = await _unitOfWork.Debtors.GetById(Id);
                if (debtor == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Debtor not found.");
                }

                return await _unitOfWork.Debtors.Remove(debtor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting a debtor.");
                throw;
            }
        }

        public async Task<List<DebtorsDto>> GetAllAsync()
        {
            try
            {
                var debtors = await _unitOfWork.Debtors.GetDebtorsFullInformationAsync();
                return _mapper.Map<List<DebtorsDto>>(debtors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all debtors.");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(AddDebtorsDto dto, Guid Id)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating a debtor.");
                throw;
            }
        }
    }
}