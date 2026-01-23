# WebProjectOOP – ToDo Web API (Öðrenme ve Geliþtirme Projesi)

Bu proje, ASP.NET Core Web API kullanýlarak geliþtirilmiþ bir ToDo (Görev Takip) uygulamasýdýr.
Projenin temel amacý Web API mantýðýný, katmanlý mimariyi, Entity Framework Core kullanýmýný
ve modern backend geliþtirme prensiplerini öðrenerek uygulamaktýr.


--------------------------------------------------------------------

KULLANILAN TEKNOLOJÝLER

- ASP.NET Core Web API
- C#
- Entity Framework Core (EF Core)
- MS SQL Server
- Swagger
- Dependency Injection (DI)

--------------------------------------------------------------------

GENEL MÝMARÝ AKIÞ

Client (Swagger / Postman / React)
        |
        v
Controller (API Katmaný)
        |
        v
Service (Business Katmaný)
        |
        v
DbContext (EF Core)
        |
        v
SQL Server (Veritabaný)

--------------------------------------------------------------------

KATMANLI MÝMARÝ MANTIÐI

Controller:
- HTTP isteklerini karþýlar (GET, POST, PUT, DELETE)
- Ýþ mantýðý içermez
- Service katmanýna yönlendirir
- HTTP response üretir (200, 404 vb.)

Service:
- Ýþ kurallarý burada yazýlýr
- Controller’dan gelen isteði iþler
- Veritabaný iþlemlerini DbContext üzerinden yapar

Entity:
- Veritabanýndaki tablolarýn C# karþýlýðýdýr
- Her nesne (instance) bir satýrý temsil eder

DbContext:
- Uygulama ile veritabaný arasýndaki köprüdür
- EF Core’un kalbidir
- Change Tracker sayesinde entity’leri izler

DTO:
- Client ile API arasýnda veri taþýmak için kullanýlýr
- Entity’ler dýþ dünyaya açýlmaz
- Sadece gerekli alanlar gönderilir

--------------------------------------------------------------------

PROJE KLASÖR YAPISI

WebProjectOOP
|
|-- Controllers
|   |-- TasksController.cs
|
|-- Business
|   |-- Abstract
|   |   |-- ITaskService.cs
|   |
|   |-- Concrete
|       |-- TaskService.cs
|
|-- Entities
|   |-- ToDoTask.cs
|   |
|   |-- Enums
|       |-- TaskState.cs
|
|-- DTOs
|   |-- TaskCreateDto.cs
|   |-- TaskUpdateDto.cs
|   |-- TaskDto.cs
|
|-- DataAccess
|   |-- ToDoContext.cs
|
|-- README.md

--------------------------------------------------------------------

CONTROLLER KAVRAMLARI

- ControllerBase: API controller’lar için temel sýnýf
- [ApiController]: Web API davranýþlarýný otomatikleþtirir
- [Route]: URL adresini belirler
- IActionResult: HTTP response tipidir. HTTP cevabý döndürür. Controller da her zaman kullanýlýr. 
- Ok(): 200 döndürür
- NotFound(): 404 döndürür

Controller içinde:
- new ile nesne oluþturulmaz
- Service constructor injection ile alýnýr

--------------------------------------------------------------------

SERVICE VE INTERFACE

Interface (ITaskService):
- Servisin ne yapacaðýný tanýmlar
- Nasýl yapacaðýný söylemez
- Loose coupling saðlar

Service (TaskService):
- Interface’i uygular
- Ýþ mantýðýný içerir
- DbContext ile çalýþýr

--------------------------------------------------------------------

DEPENDENCY INJECTION (DI)

Amaç:
- new anahtar kelimesini kullanmamak
- Baðýmlýlýklarý dýþarýdan almak

Kayýt:
AddScoped<ITaskService, TaskService>();

AddScoped:
- Her HTTP request için 1 instance
- Request bitince nesne yok edilir
- DbContext için en doðru lifetime

--------------------------------------------------------------------

DBCONTEXT VE EF CORE

DbContext:
- Veritabaný baðlantýsýný yönetir
- DbSet ile tablolarý temsil eder

