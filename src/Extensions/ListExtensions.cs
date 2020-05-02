using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using LeaderboardScraper.Models;

// ReSharper disable once CheckNamespace
namespace System.Collections.Generic
{
  public static class ListExtensions
  {
    public static void WriteRecordsToCsv(this IEnumerable<Record> instance, string fileName)
    {
      using (var writer = new StreamWriter(fileName, true))
      {
        using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.CurrentCulture)
          { HasHeaderRecord = false }))
        {
          csv.WriteRecords(instance);
        }
      }
    }
  }
}
