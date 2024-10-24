namespace Application.Services.Filter;


public static class PrimitiveHelper
{
  public static readonly Type[] PrimitiveTypes =
  [
    typeof(string),
    typeof(int),
    typeof(decimal),
    typeof(double),
    typeof(float),
    typeof(long),
    typeof(short),
    typeof(byte),
    typeof(bool)
  ];

  // Nombres comunes de propiedades que encapsulan primitivos en Value Objects
  public static readonly string[] EncapsulatedValuePropertyNames =
  [
    "Value", // Por ejemplo, PermissionId.Value o Price.Value
    "Amount" // Por ejemplo, Price.Amount
  ];

  // Verifica si el tipo es primitivo o encapsula un primitivo en un Value Object
  public static bool IsPrimitiveOrEncapsulatedPrimitive(Type type)
  {
    if (PrimitiveTypes.Contains(type))
    {
      return true;
    }

    var encapsulatedProperty = EncapsulatedValuePropertyNames
      .Select(name => type.GetProperty(name))
      .FirstOrDefault(p => p != null && PrimitiveTypes.Contains(p.PropertyType));

    return encapsulatedProperty != null;
  }
}
