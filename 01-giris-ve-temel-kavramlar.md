# Okuma Notları 1: Giriş, Arka Plan ve Temel Kavramlar

*Makalenin 1. (Giriş) ve 2. (Arka Plan ve İlgili Çalışmalar) bölümlerinden çıkarılan
notlar ile konuyu anlamak için hazırladığım C# örnekleri.*

---

## 1. Temel Kavramlar

### 1.1 Açılımlar

| Kısaltma | İngilizce | Türkçe |
|---|---|---|
| **DIP** | Dependency Inversion Principle | Bağımlılığın Tersine Çevrilmesi Prensibi |
| **IoC** | Inversion of Control | Kontrolün Tersine Çevrilmesi |
| **DI** | Dependency Injection | Bağımlılık Enjeksiyonu |

- **Bağımlılık:** Bir sınıfın çalışmak için ihtiyaç duyduğu başka bir nesne.
- **Enjeksiyon:** Bu nesnenin sınıfa dışarıdan verilmesi.
- **Kontrol:** "Ne zaman, kim, neyi yaratacak veya çalıştıracak?" kararı.

### 1.2 Benzetme: Priz

Bir lamba iki şekilde bağlanabilir:

- **Duvara lehimli lamba:** Değiştirmek için duvarı açmak gerekir.
- **Prize takılan lamba:** Standarda uyan her cihaz takılabilir; değiştirmek için fişi
  çekmek yeterlidir.

Yazılımda **priz = arayüz**, **fiş = arayüzü uygulayan sınıf**,
**lehimlemek = sınıfın içinde `new` ile somut nesne yaratmak**.

### 1.3 Başlangıç: Sıkı bağlı (lehimli) kod

```csharp
public class SmsGonderici
{
    public void SmsAt(string telefon, string mesaj)
        => Console.WriteLine($"SMS -> {telefon}: {mesaj}");
}

public class RandevuServisi
{
    private SmsGonderici _sms = new SmsGonderici();   // lehimli

    public void Onayla(string telefon)
        => _sms.SmsAt(telefon, "Randevunuz onaylandı");
}
```

Sorunlar:
1. E-postaya geçmek için `RandevuServisi`'ni değiştirmek gerekir.
2. Test edilemez: her testte gerçek SMS gider.
3. Randevu sınıfı, SMS'in nasıl gönderildiğini bilmek zorunda kalır.

### 1.4 DIP: "Duvara lehimleme, priz kullan"

> Üst seviye modüller alt seviye modüllere bağımlı olmamalı; ikisi de soyutlamalara
> bağımlı olmalı. (Robert C. Martin)

```csharp
public interface IBildirimGonderici              // priz
{
    void Gonder(string alici, string mesaj);
}

public class SmsGonderici : IBildirimGonderici    // fiş 1
{
    public void Gonder(string alici, string mesaj)
        => Console.WriteLine($"SMS -> {alici}: {mesaj}");
}

public class EpostaGonderici : IBildirimGonderici // fiş 2
{
    public void Gonder(string alici, string mesaj)
        => Console.WriteLine($"E-posta -> {alici}: {mesaj}");
}
```

Bağımlılığın yönü tersine döner:

```
Önce:   RandevuServisi ───► SmsGonderici
Sonra:  RandevuServisi ───► IBildirimGonderici ◄─── SmsGonderici
                                    ▲
                                    └────────────── EpostaGonderici
```

**NDP ile ilişkisi:** Arayüz kullanımı (3. hafta), çok biçimlilik ve dinamik bağlama
(2. hafta). `_bildirim.Gonder(...)` çağrısında hangi sınıfın metodunun çalışacağı
çalışma zamanında belirlenir.

⚠️ Sadece arayüz eklemek yetmez:

```csharp
private IBildirimGonderici _bildirim = new SmsGonderici();   // priz var ama hâlâ lehimli
```

### 1.5 IoC: "Cihazı lamba seçmez, ev sahibi seçer"

Nesne yaratma / karar verme kontrolü sınıftan alınıp dışarıya verilir.
("Hollywood prensibi": *Bizi aramayın, biz sizi ararız.*)

```csharp
// ❌ Kontrol sınıfta
public class RandevuServisi
{
    private IBildirimGonderici _bildirim;
    public RandevuServisi()
    {
        _bildirim = DateTime.Now.Hour < 22 ? new SmsGonderici() : new EpostaGonderici();
    }
}

// ✅ Kontrol dışarıda (Program.cs karar veriyor)
IBildirimGonderici secilen = DateTime.Now.Hour < 22
    ? new SmsGonderici()
    : new EpostaGonderici();
var servis = new RandevuServisi(secilen);
```

