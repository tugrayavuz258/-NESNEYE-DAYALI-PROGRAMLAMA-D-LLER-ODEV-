# Okuma Notları 2: Yeni Makale – İlk İzlenim

*Aniche vd. (2018), "Code smells for Model-View-Controller architectures",
Empirical Software Engineering. Özet ve giriş bölümünden çıkarılan notlar.*

---

## Problem

- Düşük kaliteli kodun bakım maliyetini artırdığı biliniyor; **code smell'ler** bunun
  belirtilerinden biri.
- Literatürde en çok kullanılan katalog Fowler ve Beck'inki (örn. *God Class*,
  *Feature Envy*). Ancak bu smell'ler **genel**: sınıfın mimarideki rolüne bakmıyor.
- Oysa MVC gibi belirli bir mimariyi kullanan sistemlerde, **sadece o mimaride
  ortaya çıkan** kötü pratikler var. Örneğin bir Controller'ın sade kalması, bir
  veri erişim sınıfının SQL karmaşıklığının sınırlı olması beklenir.

## Temel kavramlar: Spring MVC'de sınıf rolleri

| Rol | Görevi |
|---|---|
| **Controller** | Gelen isteği karşılar, akışı yönetir; iş mantığı içermemeli |
| **Service** | Durum tutmayan iş işlemlerini yapar (Model katmanı) |
| **Repository** | Veritabanı erişimini kapsüller (DAO benzeri) |
| **Entity** | Bir alan nesnesini temsil eder (örn. Hasta, Randevu) |
| **Component** | Yardımcı sınıflar (biçimlendirme, dönüştürme…) |

Spring MVC, her sınıfın rolünü bir işaretle (`@Controller`, `@Service`,
`@Repository`, `@Entity`) belirttiği için roller kolayca tespit edilebiliyor.

## Önerilen 6 smell

| Smell | Kısaca |
|---|---|
| **Promiscuous Controller** | Çok fazla işlem (route) sunan ya da çok fazla servise bağımlı Controller |
| **Brain Controller** | Çok fazla akış kontrolü (iş mantığı) içeren Controller |
| **Meddling Service** | Veritabanını doğrudan sorgulayan Service (katman ihlali) |
| **Brain Repository** | Karmaşık mantık içeren Repository |
| **Laborious Repository Method** | Birden fazla veritabanı işlemi yapan tek Repository metodu |
| **Fat Repository** | Çok fazla Entity'yi yöneten Repository |

## Katalog nasıl oluşturulmuş?

Toplam **53 geliştirici** ile:
1. Katman odaklı anket (22 cevap)
2. Rol odaklı anket – 120 Spring MVC projesinin geliştiricilerine (14 cevap)
3. Bir şirkette 17 geliştiriciyle görüşme (11 yıllık, 1 milyon satırlık bir ERP sistemi)

Cevaplar iki yazar tarafından bağımsız olarak kodlanmış → 9 aday smell →
bir Spring MVC çekirdek geliştiricisinin görüşüyle 3'ü elenmiş → **6 smell**.

## Araştırma soruları

| | Soru |
|---|---|
| RQ1 | Smell'li sınıflar daha sık **değişiyor** mu? |
| RQ2 | Smell'li sınıflarda daha fazla **hata** çıkıyor mu? |
| RQ3 | Geliştiriciler bu sınıfları **sorunlu** olarak algılıyor mu? |
| RQ4 | Smell'ler ne kadar süre **yaşıyor**? |
| RQ5 | Smell'ler **ne zaman** ortaya çıkıyor? |
| RQ6 | Smell'ler **diğer MVC framework'lerine** genellenebilir mi? |

## Sonuçlar (özetten)

- Smell'li sınıfların değişme ve hata çıkarma ihtimali daha yüksek.
- Smell'ler çoğunlukla dosya **ilk kez commit edildiğinde** ortaya çıkıyor ve uzun süre
  yaşıyor.
- Geliştiriciler smell'li sınıfları ciddi sorun olarak algılıyor.
- Smell'ler diğer MVC framework'lerine (Ruby on Rails, **ASP.NET MVC**, Play!, VRaptor)
  büyük ölçüde genellenebiliyor.

## Önceki DI notlarıyla bağlantı

- **Promiscuous Controller**'ın tespit kuralı "servis olarak alınan bağımlılık sayısı"na
  bakıyor → DI'deki *Fat DI Class* (AP4) fikrine çok yakın.
- **Brain Controller**, **Brain Repository**, **Laborious Repository Method** →
  tek sorumluluk ihlalleri.
- **Meddling Service** → katmanlar arası bağımlılığın yanlış yönde kurulması.

## Okurken cevaplanacak sorular
- Her smell'in tespit kuralı ve eşik değerleri nasıl belirlenmiş?
- Değişim ve hata eğilimi nasıl ölçülmüş (hangi istatistik)?
- ASP.NET MVC uzmanı smell'ler hakkında ne söylemiş?
