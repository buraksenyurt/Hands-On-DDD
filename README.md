# Hands-On-DDD

C# ile basit bir Domain Driven Design macerası. Senaryoda ikinci el kitaplara ait ilanların verildiği bir platform söz konusu.

Referans kaynak olarak Alexey Zimarev'in 2019 yılında Packt yayınlarından çıkan Hands-On Domain Driven Design with .NET Core kitabı baz alınmıştır. C# ve .Net'in güncel bazı özellikleri de işin içerisine katılmaya çalışılmıştır.

## Veritabanı

Bu çalışmada Mongodb veritabanı tercih edilmiştir.

```bash
docker compose up -d
```

## Testler

Api tarafı için örnek curl komutları.

```bash
curl -X 'POST' \
  'https://localhost:5197/api/notice' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "ownerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "createdDate": "2025-05-09T08:16:58.987Z"
}'
```

```bash
curl -X 'PUT' \
  'https://localhost:5197/api/notice/title' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "title": "Programming with C#"
}'
```

```bash
curl -X 'PUT' \
  'https://localhost:5197/api/notice/summary' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "summary": "Temiz yaprakları sararmamış, ilk bin baskıdan."
}'
```

```bash
curl -X 'PUT' \
  'https://localhost:5197/api/notice/sales-price' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "currencyCode": "TL",
  "salesPrice": 500.00
}'
```

```bash
curl -X 'PUT' \
  'https://localhost:5197/api/notice/request-to-publish' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "sentDate": "2025-05-10T08:18:04.006Z"
}'
```
