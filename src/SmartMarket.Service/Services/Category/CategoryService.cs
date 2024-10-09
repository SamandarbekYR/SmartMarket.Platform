using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Interfaces;
using Et = SmartMarket.Domain.Entities.Categories;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.Common.Validators;
using System.Net;
using SmartMarket.Service.DTOs.Category;
using SmartMarket.Service.Interfaces.Category;
using Microsoft.Extensions.Logging;

namespace SmartMarket.Service.Services.Category
{
    public class CategoryService(IUnitOfWork unitOfWork,
                                 IMapper mapper,
                                 IValidator<CategoryDto> validator,
                                 ILogger<CategoryService> logger) : ICategoryService 
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<CategoryDto> _validator = validator;
        private readonly ILogger<CategoryService> _logger = logger; 

        public async Task<bool> AddAsync(CategoryDto dto)
        {
            try
            {
                var validationResult = _validator.Validate(dto);
                if (!validationResult.IsValid)
                {
                    throw new ValidatorException(validationResult.Errors.First().ErrorMessage);
                }

                var category = _mapper.Map<Et.Category>(dto);
                return await _unitOfWork.Category.Add(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a category."); 
                throw; 
            }
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            try
            {
                var category = await _unitOfWork.Category.GetById(Id);
                if (category == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Category not found.");
                }

                return await _unitOfWork.Category.Remove(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting a category.");
                throw;
            }
        }

        public async Task<List<Et.Category>> GetAllAsync()
        {
            try
            {
                return await _unitOfWork.Category.GetAll().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all categories.");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(CategoryDto dto, Guid Id)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating a category.");
                throw;
            }
        }
        public async Task<CategoryDto> GetByIdAsync(Guid Id)
        {
            try
            {
                var category = await _unitOfWork.Category.GetById(Id);
                if (category == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Category not found.");
                }

                return _mapper.Map<CategoryDto>(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting category by ID.");
                throw;
            }
        }

    }
}