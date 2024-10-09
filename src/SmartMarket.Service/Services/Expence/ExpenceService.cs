using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Interfaces;
using SmartMarket.Domain.Entities.Expenses;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.Common.Extentions;
using SmartMarket.Service.Common.Utils;
using SmartMarket.Service.Common.Validators;
using SmartMarket.Service.DTOs.Expence;
using SmartMarket.Service.Interfaces.Expence;
using System.Net;
using Microsoft.Extensions.Logging; 

namespace SmartMarket.Service.Services.Expence
{
    public class ExpenceService(IUnitOfWork unitOfWork,
                                 IMapper mapper,
                                 IValidator<AddExpenceDto> validator,
                                 ILogger<ExpenceService> logger) : IExpenceService 
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<AddExpenceDto> _validator = validator;
        private readonly ILogger<ExpenceService> _logger = logger; 

        public async Task<bool> AddAsync(AddExpenceDto dto)
        {
            try
            {
                var validationResult = _validator.Validate(dto);
                if (!validationResult.IsValid)
                {
                    throw new ValidatorException(validationResult.Errors.First().ErrorMessage);
                }

                var workerExists = await _unitOfWork.Worker.GetById(dto.WorkerId) != null;
                if (!workerExists)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Worker not found.");
                }

                var payDeskExists = await _unitOfWork.PayDesk.GetById(dto.PayDeskId) != null;
                if (!payDeskExists)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Pay Desk not found.");
                }

                var expense = _mapper.Map<Expense>(dto);
                return await _unitOfWork.Expense.Add(expense);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding an expense.");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            try
            {
                var expense = await _unitOfWork.Expense.GetById(Id);
                if (expense == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Expense not found.");
                }

                return await _unitOfWork.Expense.Remove(expense);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting an expense.");
                throw;
            }
        }

        public async Task<IEnumerable<FullExpenceDto>> GetAllAsync(PaginationParams paginationParams)
        {
            try
            {
                var expenses = await _unitOfWork.Expense.GetExpensesFullInformationAsync()
                                        .AsNoTracking()
                                        .ToPagedListAsync(paginationParams);

                var expenseDtos = expenses.Select(e => new FullExpenceDto
                {
                    Id = e.Id,
                    WorkerId = e.Worker.Id,
                    PayDeskId = e.PayDesk.Id,
                    Reason = e.Reason,
                    Amount = e.Amount,
                    TypeOfPayment = e.TypeOfPayment,
                    WorkerFirsName = e.Worker.FirstName,
                    WorkerLastName = e.Worker.LastName,
                    PayDeskName = e.PayDesk.Name
                });

                return expenseDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all expenses.");
                throw;
            }
        }


        public async Task<IEnumerable<ExpenceDto>> GetExpensesByReasonAsync(string reason, PaginationParams paginationParams)
        {
            try
            {
                var expenses = await _unitOfWork.Expense.GetAll()
                    .Where(e => e.Reason.ToLower().Contains(reason.ToLower()))
                    .AsNoTracking()
                    .ToPagedListAsync(paginationParams);

                return _mapper.Map<IEnumerable<ExpenceDto>>(expenses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while searching for expenses by reason.");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(AddExpenceDto dto, Guid Id)
        {
            try
            {
                var expense = await _unitOfWork.Expense.GetById(Id);
                if (expense == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Expense not found.");
                }

                var workerExists = await _unitOfWork.Worker.GetById(dto.WorkerId) != null;
                if (!workerExists)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Worker not found.");
                }

                var payDeskExists = await _unitOfWork.PayDesk.GetById(dto.PayDeskId) != null;
                if (!payDeskExists)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Pay Desk not found.");
                }

                _mapper.Map(dto, expense);

                return await _unitOfWork.Expense.Update(expense);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating an expense.");
                throw;
            }
        }
    }
}