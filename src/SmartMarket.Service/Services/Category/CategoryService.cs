using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Interfaces;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.DTOs.Category;
using SmartMarket.Service.Interfaces.Category;
using System.Net;
using Et = SmartMarket.Domain.Entities.Categories;

namespace SmartMarket.Service.Services.Category
{
    public class CategoryService(IUnitOfWork unitOfWork,
                                 IMapper mapper,
                                 IValidator<CategoryDto> validator) : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<CategoryDto> _validator = validator;

        public async Task<bool> AddAsync(CategoryDto dto)
        {
            var validationResult = _validator.Validate(dto);
            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult.Errors.First().ErrorMessage);
            }

            var category = _mapper.Map<Et.Category>(dto);
            return await _unitOfWork.Category.Add(category);
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            var category = await _unitOfWork.Category.GetById(Id);
            if (category == null)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "Category not found.");
            }

            return await _unitOfWork.Category.Remove(category);
        }

        public async Task<List<Et.Category>> GetAllAsync()
        {
            return await _unitOfWork.Category.GetAll().ToListAsync();
        }

        public async Task<bool> UpdateAsync(CategoryDto dto, Guid Id)
        {
            var category = await _unitOfWork.Category.GetById(Id);
            if (category == null)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "Category not found.");
            }

            var validationResult = _validator.Validate(dto);
            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult.Errors.First().ErrorMessage);
            }

            category.Name = dto.Name;
            category.Description = dto.Description;

            return await _unitOfWork.Category.Update(category);
        }
        public async Task<CategoryDto> GetByIdAsync(Guid Id)
        {
            var category = await _unitOfWork.Category.GetById(Id);
            if (category == null)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "Category not found.");
            }

            return _mapper.Map<CategoryDto>(category);
        }

    }
}