### 1.6 DI: "Fişi dışarıdan takmak"

IoC'yi uygulamanın en yaygın yolu. Üç biçimi vardır:

```csharp
// 1) Constructor injection (en çok önerilen)
public class RandevuServisi
{
    private readonly IBildirimGonderici _bildirim;
    public RandevuServisi(IBildirimGonderici bildirim) => _bildirim = bildirim;
}

// 2) Property (setter) injection: unutulursa null hatası, herkes değiştirebilir
public class RandevuServisi2
{
    public IBildirimGonderici Bildirim { get; set; }
}

// 3) Method injection: bağımlılık sadece bir metotta lazımsa
public class RandevuServisi3
{
    public void Onayla(string alici, IBildirimGonderici bildirim)
        => bildirim.Gonder(alici, "Onaylandı");
}
```

**En büyük faydası: test edilebilirlik.**

```csharp
public class SahteGonderici : IBildirimGonderici
{
    public int GonderimSayisi = 0;
    public void Gonder(string alici, string mesaj) => GonderimSayisi++;
}

[Fact]
public void Onayla_BirKezBildirimGonderir()
{
    var sahte = new SahteGonderici();
    var servis = new RandevuServisi(sahte);
    servis.Onayla("0555 123 45 67");
    Assert.Equal(1, sahte.GonderimSayisi);   // gerçek SMS gönderilmedi
}
```

### 1.7 DI Container: "Otomatik takan elektrikçi"

Büyük sistemlerde bağımlılıkların da bağımlılıkları olur; elle yazmak zorlaşır:

```csharp
var servis = new RandevuServisi(
    new SmsGonderici(new DosyaLogger("log.txt")),
    new SqlRandevuDeposu(new SqlBaglanti("..."))
);
```

DI container'a "hangi arayüz için hangi sınıf" kuralları bir kez verilir, nesne
zincirini kendisi kurar. Java'da **Spring** ve **Guice** (makalenin incelediği
framework'ler), C#'ta `Microsoft.Extensions.DependencyInjection` kütüphanesi bu işi yapar.
Bu çalışmadaki C# örnekleri container gerektirmediği sürece **elle DI** ile
(Program.cs içinde `new`) yazılacaktır.

---

## 2. IoC, DI ve DIP Arasındaki Farklar

### 2.1 IoC ve DI

**IoC genel fikir, DI onun bir uygulamasıdır.** Her DI bir IoC'dir, ama her IoC DI değildir.

DI olmayan IoC örnekleri:
- **Event:** `buton.Click += Kaydet;` → `Kaydet`'i ne zaman çağıracağına framework karar verir.
- **xUnit:** `[Fact]` metotlarını biz çağırmayız, test çerçevesi çağırır.
- **Template Method deseni:** Akışı üst sınıf yönetir, alt sınıf sadece bir adımı doldurur.

| | IoC | DI |
|---|---|---|
| Nedir? | Genel prensip | Somut teknik / desen |
| Neyi tersine çevirir? | Herhangi bir kontrolü | Bağımlılıkların yaratılıp verilmesini |
| Tek cümleyle | "Kontrolü sen tutma" | "İhtiyacını kendin yaratma, sana verilsin" |

### 2.2 DIP ve DI

- **DIP:** **Neye** bağımlı olunacağını söyler → soyutlamaya.
- **DI:** Bağımlılığın **nasıl** alınacağını söyler → dışarıdan.

| | Dışarıdan alıyor (DI ✅) | Kendisi yaratıyor (DI ❌) |
|---|---|---|
| **Arayüze bağlı (DIP ✅)** | ✅ `RandevuServisi(IBildirimGonderici b)` | ⚠️ `IBildirimGonderici b = new SmsGonderici();` |
| **Somut sınıfa bağlı (DIP ❌)** | ⚠️ `RandevuServisi(SmsGonderici s)` → **AP2** | ❌ `SmsGonderici s = new SmsGonderici();` |

Sol alt kutu makalenin ana fikrini özetliyor: **DI kullanılıyor ama DIP unutuluyor.**

### 2.3 Özet

| Kavram | Benzetme | Kodda |
|---|---|---|
| DIP | Priz kullan | `interface IBildirimGonderici` |
| IoC | Ampulü ev sahibi seçer | Kararın `Program.cs`'e taşınması |
| DI | Fişi dışarıdan tak | `RandevuServisi(IBildirimGonderici b)` |
| Container | Otomatik takan elektrikçi | Spring / Guice / .NET DI kütüphanesi |

---

## 3. Makale: Giriş Bölümü

