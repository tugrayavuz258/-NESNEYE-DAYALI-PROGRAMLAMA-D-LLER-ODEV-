# Dependency Injection Anti-Pattern'leri: Makale İncelemesi

**Ders:** Nesneye Dayalı Programlama (2026 Güz)
**Öğrenci:** Tuğra Yavuz (Y265012020)
**Proje türü:** Seçenek B – Akademik Makale İncelemesi

## İncelenen Makale

> Laigner, R., Mendonça, D., Garcia, A., & Kalinowski, M. (2022).
> **Cataloging dependency injection anti-patterns in software systems.**
> *Journal of Systems and Software*, 184, 111125.
> DOI: [10.1016/j.jss.2021.111125](https://doi.org/10.1016/j.jss.2021.111125)

- **İndeks:** Science Citation Index Expanded (SCIE), Web of Science
- **Erişim:** Açık erişim (CC BY-NC-ND), ön baskı: [arXiv:2109.04256](https://arxiv.org/abs/2109.04256)

## Neden Bu Makale?

Dependency Injection (DI), nesneye dayalı tasarımda sınıflar arasındaki bağımlılığı
azaltmak için kullanılan temel bir mekanizmadır ve SOLID prensiplerinden
**Dependency Inversion Principle (DIP)** ile doğrudan ilişkilidir. Java'da Spring ve
Guice, C#'ta .NET'in DI kütüphanesi gibi araçların temelinde yer alır.

Makale, DI'nin **yanlış** kullanıldığı durumları (anti-pattern) sistematik olarak
kataloglayan ve bunların gerçek projelerde ne sıklıkla görüldüğünü ölçen ampirik bir
çalışmadır. Bu yönüyle dersin arayüz/soyut sınıf, tasarım desenleri, Java ve C#
konularının kesişiminde yer alır.

## Çalışmanın Kapsamı

Rapor, ders kapsamında belirlenen yapıyı izleyecektir:

1. Makalenin temel konusu, amacı ve araştırma soruları
2. Kullanılan yöntem ve teknolojik yaklaşımlar
3. Bulguların ve önerilen kataloğun değerlendirilmesi
4. Makaledeki fikirlerin Nesneye Dayalı Programlama ile ilişkisi
5. Alanın gelişim süreci (makalenin referanslarından en az 3 temel kaynak)
6. Kendi yorumlarım ve geliştirme önerileri
7. Kaynakça

**Ek katkı:** Makale Java (Spring, Guice) üzerinde çalışmaktadır. Bu incelemede
seçilen anti-pattern'ler **C# konsol uygulaması** ile örneklenecek; her biri için
hatalı ve düzeltilmiş kod ile birlikte birim testleri (xUnit) hazırlanacaktır.

## Klasör Yapısı (planlanan)

```
notlar/   → makale okuma notları
rapor/    → inceleme raporunun bölümleri
kod/      → C# örnekleri ve testler
sunum/    → sunum dosyası
```

## Durum

- [x] Makale seçimi ve repo kurulumu
- [x] Makale okuma notları: giriş, arka plan ve temel kavramlar
- [ ] Makale okuma notları: anti-pattern kataloğu
- [ ] Rapor bölümleri
- [ ] C# örnekleri ve testler
- [ ] Sunum
