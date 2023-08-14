using AutoMapper;
using DevsuTest.Domain;
using DevsuTest.Repository.UOW;
using DevsuTest.Application.DTO;
using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using DevsuTest.Application.Services.Interfaces;
using DevsuTest.Core.Interfaces;
using DevsuTest.Core.Exceptions;

namespace DevsuTest.Application.Services.Implementations
{
    public class ClientesService : IClientesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<ClienteDto> _validator;
        private readonly IPasswordHasher _passwordHasher;

        public ClientesService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<ClienteDto> validator, IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
            _passwordHasher = passwordHasher;
        }

        public async Task<ClienteDto> GetById(int clienteId)
        {
            Cliente? cliente = await _unitOfWork.ClientesRepository.GetByIdAsync(clienteId);

            if (cliente == null)
                throw new EntityNotFoundException(typeof(Cliente).Name);

            return _mapper.Map<ClienteDto>(await _unitOfWork.ClientesRepository.GetByIdAsync(clienteId));
        }

        public async Task<IEnumerable<ClienteDto>> GetAll()
        {
            return _mapper.Map<IEnumerable<ClienteDto>>(await _unitOfWork.ClientesRepository.FindAll().ToListAsync());
        }

        public async Task<ClienteDto> Create(ClienteDto clienteDto)
        {
            await _validator.ValidateAndThrowAsync(clienteDto);

            Cliente cliente = Cliente.Create(clienteDto.Nombre, clienteDto.Genero, clienteDto.FechaNacimiento, clienteDto.Identificacion, clienteDto.Direccion, clienteDto.Telefono);
            var hashedpassword = _passwordHasher.Hash(clienteDto.Contraseña.ToString(), cliente.ClienteId.ToByteArray());
            cliente.ContraseñaHash = hashedpassword;
            cliente = await _unitOfWork.ClientesRepository.AddAsync(cliente);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<ClienteDto>(cliente);
        }

        public async Task<ClienteDto> Update(int clienteId, ClienteDto clienteDto)
        {
            clienteDto.Id = clienteId;
            await _validator.ValidateAndThrowAsync(clienteDto);

            Cliente cliente = await _unitOfWork.ClientesRepository.GetByIdAsync(clienteDto.Id);
            cliente.Nombre = clienteDto.Nombre;
            cliente.Telefono = clienteDto.Telefono;
            cliente.Estado = clienteDto.Estado;
            cliente.Direccion = clienteDto.Direccion;
            cliente.FechaNacimiento = clienteDto.FechaNacimiento;
            cliente.Genero = cliente.Genero;

            cliente = _unitOfWork.ClientesRepository.Update(cliente);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<ClienteDto>(cliente);
        }

        public async Task<ClienteDto> Patch(int clienteId, JsonPatchDocument<ClienteDto> patchDocument)
        {
            Cliente? cliente = await _unitOfWork.ClientesRepository.GetByIdAsync(clienteId);

            if (cliente == null)
                throw new EntityNotFoundException(typeof(Cliente).Name);
            
            ClienteDto clienteDto = _mapper.Map<ClienteDto>(cliente);
            patchDocument.ApplyTo(clienteDto);
            return await Update(clienteId, clienteDto);
        }

        public async Task Delete(int clienteId)
        {
            Cliente? cliente = await _unitOfWork.ClientesRepository.GetByIdAsync(clienteId);
            if (cliente == null)
                throw new EntityNotFoundException(typeof(Cliente).Name);

            _unitOfWork.ClientesRepository.Delete(cliente);
            await _unitOfWork.CompleteAsync();
        }
    }
}