namespace MezuniyetKontrol.Models;

public class Ogrenci
{
    public string Ad { get; set; } = "";
    public string Soyad { get; set; } = "";
    public string Numara { get; set; } = "";
    public string Seviye { get; set; } = "";
    public bool TezTamamlandi { get; set; }
    public List<Ders> Dersler { get; set; } = new List<Ders>();
}
