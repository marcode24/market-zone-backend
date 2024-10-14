using Domain.Shared.Extensions;

namespace Domain.Shared.ValueObjects;

public abstract record BaseValueObject
{
  public string Value { get; }

  protected BaseValueObject(string value)
  {
    Value = NormalizeValue(value);
  }

  protected static string NormalizeValue(string value)
  {
    return value.ToNormalize();
  }
}
