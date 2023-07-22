# ElasticSearch

Tarih: May 27, 2023 1:55 PM
Tip: KonuNotu

**Elasticsearch** : text üzerinden doğrudan arama yapmak yerine, indexler üzerinden arama yapar ve çok hızlı bir şekilde sonuçlar üretir.

ElasticSearch api mantığında sorgular ile çalışır ve cevap döner.

**Kibana  :** ElasticSearche ulaşmak için kullanılan bir arayüzdür.

ElastichSearch  SqlServer ise Kibana SSMS gibi düşünebiliriz.

<aside>
💡 Docker üzerinde ElasticSearch ve Kibana kurmak için

</aside>

```csharp
version: '3.8'
services:
  elasticsearch:
    image: docker.elastic.co/elasticsearch/elasticsearch:8.7.1
    expose:
      - 9200
    environment:
      - xpack.security.enabled=false
      - "discovery.type=single-node"
      - ELASTIC_USERNAME=elastic
      - ELASTIC_PASSWORD=DkIedPPSCb
    networks:
      - es-net
    ports:
      - 9200:9200
    volumes:
      - elasticsearch-data:/usr/share/elasticsearch/data
  kibana:
    image: docker.elastic.co/kibana/kibana:8.7.1
    environment:
      - ELASTICSEARCH_HOSTS=http://elasticsearch:9200
    expose:
      - 5601
    networks:
      - es-net
    depends_on:
      - elasticsearch
    ports:
      - 5601:5601
    volumes:
      - kibana-data:/usr/share/kibana/data
networks:
  es-net:
    driver: bridge
volumes:
  elasticsearch-data:
    driver: local
  kibana-data:
    driver: local
```

<aside>
💡 Create Document —> Data Oluşturma

</aside>

![Untitled](ElasticSearch%205873346b7e1c4f099bbfe14f6df01ebe/Untitled.png)

query

```json
PUT products/_doc/1
{
  "name":"iphone 14",
  "rating":8.5,
  "published":true,
  "category":"mobile phones",
  "price":{
    "usd":2500,
    "eur":2000
  }
}
```

response 201 created

```json
{
  "_index": "products",
  "_id": "1",
  "_version": 1,
  "result": "created",
  "_shards": {
    "total": 2,
    "successful": 1,
    "failed": 0
  },
  "_seq_no": 0,
  "_primary_term": 1
}
```

<aside>
💡 Retrieve Document —> Data Okuma

</aside>

![Untitled](ElasticSearch%205873346b7e1c4f099bbfe14f6df01ebe/Untitled%201.png)

```json
GET products/_doc/1
{
  "_index": "products",
  "_id": "1",
  "_version": 1,
  "_seq_no": 0,
  "_primary_term": 1,
  "found": true,

  "_source": {
    "name": "iphone 14",
    "rating": 8.5,
    "published": true,
    "category": "mobile phones",
    "price": {
      "usd": 2500,
      "eur": 2000
    }
  }
}
```

```json
GET products/_doc/1
{
  "name": "iphone 14",
  "rating": 8.5,
  "published": true,
  "category": "mobile phones",
  "price": {
    "usd": 2500,
    "eur": 2000
  }
}
```

<aside>
💡 Identifiers

</aside>

```csharp
PUT products/_doc/1
//yoksa oluşturur varsa günceller

PUT products/_create/1
//yoksa oluşturur varsa hata döndürür

POST products/_doc
//id'yi kendisi verir
```

<aside>
💡 Güncelleme İşlemi

</aside>

```csharp
PUT products/_doc/1
{
  "name": "iphone 18 UPDATED",
  "rating": 8.5,
  "published": true,
  "category": "mobile phones",
  "price": {
    "usd": 2500,
    "eur": 2000
  }
}

GET products/_source/1
```

<aside>
💡 Silme işlemi

</aside>

```csharp
DELETE products/_doc/2
```

<aside>
💡 Data varlık kontrolü yapmak

</aside>

```csharp
GET products/_doc/2
//sonuç 
{
  "_index": "products",
  "_id": "2",
  "found": false
}
// --------------------------------------------
HEAD products/_doc/2
//sonuç
404 - Not Found
```

