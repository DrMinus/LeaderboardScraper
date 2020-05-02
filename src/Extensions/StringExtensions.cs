using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace System
{
  public static class StringExtensions
  {
    public static bool IsACategory(this string instance, string currentLocation)
    {
      return !instance.EndsWith("/overall")
             && !instance.Contains("/champions")
             && !instance.Contains("/sitewide")
             && !instance.Contains("/new")
             && !instance.Contains("/matchup")
             && !instance.Contains("|order")
             && !instance.Contains("|mode")
             && !instance.Contains("|skin")
             && !instance.Equals(currentLocation);
    }

    public static bool IsAGame(this string instance)
    {
      return !instance.Contains("/champions")
             && !instance.Contains("/sitewide")
             && !instance.Contains("/new")
             && !instance.Contains("/matchup");
    }

    public static bool IsALevel(this string instance, string currentLocation)
    {
      return !instance.Contains($"{currentLocation}/total")
             && !instance.Contains($"{currentLocation}/overall");
    }

    public static string GetCurrentSiteLocation(this string instance)
    {
      const int SITE_ROOT_END = 3;
      return GetLocationOnSiteFromPart(instance, SITE_ROOT_END);
    }

    public static string GetCurrentSubLocation(this string instance)
    {
      const int SITE_ROOT_END = 4;
      return GetLocationOnSiteFromPart(instance, SITE_ROOT_END);
    }

    private static string GetLocationOnSiteFromPart(string url, int indexToStartFrom)
    {
      var urlSplit = url.Split("/");
      var currentLocation = new List<string>();
      for (var i = indexToStartFrom; i < urlSplit.Length; i++)
      {
        currentLocation.Add(urlSplit[i]);
      }
      return string.Join('/', currentLocation);
    }
  }
}
