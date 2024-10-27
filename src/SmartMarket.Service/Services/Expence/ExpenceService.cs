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
using System.Collections.Immutable;

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

        public async Task<IEnumerable<FullExpenceDto>> FilterExpenceAsync(FilterExpenseDto filterExpenseDto)
        {
            try
            {
                var expences = await _unitOfWork.Expense.GetExpensesFullInformationAsync().ToListAsync();

                if (filterExpenseDto.FromDateTime.HasValue && filterExpenseDto.ToDateTime.HasValue)
                {
                    expences = expences.Where(
                        ps => ps.CreatedDate >= filterExpenseDto.FromDateTime.Value
                        && ps.CreatedDate <= filterExpenseDto.ToDateTime.Value).ToList();
                }
                else
                {
                    expences = expences.Where(
                        ps => ps.CreatedDate.Value.Date == DateTime.Today).ToList();
                }

                if(!string.IsNullOrEmpty(filterExpenseDto.Reason))
                {
                    expences = expences.Where(
                        ps => ps.Reason.Contains(filterExpenseDto.Reason)).ToList();
                }

                if(!string.IsNullOrWhiteSpace(filterExpenseDto.WorkerName))
                {
                    expences = expences.Where(
                        ps => ps.Worker.FirstName.Contains(filterExpenseDto.WorkerName)).ToList();
                }

                var expenseDtos = expences.Select(e => new FullExpenceDto
                {
                    Id = e.Id,
                    WorkerId = e.Worker.Id,
                    PayDeskId = e.PayDesk.Id,
                    Reason = e.Reason,
                    Amount = e.Amount,
                    TypeOfPayment = e.TypeOfPayment,
                    WorkerFirstName = e.Worker.FirstName,
                    WorkerLastName = e.Worker.LastName,
                    PayDeskName = e.PayDesk.Name
                }).ToList();

                return expenseDtos;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occurred while filter expenses.");
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
                    WorkerFirstName = e.Worker.FirstName,
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

        public async Task<FullExpenceDto> GetByIdAsync(Guid Id)
        {
            try
            {
                var expense = await _unitOfWork.Expense.GetExpensesFullInformationAsync().ToListAsync();

                var expenseDto = expense.FirstOrDefault(x => x.Id == Id);

                var expenseResult = new FullExpenceDto
                {
                    Id = expenseDto.Id,
                    WorkerId = expenseDto.Worker.Id,
                    PayDeskId = expenseDto.PayDesk.Id,
                    Reason = expenseDto.Reason,
                    Amount = expenseDto.Amount,
                    TypeOfPayment = expenseDto.TypeOfPayment,
                    PayDeskName = expenseDto.PayDesk.Name,
                    WorkerFirstName= expenseDto.Worker.FirstName,
                    WorkerLastName = expenseDto.Worker.LastName
                };

                return expenseResult;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting expense by id.");
                throw;
            }
        }

        public async Task<IEnumerable<FullExpenceDto>> GetExpensesByReasonAsync(string reason, PaginationParams paginationParams)
        {
            try
            {
                var expenses = await _unitOfWork.Expense.GetExpensesFullInformationAsync()
                    .Where(e => e.Reason.ToLower().Contains(reason.ToLower()))
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
                    WorkerFirstName = e.Worker.FirstName,
                    WorkerLastName = e.Worker.LastName,
                    PayDeskName = e.PayDesk.Name
                });

                return expenseDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while searching for expenses by reason.");
                throw;
            }
        }

        public async Task<ExpenseSummaryDto> GetExpenseSummaryAsync()
        {
            try
            {
                var expenses = await _unitOfWork.Expense.GetExpensesFullInformationAsync()
                    .ToListAsync();

                var totalCash = expenses.Where(x => x.TypeOfPayment == "Naqd").Sum(x => x.Amount);
                var totalCard = expenses.Where(x => x.TypeOfPayment == "Karta").Sum(x => x.Amount);
                var totalAmount = expenses.Sum(x => x.Amount);

                return new ExpenseSummaryDto
                {
                    TotalCash = totalCash,
                    TotalCard = totalCard,
                    TotalAmount = totalAmount
                };
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occured while getting by expense total amount.");
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