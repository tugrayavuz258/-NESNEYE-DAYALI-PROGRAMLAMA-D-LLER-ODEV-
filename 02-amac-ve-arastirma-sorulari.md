# 2. Makalenin Temel Konusu, Amacı ve Araştırma Soruları

## 2.1 Temel Konu

Makalenin konusu, web tabanlı MVC uygulamalarında ortaya çıkan ve **yalnızca bu
mimariye özgü** olan kötü tasarım uygulamalarıdır. Yazarlar, kod kokusu literatürünün
büyük ölçüde Fowler ve Beck'in genel kataloğuna dayandığını ve bu kataloğun bir sınıfın
mimarideki rolünü (Controller, Service, Repository vb.) hesaba katmadığını
vurgulamaktadır.

Makalede incelenen Spring MVC uygulamalarında sınıflar şu rollerden birini üstlenir:

| Rol | Görevi |
|---|---|
| **Controller** | Gelen isteği karşılar ve akışı yönetir; durumsuzdur, iş mantığı içermemelidir |
| **Service** | Durum tutmayan iş işlemlerini gerçekleştirir |
| **Repository** | Veritabanı erişimini kapsüller (Data Access Object benzeri) |
| **Entity** | Alan nesnelerini (ör. ürün, sipariş) temsil eder |
| **Component** | Biçimlendirme, dönüştürme gibi yardımcı işleri yapar |

Yazarlara göre, mimariye özgü bir kod kokusu kataloğunun yokluğu üç soruna yol açar:
(i) iyi ve kötü uygulamalar hakkında ortak bir bilgi birikimi oluşmaz, (ii) bu sorunları
tespit edecek araçlar geliştirilemez ve (iii) bu uygulamaların etkisine dair ampirik
kanıt elde edilemez.

## 2.2 Amaç ve Katkılar

Makalenin amacı, MVC'ye özgü kod kokularını **geliştiricilerin deneyiminden** çıkarıp
tanımlamak ve bu kokuların **gerçekten zararlı olup olmadığını** ölçmektir. Yazarların
belirttiği katkılar şunlardır:

1. 53 geliştiriciyle yapılan anket ve görüşmelerden elde edilen **altı MVC kod kokusu**:
   *Promiscuous Controller, Brain Controller, Meddling Service, Brain Repository,
   Laborious Repository Method* ve *Fat Repository*.
2. Her koku için metriklere dayalı bir **tespit stratejisi** ve bu stratejileri uygulayan
   açık kaynak **Springlint** aracı.
3. Bu kokuların değişim ve hata eğilimi, geliştiriciler tarafından nasıl algılandığı,
   ne zaman ortaya çıktığı, ne kadar süre yaşadığı ve diğer MVC framework'lerine
   genellenip genellenemeyeceği üzerine **ampirik kanıt**.
4. Çalışmanın tekrarlanabilmesi için herkese açık bir **replikasyon paketi**.

## 2.3 Araştırma Soruları

Makale, önerilen kataloğu altı araştırma sorusuyla değerlendirmektedir:

| | Araştırma sorusu | Kısaca |
|---|---|---|
| **RQ1** | MVC kokusu taşıyan sınıflar ile temiz sınıflar arasında **değişim eğilimi** bakımından fark var mıdır? | Daha sık mı değişiyor? |
| **RQ2** | MVC kokusu taşıyan sınıflar ile temiz sınıflar arasında **hata eğilimi** bakımından fark var mıdır? | Daha çok hata mı çıkarıyor? |
| **RQ3** | Geliştiriciler MVC kokusu taşıyan sınıfları **sorunlu olarak algılıyor** mu? | Geliştiriciler ne düşünüyor? |
| **RQ4** | MVC kokuları sistemde **ne kadar süre yaşıyor**? | Ne kadar kalıcı? |
| **RQ5** | MVC kokuları **ne zaman ortaya çıkıyor**? | Baştan mı, sonradan mı? |
| **RQ6** | MVC kokuları **diğer MVC framework'lerine genellenebilir** mi? | Spring dışında da geçerli mi? |

İlk iki soru kokuların yazılımın bakımı üzerindeki **nesnel** etkisini, üçüncü soru
geliştiricilerin **öznel** değerlendirmesini, dördüncü ve beşinci sorular kokuların
**yaşam döngüsünü**, altıncı soru ise bulguların **genellenebilirliğini** ele almaktadır.
Bu yapı, bir kod kokusu kataloğunun yalnızca önerilmesinin değil, çok yönlü olarak
doğrulanmasının da amaçlandığını göstermektedir.