DbSet<ToDoTask> Tasks:
- SQL tablosunun EF karþýlýðýdýr
- LINQ ile sorgulanabilir

--------------------------------------------------------------------

ENTITY VE ENUM

Entity (ToDoTask):
- Veritabaný tablosu
- Property’ler public get/set olmalýdýr

Enum (TaskState):
- Sabit deðerleri temsil eder
- Magic number/string kullanýmýný engeller

--------------------------------------------------------------------

DTO (DATA TRANSFER OBJECT)

Amaç:
- Client ile API arasýnda veri taþýmak
- Entity’yi dýþ dünyadan gizlemek

Örnek DTO’lar:
- TaskCreateDto
- TaskUpdateDto
- TaskDto

DTO sadece veri taþýr, iþ mantýðý içermez.

--------------------------------------------------------------------

ASYNC / AWAIT VE TASK

async:
- Metodun asenkron çalýþacaðýný belirtir

await:
- Uzun süren iþlemleri bekler
- Thread’i bloklamaz

Task:
- Asenkron iþlem, dönüþ yok

Task<T>:
- Asenkron iþlem, dönüþ var

--------------------------------------------------------------------

OBJECT INITIALIZER

Amaç:
- Nesne oluþtururken property’leri set etmek

Özellikler:
- Constructor’dan hemen sonra çalýþýr
- Property’lerin public set olmasý gerekir

--------------------------------------------------------------------

READONLY

readonly deðiþken:
- Sadece constructor içinde set edilebilir
- Sonradan deðiþtirilemez
- DI ile gelen baðýmlýlýklar için kullanýlýr

--------------------------------------------------------------------

EF CORE CHANGE TRACKER

EF Core entity’leri izler:

Added:
- Yeni kayýt
- Add / AddAsync sonrasý

Modified:
- Güncellenmiþ kayýt

Deleted:
- Silinecek kayýt

SaveChangesAsync:
- Tüm deðiþiklikleri veritabanýna yazar

--------------------------------------------------------------------

CRUD VE HTTP METOTLARI

Create  -> POST   -> [HttpPost]
Read    -> GET    -> [HttpGet]
Update  -> PUT    -> [HttpPut]
Delete  -> DELETE -> [HttpDelete]

--------------------------------------------------------------------

SWAGGER

- API endpoint’lerini test etmek için kullanýlýr
- Otomatik dokümantasyon saðlar
- Request/Response yapýlarý görülebilir

--------------------------------------------------------------------

ÖÐRENÝLEN ANA KAVRAMLAR

- Katmanlý mimari
- Dependency Injection
- Entity Framework Core
- DbContext ve DbSet
- async / await
- Task ve Task<T>
- DTO kullanýmý
- RESTful API mantýðý
- Controller – Service – Entity ayrýmý

--------------------------------------------------------------------

ÖNEMLÝ NOTLAR

- Controller veritabaný bilmez
- Service iþ mantýðýný içerir
- DbContext Scoped olmalýdýr
- DTO ile Entity ayrýmý profesyonel bir zorunluluktur
- Asenkron DB iþlemleri Web API için gereklidir

--------------------------------------------------------------------

ÝLERÝDE EKLENEBÝLECEKLER

- Validation (Required, MinLength vb.)
- GetAll (listeleme) endpoint’i
- AutoMapper
- React frontend entegrasyonu
- Authentication / Authorization


# GENEL C# SYNTAX NOTLARI (ÖÐRENME SAYFASI)

--------------------------------------------------

DEÐÝÞKEN TANIMLAMA

string name = "Ali";
int age = 25;
bool isActive = true;
double price = 12.5m;

var number = 10;           // Derleyici tipi kendisi belirler
const int MaxValue = 100;  // Sabit deðiþken (deðiþtirilemez)

--------------------------------------------------

TÝP DÖNÜÞÜMLERÝ

int a = 5;
double b = a;              // Implicit (otomatik)

double x = 5.6;
int y = (int)x;            // Explicit (cast)

int z = Convert.ToInt32("10");
int.TryParse("20", out int result);

--------------------------------------------------

OPERATÖRLER

Aritmetik:
+  -  *  /  %

Karþýlaþtýrma:
==  !=  >  <  >=  <=

Mantýksal:
&&  ||  !

