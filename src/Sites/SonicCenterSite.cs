using LeaderboardScraper.Pages;
using UnderTest.Attributes;
using UnderTest.Strategy.Selenium;

namespace LeaderboardScraper.Sites
{
  public class SonicCenterSite : SiteBase
  {
    [PropertyInjected]
    public CategoryPage CategoryPage { get; set; }
    [PropertyInjected]
    public GamePage GamePage { get; set; }
    [PropertyInjected]
    public LevelPage LevelPage { get; set; }
    [PropertyInjected]
    public RankingsPage RankingsPage { get; set; }
    [PropertyInjected]
    public RecordPage RecordPage { get; set; }

    public RankingsPage GoToRankingsPage()
    {
      CurrentDriver.Navigate().GoToUrl("https://www.soniccenter.org/rankings");
      return RankingsPage;
    }
  }
}
