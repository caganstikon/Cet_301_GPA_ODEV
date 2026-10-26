namespace MezuniyetKontrol.Models;

public class Ders
{
    public string Ad { get; set; } = "";
    public int Kredi { get; set; }
    public string Not { get; set; } = "";

    public double NotPuani()
    {
        switch (Not)
        {
            case "AA": return 4.0;
            case "BA": return 3.5;
            case "BB": return 3.0;
            case "CB": return 2.5;
            case "CC": return 2.0;
            case "DC": return 1.5;
            case "DD": return 1.0;
            case "FD": return 0.5;
            default: return 0.0;
        }
    }

    public bool KaldiMi()
    {
        return Not == "FD" || Not == "FF";
    }
}
