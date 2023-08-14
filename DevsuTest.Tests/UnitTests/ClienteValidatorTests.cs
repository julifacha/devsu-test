using DevsuTest.Application.DTO;
using DevsuTest.Application.Validators;
using DevsuTest.Core.Interfaces;
using DevsuTest.Domain;
using DevsuTest.Repository.UOW;
using Moq;

namespace DevsuTest.Tests.UnitTests
{
    public class ClienteValidatorTest
    {
        private readonly ClienteValidator _clienteValidator;

        public ClienteValidatorTest()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var clienteRepository = new Mock<IRepository<Cliente>>();
            unitOfWork.Setup(uow => uow.ClientesRepository).Returns(clienteRepository.Object);
            _clienteValidator = new ClienteValidator(unitOfWork.Object);
        }

        [Fact]
        public async void Test_Cliente_Valido()
        {
            // arrange
            var clienteDto = 
                new ClienteDto {
                    Nombre = "Julian Sosa",
                    Contraseña = 1234, 
                    Direccion = "Calle Falsa 123",
                    Telefono = "5412345678", 
                    FechaNacimiento = new DateTime(1996, 1, 12), 
                    Genero = 'M', 
                    Identificacion = "39161715" 
                };

            // act
            var validationResult = await _clienteValidator.ValidateAsync(clienteDto);

            // assert
            Assert.Empty(validationResult.Errors);
        }

        [Fact]
        public async void Test_Nombre_Muy_Largo()
        {
            // arrange
            var clienteDto = new ClienteDto { Nombre = string.Join("", Enumerable.Range(0, 101).Select(_ => "a")) };
            string expectedError = "The length of 'Nombre' must be 100 characters or fewer. You entered 101 characters.";

            // act
            var validationResult = await _clienteValidator.ValidateAsync(clienteDto);

            // assert
            Assert.Contains(expectedError, validationResult.Errors.Select(e => e.ErrorMessage));
        }
    }
}