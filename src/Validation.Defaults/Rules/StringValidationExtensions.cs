using System.Text.RegularExpressions;
using FluentValidation;

namespace Validation.Defaults.Rules;

/// <summary>
/// Validation extensions for string fields.
/// </summary>
public static partial class StringValidationExtensions
{
  [GeneratedRegex(@"^https?://\S+$", RegexOptions.IgnoreCase)]
  private static partial Regex UrlRegex();

  [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
  private static partial Regex EmailRegex();

  /// <summary>
  /// Validates that a string is a well-formed HTTP or HTTPS URL.
  /// Null/empty values are allowed (use .NotEmpty() to require a value).
  /// </summary>
  public static IRuleBuilderOptions<T, string?> ValidUrl<T>(this IRuleBuilder<T, string?> ruleBuilder)
  {
    return ruleBuilder
      .Must(value => string.IsNullOrEmpty(value) || UrlRegex().IsMatch(value))
      .WithMessage("{PropertyName} must be a valid HTTP or HTTPS URL.");
  }

  /// <summary>
  /// Validates that a string is a valid email address format.
  /// Null/empty values are allowed (use .NotEmpty() to require a value).
  /// </summary>
  public static IRuleBuilderOptions<T, string?> ValidEmail<T>(this IRuleBuilder<T, string?> ruleBuilder)
  {
    return ruleBuilder
      .Must(value => string.IsNullOrEmpty(value) || EmailRegex().IsMatch(value))
      .WithMessage("{PropertyName} must be a valid email address.");
  }
}
