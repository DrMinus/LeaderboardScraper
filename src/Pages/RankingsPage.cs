using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using UnderTest.Strategy.Selenium;

namespace LeaderboardScraper.Pages
{
  public class RankingsPage : PageObjectBase
  {
    private IEnumerable<PageElement> Games => this.FindPageElements(GameLocation);

    private readonly By GameLocation = By.CssSelector("a[href^='/rankings/']");

    public List<By> GetGames()
    {
      return Games.Where(x => x.GetAttribute("href").IsAGame())
        .Select(x => x.GetXPathFromElement(this)).Take(1).ToList();
    }
  }
}
