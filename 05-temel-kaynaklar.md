# Okuma Notları 5: Temel Kaynaklar ve Alanın Gelişimi

*Ödev gereği: makalenin kaynakçasından en az 3 temel kaynak.*

---

## 1. Krasner & Pope (1988) – MVC'nin kökeni

> Krasner, G. E., & Pope, S. T. (1988). A description of the model-view-controller user
> interface paradigm in the Smalltalk-80 system. *Journal of Object-Oriented Programming*,
> 1(3), 26–49.

- **Ne yaptı?** Smalltalk-80 dilinde etkileşimli (masaüstü) arayüzlerin nasıl kurulduğunu
  anlatan, MVC'yi tanımlayan ilk yaygın yayın.
- **Üç parça:**
  - **Model:** Uygulamanın verisi ve davranışı
  - **View:** Modelin ekranda gösterimi
  - **Controller:** Kullanıcının fare/klavye girdilerini yorumlayıp modele ve görünüme iletmesi
- **Bağımlılık (dependency) mekanizması:** Model değişince kendisine bağlı görünümlere
  "değiştim" haberi verir, görünümler kendini günceller. Bu fikir daha sonra
  **Observer** tasarım deseni olarak bilinir hale gelmiştir.
- **Amaç:** Sorumlulukları ayırarak modülerlik ve **yeniden kullanılabilirlik** sağlamak
  (aynı model için farklı görünümler).
- **İncelenen makaleyle bağı:** Aniche vd. MVC'yi tanımlarken bu kaynağa dayanıyor.
  1988'deki masaüstü MVC, 2000'lerde web'e uyarlanırken Model katmanı Service,
  Repository, Entity gibi rollere bölünmüştür; makalenin smell'leri bu rollere özgüdür.

## 2. Fowler (1999) – "Code smell" kavramının kökeni

> Fowler, M. (1999). *Refactoring: Improving the Design of Existing Code.* Addison-Wesley.
> (Not: İncelenen makalenin kaynakçasında yılı 1997 olarak yazılmıştır; kitabın yayın
> yılı 1999'dur.)

- **Ne yaptı?** Kodun dışarıdan görünen davranışını değiştirmeden iç yapısını iyileştirme
  işlemi olan **refactoring**'i sistematik hale getirdi ve onlarca refactoring tekniğini
  katalogladı.
- **3. bölüm "Bad Smells in Code"** (Kent Beck ile birlikte): *"If it stinks, change it."*
  Refactoring'in **ne zaman** gerektiğini gösteren 22 **genel** kod kokusu listelenir.
  Örnekler: *Duplicated Code, Long Method, Large Class, Long Parameter List, Feature Envy,
  Shotgun Surgery, Divergent Change*.
- **Önemli bir tutum:** Fowler ve Beck, bir kokunun ne zaman sorun olduğuna karar vermede
  hiçbir metrik setinin deneyimli insan sezgisinin yerini tutamayacağını söyler.
- **İncelenen makaleyle bağı:** Aniche vd. bu genel kataloğu **çıkış noktası** olarak
  alıp "sınıfın mimarideki rolünü dikkate almıyor" diye eleştirir. Ayrıca Fowler'ın
  sezgiye vurgusunun aksine, smell'leri **metrik ve eşiklerle** otomatik tespit eder;
  bu iki yaklaşım arasındaki gerilim yorum bölümünde tartışılabilir.

## 3. Khomh vd. (2012) – Smell'lerin zararını ölçmek

> Khomh, F., Di Penta, M., Guéhéneuc, Y.-G., & Antoniol, G. (2012). An exploratory study of
> the impact of antipatterns on class change- and fault-proneness. *Empirical Software
> Engineering*, 17(3), 243–275.

- **Ne yaptı?** 4 açık kaynak sistemin (ArgoUML, Eclipse, Mylyn, Rhino) **54 sürümünde**
  **13 anti-pattern**'i tespit edip bu sınıfların değişim ve hata eğilimini inceledi.
- **Bulgular:**
  - Anti-pattern içeren sınıflar, neredeyse tüm sürümlerde diğer sınıflara göre daha
    fazla **değişime** ve **hataya** eğilimli.
  - Sınıf **büyüklüğü tek başına** bu farkı açıklamıyor.
  - Yapısal değişiklikler anti-pattern içeren sınıfları daha çok etkiliyor.
- **İncelenen makaleyle bağı:** Aniche vd.'nin RQ1–RQ2 tasarımı (değişim/hata eğilimi,
  odds ratio, büyüklük kontrolü) bu çalışmanın yöntemini **MVC'ye özgü smell'lere**
  uygular. Khomh vd. genel anti-pattern'ler için bulduğu ilişkiyi, Aniche vd. MVC
  smell'leri için test eder.

---

## Alanın gelişimi: zaman çizelgesi

| Yıl | Çalışma | Katkı |
|---|---|---|
| 1988 | Krasner & Pope | MVC, Smalltalk-80'de tanımlanıyor |
| 1994 | Chidamber & Kemerer | OOP metrikleri (RFC, CBO…) |
| 1999 | Fowler | "Code smell" kavramı, 22 genel smell |
| 2002 | Fowler | Repository, Service Layer (web MVC katmanları) |
| 2004 | Marinescu | Metriklerle smell tespit stratejileri |
| 2007 | Lanza & Marinescu | Eşik belirleme (çeyreklik analizi) |
| 2012 | Khomh vd. | Smell/anti-pattern'lerin değişim ve hataya etkisi |
| 2016 | Aniche vd. (ICSME) | MVC smell'lerinin ilk sürümü |
| **2018** | **Aniche vd. (EMSE)** | **İncelenen makale: 6 MVC smell, genişletilmiş değerlendirme** |
| 2022 | Laigner vd. (JSS) | Sonraki gelişme: DI'ye özgü 12 anti-pattern |

**Ana fikir:** Kod kalitesi araştırması **genelden özele** ilerlemiştir: önce her koda
uygulanan genel smell'ler (Fowler), sonra bunların zararının ölçülmesi (Khomh vd.),
ardından belirli **mimarilere** (MVC – Aniche vd.) ve **mekanizmalara** (DI – Laigner vd.)
özgü kataloglar.
