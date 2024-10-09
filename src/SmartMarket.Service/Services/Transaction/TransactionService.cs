using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Interfaces;
using Et = SmartMarket.Domain.Entities.Transactions;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.Common.Validators;
using SmartMarket.Service.DTOs.Transaction;
using SmartMarket.Service.Interfaces.Transaction;
using System.Net;
using Microsoft.Extensions.Logging;

namespace SmartMarket.Service.Services.Transaction
{
    public class TransactionService(IUnitOfWork unitOfWork,
                             IMapper mapper,
                             IValidator<AddTransactionDto> validator,
                             ILogger<TransactionService> logger) : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<AddTransactionDto> _validator = validator;
        private readonly ILogger<TransactionService> _logger = logger;

        public async Task<bool> AddAsync(AddTransactionDto dto)
        {
            try
            {
                var validationResult = _validator.Validate(dto);
                if (!validationResult.IsValid)
                {
                    throw new ValidatorException(validationResult.Errors.First().ErrorMessage);
                }

                var transaction = _mapper.Map<Et.Transaction>(dto);
                return await _unitOfWork.Transaction.Add(transaction);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a transaction.");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            try
            {
                var transaction = await _unitOfWork.Transaction.GetById(Id);
                if (transaction == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Transaction not found.");
                }

                return await _unitOfWork.Transaction.Remove(transaction);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting a transaction.");
                throw;
            }
        }

        public async Task<List<TransactionDto>> GetAllAsync()
        {
            try
            {
                var transactions = await _unitOfWork.Transaction.GetAll().ToListAsync();
                return _mapper.Map<List<TransactionDto>>(transactions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all transactions.");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(AddTransactionDto dto, Guid Id)
        {
            try
            {
                var transaction = await _unitOfWork.Transaction.GetById(Id);
                if (transaction == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Transaction not found.");
                }

                _mapper.Map(dto, transaction);

                return await _unitOfWork.Transaction.Update(transaction);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating a transaction.");
                throw;
            }
        }
    }
}