### 3.1 Motivasyon ve problem
- DI, modülleri birbirinin somut sınıflarından habersiz bırakarak modülerliği artırır;
  DIP ve IoC'ye dayanır.
- Endüstride yaygındır (Spring, Guice, Ninject, Google AdWords, Microsoft Orleans);
  Java'da JSR-330 standardı vardır, .NET Core yerleşik DI desteği sunar.
- DI'yi doğru uygulamak sağlam OOP tasarım bilgisi gerektirir; yanlış kullanım
  gevşek bağlılık hedefini bozar.
- Kitap ve bloglarda DI anti-pattern'lerinden söz edilir, ancak bunların pratikte
  ne sıklıkla görüldüğü ve geliştiricilerce nasıl algılandığı incelenmemiştir.

### 3.2 Neden Java?
Yazarlara göre Java'da belgelenmiş DI kötü pratikleri eksiktir; DI framework'leri
çoktur, endüstride yaygındır ve çok sayıda açık kaynak proje vardır.
**Endüstriye yönelik DI yayınlarının çoğu .NET'i hedeflemektedir.**
→ *Bu çalışmadaki C# örnekleri için önemli bir bağlantı noktası.*

### 3.3 Araştırma soruları

| | Soru | Türkçesi |
|---|---|---|
| RQ1 | Are there problem candidates associated with DI implementation that are not properly covered by the currently documented DI bad practices? | Mevcut belgelenmiş kötü pratiklerin kapsamadığı DI problemleri var mı? |
| RQ2 | Do the proposed DI anti-patterns occur in practice? | Önerilen anti-pattern'ler gerçek projelerde görülüyor mu? |
| RQ3 | Do expert developers understand the anti-patterns proposed as useful in practice? | Uzman geliştiriciler bunları faydalı buluyor mu? |
| RQ4 | Are practitioners willing to refactor DI anti-pattern instances from source code? | Geliştiriciler bunları düzeltmeye istekli mi? |

### 3.4 Yöntem (genel bakış)
- Katalog **tümevarımla** (yazarların endüstriyel bakım deneyimi) ve
  **tümdengelimle** (GRASP ve SOLID prensipleri) oluşturulmuş.
- **DIAnalyzer** adlı statik analiz aracıyla projelerde aranmış.
- Uzman geliştiricilerle anket yapılmış.

### 3.5 Katkılar
1. DI üzerine kapsamlı çalışma eksikliğini ortaya koyma
2. Yeni bir Java DI anti-pattern kataloğu
3. DIAnalyzer aracı (GitHub'da açık)
4. Anti-pattern'lerin açık ve kapalı kaynak projelerde sık görüldüğünün gösterilmesi
5. Geliştiricilerin kataloğu faydalı bulduğunun gösterilmesi
6. Anket sonuçlarına göre güncellenmiş katalog

Makale, yazarların SBES 2019'da yayımlanan *"Towards a Catalog of Java Dependency
Injection Anti-Patterns"* bildirisinin genişletilmiş dergi sürümüdür
(→ "alanın gelişimi" bölümü için not).

---

## 4. Makale: Arka Plan ve İlgili Çalışmalar

| Çalışma | Ne yapılmış | Bulgu |
|---|---|---|
| Yang vd. | 34 açık kaynak Java projesinde DI biçimleri aranmış | DI az kullanılıyor; geliştiriciler daha çok Service Locator gibi yöntemlere başvuruyor |
| Crasso vd. | Web servislerinde DI kullanımı | Hızlı geliştirme, temiz kod, gevşek bağlılık; bedeli biraz fazla bellek |
| Razina & Janzen | DI kullanan / kullanmayan 20 proje çifti (CBO, RFC, LCOM) | Genel fark yok; DI kullanımı %10'u aşan projelerde coupling düşüyor |

**Sonuç:** Önceki çalışmalar DI'nin faydalarına odaklanmış; kötü kullanımını
sistematik olarak inceleyen bir çalışma yok. Makale bu boşluğu dolduruyor.

**Not:** CBO (Coupling Between Objects), RFC (Response For a Class) ve LCOM (Lack of
Cohesion in Methods), Chidamber–Kemerer OOP metrikleridir.

---

## Okurken aklıma gelenler
- Field injection Java/Spring'e özgü; C#'ın yaygın DI kütüphanesinde yok. Bu, bazı
  anti-pattern'lerin C#'ta hiç ortaya çıkmayabileceği anlamına gelebilir mi?
- Razina & Janzen'in "%10 eşiği" ilginç: DI az kullanılınca fayda görülmüyor mu?

*Kaynak: Makalenin arXiv ön baskısı (2109.04256) okunmuştur; rakamlar JSS sürümüyle
karşılaştırılacaktır.*
