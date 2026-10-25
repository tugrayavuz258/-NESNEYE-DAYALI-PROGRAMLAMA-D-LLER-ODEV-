# MVC Mimarilerine Özgü Code Smell'ler: Makale İncelemesi

**Ders:** Nesneye Dayalı Programlama (2026 Güz)
**Öğrenci:** Tuğra Yavuz (Y265012020)
**Proje türü:** Seçenek B – Akademik Makale İncelemesi

## İncelenen Makale

> Aniche, M., Bavota, G., Treude, C., Gerosa, M. A., & van Deursen, A. (2018).
> **Code smells for Model-View-Controller architectures.**
> *Empirical Software Engineering*, 23(4), 2121–2157.
> DOI: [10.1007/s10664-017-9540-2](https://doi.org/10.1007/s10664-017-9540-2)

- **İndeks:** Science Citation Index Expanded (SCIE), Web of Science

## Makale Değişikliği Hakkında Not

Bu çalışmaya başlangıçta *Laigner vd. (2022), "Cataloging dependency injection
anti-patterns in software systems"* makalesiyle başlanmıştı. Aynı makalenin ders
listesinde başka bir öğrenci tarafından daha önce seçildiği görüldüğü için incelenen
makale değiştirilmiştir. `notlar/00` ve `notlar/01` dosyalarındaki Dependency Injection
notları **arka plan bilgisi** olarak korunmuştur; çünkü Spring MVC uygulamalarında
Controller ve Service sınıfları bağımlılıklarını DI ile alır ve makaledeki bazı smell'ler
(örn. *Promiscuous Controller*) doğrudan enjekte edilen bağımlılık sayısına dayanır.

## Neden Bu Makale?

Model-View-Controller (MVC), ödev metninde de belirtilen temel nesneye dayalı mimari
desenlerden biridir. Makale, genel amaçlı code smell kataloglarının (Fowler ve Beck)
bir sınıfın mimarideki **rolünü** (Controller, Service, Repository…) dikkate almadığını
gözlemleyerek **MVC'ye özgü 6 smell**'den oluşan bir katalog önermekte ve bunu
100 açık kaynak Spring MVC projesi, geliştirici anketleri ve uzman görüşmeleriyle
değerlendirmektedir.

Bu yönüyle dersin sınıf/nesne, kapsülleme, tek sorumluluk ve tasarım desenleri
konularının kesişiminde yer alır.

## Çalışmanın Kapsamı

1. Makalenin temel konusu, amacı ve araştırma soruları
2. Kullanılan yöntem ve teknolojik yaklaşımlar
3. Bulguların ve önerilen kataloğun değerlendirilmesi
4. Makaledeki fikirlerin Nesneye Dayalı Programlama ile ilişkisi
5. Alanın gelişim süreci (makalenin referanslarından en az 3 temel kaynak)
6. Kendi yorumlarım ve geliştirme önerileri
7. Kaynakça

**Ek katkı:** Makale Java (Spring MVC) üzerinde çalışmaktadır; genellenebilirlik
bölümünde ASP.NET MVC (C#) uzmanıyla da görüşülmüştür. Bu incelemede 6 smell
**C#** ile örneklenecek; her biri için hatalı ve düzeltilmiş kod hazırlanacaktır.

## Klasör Yapısı

```
notlar/   → makale okuma notları (00–01: DI arka plan notları)
rapor/    → inceleme raporunun bölümleri
kod/      → C# örnekleri
sunum/    → sunum dosyası
```

## Durum

- [x] Makale seçimi ve repo kurulumu
- [x] Arka plan: Dependency Injection ve temel OOP kavramları
- [x] Makale değişikliği ve yeni makalenin ilk incelemesi
- [ ] Okuma notları: 6 MVC smell'i
- [ ] Okuma notları: değerlendirme ve sonuçlar
- [ ] Rapor bölümleri
- [ ] C# örnekleri
- [ ] Sunum
