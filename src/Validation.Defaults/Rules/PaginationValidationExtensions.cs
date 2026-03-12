using FluentValidation;
using Validation.Defaults.Constants;

namespace Validation.Defaults.Rules;

/// <summary>
/// Validation extensions for pagination parameters.
/// </summary>
public static class PaginationValidationExtensions
{
  /// <summary>
  /// Validates that a page number is >= 1.
  /// </summary>
  public static IRuleBuilderOptions<T, int> ValidPageNumber<T>(this IRuleBuilder<T, int> ruleBuilder)
  {
    return ruleBuilder
      .GreaterThanOrEqualTo(1)
      .WithMessage("{PropertyName} must be at least 1.");
  }

  /// <summary>
  /// Validates that a page size is between 1 and <see cref="ValidationLimits.MaxPageSize"/>.
  /// </summary>
  public static IRuleBuilderOptions<T, int> ValidPageSize<T>(this IRuleBuilder<T, int> ruleBuilder)
  {
    return ruleBuilder
      .InclusiveBetween(1, ValidationLimits.MaxPageSize)
      .WithMessage($"{{PropertyName}} must be between 1 and {ValidationLimits.MaxPageSize}.");
  }

  /// <summary>
  /// Validates that a skip value is >= 0.
  /// </summary>
  public static IRuleBuilderOptions<T, int> ValidSkip<T>(this IRuleBuilder<T, int> ruleBuilder)
  {
    return ruleBuilder
      .GreaterThanOrEqualTo(0)
      .WithMessage("{PropertyName} must be at least 0.");
  }
}
