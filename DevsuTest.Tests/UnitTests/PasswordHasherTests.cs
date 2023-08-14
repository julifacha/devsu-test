using DevsuTest.Application.DTO;
using DevsuTest.Application.Validators;
using DevsuTest.Core.Interfaces;
using DevsuTest.Domain;
using DevsuTest.Infrastructure.Security;
using DevsuTest.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevsuTest.Tests.UnitTests
{
    public class PasswordHasherTests
    {
        private readonly PasswordHasher passwordHasher;

        public PasswordHasherTests()
        {
            passwordHasher = new PasswordHasher();
        }

        [Fact]
        public void Test_Contraseña_Valida()
        {
            // arrange
            Guid mockGuid = Guid.NewGuid();
            int password = 1234;

            // act
            byte[] hashedPassword = passwordHasher.Hash(password.ToString(), mockGuid.ToByteArray());

            // assert
            Assert.Equal(hashedPassword, passwordHasher.Hash(password.ToString(), mockGuid.ToByteArray()));
        }

        [Fact]
        public void Test_Contraseña_Invalida()
        {
            // arrange
            Guid mockGuid = Guid.NewGuid();
            int contraseñaCorrecta = 1234;
            int contraseñaIncorrecta = 2345;

            // act
            byte[] hashedPassword = passwordHasher.Hash(contraseñaCorrecta.ToString(), mockGuid.ToByteArray());

            // assert
            Assert.NotEqual(hashedPassword, passwordHasher.Hash(contraseñaIncorrecta.ToString(), mockGuid.ToByteArray()));
        }
    }
}
