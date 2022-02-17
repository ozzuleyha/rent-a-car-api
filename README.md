
# Rent a Car Project

Araba kiralama ofisleri ve müşterileri arasındaki işlemleri bu proje içerisinde yapabilmek mümkün. Müşteriler kiralama talebinde bulunabiliyor, çalışanlar talepleri değerlendirebiliyor ve müşteriler kiralama taleplerinin sonuçlarını görebiliyor
## API Kullanımı

#### Login

```http
  POST /api/User/login
```

| Parametre | Tip     | Açıklama                |
| :-------- | :------- | :------------------------- |
| `UserName` | `string` | **Gerekli**. Giriş yapacak kullanıcının kullanıcı adı. |
| `Password` | `string` | **Gerekli**. Giriş yapacak kullanıcının şifresi. |


#### User tablosuna kullanıcı ekleme

```http
  POST /api/User/add-user
```

| Parametre | Tip     | Açıklama                       |
| :-------- | :------- | :-------------------------------- |
| `UserName`      | `string` | **Gerekli**. Eklenecek kullanıcının kullanıcı adı |
| `Password`      | `string` | **Gerekli**. Eklenecek kullanıcının şifresi |
| `UserRoleId`      | `int` | **Gerekli**. Eklenecek kullanıcının kullanıcı tipi |


#### Customer tablosuna kullanıcı kaydetme

```http
  POST /api/Customer/add-customer
```

| Parametre | Tip     | Açıklama                       |
| :-------- | :------- | :-------------------------------- |
| `CustomerName`      | `string` | **Gerekli**. Eklenecek kullanıcının ismi |
| `CustomerSurname`      | `string` | **Gerekli**. Eklenecek kullanıcının soyismi |
| `CustomerEmail`      | `string` | **Gerekli**. Eklenecek kullanıcının emaili |



#### Employee tablosuna kullanıcı kaydetme

```http
  POST /api/Employee/add-employee
```

| Parametre | Tip     | Açıklama                       |
| :-------- | :------- | :-------------------------------- |
| `EmployeeName`      | `string` | **Gerekli**. Eklenecek kullanıcının ismi |
| `EmployeeSurname`      | `string` | **Gerekli**. Eklenecek kullanıcının soyismi |
| `CompanyId`      | `int` | **Gerekli**. Eklenecek kullanıcının bağlı olduğu şirket |


#### Company tablosuna şirket kaydetme

```http
  POST /api/Company/add-company
```

| Parametre | Tip     | Açıklama                       |
| :-------- | :------- | :-------------------------------- |
| `CompanyName`      | `string` | **Gerekli**. Eklenecek şirketin ismi |
| `CompanyCity`      | `string` | **Gerekli**. Eklenecek şirketin bulunduğu şehir |
| `CompanyAdress`      | `string` | **Gerekli**. Eklenecek şirketin adresi |


#### Car tablosuna araç kaydetme

```http
  POST /api/Car/add-car
```

| Parametre | Tip     | Açıklama                       |
| :-------- | :------- | :-------------------------------- |
| `CarName`      | `string` | **Gerekli**. Eklenecek aracın ismi |
| `CarModel`      | `string` | **Gerekli**. Eklenecek aracın modeli |
| `RentPrice`      | `string` | **Gerekli**. Eklenecek aracın kiralama fiyatı |
| `RequiredLicenseAge`      | `int` | **Gerekli**. Eklenecek aracın gerektirdiği sürücü ehliyet yaşı |
| `SeatingCapacity`      | `int` |  Eklenecek aracın koltuk kapasitesi |
| `Airbag`      | `string` |  Eklenecek aracın airbag bilgisi |
| `CompanyId`      | `int` | **Gerekli**. Eklenecek aracın hangi şirkete ait olduğu |


#### RentInformation tablosuna kiralama bilgileri kaydetme

```http
  POST /api/RentInformation/add-rent-information
```

| Parametre | Tip     | Açıklama                       |
| :-------- | :------- | :-------------------------------- |
| `SituationId`      | `int` | **Gerekli**. Kiralama işleminin durumu |
| `RentPrice`      | `int` | **Gerekli**. Kiralama işleminin toplam ücreti |
| `RentCustomerId`      | `int` | **Gerekli**. Kiralayan müşteri bilgisi |
| `RentCarId`      | `int` | **Gerekli**. Kiralanan araç bilgisi |


#### Kiralama taleplerinin sonuçları

```http
  POST /api/RentInformation/rent-result-list
```

| Parametre | Tip     | Açıklama                |
| :-------- | :------- | :------------------------- |
| `CustomerId` | `int` | **Gerekli**. Kendi kiralama talebinin sonuçlarını görmek isteyen kullanıcının bilgisi. |


