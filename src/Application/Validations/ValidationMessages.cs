namespace Application.Validations;

public static class ValidationsMessages
{
  public static string FieldRequired(string field) => $"El campo {field} es requerido.";
  public static string MinLength(string field, int length) => $"El campo {field} debe tener al menos {length} caracteres.";
  public static string MaxLength(string field, int length) => $"El campo {field} debe tener como máximo {length} caracteres.";
  public static string EmailFormat(string field) => $"El campo {field} no tiene un formato válido de email.";
}
