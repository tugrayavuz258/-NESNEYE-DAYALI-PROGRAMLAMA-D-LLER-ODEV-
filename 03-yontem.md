# 3. Kullanılan Yöntem ve Teknolojik Yaklaşımlar

## 3.1 Kod kokusu kataloğunun oluşturulması
Katalog, toplam **53 geliştiriciden** üç adımda elde edilen verilerle oluşturulmuştur:
1. **Katman odaklı anket (22 cevap):** Model, View ve Controller katmanlarındaki iyi ve kötü uygulamalar soruldu.
2. **Rol odaklı anket (14 cevap):** 120 Spring MVC projesine katkı veren geliştiricilere Controller, Service, Repository, Entity ve Component sınıfları için iyi/kötü uygulamalar soruldu.
3. **Yüz yüze görüşmeler (17 geliştirici):** 11 yıllık, 1 milyon satırı aşan bir Spring MVC ERP sistemini geliştiren bir şirkette yapıldı.

Cevaplar iki yazar tarafından bağımsız olarak **açık kodlama** ile kategorilere ayrıldı ve 9 aday koku elde edildi. Bir Spring MVC çekirdek geliştiricisinin değerlendirmesiyle, genellenemeyen 3 aday çıkarıldı ve **6 kod kokusu** kaldı.

## 3.2 Tespit stratejileri ve Springlint aracı
Her koku için Marinescu'nun (2004) **tespit stratejisi** yaklaşımıyla, metriklerin VE/VEYA ile birleştirildiği kurallar tanımlandı:

| Koku | Kural |
|---|---|
| Promiscuous Controller | NOR > 10 **veya** NSD > 3 |
| Brain Controller | NFRFC > 55 |
| Meddling Service | Serviste kalıcılık API'si (JDBC, Hibernate, JPA…) kullanımı |
| Brain Repository | McCabe > 24 **ve** SQL karmaşıklığı > 29 |
| Laborious Repository Method | Bir metotta birden fazla kalıcılık çağrısı |
| Fat Repository | CTE > 1 |

Eşikler, 20 projede Lanza ve Marinescu'nun (2007) çeyreklik analizi (3. çeyrek + 1,5 × çeyrekler arası açıklık) ile belirlendi. Kurallar açık kaynak **Springlint** aracında uygulandı; Spring'in `@Controller`, `@Service`, `@Repository` işaretleri sınıf rollerinin otomatik belirlenmesini sağladı.

## 3.3 Veri seti
BOA veri setinden **500'den fazla commit'i** ve **en az 10 Controller'ı** olan **120 Spring MVC** projesi seçildi; 20'si eşik ayarı, **100'ü** araştırma soruları için kullanıldı.

## 3.4 Araştırma sorularına göre yöntem
- **RQ1–RQ2:** Proje geçmişi **500 commit'lik dilimlere** bölündü. Dilim başında kokulu olan sınıfların dilim boyunca değişip değişmediği (RQ1, 291 dilim) ve "bug/fix" içeren hata düzeltme commit'lerine karışıp karışmadığı (RQ2, 16 proje, 77 dilim) incelendi. Temiz, MVC kokulu ve geleneksel kokulu (PMD ile) sınıflar **Fisher'ın kesin testi** ve **odds ratio** ile karşılaştırıldı. Sınıf büyüklüğünün etkisini ayırmak için sonuçlar satır sayısına göre (tümü, orta/büyük, büyük) ayrıca hesaplandı.
- **RQ3:** Kataloğun oluşturulmasına katılmamış **21 geliştiriciye**, hangi gruptan olduğu söylenmeden 6'şar sınıf (2 MVC kokulu, 2 geleneksel kokulu, 2 temiz) gösterilip sorun ciddiyeti 0–5 ölçeğinde soruldu; **Mann-Whitney** testi ve **Cliff's delta** kullanıldı.
- **RQ4–RQ5:** 100 projenin geçmişi commit commit taranarak kokuların ortaya çıkış ve kayboluş anları bulundu; ömür **hayatta kalma analizi** ile modellendi.
- **RQ6:** VRaptor, Ruby on Rails, **ASP.NET MVC** ve Play! framework'lerinden birer uzmanla e-posta yoluyla görüşüldü.