<aside>
💡 Tüm datayı getirmek

</aside>

```csharp
GET products/_search
{
  "took": 294,
  "timed_out": false,
  "_shards": {
    "total": 1,
    "successful": 1,
    "skipped": 0,
    "failed": 0
  },
  "hits": {
    "total": {
      "value": 11,
      "relation": "eq"
    },
    "max_score": 1,
    "hits": [
      {
        "_index": "products",
        "_id": "3",
        "_score": 1,
        "_source": {
          "name": "iphone 16",
          "rating": 8.5,
          "published": true,
          "category": "mobile phones",
          "price": {
            "usd": 2500,
            "eur": 2000
          }
        }
      },
      {
        "_index": "products",
        "_id": "4",
        "_score": 1,
        "_source": {
          "name": "iphone 17",
          "rating": 8.5,
          "published": true,
          "category": "mobile phones",
          "price": {
            "usd": 2500,
            "eur": 2000
          }
        }
      },
      {
        "_index": "products",
        "_id": "ov-4dogBBjoAlMkXSZkr",
        "_score": 1,
        "_source": {
          "name": "iphone 18",
          "rating": 8.5,
          "published": true,
          "category": "mobile phones",
          "price": {
            "usd": 2500,
            "eur": 2000
          }
        }
      },
      {
        "_index": "products",
        "_id": "-n-7w4gBgYjlURugsN5w",
        "_score": 1,
        "_source": {
          "name": "iphone 18",
          "rating": 8.5,
          "published": true,
          "category": "mobile phones",
          "price": {
            "usd": 2500,
            "eur": 2000
          }
        }
      },
      {
        "_index": "products",
        "_id": "-3-8w4gBgYjlURugK95k",
        "_score": 1,
        "_source": {
          "name": "iphone 18",
          "rating": 8.5,
          "published": true,
          "category": "mobile phones",
          "price": {
            "usd": 2500,
            "eur": 2000
          }
        }
      },
      {
        "_index": "products",
        "_id": "_H-8w4gBgYjlURugOt7u",
        "_score": 1,
        "_source": {
          "name": "iphone 18",
          "rating": 8.5,
          "published": true,
          "category": "mobile phones",
          "price": {
            "usd": 2500,
            "eur": 2000
          }
        }
      },
      {
        "_index": "products",
        "_id": "_X-8w4gBgYjlURugPd6G",
        "_score": 1,
        "_source": {
          "name": "iphone 18",
          "rating": 8.5,
          "published": true,
          "category": "mobile phones",
          "price": {
            "usd": 2500,
            "eur": 2000
          }
        }
      },
      {
        "_index": "products",
        "_id": "_n-8w4gBgYjlURugQt4E",
        "_score": 1,
        "_source": {
          "name": "iphone 18",
          "rating": 8.5,
          "published": true,
          "category": "mobile phones",
          "price": {
            "usd": 2500,
            "eur": 2000
          }
        }
      },
      {
        "_index": "products",
        "_id": "_3-8w4gBgYjlURugRN5n",
        "_score": 1,
        "_source": {
          "name": "iphone 18",
          "rating": 8.5,
          "published": true,
          "category": "mobile phones",
          "price": {
            "usd": 2500,
            "eur": 2000
          }
        }
      },
      {
        "_index": "products",
        "_id": "AH-8w4gBgYjlURugVN-V",
        "_score": 1,
        "_source": {
          "name": "iphone 18",
          "rating": 8.5,
          "published": true,
          "category": "mobile phones",
          "price": {
            "usd": 2500,
            "eur": 2000
          }
        }
      }
    ]
  }
}
```

<aside>
💡 Veri Tipleri

</aside>

![Untitled](ElasticSearch%205873346b7e1c4f099bbfe14f6df01ebe/Untitled%202.png)

![Untitled](ElasticSearch%205873346b7e1c4f099bbfe14f6df01ebe/Untitled%203.png)

<aside>
💡 Query arama şekilleri

</aside>

![Untitled](ElasticSearch%205873346b7e1c4f099bbfe14f6df01ebe/Untitled%204.png)