Atama:
=  +=  -=  *=  /=

--------------------------------------------------

IF / ELSE

if (age > 18)
{
    // kod
}
else if (age == 18)
{
    // kod
}
else
{
    // kod
}

--------------------------------------------------

SWITCH / CASE

switch (age)
{
    case 18:
        break;
    case 20:
        break;
    default:
        break;
}

--------------------------------------------------

LOOP (DÖNGÜLER)

for (int i = 0; i < 10; i++)
{
}

while (true)
{
}

do
{
}
while (false);

foreach (var item in list)
{
}

--------------------------------------------------

METOT (METHOD) TANIMLAMA

void Print()
{
}

int Sum(int a, int b)
{
    return a + b;
}

--------------------------------------------------

ASYNC / AWAIT

public async Task DoWork()
{
    await Task.Delay(1000);
}

public async Task<int> GetNumber()
{
    return 5;
}

--------------------------------------------------

TASK ve TASK<T>

Task        -> Asenkron iþlem, dönüþ yok
Task<T>     -> Asenkron iþlem, dönüþ var

--------------------------------------------------

CLASS TANIMLAMA

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}

--------------------------------------------------

CONSTRUCTOR

public class Person
{
    public Person(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
}

--------------------------------------------------

PROPERTY (ÖZELLÝK)

public string Name { get; set; }

public int Age { get; private set; }

--------------------------------------------------

OBJECT INITIALIZER

var person = new Person
{
    Name = "Ali",
    Age = 25
};

--------------------------------------------------

INTERFACE

public interface IService
{
    void Run();
}

--------------------------------------------------

INTERFACE IMPLEMENTATION

public class MyService : IService
{
    public void Run()
    {
    }
}

--------------------------------------------------

INHERITANCE (MÝRAS)

public class BaseClass
{
}

public class ChildClass : BaseClass
{
}

--------------------------------------------------

ABSTRACT CLASS

public abstract class Animal
{
    public abstract void Speak();
}

--------------------------------------------------

OVERRIDE

public override void Speak()
{
}

--------------------------------------------------

ENUM

public enum Status
{
    Active = 1,
    Passive = 2
}

--------------------------------------------------

STRUCT

public struct Point
{
    public int X;
    public int Y;
}

--------------------------------------------------

LIST / COLLECTIONS

List<int> numbers = new List<int>();

numbers.Add(1);
numbers.Remove(1);

--------------------------------------------------

DICTIONARY

Dictionary<int, string> dict = new Dictionary<int, string>();
dict.Add(1, "One");

--------------------------------------------------

LINQ (TEMEL)

var result = list.Where(x => x > 5).ToList();
var first = list.FirstOrDefault();
var any = list.Any();

--------------------------------------------------

NULL KONTROLLERÝ

if (obj == null)
{
}

obj?.ToString();

string name = obj ?? "default";

--------------------------------------------------

TRY / CATCH

try
{
}
catch (Exception ex)
{
}
finally
{
}

--------------------------------------------------

READONLY

private readonly IService _service;

--------------------------------------------------

ACCESS MODIFIERS (ERÝÞÝM BELÝRLEYÝCÝLER)

public      -> Her yerden
private     -> Sadece class içi
protected   -> Miras alanlar
internal    -> Ayný proje
protected internal -> Karma

--------------------------------------------------

STATIC

public static void Run()
{
}

--------------------------------------------------

DEPENDENCY INJECTION (KULLANIM)

public class MyController
{
    private readonly IService _service;

    public MyController(IService service)
    {
        _service = service;
    }
}

--------------------------------------------------

ATTRIBUTE

[HttpGet]
[HttpPost]
[Route("api/test")]

--------------------------------------------------

NAMESPACE

namespace MyProject.Services
{
}

--------------------------------------------------

USING

using System;
using System.Collections.Generic;

--------------------------------------------------

GENEL NOTLAR

- Class referans tiptir
- Struct value typedir
- Interface sadece imza içerirs
- async metod içinde await kullanýlýr
- Controller iþ mantýðý içermez
- Service iþ mantýðý içerir
- Entity veritabanýný temsil eder
- DTO veri taþýmak içindir

--------------------------------------------------

