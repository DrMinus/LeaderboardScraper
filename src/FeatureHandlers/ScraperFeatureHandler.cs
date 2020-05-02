using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using CsvHelper;
using LeaderboardScraper.Models;
using LeaderboardScraper.Sites;
using OpenQA.Selenium;
using UnderTest.Attributes;
using UnderTest.Strategy.Selenium;

namespace LeaderboardScraper.FeatureHandlers
{
  [ExcludeFromCodeCoverage]
  [HandlesFeature("SimpleFeature.feature")]
  public class ScraperFeatureHandler : SeleniumFeatureHandler
  {
    private const string FILE_NAME = "records.csv";

    [PropertyInjected]
    private SonicCenterSite SonicCenterSite { get; set; }

    [Given(@"I scrape TSC")]
    public void GivenIScrapeTsc()
    {
      Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
      File.Create(FILE_NAME).Close();
      WriteCsvHeader();

      SonicCenterSite
        .GoToRankingsPage()
        .GetGames().ForEach(gameLocator =>
        {
          OpenPage(gameLocator, SonicCenterSite.GamePage)
            .GetCategoryPageLocations()
            .ForEach(categoryLocator =>
            {
              OpenPage(categoryLocator, SonicCenterSite.CategoryPage)
                .GetLevelLinkLocations().ForEach(levelLocator =>
                {
                  OpenPage(levelLocator, SonicCenterSite.LevelPage)
                    .GetRecordLinksLocations().ForEach(recordLocator =>
                    {
                      OpenPage(recordLocator, SonicCenterSite.RecordPage)
                        .GetRecords()
                        .WriteRecordsToCsv(FILE_NAME);
                      CurrentDriver.Navigate().Back();
                    });
                  CurrentDriver.Navigate().Back();
                });
              CurrentDriver.Navigate().Back();
            });
          CurrentDriver.Navigate().Back();
        });
    }

    [When(@"a bruh moment happens")]
    [Then(@"this is a bruh moment")]
    public void WhenABruhMomentHappens()
    {
    }

    private T OpenPage<T>(By linkLocation, T page)
      where T: PageObjectBase
    {
      this.FindPageElement(linkLocation).WaitUntilClickable().Click();
      return page;
    }

    private static void WriteCsvHeader()
    {
      using (var writer = new StreamWriter(FILE_NAME, true))
      {
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
          csv.WriteHeader(typeof(Record));
        }
      }
      using (var writer = new StreamWriter(FILE_NAME, true))
      {
        writer.WriteLine();
      }
    }
  }
}
