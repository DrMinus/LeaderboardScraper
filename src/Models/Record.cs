namespace LeaderboardScraper.Models
{
  public class Record
  {
    public string Category { get; set; }
    public string Division { get; set; }
    public string Comment { get; set; }
    public string Date { get; set; }
    public string Game { get; set; }
    public bool IsMostRecent { get; set; }
    public string Level { get; set; }
    public string Value { get; set; }
    public string Username { get; set; }
  }
}