#### Çalışan silme işlemi

```http
  DELETE /api/Employee/delete-employee
```

| Parametre | Tip     | Açıklama                |
| :-------- | :------- | :------------------------- |
| `EmployeeId` | `int` | **Gerekli**. Girilen id'ye sahip çalışanın silinmesi |


#### Şirket silme işlemi

```http
  DELETE /api/Company/delete-company
```

| Parametre | Tip     | Açıklama                |
| :-------- | :------- | :------------------------- |
| `CompanyId` | `int` | **Gerekli**. Girilen id'ye sahip şirketin silinmesi |


#### Araç silme işlemi

```http
  DELETE /api/Car/delete-car
```

| Parametre | Tip     | Açıklama                |
| :-------- | :------- | :------------------------- |
| `CarId` | `int` | **Gerekli**. Girilen id'ye sahip aracın silinmesi |


#### Çalışan günclleme işlemi

```http
  PUT /api/Employee/update-employee
```

| Parametre | Tip     | Açıklama                |
| :-------- | :------- | :------------------------- |
| `EmployeeId` | `int` | **Gerekli**. Girilen id'ye sahip çalışanın güncellenmesi |
| `EmployeeName`      | `string` | **Gerekli**. Güncellenecek kullanıcının ismi |
| `EmployeeSurname`      | `string` | **Gerekli**. Güncellenecek kullanıcının soyismi |

#### Şirket güncelleme işlemi

```http
  PUT /api/Company/update-company
```

| Parametre | Tip     | Açıklama                |
| :-------- | :------- | :------------------------- |
| `CompanyId` | `int` | **Gerekli**. Girilen id'ye sahip şirketin güncellenmesi |
| `CompanyName`      | `string` | **Gerekli**. Güncellenecek şirketin ismi |
| `CompanyCity`      | `string` | **Gerekli**. Güncellenecek şirketin bulunduğu şehir |
| `CompanyAdress`      | `string` | **Gerekli**. Güncellenecek şirketin adresi |


#### Kiralama bilgisi güncelleme işlemi

```http
  PUT /api/RentInformation/update-rent-information
```

| Parametre | Tip     | Açıklama                |
| :-------- | :------- | :------------------------- |
| `RentId` | `int` | **Gerekli**. Girilen id'ye sahip kiralama bilgisinin güncellenmesi |
| `SituationId`      | `int` | **Gerekli**. Kiralama bilgisinin durum bilgisi |

#### Araç durum bilgisi güncelleme işlemi

```http
  PUT /api/Car/update-car-situation
```

| Parametre | Tip     | Açıklama                |
| :-------- | :------- | :------------------------- |
| `carId` | `int` | **Gerekli**. Girilen id'ye sahip aracın güncellenmesi |
| `SituationId`      | `string` | **Gerekli**. Aracın durum bilgisi |


#### Araç güncelleme işlemi

```http
  PUT /api/Car/update-car
```

| Parametre | Tip     | Açıklama                       |
| :-------- | :------- | :-------------------------------- |
| `carId` | `int` | **Gerekli**. Girilen id'ye sahip aracın güncellenmesi |
| `CarName`      | `string` | **Gerekli**. Aracın güncel ismi |
| `CarModel`      | `string` | **Gerekli**. Aracın güncel modeli |
| `RentPrice`      | `string` | **Gerekli**. Aracın güncel kiralama fiyatı |
| `RequiredLicenseAge`      | `int` | **Gerekli**. Aracın güncel gerektirdiği sürücü ehliyet yaşı |
| `SeatingCapacity`      | `int` |  Aracın güncel koltuk kapasitesi |
| `Airbag`      | `string` |  Aracın güncel airbag bilgisi |


#### Toplam müşteri sayısı

```http
  GET /api/Customer/customer-count
```


#### Toplam çalışan sayısı

```http
  GET /api/Employee/employee-count
```

#### Toplam şirket sayısı

```http
  GET /api/Company/company-count
```

#### Toplam araç sayısı

```http
  GET /api/Car/car-count
```

#### Tüm kullanıcıların listesi

```http
  GET /api/User/user-list
```

#### Tüm müşterilerin listesi

```http
  GET /api/Customer/customer-list
```

#### Tüm çalışanların listesi

```http
  GET /api/Employee/employee-list
```

#### Tüm şirketlerin listesi

```http
  GET /api/Company/company-list
```

#### Tüm araçların listesi

```http
  GET /api/Car/car-list
```

#### Tüm kiralama taleplerinin listesi

```http
  GET /api/RentInformation/rent-request-list
```
## Kullanılan Teknolojiler

**İstemci:** React

**Sunucu:** .NET Core

  