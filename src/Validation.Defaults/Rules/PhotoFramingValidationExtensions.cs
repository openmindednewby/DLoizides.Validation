using FluentValidation;
using Validation.Defaults.Constants;

namespace Validation.Defaults.Rules;

/// <summary>
/// Rule extensions for photo framing components. Null is allowed (the renderer's default applies);
/// NaN and infinities are rejected.
/// </summary>
public static class PhotoFramingValidationExtensions
{
  /// <summary>
  /// Validates a framing offset lies within <see cref="PhotoFramingLimits.MinOffsetPercent"/> and
  /// <see cref="PhotoFramingLimits.MaxOffsetPercent"/> inclusive.
  /// </summary>
  public static IRuleBuilderOptions<T, double?> ValidPhotoOffset<T>(this IRuleBuilder<T, double?> ruleBuilder)
  {
    return ruleBuilder
      .Must(value => IsWithin(value, PhotoFramingLimits.MinOffsetPercent, PhotoFramingLimits.MaxOffsetPercent))
      .WithMessage($"{{PropertyName}} must be between {PhotoFramingLimits.MinOffsetPercent} and {PhotoFramingLimits.MaxOffsetPercent}.");
  }

  /// <summary>
  /// Validates a framing scale lies within <see cref="PhotoFramingLimits.MinScale"/> and
  /// <see cref="PhotoFramingLimits.MaxScale"/> inclusive.
  /// </summary>
  public static IRuleBuilderOptions<T, double?> ValidPhotoScale<T>(this IRuleBuilder<T, double?> ruleBuilder)
  {
    return ruleBuilder
      .Must(value => IsWithin(value, PhotoFramingLimits.MinScale, PhotoFramingLimits.MaxScale))
      .WithMessage($"{{PropertyName}} must be between {PhotoFramingLimits.MinScale} and {PhotoFramingLimits.MaxScale}.");
  }

  private static bool IsWithin(double? value, double min, double max)
  {
    if (value is null)
      return true;

    var v = value.Value;
    return double.IsFinite(v) && v >= min && v <= max;
  }
}
