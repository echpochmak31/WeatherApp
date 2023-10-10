# WeatherApp

Веб приложение на ASP.NET Core.

Особенности:
- В качестве ORM используется Entity Framework Core (с миграциями)
- Для хранения данных выбрана PostgreSQL
- Контейнеризация с использование Docker Compose, в котором развернута сама БД и pgAdmin для удобного взаимодействия с ней
- Аутентификация пользователей с использованием JWT