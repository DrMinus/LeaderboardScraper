using LeaderboardScraper.Models;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using UnderTest.Strategy.Selenium;

namespace LeaderboardScraper.Pages
{
  public class RecordPage : PageObjectBase
  {
    private const int ROW_OFFSET = 2;
    private const string TIME_CELL = "/td[1]";
    private const string DATE_CELL = "/td[3]";
    private const string COMMENT_CELL = "/td[4]";
    private const int USERNAME = 1;
    private const int GAME = 2;
    private const int CATEGORY = 3;
    private const int LEVEL = 4;
    private const int CHARACTER = 5;

    private readonly By RecordLocator = By.XPath("//*[@id='content']/table/tbody/tr[2]/td[2]/table/tbody/tr");

    public IEnumerable<Record> GetRecords()
    {
      var recordRows = this.FindPageElements(RecordLocator).Count;
      var records = new List<Record>();
      var urlParts = GetUrlParts();

      for (var i = ROW_OFFSET; i < recordRows; i++)
      {
        var selectorPrefix = $"//*[@id='content']/table/tbody/tr[2]/td[2]/table/tbody/tr[{i}]";
        var value = this.FindPageElement(By.XPath($"{selectorPrefix}{TIME_CELL}")).Text;
        if (string.IsNullOrEmpty(value))
        {
          continue;
        }
        records.Add(new Record
        {
          Category = urlParts[CATEGORY],
          Division = urlParts[CHARACTER],
          Comment = this.FindPageElement(By.XPath($"{selectorPrefix}{COMMENT_CELL}")).Text,
          Game = urlParts[GAME],
          Date = this.FindPageElement(By.XPath($"{selectorPrefix}{DATE_CELL}")).Text,
          IsMostRecent = false,
          Level = urlParts[LEVEL],
          Value = value,
          Username = urlParts[USERNAME]
        });
      }

      records[^1].IsMostRecent = true;
      return records;
    }

    private List<string> GetUrlParts() => CurrentDriver.Url.Split("/").Skip(3).ToList();
  }
}
