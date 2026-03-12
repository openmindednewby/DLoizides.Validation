namespace Validation.Defaults.Constants;

/// <summary>
/// Standard field length limits shared across all services.
/// </summary>
public static class ValidationLimits
{
  public const int MaxNameLength = 200;
  public const int MaxDescriptionLength = 2000;
  public const int MaxUrlLength = 2048;
  public const int MaxEmailLength = 254;
  public const int MaxPhoneLength = 20;
  public const int MaxColorLength = 9; // #RRGGBBAA
  public const int MaxPageSize = 100;
  public const int DefaultPageSize = 20;
  public const int MaxPasswordLength = 128;
  public const int MinPasswordLength = 8;
}
