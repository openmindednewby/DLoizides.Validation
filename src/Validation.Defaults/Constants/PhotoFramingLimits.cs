namespace Validation.Defaults.Constants;

/// <summary>
/// Bounds for a photo's framing inside a fixed card: a translate offset (percent of the card,
/// either direction) plus a scale factor. Renderers clamp to these same numbers, so a value the
/// validator accepts is never silently altered on the page.
/// </summary>
public static class PhotoFramingLimits
{
  /// <summary>Widest horizontal or vertical nudge, as a percentage of the card.</summary>
  public const double MaxOffsetPercent = 60;

  /// <summary>Widest nudge in the negative direction.</summary>
  public const double MinOffsetPercent = -MaxOffsetPercent;

  /// <summary>Smallest scale: below 1 the photo shrinks inside the frame.</summary>
  public const double MinScale = 0.5;

  /// <summary>Largest scale: far above 2 the subject leaves the card.</summary>
  public const double MaxScale = 3;
}
