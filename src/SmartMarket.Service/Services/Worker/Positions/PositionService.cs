using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Interfaces;
using Et = SmartMarket.Domain.Entities.Workers;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.Common.Validators;
using SmartMarket.Service.DTOs.Workers.Position;
using SmartMarket.Service.Interfaces.Worker.Positions;
using System.Net;
using Microsoft.Extensions.Logging;
using SmartMarket.Domain.Entities.Workers;

namespace SmartMarket.Service.Services.Worker.Positions
{
    public class PositionService(IUnitOfWork unitOfWork,
                                 IMapper mapper,
                                 IValidator<AddPositionDto> validator,
                                 ILogger<PositionService> logger) : IPositionService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<AddPositionDto> _validator = validator;
        private readonly ILogger<PositionService> _logger = logger;

        public async Task<bool> AddAsync(AddPositionDto dto)
        {
            try
            {
                var validationResult = _validator.Validate(dto);

                if (!validationResult.IsValid)
                {
                    throw new ValidatorException(validationResult.Errors.First().ErrorMessage);
                }

                var position = _mapper.Map<Position>(dto);

                return await _unitOfWork.Position.Add(position);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a position.");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            try
            {
                var position = await _unitOfWork.Position.GetById(Id);

                if (position == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Position not found.");
                }

                return await _unitOfWork.Position.Remove(position);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting a position.");
                throw;
            }
        }

        public async Task<List<PositionDto>> GetAllAsync()
        {
            try
            {
                var positions = await _unitOfWork.Position.GetAll().ToListAsync();
                return _mapper.Map<List<PositionDto>>(positions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all positions.");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(AddPositionDto dto, Guid Id)
        {
            try
            {
                var position = await _unitOfWork.Position.GetById(Id);

                if (position == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Position not found.");
                }

                _mapper.Map(dto, position);

                return await _unitOfWork.Position.Update(position);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating a position.");
                throw;
            }
        }
    }
}