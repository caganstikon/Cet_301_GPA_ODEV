using MezuniyetKontrol.Models;

namespace MezuniyetKontrol;

public partial class MainPage : ContentPage
{
    List<Entry> dersAdlari = new List<Entry>();
    List<Entry> krediler = new List<Entry>();
    List<Picker> notlar = new List<Picker>();
    string[] harfNotlari = { "AA", "BA", "BB", "CB", "CC", "DC", "DD", "FD", "FF" };

    public MainPage()
    {
        InitializeComponent();
    }

    private void SeviyeDegisti(object? sender, EventArgs e)
    {
        if (seviyePicker.SelectedIndex == 1)
            tezAlani.IsVisible = true;
        else
            tezAlani.IsVisible = false;
    }

    private async void DersleriOlustur(object? sender, EventArgs e)
    {
        int sayi;
        if (!int.TryParse(dersSayisiEntry.Text, out sayi) || sayi <= 0)
        {
            await DisplayAlertAsync("Hata", "Geçerli bir ders sayısı girin.", "Tamam");
            return;
        }

        derslerLayout.Children.Clear();
        dersAdlari.Clear();
        krediler.Clear();
        notlar.Clear();

        for (int i = 1; i <= sayi; i++)
        {
            Label baslik = new Label { Text = i + ". Ders", FontAttributes = FontAttributes.Bold };
            Entry ad = new Entry { Placeholder = "Ders Adı" };
            Entry kredi = new Entry { Placeholder = "Ders Kredisi", Keyboard = Keyboard.Numeric };
            Picker not = new Picker { Title = "Harf Notu" };
            foreach (string h in harfNotlari)
                not.Items.Add(h);

            derslerLayout.Children.Add(baslik);
            derslerLayout.Children.Add(ad);
            derslerLayout.Children.Add(kredi);
            derslerLayout.Children.Add(not);

            dersAdlari.Add(ad);
            krediler.Add(kredi);
            notlar.Add(not);
        }

        hesaplaButton.IsVisible = true;
    }

    private async void Hesapla(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(adEntry.Text) || string.IsNullOrWhiteSpace(soyadEntry.Text) || string.IsNullOrWhiteSpace(numaraEntry.Text))
        {
            await DisplayAlertAsync("Hata", "Ad, soyad ve numara boş olamaz.", "Tamam");
            return;
        }
        if (seviyePicker.SelectedIndex == -1)
        {
            await DisplayAlertAsync("Hata", "Eğitim seviyesini seçin.", "Tamam");
            return;
        }
        if (seviyePicker.SelectedIndex == 1 && tezPicker.SelectedIndex == -1)
        {
            await DisplayAlertAsync("Hata", "Tez durumunu seçin.", "Tamam");
            return;
        }

        Ogrenci ogrenci = new Ogrenci();
        ogrenci.Ad = adEntry.Text;
        ogrenci.Soyad = soyadEntry.Text;
        ogrenci.Numara = numaraEntry.Text;
        ogrenci.Seviye = seviyePicker.SelectedItem.ToString()!;
        ogrenci.TezTamamlandi = tezPicker.SelectedIndex == 0;

        for (int i = 0; i < dersAdlari.Count; i++)
        {
            int kredi;
            if (string.IsNullOrWhiteSpace(dersAdlari[i].Text) || !int.TryParse(krediler[i].Text, out kredi) || kredi <= 0 || notlar[i].SelectedIndex == -1)
            {
                await DisplayAlertAsync("Hata", (i + 1) + ". dersin bilgileri eksik veya hatalı.", "Tamam");
                return;
            }

            Ders ders = new Ders();
            ders.Ad = dersAdlari[i].Text;
            ders.Kredi = kredi;
            ders.Not = notlar[i].SelectedItem.ToString()!;
            ogrenci.Dersler.Add(ders);
        }

        await Navigation.PushAsync(new OzetSayfasi(ogrenci));
    }
}
