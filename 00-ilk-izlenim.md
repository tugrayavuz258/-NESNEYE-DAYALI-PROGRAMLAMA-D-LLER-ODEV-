# İlk İzlenim – Makale Özeti

*Bu not, makalenin özeti (abstract) okunarak hazırlanmıştır. Tam metin okundukça
ayrıntılı notlar eklenecektir.*

## Problem

Dependency Injection (DI), bir sınıfın ihtiyaç duyduğu nesneleri kendisinin
oluşturması yerine dışarıdan almasını sağlar. Amaç, sınıflar arasındaki bağımlılığı
(coupling) azaltmak ve modülerliği artırmaktır. Ancak DI kötü uygulandığında tam
tersi sonuç doğabilir ve bağımlılık artabilir.

Literatürde (özellikle kitap ve bloglarda) bazı DI anti-pattern'lerinden söz
edilmiş, fakat bunların gerçek projelerde ne kadar görüldüğüne ve ne kadar faydalı
olduğuna dair bilimsel kanıt yok.

## Amaç

DI anti-pattern'lerinden ve bunlara karşılık gelen refactoring önerilerinden oluşan
bir katalog önermek ve bu kataloğu değerlendirmek.

## Yöntem (özetten anlaşılan)

1. Daha önce raporlanmış DI anti-pattern'lerini inceleyip eksiklerini belirleme
2. 12 anti-pattern'den oluşan yeni bir katalog önerme
3. Bu anti-pattern'leri tespit eden bir statik kod analiz aracı geliştirme ve açık
   kaynak + endüstriyel projelerde çalıştırma
4. Geliştiricilerle anket yaparak kataloğun önemini ve faydasını ölçme

## Sonuçlar (özetten)

- Analiz aracı %92,19 relative recall ve yüksek precision elde etmiş.
- En az 9 farklı anti-pattern, incelenen projelerde sıkça görülmüş.
- Geliştiriciler kataloğu önemli bulmuş ve bu anti-pattern'leri refactor etmeye
  istekli olduklarını belirtmiş.

## Aklıma gelen sorular (okurken cevaplanacak)

- 12 anti-pattern'in her biri tam olarak nedir, hangi OOP prensibini ihlal ediyor?
- Hangi projeler incelenmiş, neden bu projeler seçilmiş?
- Anti-pattern'ler Java'ya mı özgü, yoksa C# / ASP.NET Core'da da aynen geçerli mi?
- Anket kaç kişiyle yapılmış, sonuçlar genellenebilir mi?
