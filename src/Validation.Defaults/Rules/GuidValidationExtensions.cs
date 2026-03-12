using FluentValidation;

namespace Validation.Defaults.Rules;

/// <summary>
/// Validation extensions for GUID fields.
/// </summary>
public static class GuidValidationExtensions
{
  /// <summary>
  /// Validates that a GUID is not <see cref="Guid.Empty"/>.
  /// </summary>
  public static IRuleBuilderOptions<T, Guid> NotEmptyGuid<T>(this IRuleBuilder<T, Guid> ruleBuilder)
  {
    return ruleBuilder
      .Must(g => g != Guid.Empty)
      .WithMessage("{PropertyName} must not be empty.");
  }

  /// <summary>
  /// Validates that a nullable GUID, when present, is not <see cref="Guid.Empty"/>.
  /// </summary>
  public static IRuleBuilderOptions<T, Guid?> NotEmptyGuid<T>(this IRuleBuilder<T, Guid?> ruleBuilder)
  {
    return ruleBuilder
      .Must(g => !g.HasValue || g.Value != Guid.Empty)
      .WithMessage("{PropertyName} must not be empty when provided.");
  }
}
