using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using UnderTest.Strategy.Selenium;

namespace LeaderboardScraper.Pages
{
  public class CategoryPage : PageObjectBase
  {
    private readonly By LevelsLocation = By.XPath("//*[@id='content']/table/tbody/tr[2]/td[2]/table/tbody/tr");
    private readonly By SecondColumnLocation = By.XPath("//*[@id='content']/table/tbody/tr[2]/td[2]/table/tbody/tr[1]/th[2]/a");

    private IEnumerable<PageElement> Levels => this.FindPageElements(LevelsLocation);
    private PageElement SecondColumn => this.FindPageElement(SecondColumnLocation);

    public List<By> GetLevelLinkLocations()
    {
      var levels = new List<By>();
      var columnToUse = SecondColumn.Text == "Division" ? "2" : "1";

      for (var i = 1; i < Levels.ToList().Count; i++)
      {
        var tableNodeXpath = GetXpathForLevel(i.ToString(), columnToUse);
        if (!this.ElementExists(tableNodeXpath) || !this.FindPageElement(tableNodeXpath).GetAttribute("href").IsALevel(CurrentDriver.Url.GetCurrentSubLocation()))
        {
          continue;
        }
        levels.Add(tableNodeXpath);
      }

      return levels.Take(1).ToList();
    }

    private static By GetXpathForLevel(string rowIndex, string columnIndex) => By.XPath($"//*[@id='content']/table/tbody/tr[2]/td[2]/table/tbody/tr[{rowIndex}]/td[{columnIndex}]/a");
  }
}
