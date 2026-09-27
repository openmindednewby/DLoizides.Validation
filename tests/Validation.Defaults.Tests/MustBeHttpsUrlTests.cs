using FluentValidation;
using Shouldly;
using Validation.Defaults.Rules;

namespace Validation.Defaults.Tests;

public class MustBeHttpsUrlTests
{
  private sealed record Link(string? Url);

  private sealed class LinkValidator : AbstractValidator<Link>
  {
    public LinkValidator() => RuleFor(x => x.Url).MustBeHttpsUrl();
  }

  private readonly LinkValidator _validator = new();

  [Theory]
  [InlineData("https://example.com")]
  [InlineData("https://example.com/path?q=1#frag")]
  [InlineData("HTTPS://EXAMPLE.COM/photo.jpg")]
  [InlineData("https://cdn.example.com:8443/a/b.png")]
  public void MustBeHttpsUrl_WhenAbsoluteHttps_IsValid(string url)
  {
    _validator.Validate(new Link(url)).IsValid.ShouldBeTrue();
  }

  [Theory]
  [InlineData(null)]
  [InlineData("")]
  public void MustBeHttpsUrl_WhenNullOrEmpty_IsValid(string? url)
  {
    _validator.Validate(new Link(url)).IsValid.ShouldBeTrue();
  }

  [Theory]
  [InlineData("http://example.com")]
  [InlineData("javascript:alert(1)")]
  [InlineData("JavaScript:alert(1)")]
  [InlineData("data:text/html;base64,PHNjcmlwdD4=")]
  [InlineData("data:image/png;base64,iVBORw0KGgo=")]
  [InlineData("//example.com/photo.jpg")]
  [InlineData("/relative/photo.jpg")]
  [InlineData("example.com")]
  [InlineData("ftp://example.com/file")]
  [InlineData("file:///etc/passwd")]
  [InlineData("https://")]
  [InlineData(" https://example.com")]
  [InlineData("https://example.com/a b")]
  [InlineData("https://exa\tmple.com")]
  [InlineData("   ")]
  public void MustBeHttpsUrl_WhenNotAbsoluteHttps_IsInvalid(string url)
  {
    var result = _validator.Validate(new Link(url));

    result.IsValid.ShouldBeFalse();
    result.Errors.ShouldHaveSingleItem().ErrorMessage.ShouldContain("absolute HTTPS URL");
  }
}
