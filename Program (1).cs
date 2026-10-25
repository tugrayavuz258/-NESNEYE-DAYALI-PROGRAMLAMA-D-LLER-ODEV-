var db = new SqlVeritabani();
var depo = new RandevuRepository(db);

Console.WriteLine("==== BRAIN CONTROLLER (kötü) ====");
var beyinli = new BeyinliRandevuController(depo);
Console.WriteLine(beyinli.RandevuAl("Ayşe", "Dr. Kaya", new DateTime(2026, 10, 26, 10, 0, 0), 65));
Console.WriteLine(beyinli.RandevuAl("Ali", "Dr. Kaya", new DateTime(2026, 10, 26, 19, 0, 0), 30));

Console.WriteLine("==== BRAIN CONTROLLER düzeltilmiş ====");
var servis = new RandevuService(depo);
var sade = new RandevuController(servis);
Console.WriteLine(sade.RandevuAl("Mehmet", "Dr. Kaya", new DateTime(2026, 10, 26, 11, 0, 0), 30));
Console.WriteLine(sade.RandevuAl("Zeynep", "Dr. Kaya", new DateTime(2026, 10, 26, 11, 0, 0), 70));

Console.WriteLine("==== MEDDLING SERVICE (kötü) ====");
var karisan = new KarisanRaporService(db);
Console.WriteLine(karisan.GunlukOzet("Dr. Kaya"));
Console.WriteLine("==== MEDDLING SERVICE düzeltilmiş ====");
var temizRapor = new RaporService(depo);
Console.WriteLine(temizRapor.GunlukOzet("Dr. Kaya"));

// ================= ORTAK PARÇALAR =================
public class Randevu
{
    public string HastaAdi { get; set; } = "";
    public string Doktor { get; set; } = "";
    public DateTime Zaman { get; set; }
    public decimal Ucret { get; set; }
}

// "Kalıcılık API'si" (gerçekte SqlConnection / Entity Framework olurdu)
public class SqlVeritabani
{
    private readonly List<Randevu> _tablo = new();
    public void Calistir(string sql, Randevu r) { _tablo.Add(r); Console.WriteLine($"   [DB] {sql}"); }
    public List<Randevu> Sorgula(string sql, string doktor)
    {
        Console.WriteLine($"   [DB] {sql}");
        return _tablo.Where(x => x.Doktor == doktor).ToList();
    }
}

public class RandevuRepository
{
    private readonly SqlVeritabani _db;
    public RandevuRepository(SqlVeritabani db) => _db = db;
    public void Ekle(Randevu r) => _db.Calistir("INSERT INTO Randevular ...", r);
    public List<Randevu> DoktorunRandevulari(string doktor)
        => _db.Sorgula("SELECT * FROM Randevular WHERE Doktor = @doktor", doktor);
}

// ================= BRAIN CONTROLLER =================
public class BeyinliRandevuController
{
    private readonly RandevuRepository _depo;
    public BeyinliRandevuController(RandevuRepository depo) => _depo = depo;

    public string RandevuAl(string hasta, string doktor, DateTime zaman, int yas)
    {
        if (zaman.Hour < 9 || zaman.Hour >= 17)
            return "❌ Mesai saatleri dışında randevu verilmez.";
        if (zaman.DayOfWeek == DayOfWeek.Sunday)
            return "❌ Pazar günü randevu verilmez.";
        if (_depo.DoktorunRandevulari(doktor).Any(r => r.Zaman == zaman))
            return "❌ Bu saat dolu.";

        decimal ucret = 500;
        if (yas >= 65) ucret *= 0.5m;
        else if (yas < 18) ucret *= 0.7m;

        _depo.Ekle(new Randevu { HastaAdi = hasta, Doktor = doktor, Zaman = zaman, Ucret = ucret });
        return $"✅ {hasta}: randevu alındı, ücret {ucret} TL";
    }
}

public class RandevuService
{
    private readonly RandevuRepository _depo;
    public RandevuService(RandevuRepository depo) => _depo = depo;

    public (bool Basarili, string Mesaj) RandevuOlustur(string hasta, string doktor, DateTime zaman, int yas)
    {
        if (!MesaiIcindeMi(zaman)) return (false, "Mesai saatleri dışında randevu verilmez.");
        if (SaatDoluMu(doktor, zaman)) return (false, "Bu saat dolu.");

        decimal ucret = UcretHesapla(yas);
        _depo.Ekle(new Randevu { HastaAdi = hasta, Doktor = doktor, Zaman = zaman, Ucret = ucret });
        return (true, $"randevu alındı, ücret {ucret} TL");
    }

    private bool MesaiIcindeMi(DateTime z) => z.Hour >= 9 && z.Hour < 17 && z.DayOfWeek != DayOfWeek.Sunday;
    private bool SaatDoluMu(string doktor, DateTime z) => _depo.DoktorunRandevulari(doktor).Any(r => r.Zaman == z);
    private decimal UcretHesapla(int yas) => yas >= 65 ? 250m : yas < 18 ? 350m : 500m;
}

public class RandevuController
{
    private readonly RandevuService _servis;
    public RandevuController(RandevuService servis) => _servis = servis;

    public string RandevuAl(string hasta, string doktor, DateTime zaman, int yas)
    {
        var sonuc = _servis.RandevuOlustur(hasta, doktor, zaman, yas);
        return sonuc.Basarili ? $"✅ {hasta}: {sonuc.Mesaj}" : $"❌ {sonuc.Mesaj}";
    }
}

// ================= MEDDLING SERVICE =================
public class KarisanRaporService
{
    private readonly SqlVeritabani _db;                       // servis doğrudan veritabanını tanıyor
    public KarisanRaporService(SqlVeritabani db) => _db = db;

    public string GunlukOzet(string doktor)
    {
        var liste = _db.Sorgula("SELECT * FROM Randevular WHERE Doktor = @doktor", doktor);   // SQL serviste!
        return $"📊 {doktor}: {liste.Count} randevu, toplam {liste.Sum(r => r.Ucret)} TL";
    }
}

public class RaporService
{
    private readonly RandevuRepository _depo;                 // servis sadece Repository'yi tanıyor
    public RaporService(RandevuRepository depo) => _depo = depo;

    public string GunlukOzet(string doktor)
    {
        var liste = _depo.DoktorunRandevulari(doktor);
        return $"📊 {doktor}: {liste.Count} randevu, toplam {liste.Sum(r => r.Ucret)} TL";
    }
}
