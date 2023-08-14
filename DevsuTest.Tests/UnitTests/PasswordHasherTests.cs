using DevsuTest.Core.Security;

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
