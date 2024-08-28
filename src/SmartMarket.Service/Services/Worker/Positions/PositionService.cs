using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Interfaces;
using SmartMarket.Domain.Entities.Workers;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.Common.Validators;
using SmartMarket.Service.DTOs.Workers.Position;
using SmartMarket.Service.Interfaces.Worker.Positions;
using System.Net;

namespace SmartMarket.Service.Services.Worker.Positions
{
    public class PositionService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<AddPositionDto> validator) : IPositionService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<AddPositionDto> _validator = validator;

        public async Task<bool> AddAsync(AddPositionDto dto)
        {
            var validationResult = _validator.Validate(dto);

            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult.Errors.First().ErrorMessage);
            }

            var position = _mapper.Map<Position>(dto);

            return await _unitOfWork.Position.Add(position);
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            var position = await _unitOfWork.Position.GetById(Id);

            if (position == null)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "Position not found.");
            }

            return await _unitOfWork.Position.Remove(position);
        }

        public async Task<List<PositionDto>> GetAllAsync()
        {
            var positions = await _unitOfWork.Position.GetAll().ToListAsync();
            return _mapper.Map<List<PositionDto>>(positions);
        }

        public async Task<bool> UpdateAsync(AddPositionDto dto, Guid Id)
        {
            var position = await _unitOfWork.Position.GetById(Id);

            if (position == null)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "Position not found.");
            }

            _mapper.Map(dto, position);

            return await _unitOfWork.Position.Update(position);
        }
    }
}