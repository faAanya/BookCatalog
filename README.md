# Book Library Management System

## Описание

Необходимо разработать backend-приложение для управления библиотекой книг, которое позволит пользователям вести учет книг, авторов и жанров.

## Функционал Web API

- Получение списка всех книг;
- Получение конкретной книги по её Id;
- Добавление новой книги в каталог;
- Редактирование информации о существующей книге;
- Удаление книги из каталога;
- Работа с обложками книг (загрузка, хранение и получение изображений обложек).

## Стек технологий

- **C#** & **.NET CORE 8** - язык и фреймворк для создания кроссплатформенных серверных приложений;
- **PostgreSQL** - реляционная база данных для хранения и управления данными;
- **ASP.NET** - фреймворк для построения RESTful API на платформе .NET;
- **EntityFramework Core** - ORM для работы с базой данных через C#-код;
- **xUnit** - инструмент для написания и запуска модульных тестов;
- **Docker** - платформа для контейнеризации и удобного развертывания приложений.

## Дополнительный функционал

- Система аутентификации с использованием **JWT**;
- Валидация всех входных данных;
- Расширенный поиск книг с возможностью:
- Фильтрации по автору, жанру, году издания;
- Сортировки по названию, году издания;
- Постраничной выдачи результатов;
- Управление авторами и жанрами (CRUD операции).

## Модели

```csharp
public interface IEntity
{
    public Guid Id { get; set; }
}

public class Book : IEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string ISBN { get; set; }
    public int PublicationYear { get; set; }
    public List<Author> Authors { get; set; }
    public List<Genre> Genres { get; set; }
    public string CoverImageUrl { get; set; }
    public int PageCount { get; set; }
}

public class Author : IEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Biography { get; set; }
}

public class Genre : IEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
}
```

## Так же проект предполагает

- Строгое соответствие принципам **RESTful API**;
- Глобальная обработка ошибок через **middleware**;
- Следование **GitFlow** в процессе разработки;
- Следование **Conventional Commits** в процессе разработки;
- Использование **Docker** и **Docker-compose**;
- Cоблюдение принципов **SOLID**.

**ВАЖНО!** Реализация должна находиться на **приватном** репозитории, на который необходимо добавить <code>SU-MCC</code> аккаунт.

## Полезные источники
- [C#](https://learn.microsoft.com/ru-ru/dotnet/csharp/)
- [ASP.NET](https://learn.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-9.0)
- [PostgreSQL](https://www.postgresql.org/docs/)
- [EntityFramework](https://learn.microsoft.com/ru-ru/ef/)
- [JWT](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/configure-jwt-bearer-authentication?view=aspnetcore-9.0)
- [xUnit](https://learn.microsoft.com/ru-ru/dotnet/core/testing/unit-testing-csharp-with-xunit)
- [Docker](https://www.docker.com/)
- [GitFlow](https://www.atlassian.com/ru/git/tutorials/comparing-workflows/gitflow-workflow)
- [Conventional Commits](https://www.conventionalcommits.org/en/v1.0.0/)
