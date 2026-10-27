# 1. Giriş

## 1.1 İncelenen Makale

> Aniche, M., Bavota, G., Treude, C., Gerosa, M. A., & van Deursen, A. (2018).
> Code smells for Model-View-Controller architectures. *Empirical Software Engineering*,
> 23(4), 2121–2157. https://doi.org/10.1007/s10664-017-9540-2

Makale, Web of Science'ta **Science Citation Index Expanded (SCIE)** kapsamında taranan
*Empirical Software Engineering* dergisinde yayımlanmıştır. Çalışma, yazarların 2016
yılında ICSME konferansında sundukları bildirinin genişletilmiş dergi sürümüdür.

## 1.2 Konunun Arka Planı

Nesneye dayalı programlamada iyi bir tasarımın temel hedeflerinden biri,
sorumlulukların doğru sınıflara dağıtılmasıdır. Tek sorumluluk prensibi, kapsülleme ve
katmanlı mimari bu hedefe hizmet eden temel fikirlerdir. **Model-View-Controller (MVC)**
deseni de bir uygulamayı veri ve iş kuralları (Model), kullanıcıya gösterim (View) ve
bu ikisi arasındaki akış (Controller) olmak üzere üç ayrı sorumluluğa bölen bir mimari
desendir. İlk olarak 1980'lerde Smalltalk-80 dilinde masaüstü arayüzler için tanımlanan
MVC, bugün Spring MVC (Java), ASP.NET MVC (C#) ve Ruby on Rails gibi web
framework'lerinin temelini oluşturmaktadır.

Bir yazılımın tasarımında ileride bakım sorunlarına yol açabilecek zayıflıklar
literatürde **code smell (kod kokusu)** olarak adlandırılır. Kod kokusu bir hata
değildir: kod doğru çalışır, ancak yapısı değiştirilmesi zor, hataya açık ya da
anlaşılması güç bir hal almıştır. Fowler (1999) bu kavramı 22 genel kod kokusuyla
tanımlamış, sonraki çalışmalar bu kokuların değişim ve hata eğilimiyle ilişkisini
göstermiştir (Khomh vd., 2012).

## 1.3 Bu İncelemenin Amacı

Fowler'ın kataloğu gibi genel kod kokuları, bir sınıfın **mimarideki rolünü** dikkate
almaz. Oysa MVC'de bir Controller sınıfında kabul edilemeyecek bir yapı (ör. uzun bir
iş kuralı zinciri), bir Service sınıfında son derece olağan olabilir. İncelenen makale
bu boşluktan yola çıkarak **MVC mimarisine özgü altı kod kokusu** tanımlamakta ve bu
kokuların gerçek projelerdeki etkilerini ampirik olarak değerlendirmektedir.

Bu rapor, söz konusu makaleyi; amacı ve araştırma soruları, yöntemi, bulguları,
nesneye dayalı programlama kavramlarıyla ilişkisi ve alandaki yeri açısından
incelemektedir. Makale Java (Spring MVC) projeleri üzerinde çalıştığından, raporda
ayrıca altı kod kokusu **C#** ile örneklenmiş ve makalenin genellenebilirlik bulgusu
(ASP.NET MVC) bu örneklerle ilişkilendirilmiştir.

## 1.4 Raporun Yapısı

- **Bölüm 2:** Makalenin temel konusu, amacı ve araştırma soruları
- **Bölüm 3:** Kullanılan yöntem ve teknolojik yaklaşımlar
- **Bölüm 4:** Bulguların değerlendirilmesi
- **Bölüm 5:** Makaledeki fikirlerin Nesneye Dayalı Programlama ile ilişkisi (C# örnekleriyle)
- **Bölüm 6:** Temel kaynaklar üzerinden alanın gelişim süreci
- **Bölüm 7:** Yorumlar ve geliştirme önerileri
- **Kaynakça**
