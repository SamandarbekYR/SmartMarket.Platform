using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Interfaces;
using SmartMarket.Domain.Entities.Expenses;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.Common.Validators;
using SmartMarket.Service.DTOs.Expence;
using SmartMarket.Service.Interfaces.Expence;
using System.Net;

namespace SmartMarket.Service.Services.Expence
{
    public class ExpenceService(IUnitOfWork unitOfWork,
                                 IMapper mapper,
                                 IValidator<AddExpenceDto> validator) : IExpenceService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<AddExpenceDto> _validator = validator;

        public async Task<bool> AddAsync(AddExpenceDto dto)
        {
            var validationResult = _validator.Validate(dto);
            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult.Errors.First().ErrorMessage);
            }

            var expense = _mapper.Map<Expense>(dto);
            return await _unitOfWork.Expense.Add(expense);
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            var expense = await _unitOfWork.Expense.GetById(Id);
            if (expense == null)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "Expense not found.");
            }

            return await _unitOfWork.Expense.Remove(expense);
        }

        public async Task<List<ExpenceDto>> GetAllAsync()
        {
            var expenses = await _unitOfWork.Expense.GetAll().ToListAsync();
            return _mapper.Map<List<ExpenceDto>>(expenses);
        }

        public async Task<bool> UpdateAsync(AddExpenceDto dto, Guid Id)
        {
            var expense = await _unitOfWork.Expense.GetById(Id);
            if (expense == null)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "Expense not found.");
            }

            _mapper.Map(dto, expense);

            return await _unitOfWork.Expense.Update(expense);
        }
    }
}