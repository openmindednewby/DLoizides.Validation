namespace Validation.Defaults.Abstractions;

/// <summary>
/// A photo's framing inside a card. A null component means "use the default"
/// (offset 0, scale 1).
/// </summary>
public interface IPhotoFraming
{
  /// <summary>Horizontal offset, percent of the card.</summary>
  double? X { get; }

  /// <summary>Vertical offset, percent of the card.</summary>
  double? Y { get; }

  /// <summary>Scale factor.</summary>
  double? Scale { get; }
}
