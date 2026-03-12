using System.Text.RegularExpressions;
using FluentValidation;

namespace Validation.Defaults.Rules;

/// <summary>
/// Validation extensions for hex color strings.
/// </summary>
public static partial class HexColorValidationExtensions
{
  [GeneratedRegex(@"^#(?:[0-9a-fA-F]{3}|[0-9a-fA-F]{6}|[0-9a-fA-F]{8})$")]
  private static partial Regex HexColorRegex();

  /// <summary>
  /// Validates that a string is a valid hex color (#RGB, #RRGGBB, or #RRGGBBAA).
  /// Null/empty values are allowed (use .NotEmpty() to require a value).
  /// </summary>
  public static IRuleBuilderOptions<T, string?> ValidHexColor<T>(this IRuleBuilder<T, string?> ruleBuilder)
  {
    return ruleBuilder
      .Must(value => string.IsNullOrEmpty(value) || HexColorRegex().IsMatch(value))
      .WithMessage("{PropertyName} must be a valid hex color (#RGB, #RRGGBB, or #RRGGBBAA).");
  }
}
