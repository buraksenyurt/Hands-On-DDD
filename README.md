# Hands-On-DDD

C# ile basit bir Domain Driven Design macerası. Senaryoda ikinci el kitaplara ait ilanların verildiği bir platform söz konusu. Referans kaynak olarak Alexey Zimarev'in 2019 yılında Packt yayınlarından çıkan Hands-On Domain Driven Design with .NET Core kitabı baz alınmıştır. C# ve .Net'in güncel bazı özellikleri de işin içerisine katılmaya çalışılmıştır. Orjinal kitap RavenDb ve Postgresql tabanlı yaklaşımları ele alır ama ya EF ya RavenDb ile ilerlenir. Ben domain bazında bazı context içeriklerini farklı şekillerde ele almak istedim. Örneğin ikinci el kitap satışı ile ilgili olanları MongoDb üzerinde doküman formatında yönetmek, üyelik işlemleri içinse Postgresl kullanmak istedim. Bu yer yer zorluklara sebep oldu. Sistemdeki tüm bileşenler, ihtiyaç duyduğu tüm diğer bileşenleri DI Container üzerinden aldığından tek IUnitOfWork implementasyonları işe yaramayacaktı. Dolayısıyla MongoDb ve Postgresql için hem repository hem unit of work bileşenleri ayrı interface türleri üzerinden yönetilmekte. Bu DDD tasarımına aykırı görünmüyor.

## Veritabanı

Bu çalışmada ikinci el kitap satış tarafı için Mongodb veritabanı tercih edilmiştir.

```bash
docker compose up -d
```

Örnek bir swagger testini takiben MongoDb terminaline bağlanarak verinin oluşup oluşmadığı kontrol edilebilir.

```bash
docker exec -it mongodb mongosh -u scoth -p tiger1234 --authenticationDatabase admin

use BargainStore
db.Books.find()
```

![Mongo Db Runtime](MongoDbRuntime.png)

Senaryoyu farklılaştırmak adınma membership management kısmında Postgresql kullanımı tercih edilmiştir. Hem MongoDb hem Postgresql servisleri docker compose aracılığı ile ayağa kaldırılır.

## Postgresql

Member profile tarafı Entity Framework üzerinden postgresql kullanmaktadır. Migration işlemleri için ef aracından yararlanılabilir. Aşağıdaki komutları App klasöründeyken çalıştırmak yeterlidir.

```bash
# Eğer App projesinde yoksa eklenmeli
 dotnet add package Microsoft.EntityFrameworkCore.Design

 # Migration planını hazırlamak için
 dotnet ef migrations add Initial

 # Migration'ı işletmek için
 dotnet ef database update
 ```

## Testler

Api tarafını test etmek için komut satırından curl kullanılabilir veya Swagger arabiriminden ilerlenebilir ya da Postman aracı ele alınabilir. Güncel api talepleri için [buradaki](Hands%20on%20DDD.postman_collection.json) postman çıktısını kullanabilirsiniz.
