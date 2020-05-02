using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using UnderTest.Strategy.Selenium;

namespace LeaderboardScraper.Pages
{
  public class GamePage : PageObjectBase
  {
    private readonly By CategoryLocation = By.CssSelector("a[href^='/rankings/sonic_1/']");

    private IEnumerable<PageElement> Categories => this.FindPageElements(CategoryLocation);

    public List<By> GetCategoryPageLocations()
    {
      return Categories
        .Where(x => x.GetAttribute("href").IsACategory(CurrentDriver.Url.GetCurrentSiteLocation()))
        .Select(x => x.GetXPathFromElement(this)).Take(1).ToList();
    }
  }
}
