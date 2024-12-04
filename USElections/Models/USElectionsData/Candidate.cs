namespace USElections.Models.USElectionsData;

public class Candidate
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Party { get; set; }
    public string TermStart { get; set; }
    public string TermEnd { get; set; }
    public string Image { get; set; }
    public string ImageFull { get; set; }
}
