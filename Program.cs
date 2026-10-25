var db = new SqlVeritabani();

Console.WriteLine("==== BRAIN REPOSITORY (kötü) ====");
var beyinli = new BeyinliDoktorRepository(db);
beyinli.UygunDoktorlar("Kardiyoloji", 70, true);
Console.WriteLine("==== BRAIN REPOSITORY düzeltilmiş ====");
var doktorServis = new DoktorService(new DoktorRepository(db));
doktorServis.UygunDoktorlar("Kardiyoloji", 70, true);

Console.WriteLine("==== LABORIOUS REPOSITORY METHOD (kötü) ====");
new YorgunRandevuRepository(db).IptalEt(42);
Console.WriteLine("==== LABORIOUS düzeltilmiş ====");
new IptalService(new RandevuRepository(db), new IslemKaydiRepository(db)).IptalEt(42);

public class SqlVeritabani
{
    public void Calistir(string sql) => Console.WriteLine($"   [DB] {sql}");
    public void Sorgula(string sql) => Console.WriteLine($"   [DB] {sql}");
}
public class Hasta { public string Ad { get; set; } = ""; }
public class Doktor { public string Ad { get; set; } = ""; }
public class Randevu { public int Id { get; set; } }

// ===== BRAIN REPOSITORY =====
public class BeyinliDoktorRepository
{
    private readonly SqlVeritabani _db;
    public BeyinliDoktorRepository(SqlVeritabani db) => _db = db;

    public void UygunDoktorlar(string brans, int hastaYasi, bool ozelSigorta)
    {
        string sql = "SELECT * FROM Doktorlar d JOIN Branslar b ON d.BransId = b.Id WHERE b.Ad = @brans";
        if (hastaYasi >= 65)
            sql += " AND d.GeriatriSertifikasi = 1";          // iş kuralı: yaşlı hastaya sertifikalı doktor
        else if (hastaYasi < 18)
            sql += " AND d.CocukHastaKabul = 1";              // iş kuralı: çocuğa çocuk kabul eden doktor
        if (!ozelSigorta)
            sql += " AND d.Id NOT IN (SELECT DoktorId FROM SadeceOzelSigorta)";   // iş kuralı: sigorta
        if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
            sql += " AND d.CumartesiCalisir = 1";             // iş kuralı: cumartesi
        _db.Sorgula(sql);
    }
}

public class DoktorBilgisi
{
    public string Ad { get; set; } = "";
    public bool GeriatriSertifikasi { get; set; }
    public bool CocukHastaKabul { get; set; }
    public bool SadeceOzelSigorta { get; set; }
}

public class DoktorRepository
{
    private readonly SqlVeritabani _db;
    public DoktorRepository(SqlVeritabani db) => _db = db;

    public List<DoktorBilgisi> BransaGoreGetir(string brans)
    {
        _db.Sorgula("SELECT * FROM Doktorlar d JOIN Branslar b ON d.BransId = b.Id WHERE b.Ad = @brans");
        return new List<DoktorBilgisi>
        {
            new() { Ad = "Dr. Kaya",  GeriatriSertifikasi = true },
            new() { Ad = "Dr. Demir", SadeceOzelSigorta = true, GeriatriSertifikasi = true },
            new() { Ad = "Dr. Ak",    CocukHastaKabul = true },
        };
    }
}

public class DoktorService
{
    private readonly DoktorRepository _depo;
    public DoktorService(DoktorRepository depo) => _depo = depo;

    public List<DoktorBilgisi> UygunDoktorlar(string brans, int hastaYasi, bool ozelSigorta)
    {
        var sonuc = _depo.BransaGoreGetir(brans)
            .Where(d => YasaUygunMu(d, hastaYasi))
            .Where(d => SigortayaUygunMu(d, ozelSigorta))
            .ToList();
        Console.WriteLine("   [Service] Uygun: " + string.Join(", ", sonuc.Select(d => d.Ad)));
        return sonuc;
    }

    private static bool YasaUygunMu(DoktorBilgisi d, int yas)
        => yas >= 65 ? d.GeriatriSertifikasi : yas < 18 ? d.CocukHastaKabul : true;

    private static bool SigortayaUygunMu(DoktorBilgisi d, bool ozelSigorta)
        => ozelSigorta || !d.SadeceOzelSigorta;
}

// ===== LABORIOUS REPOSITORY METHOD =====
public class YorgunRandevuRepository
{
    private readonly SqlVeritabani _db;
    public YorgunRandevuRepository(SqlVeritabani db) => _db = db;

    public void IptalEt(int randevuId)
    {
        _db.Calistir($"UPDATE Randevular SET Durum = 'Iptal' WHERE Id = {randevuId}");
        _db.Calistir($"DELETE FROM Hatirlatmalar WHERE RandevuId = {randevuId}");
        _db.Calistir($"INSERT INTO IslemKaydi (Islem) VALUES ('Randevu {randevuId} iptal')");
    }
}

public class RandevuRepository
{
    private readonly SqlVeritabani _db;
    public RandevuRepository(SqlVeritabani db) => _db = db;
    public void DurumuIptalYap(int id) => _db.Calistir($"UPDATE Randevular SET Durum = 'Iptal' WHERE Id = {id}");
    public void HatirlatmalariSil(int id) => _db.Calistir($"DELETE FROM Hatirlatmalar WHERE RandevuId = {id}");
}
public class IslemKaydiRepository
{
    private readonly SqlVeritabani _db;
    public IslemKaydiRepository(SqlVeritabani db) => _db = db;
    public void Ekle(string islem) => _db.Calistir($"INSERT INTO IslemKaydi (Islem) VALUES ('{islem}')");
}
public class IptalService
{
    private readonly RandevuRepository _randevular;
    private readonly IslemKaydiRepository _kayitlar;
    public IptalService(RandevuRepository r, IslemKaydiRepository k) { _randevular = r; _kayitlar = k; }
    public void IptalEt(int id)
    {
        _randevular.DurumuIptalYap(id);
        _randevular.HatirlatmalariSil(id);
        _kayitlar.Ekle($"Randevu {id} iptal");
    }
}

// ===== FAT REPOSITORY =====
public class SismanRepository
{
    public void RandevuEkle(Randevu r) { }
    public List<Randevu> RandevulariGetir() => new();
    public void HastaEkle(Hasta h) { }
    public Hasta? HastaBul(string tc) => null;
    public void DoktorEkle(Doktor d) { }
    public List<Doktor> DoktorlariGetir() => new();
}
public class HastaRepository { public void Ekle(Hasta h) { } public Hasta? Bul(string tc) => null; }
