using AutoMapper;
using FluentValidation;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartMarket.DataAccess.Interfaces;
using SmartMarket.Domain.Entities.PayDesks;
using SmartMarket.Domain.Entities.Scales;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.DTOs.PayDesks;
using SmartMarket.Service.DTOs.Scales;
using SmartMarket.Service.Interfaces.Scales;
using SmartMarket.Service.Services.PayDesks;

using System.Net;

namespace SmartMarket.Service.Services.Scales
{
    public class ScaleService(IUnitOfWork unitOfWork,
                                 IMapper mapper,
                                 IValidator<AddScaleDto> validator,
                                 ILogger<ScaleService> logger) : IScaleService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<AddScaleDto> _validator = validator;
        private readonly ILogger<ScaleService> _logger = logger;

        public async Task<bool> AddAsync(AddScaleDto dto)
        {
            try
            {
                var validationResult = _validator.Validate(dto);
                if (!validationResult.IsValid)
                {
                    throw new ValidatorException(validationResult.Errors.First().ErrorMessage);
                }

                var scale = _mapper.Map<Scale>(dto);
                return await _unitOfWork.Scale.Add(scale);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a scale.");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            try
            {
                var scale = await _unitOfWork.Scale.GetById(Id);
                if (scale == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Scale not found.");
                }

                return await _unitOfWork.Scale.Remove(scale);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting a pay desk.");
                throw;
            }
        }

        public async Task<List<ScaleDto>> GetAllAsync()
        {
            try
            {
                var scales = await _unitOfWork.Scale.GetAll().ToListAsync();
                return _mapper.Map<List<ScaleDto>>(scales);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all scales.");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(AddScaleDto dto, Guid Id)
        {
            try
            {
                var scale = await _unitOfWork.Scale.GetById(Id);
                if (scale == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Scale not found.");
                }

                _mapper.Map(dto, scale);

                return await _unitOfWork.Scale.Update(scale);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating a scale.");
                throw;
            }
        }
    }
}
