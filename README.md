# Validation.Defaults

Shared FluentValidation rules and constants for FastEndpoints services.

## Installation

```bash
dotnet add package DLoizides.Validation
```

## Usage

### Validation Extensions

```csharp
using FluentValidation;
using Validation.Defaults.Rules;

public class CreateMenuValidator : AbstractValidator<CreateMenuRequest>
{
    public CreateMenuValidator()
    {
        RuleFor(x => x.Id).NotEmptyGuid();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(ValidationLimits.MaxNameLength);
        RuleFor(x => x.Color).ValidHexColor();
        RuleFor(x => x.Url).ValidUrl();
        RuleFor(x => x.Page).ValidPageNumber();
        RuleFor(x => x.PageSize).ValidPageSize();
    }
}
```

### Available Extensions

| Extension | Description |
|-----------|-------------|
| `.NotEmptyGuid()` | Rejects `Guid.Empty` |
| `.ValidHexColor()` | Accepts `#RGB`, `#RRGGBB`, `#RRGGBBAA` |
| `.ValidUrl()` | Validates well-formed HTTP/HTTPS URLs |
| `.MustBeHttpsUrl()` | Absolute `https://` URL with a host only; rejects `http:`, `javascript:`, `data:`, relative and protocol-relative URLs, whitespace |
| `.ValidPhotoOffset()` | `double?` within -60..60 (percent), finite; null allowed |
| `.ValidPhotoScale()` | `double?` within 0.5..3, finite; null allowed |
| `.ValidEmail()` | Validates email format |
| `.ValidPageNumber()` | Page >= 1 |
| `.ValidPageSize()` | 1 <= pageSize <= 100 |
| `.ValidSkip()` | skip >= 0 |

### Constants

```csharp
using Validation.Defaults.Constants;

// Field length limits
ValidationLimits.MaxNameLength        // 200
ValidationLimits.MaxDescriptionLength // 2000
ValidationLimits.MaxUrlLength         // 2048
ValidationLimits.MaxEmailLength       // 254
ValidationLimits.MaxPhoneLength       // 20
ValidationLimits.MaxColorLength       // 9 (#RRGGBBAA)
ValidationLimits.MaxPageSize          // 100
```

### Photo framing

```csharp
using Validation.Defaults.Abstractions;
using Validation.Defaults.Validators;

public sealed record Framing(double? X, double? Y, double? Scale) : IPhotoFraming;

RuleFor(x => x.Framing!).SetValidator(new PhotoFramingValidator<Framing>())
    .When(x => x.Framing is not null);
```

Bounds are public consts in `PhotoFramingLimits` (`MinOffsetPercent` -60, `MaxOffsetPercent` 60,
`MinScale` 0.5, `MaxScale` 3). Keep them equal to the renderer's clamp so an accepted value is
never silently altered on the page.
