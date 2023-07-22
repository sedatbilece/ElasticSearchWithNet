# ElasticSearch - Term Level Queries

Tarih: June 25, 2023 5:33 PM
Tip: KonuNotu

Genelde term level queryler tam eşleşen data aramak iin kullanılır.

<aside>
💡 arama işlemi

</aside>

birebir eşleme için eğer aranacak değerin text ve keyword 2 tipi birden varsa keyword tipinde aranmalı 

Çünkü : keyword birebir eşleme için iken text tipi indexleme yapar ve eşleşme oranı gibi değerlerde döndürür.

```json
POST kibana_sample_data_ecommerce/_search
{
  "query":{
    "term": {
      "customer_first_name.keyword": {
        "value": "Sonya"
      }
    }
  }
}
```

<aside>
💡 birden fazla parametre ile arama için

</aside>

```json
POST kibana_sample_data_ecommerce/_search
{
  "query":{
    "terms": {
      "customer_first_name.keyword": ["Sonya","Youssef"]
    }
  }
}
```

<aside>
💡 ID’ye göre arama

</aside>

```json
POST kibana_sample_data_ecommerce/_search
{
  "query":{
    "ids": {
      "values": ["fnXIgBQmIHourAe98L","4OfnXIgBQmIHourAe98M"]
    }
  }
}
```

<aside>
💡 bir özelliğin kayıtlarda olup olmadığının kontrolü

</aside>

```json
POST kibana_sample_data_ecommerce/_search
{
  "query":{
    "exists": {
      "field": "order_id"
    }
  }
```

<aside>
💡 aralıklı arama

</aside>

![Untitled](ElasticSearch%20-%20Term%20Level%20Queries%20d7bf6ec8d6124c56b737a2bba1fab74e/Untitled.png)

<aside>
💡 Benzerlik ile arama  (SQL LIKE metodu gibi)

</aside>

![Untitled](ElasticSearch%20-%20Term%20Level%20Queries%20d7bf6ec8d6124c56b737a2bba1fab74e/Untitled%201.png)

<aside>
💡 aramalardaki kelimeye denk gelmeyen hataları telafi etmek için

</aside>

![Untitled](ElasticSearch%20-%20Term%20Level%20Queries%20d7bf6ec8d6124c56b737a2bba1fab74e/Untitled%202.png)

```json

```