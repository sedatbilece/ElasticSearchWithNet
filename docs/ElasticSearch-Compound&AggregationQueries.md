# ElasticSearch - Compound&Aggregation Queries

Tarih: July 6, 2023 9:21 AM
Tip: KonuNotu

must : aranan değerlerde kesinlikle olmalı , skora katkı sağlar .

filter : aranan değerlerde kesinlikle olmalı , skora katkı sağlamaz .

should : Zorunlu değil ama eğer var ise skora katkı sağlar.

must_not : aranan değerde o keyword olmamalı

örnek :

![Untitled](ElasticSearch%20-%20Compound&Aggregation%20Queries%2026561aab31954311a52d45d1cbd90148/Untitled.png)

<aside>
💡 Bucket Aggregations terms query

</aside>

kategorisi x olan dataları getir veya kategorisi x olan kaç data var gibi sorgularda kullanılır

![Untitled](ElasticSearch%20-%20Compound&Aggregation%20Queries%2026561aab31954311a52d45d1cbd90148/Untitled%201.png)

<aside>
💡 Bucket Aggregations range query

</aside>

belirli değersel aralıklardaki verileri getirmek için kullanılır

![Untitled](ElasticSearch%20-%20Compound&Aggregation%20Queries%2026561aab31954311a52d45d1cbd90148/Untitled%202.png)

<aside>
💡 Metric Aggregations

</aside>

üstteki query çalışır (where sorgusu gibi) arkasından aggs ile  avg,sum,max,min işlemlerinden biri yapılabilir. tüm datada aranacaksa query sorgusu kaldırılabilir.

![Untitled](ElasticSearch%20-%20Compound&Aggregation%20Queries%2026561aab31954311a52d45d1cbd90148/Untitled%203.png)