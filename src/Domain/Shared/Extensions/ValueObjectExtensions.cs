// using Domain.Abstractions;

// public static class ValueObjectExtensions
// {
//   public static T GetValue<T>(this object valueObject)
//   {
//     // Buscar la propiedad que almacena el valor primitivo
//     var valueProperty = valueObject
//         .GetType()
//         .GetProperties()
//         .FirstOrDefault(p => p.PropertyType == typeof(T))
//       ?? throw new InvalidOperationException($"No value property of type {typeof(T).Name} found.");

//     return (T)valueProperty.GetValue(valueObject)!;
//   }
// }
