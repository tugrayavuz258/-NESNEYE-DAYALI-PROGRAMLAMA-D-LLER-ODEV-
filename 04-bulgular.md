# 4. Bulguların Değerlendirilmesi

## 4.1 Değişim ve hata eğilimi (RQ1–RQ2)
MVC kokulu sınıfların temiz sınıflara göre odds oranları (`*` p < 0,05):

| | Tümü | Orta/Büyük | Büyük |
|---|---|---|---|
| Değişim | 2,97* | 1,42* | 1,60* |
| Hata | 2,05* | 0,72 | 1,06 |

- **RQ1:** Kokulu sınıfların değişme ihtimali belirgin şekilde yüksektir ve fark **büyüklük kontrol edildiğinde de anlamlı** kalmaktadır. En güçlü ilişki **Brain Repository**'dedir (OR 5,08*; büyük sınıflarda 2,79*).
- **RQ2:** Hata ilişkisi daha zayıftır; büyüklük kontrolünden sonra genel fark anlamlılığını yitirir. Büyük sınıflarda hata eğilimi anlamlı kalan **tek koku Meddling Service**'tir (OR 2,53*).
- Geleneksel kokular daha yüksek oranlar göstermektedir; bu nedenle MVC kokuları genel kokuların **yerine değil, yanına** eklenmesi gereken tamamlayıcı bir katalog olarak değerlendirilmelidir.

Not: Değişim sık görülen bir olay olduğundan odds oranı, olasılıklar arasındaki farkı olduğundan büyük gösterebilir; sonuçlar "x kat daha olası" değil, "odds oranı x" biçiminde okunmalıdır.

## 4.2 Geliştirici algısı (RQ3)
Temiz sınıfların ciddiyet medyanı **0**, MVC kokulu sınıflarınki **4**'tür (p < 0,001, d = 0,56 – büyük etki). MVC kokuları ile geleneksel kokular arasında anlamlı fark yoktur (p = 0,21). En ciddi bulunan kokular Meddling Service, Fat Repository ve Brain Controller; en az fark edilen Laborious Repository Method'dur.

## 4.3 Ömür ve ortaya çıkış (RQ4–RQ5)
- 1.337 koku örneğinin **928'i (%69) hiç kaldırılmamıştır**; her koku türünün 500 günden uzun yaşama ihtimali %50'nin üzerindedir.
- Kokulu sınıfların **%65'i ilk commit'ten itibaren** kokuludur. Brain Controller (%42,6) ve Brain Repository (%43,7) ise çoğunlukla sonradan, bakım sırasında büyümektedir.
- Bu iki bulgu birlikte, kontrolün kod **ilk yazılırken** (ör. kod incelemesinde) yapılması gerektiğine işaret etmektedir.

## 4.4 Genellenebilirlik (RQ6)
Promiscuous ve Brain Controller dört framework'te de geçerli bulunmuştur. Active Record kullanan Rails ve Play!'de Repository kokuları uyarlama gerektirmekte, Meddling Service ise geçerli olmamaktadır. **ASP.NET MVC (C#) uzmanı altı kokunun tamamını geçerli** bulmuş ve Meddling Service'i günlük işinde sık gördüğünü belirtmiştir.

## 4.5 Genel değerlendirme
Makalenin en güçlü bulguları değişim eğilimi (RQ1), ömür (RQ4) ve ortaya çıkış (RQ5) üzerinedir. Hata ilişkisi (RQ2) yalnızca 16 projeye ve anahtar kelimeye dayalı bir yönteme, genellenebilirlik (RQ6) ise framework başına tek uzmana dayandığından daha temkinli yorumlanmalıdır. Yazarların bu sınırlılıkları açıkça belirtmesi çalışmanın güvenilirliğini artırmaktadır.
