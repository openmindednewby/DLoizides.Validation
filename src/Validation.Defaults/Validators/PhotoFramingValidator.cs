using FluentValidation;
using Validation.Defaults.Abstractions;
using Validation.Defaults.Rules;

namespace Validation.Defaults.Validators;

/// <summary>
/// Validates a photo framing object: X and Y within the offset bounds, Scale within the scale
/// bounds (see <see cref="Constants.PhotoFramingLimits"/>). Compose it with
/// <c>RuleFor(x =&gt; x.Framing).SetValidator(new PhotoFramingValidator&lt;MyFraming&gt;())</c>.
/// </summary>
public sealed class PhotoFramingValidator<T> : AbstractValidator<T>
  where T : IPhotoFraming
{
  public PhotoFramingValidator()
  {
    RuleFor(f => f.X).ValidPhotoOffset();
    RuleFor(f => f.Y).ValidPhotoOffset();
    RuleFor(f => f.Scale).ValidPhotoScale();
  }
}
