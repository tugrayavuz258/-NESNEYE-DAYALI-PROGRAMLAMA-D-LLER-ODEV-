# Okuma Notları 4: Değerlendirme ve Sonuçlar (RQ1–RQ6)

*Aniche vd. (2018), Bölüm 3–5.*

## Veri
- BOA veri setinden **>500 commit** ve **≥10 Controller**'ı olan **120 Spring MVC** projesi.
- 20'si eşik ayarı, **100**'ü araştırma soruları için. Tespit aracı: **Springlint**.
- Geçmiş **500 commit'lik dilimlere** bölündü (RQ1: 291 dilim, RQ2: 77 dilim).
- Karşılaştırma: temiz / MVC smell'li / geleneksel smell'li (PMD ile: God Class, Long Method…).

## İstatistik kavramları
- **Odds** = olan / olmayan. **Odds ratio (OR)** = smell'li grubun odds'u / temiz grubun odds'u.
  OR = 1 fark yok, > 1 smell'li grupta daha olası. Sık görülen olaylarda OR, olasılık
  oranını olduğundan büyük gösterebilir (ör. %60'a %33 → OR 3 ama olasılık oranı 1,8).
- **Fisher's exact test** (p < 0,05 → `*`): fark tesadüf mü?
- **Büyüklük kontrolü:** Büyük sınıflar zaten daha çok değişir; benzer büyüklüktekiler
  (Orta/Büyük, Büyük) ayrıca karşılaştırıldı.
- **Mann-Whitney** (puan farkı anlamlı mı?), **Cliff's delta** (etki büyüklüğü; >0,47 büyük),
  **medyan**, **hayatta kalma analizi**.

## RQ1–RQ2: Değişim ve hata eğilimi (OR, MVC smell'li vs temiz)
| | Tümü | Orta/Büyük | Büyük |
|---|---|---|---|
| Değişim | 2,97* | 1,42* | 1,60* |
| Hata | 2,05* | 0,72 | 1,06 |

Smell bazında öne çıkanlar: **Brain Repository** değişimde en yüksek (5,08*; büyükte 2,79*).
**Meddling Service** büyük sınıflarda hem değişimde (2,89*) hem hatada (2,53*) anlamlı
kalan **tek** smell.

- **RQ1 ✅** Değişim eğilimi yüksek, büyüklük kontrolünden sonra da.
- **RQ2 🟡** Hata ilişkisi zayıf; büyüklük kontrolünden sonra yalnızca Meddling Service.
- Geleneksel smell'ler daha yüksek OR gösteriyor → MVC smell'leri onların **yerine değil yanına**.

## RQ3: Geliştirici algısı
21 geliştirici (13'ü 8+ yıl), her biri 6 sınıf (2 MVC smell'li, 2 geleneksel, 2 temiz),
**grubu bilmeden**. Ciddiyet 0–5.
- Temiz medyan **0**, MVC smell'li medyan **4** (p < 0,001, d = 0,56 büyük).
- MVC vs geleneksel fark anlamlı değil (p = 0,21).
- En ciddi: Meddling Service, Fat Repository, Brain Controller. En az: Laborious Repository Method.

## RQ4: Ömür
1.337 smell örneğinin **928'i (%69) hiç silinmemiş**. Her türün 500 günden uzun yaşama
ihtimali > %50; Fat Repository'nin 1.500 günü aşma ihtimali ≈ %80.

## RQ5: Ortaya çıkış
1.127 smell'li sınıfın **734'ü (%65)** ilk commit'ten itibaren smell'li. En yüksek:
Laborious (%86,5), Meddling (%81,3). Brain Controller (%42,6) ve Brain Repository (%43,7)
çoğunlukla sonradan büyüyor.

## RQ6: Genellenebilirlik (4 uzman, e-posta)
| Smell | VRaptor | Rails | ASP.NET MVC | Play! |
|---|---|---|---|---|
| Promiscuous Controller | ✅ | ✅ | ✅ | ✅ |
| Brain Controller | ✅ | ✅ | ✅ | ✅ |
| Brain Repository | ✅ | 🟡 | ✅ | 🟡 |
| Fat Repository | ✅ | 🟡 | ✅ | 🟡 |
| Laborious Repository Method | ✅ | 🟡 | ✅ | 🟡 |
| Meddling Service | ✅ | ❌ | ✅ | ❌ |

Rails/Play! Active Record kullandığı için ayrı Repository yok. **ASP.NET MVC (C#): 6/6
geçerli**; uzman Meddling Service'i günlük işinde sık gördüğünü, servislerin soyutlamalar
üzerinden veriye erişmesi gerektiğini söylüyor.

## Geçerlilik tehditleri (yazarların kabul ettikleri)
- Yapı: hata commit'leri anahtar kelimeyle bulundu; eşikler ve McCabe en iyi seçim olmayabilir.
- İç: karışık (tangled) commit'ler.
- Dış: eşikler 20 projeden; RQ1–5 sadece Spring; görüşmeler tek şirkette; RQ6 öznel.

## Kendi eleştirilerim (yazarların değinmedikleri dahil)
- RQ2 yalnızca 16 projeyle.
- Korelasyon nedensellik değildir.
- OR, sık olaylarda etkiyi abartabilir; "x kat daha olası" yerine "odds oranı" denmeli.
- RQ6 her framework için tek uzman → zayıf kanıt.
- Ömür gün cinsinden ölçüldü; proje aktivitesi farkı gözetilmedi.
