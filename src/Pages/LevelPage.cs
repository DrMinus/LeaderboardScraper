using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using UnderTest.Strategy.Selenium;

namespace LeaderboardScraper.Pages
{
  public class LevelPage : PageObjectBase
  {
    private By RecordLinkLocation => By.CssSelector($"a[href^='/members/'][href$='{CurrentDriver.Url.GetCurrentSubLocation()}']");

    public List<By> GetRecordLinksLocations()
    {
      return this.FindPageElements(RecordLinkLocation)
        .Select(x => x.GetXPathFromElement(this)).Take(1).ToList();
    }
  }
}
