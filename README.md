# C# Örnekleri: MVC Smell'leri

Makaledeki 6 MVC smell'inin C# ile örneklenmiş halleri. Her örnekte smell'li (❌) ve
düzeltilmiş (✅) sürüm yan yana verilmiştir. Veritabanı, `SqlVeritabani` adlı basit bir
sınıfla temsil edilmiştir (gerçek projede ADO.NET / Entity Framework olurdu).

| Klasör | Smell'ler |
|---|---|
| `01-controller-ve-service-smelleri` | Brain Controller, Meddling Service (Promiscuous Controller notlarda) |
| `02-repository-smelleri` | Brain Repository, Laborious Repository Method, Fat Repository |

Çalıştırmak için (.NET 8 SDK): klasöre girip `dotnet run`.
