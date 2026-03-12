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
