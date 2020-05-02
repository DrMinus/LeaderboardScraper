using OpenQA.Selenium.Chrome;
using System;
using System.Diagnostics.CodeAnalysis;
using UnderTest;
using UnderTest.Strategy.Selenium;

namespace LeaderboardScraper
{
  [ExcludeFromCodeCoverage]
  internal class Program
  {
    private static int Main(string[] args)
    {
      var programAssembly = typeof(Program).Assembly;
      var chromeOptions = new ChromeOptions();
      chromeOptions.AddArgument("--headless");

      return new UnderTestRunner()
        .WithCommandLineArgs(args)
        .WithProjectDetails(x => x
          .SetProjectName("Leaderboard Scraper").
          SetProjectVersion("0.1.0"))
        .WithTestSettings(settings => settings
          .AddAssembly(programAssembly)
          .AddSeleniumStrategy(strategy => strategy
          .WithFeatureHandlersFrom(programAssembly)
          .SetDriverCreationFunc(() => new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory, chromeOptions))
          .SetDriverLifeCycle(DriverLifecycle.EntireRun)
          .WithWebsiteComponentsFrom(programAssembly))
          .AttachBehaviors()
          .FinishAttaching())
        .Execute()
          .ToExitCode();
    }
  }
}
