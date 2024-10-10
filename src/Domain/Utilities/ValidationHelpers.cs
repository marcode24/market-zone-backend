using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace Domain.Utilities;

public static class ValidationHelpers
{
  public static string GetDisplayName<T>(Expression<Func<T, object>> expression)
  {
    var member = expression.Body as MemberExpression
      ?? ((UnaryExpression)expression.Body).Operand as MemberExpression;

    if (member != null)
    {
      var attribute = member.Member.GetCustomAttribute<DisplayNameAttribute>();
      return attribute?.DisplayName ?? member.Member.Name;
    }

    throw new ArgumentException("Invalid expression", nameof(expression));
  }

  private static TAttribute GetCustomAttribute<TAttribute>(this MemberInfo member) where TAttribute : Attribute
  {
    {
      return (TAttribute)member.GetCustomAttributes(typeof(TAttribute), false).FirstOrDefault()!;
    }
  }
}
