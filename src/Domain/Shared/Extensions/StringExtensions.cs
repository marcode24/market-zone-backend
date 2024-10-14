using System.Text.RegularExpressions;

namespace Domain.Shared.Extensions;

public static partial class StringExtensions
{
  public static string ToNormalize(this string value)
  {
    if (string.IsNullOrWhiteSpace(value))
      return string.Empty;

    return MyRegex().Replace(value.Trim(), " ");
  }

  [GeneratedRegex(@"\s+")]
  private static partial Regex MyRegex();
}
