using AutoMapper;
using DevsuTest.Application.DTO;
using DevsuTest.Application.Services.Interfaces;
using DevsuTest.Core.Exceptions;
using DevsuTest.Domain;
using DevsuTest.Repository.UOW;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DevsuTest.Application.Services.Implementations
{
    public class MovimientosService : IMovimientosService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<MovimientoDto> _validator;
        public MovimientosService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<MovimientoDto> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<MovimientoDto> GetById(int id)
        {
            Movimiento? movimiento = await _unitOfWork.MovimientosRepository.GetByIdAsync(id);

            if (movimiento == null)
                throw new EntityNotFoundException(typeof(Movimiento).Name);

            return _mapper.Map<MovimientoDto>(movimiento);
        }

        public async Task<IEnumerable<MovimientoDto>> GetAll()
        {
            return _mapper.Map<IEnumerable<MovimientoDto>>(await _unitOfWork.MovimientosRepository.FindAll().ToListAsync());
        }

        public async Task<MovimientoDto> Create(MovimientoDto movimientoDto)
        {
            await _validator.ValidateAndThrowAsync(movimientoDto);

            Cuenta? cuenta = await _unitOfWork.CuentasRepository
                .FindOneAsync(c => c.Id == movimientoDto.CuentaId, 
                    include: i => i.Include(c => c.Movimientos));

            Movimiento movimiento = 
                movimientoDto.TipoMovimiento == Domain.Enum.TipoMovimientoEnum.Retiro 
                    ? cuenta.Retirar(movimientoDto.Valor) 
                    : cuenta.Depositar(movimientoDto.Valor);

            _unitOfWork.CuentasRepository.Update(cuenta);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<MovimientoDto>(movimiento);
        }

        public async Task Delete(int movimientoId)
        {
            Movimiento? movimiento = await _unitOfWork.MovimientosRepository.GetByIdAsync(movimientoId);

            if (movimiento == null)
                throw new EntityNotFoundException(typeof(Movimiento).Name);

            _unitOfWork.MovimientosRepository.Delete(movimiento);
            await _unitOfWork.CompleteAsync();
        }
    }
}
