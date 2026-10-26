using MezuniyetKontrol.Models;

namespace MezuniyetKontrol;

public partial class OzetSayfasi : ContentPage
{
    public OzetSayfasi(Ogrenci ogrenci)
    {
        InitializeComponent();

        adLabel.Text = "Ad Soyad: " + ogrenci.Ad + " " + ogrenci.Soyad;
        numaraLabel.Text = "Öğrenci No: " + ogrenci.Numara;
        seviyeLabel.Text = "Eğitim Seviyesi: " + ogrenci.Seviye;

        if (ogrenci.Seviye == "Yüksek Lisans")
        {
            if (ogrenci.TezTamamlandi)
                tezLabel.Text = "Tez Durumu: Tamamlandı";
            else
                tezLabel.Text = "Tez Durumu: Tamamlanmadı";
        }
        else
        {
            tezLabel.IsVisible = false;
        }

        int toplamKredi = 0;
        int tamamlananKredi = 0;
        double toplamPuan = 0;
        bool fVar = false;

        foreach (Ders d in ogrenci.Dersler)
        {
            derslerLayout.Children.Add(new Label { Text = d.Ad + " - " + d.Kredi + " kredi - " + d.Not });

            toplamKredi += d.Kredi;
            toplamPuan += d.Kredi * d.NotPuani();

            if (d.KaldiMi())
                fVar = true;
            else
                tamamlananKredi += d.Kredi;
        }

        double gpa = 0;
        if (toplamKredi > 0)
            gpa = toplamPuan / toplamKredi;

        krediLabel.Text = "Toplam Kredi: " + toplamKredi;
        gpaLabel.Text = "GPA: " + gpa.ToString("0.00");

        string nedenler = "";

        if (ogrenci.Seviye == "Lisans")
        {
            if (gpa < 2.00)
                nedenler += "- GPA 2.00'dan düşük\n";
            if (tamamlananKredi < 10)
                nedenler += "- En az 10 kredi tamamlanmalı\n";
        }
        else
        {
            if (!ogrenci.TezTamamlandi)
                nedenler += "- Tez çalışması tamamlanmamış\n";
            if (gpa < 3.00)
                nedenler += "- GPA 3.00'dan düşük\n";
            if (tamamlananKredi < 5)
                nedenler += "- En az 5 kredi tamamlanmalı\n";
        }

        if (fVar)
            nedenler += "- F notu olan ders var\n";

        if (nedenler == "")
        {
            sonucLabel.Text = "MEZUN OLABİLİR";
            sonucLabel.TextColor = Colors.Green;
        }
        else
        {
            sonucLabel.Text = "MEZUN OLAMAZ";
            sonucLabel.TextColor = Colors.Red;
            nedenLabel.Text = nedenler;
        }
    }

    private void YeniOgrenci(object? sender, EventArgs e)
    {
        Application.Current!.Windows[0].Page = new NavigationPage(new MainPage());
    }
}
