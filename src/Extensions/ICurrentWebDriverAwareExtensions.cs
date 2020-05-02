using OpenQA.Selenium;

// ReSharper disable once CheckNamespace
namespace UnderTest.Strategy.Selenium
{
  // ReSharper disable once InconsistentNaming
  public static class ICurrentWebDriverAwareExtensions
  {
    public static bool ElementExists(this ICurrentWebDriverAware instance, By selector)
    {
      try
      {
        var enabled = instance.FindPageElement(selector).Enabled;
        return enabled;
      }
      catch (NoSuchElementException)
      {
        return false;
      }
    }
  }
}
