namespace DevsuTest.Application.Validators
{
    public static class BaseValidationMessages
    {
        public static string ValorExistente(string field)
        {
            return $"Ya existe un {field} con el valor ingresado.";
        }

        public static string EntidadNoEncontrada(string field)
        {
            return $"No se encontró {field} con el Id ingresado.";
        }
    }
}
