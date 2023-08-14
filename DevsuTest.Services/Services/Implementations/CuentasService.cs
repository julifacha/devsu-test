using AutoMapper;
using DevsuTest.Application.DTO;
using DevsuTest.Application.Services.Interfaces;
using DevsuTest.Core.Exceptions;
using DevsuTest.Domain;
using DevsuTest.Repository.UOW;
using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace DevsuTest.Application.Services.Implementations
{
    public class CuentasService : ICuentasService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<CuentaDto> _validator;

        public CuentasService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CuentaDto> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<CuentaDto> GetById(int id)
        {
            Cuenta? cuenta = await _unitOfWork.CuentasRepository
                .FindOneAsync(c => c.Id == id,
                    include: i => i.Include(c => c.Movimientos));

            if (cuenta == null)
                throw new EntityNotFoundException(typeof(Cuenta).Name);

            return _mapper.Map<CuentaDto>(cuenta);
        }

        public async Task<IEnumerable<CuentaDto>> GetAll()
        {
            return _mapper.Map<IEnumerable<CuentaDto>>(await _unitOfWork.CuentasRepository.FindAll().ToListAsync());
        }
        public async Task<CuentaDto> Create(CuentaDto cuentaDto)
        {
            await _validator.ValidateAndThrowAsync(cuentaDto);

            Cuenta cuenta = Cuenta.Create(cuentaDto.ClienteId, cuentaDto.NumeroCuenta, cuentaDto.TipoCuenta, cuentaDto.SaldoInicial);
            cuenta = await _unitOfWork.CuentasRepository.AddAsync(cuenta);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<CuentaDto>(cuenta);
        }

        public async Task<CuentaDto> Update(int cuentaId, CuentaDto cuentaDto)
        {
            cuentaDto.Id = cuentaId;
            await _validator.ValidateAndThrowAsync(cuentaDto);

            Cuenta? cuenta = await _unitOfWork.CuentasRepository
                .FindOneAsync(c => c.Id == cuentaId,
                    include: i => i.Include(c => c.Movimientos));

            cuenta.NumeroCuenta = cuentaDto.NumeroCuenta;
            cuenta.Estado = cuentaDto.Estado;
            
            cuenta = _unitOfWork.CuentasRepository.Update(cuenta);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<CuentaDto>(cuenta);
        }

        public async Task<CuentaDto> Patch(int cuentaId, JsonPatchDocument<CuentaDto> patchDocument)
        {
            CuentaDto cuentaDto = _mapper.Map<CuentaDto>(await _unitOfWork.CuentasRepository.GetByIdAsync(cuentaId));
            patchDocument.ApplyTo(cuentaDto);
            return await Update(cuentaId, cuentaDto);
        }

        public async Task Delete(int cuentaId)
        {
            Cuenta? cuenta = await _unitOfWork.CuentasRepository.GetByIdAsync(cuentaId);

            if (cuenta == null)
                throw new EntityNotFoundException(typeof(Cuenta).Name);

            _unitOfWork.CuentasRepository.Delete(cuenta);
            await _unitOfWork.CompleteAsync();
        }
    }
}
