using Shouldly;
using Validation.Defaults.Abstractions;
using Validation.Defaults.Constants;
using Validation.Defaults.Validators;

namespace Validation.Defaults.Tests;

public class PhotoFramingValidatorTests
{
  private sealed record Framing(double? X, double? Y, double? Scale) : IPhotoFraming;

  private readonly PhotoFramingValidator<Framing> _validator = new();

  [Fact]
  public void Limits_MatchRendererClampBounds()
  {
    PhotoFramingLimits.MaxOffsetPercent.ShouldBe(60);
    PhotoFramingLimits.MinOffsetPercent.ShouldBe(-60);
    PhotoFramingLimits.MinScale.ShouldBe(0.5);
    PhotoFramingLimits.MaxScale.ShouldBe(3);
  }

  [Theory]
  [InlineData(0.0, 0.0, 1.0)]
  [InlineData(-60.0, -60.0, 0.5)]
  [InlineData(60.0, 60.0, 3.0)]
  [InlineData(12.345, -7.5, 1.75)]
  [InlineData(null, null, null)]
  public void Validate_WhenWithinBounds_IsValid(double? x, double? y, double? scale)
  {
    _validator.Validate(new Framing(x, y, scale)).IsValid.ShouldBeTrue();
  }

  [Theory]
  [InlineData(60.001, 0.0, 1.0, nameof(Framing.X))]
  [InlineData(-60.001, 0.0, 1.0, nameof(Framing.X))]
  [InlineData(0.0, 60.001, 1.0, nameof(Framing.Y))]
  [InlineData(0.0, -60.001, 1.0, nameof(Framing.Y))]
  [InlineData(0.0, 0.0, 0.499, nameof(Framing.Scale))]
  [InlineData(0.0, 0.0, 3.001, nameof(Framing.Scale))]
  [InlineData(0.0, 0.0, 0.0, nameof(Framing.Scale))]
  [InlineData(0.0, 0.0, -1.0, nameof(Framing.Scale))]
  [InlineData(double.NaN, 0.0, 1.0, nameof(Framing.X))]
  [InlineData(0.0, double.PositiveInfinity, 1.0, nameof(Framing.Y))]
  [InlineData(0.0, 0.0, double.NegativeInfinity, nameof(Framing.Scale))]
  public void Validate_WhenOutOfBoundsOrNonFinite_FailsOnThatField(double? x, double? y, double? scale, string field)
  {
    var result = _validator.Validate(new Framing(x, y, scale));

    result.IsValid.ShouldBeFalse();
    result.Errors.ShouldHaveSingleItem().PropertyName.ShouldBe(field);
  }

  [Fact]
  public void Validate_WhenAllFieldsOutOfBounds_ReportsEachField()
  {
    var result = _validator.Validate(new Framing(100, -100, 10));

    result.Errors.Select(e => e.PropertyName)
      .ShouldBe([nameof(Framing.X), nameof(Framing.Y), nameof(Framing.Scale)], ignoreOrder: true);
  }
}
