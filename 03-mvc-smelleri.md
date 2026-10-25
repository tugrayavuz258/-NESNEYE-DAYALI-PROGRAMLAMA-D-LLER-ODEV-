# Okuma Notları 3: MVC'ye Özgü 6 Smell

*Aniche vd. (2018), Bölüm 2. C# örnekleri: `kod/` klasörü.*

## Code smell nedir?
Kodun **çalıştığı** ama tasarımında ileride sorun çıkarabilecek bir şeyin olduğunu gösteren
işaret (Fowler, 1999: *"If it stinks, change it"*). Bug'dan farkı: davranışta değil,
**yapıda** bir sorundur.

## MVC rolleri (Spring MVC)
| Rol | Görevi | Benzetme |
|---|---|---|
| Controller | İsteği alır, iletir; iş mantığı içermez | Garson |
| Service | İş kurallarını uygular | Şef |
| Repository | Veritabanı erişimini kapsüller | Kiler |
| Entity | Alan nesnesi (Randevu, Hasta…) | Malzeme |
| View | Kullanıcıya gösterim | Sunum |

Kural: Çağrılar hep bir alt katmana iner, cevaplar `return` ile yukarı çıkar.

## Katalog nasıl oluşturuldu?
53 geliştirici: katman odaklı anket (22), rol odaklı anket (14), bir şirkette 17 kişiyle
görüşme → iki yazarın bağımsız kodlaması → 9 aday smell → Spring çekirdek geliştiricisi
Arjen Poutsma'nın incelemesi ile 3'ü çıkarıldı → **6 smell**. Eşikler 20 projede
çeyreklik analizle (3Ç + 1,5 × ÇAA) belirlendi.

## 6 smell

| # | Smell | Rol | Tanım | Tespit | Bahseden | Çözüm |
|---|---|---|---|---|---|---|
| 1 | Promiscuous Controller | Controller | Çok fazla route veya servis | NOR > 10 **veya** NSD > 3 | 6 | Controller'ı böl |
| 2 | Brain Controller | Controller | Çok fazla akış kontrolü / iş mantığı | NFRFC > 55 | **25** | Mantığı Service/Entity/Component'e taşı |
| 3 | Meddling Service | Service | Veritabanını doğrudan sorgulayan servis | Kalıcılık API'si (JDBC, Hibernate, JPA) kullanımı | 2 + görüşülenlerin tamamı | SQL'i Repository'ye taşı |
| 4 | Brain Repository | Repository | İş kuralı + karmaşık sorgu | McCabe > 24 **ve** SQL karmaşıklığı > 29 | **24** | Mantığı ayrı metoda / Component'e, kuralları Service'e |
| 5 | Laborious Repository Method | Repository | Bir metotta birden fazla DB işlemi | > 1 kalıcılık çağrısı | 1 | Metodu böl |
| 6 | Fat Repository | Repository | Birden fazla Entity'yi yöneten Repository | CTE > 1 | 6 | Her Entity'ye ayrı Repository |

Metrikler: **NOR** route sayısı · **NSD** servis bağımlılığı sayısı · **NFRFC** framework
dışı RFC (CK metriği) · **McCabe** dallanma sayısı · **SQL karmaşıklığı** WHERE/AND/OR/JOIN…
sayısı · **CTE** Entity'lere bağımlılık sayısı.

## İki aile
- **"Çok fazla iş" (şişmanlık):** Promiscuous Controller, Laborious Repository Method, Fat Repository → **böl**
- **"Yanlış yerde iş" (rol ihlali):** Brain Controller, Meddling Service, Brain Repository → **doğru katmana taşı**

## OOP / önceki DI notlarıyla bağlantı
- Promiscuous Controller ≈ *Fat DI Class* (AP4): bağımlılık sayısına dayalı, SRP ihlali
- Brain Controller ≈ *Long Producer Method* (AP3): görev dışı mantık
- Meddling Service → **DIP** ihlali: iş mantığı soyutlamayı (Repository) atlayıp teknik detaya bağımlı
- Brain/Laborious/Fat Repository → tek sorumluluk ve uyum (cohesion)

## Eleştirel notlar
- Laborious Repository Method'dan yalnızca 1 katılımcı söz etmiş, yine de katalogda.
- CTE > 1 eşiği katı: "hastanın randevuları" gibi meşru sorgular da yakalanabilir.
- Brain Repository düzeltmesinde filtreleme uygulama tarafına taşınırsa büyük veride
  performans kaybı olabilir; makalenin "iki şart birden" kuralı bu esnekliği tanıyor.
