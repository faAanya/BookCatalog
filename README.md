# Book Library Management System

## Описание

Backend-приложение для управления библиотекой книг, которое позволит пользователям вести учет книг, авторов и жанров.

## Установка

Для запуска приложения используется Docker. Он предварительно должен быть установлен на компьютер. 

**1. Склонируйте репозиторий:**
```bash
git clone https://github.com/faAanya/modsenBookCatalog.git 
```
Или скачайте modsenBookCatalog-master.zip

**2. Перейдите в папку проекта**
```bash
cd modsenBookCatalog-master
```
**3. Запуск Докера**

Соберите проект:
```bash
docker-compose build
```

Запустите контейнер: 
```bash
docker-compose up
```

Откройте приложение в браузере по адресу:
```bash
http://localhost:5173/books
```

Остановите контейнер:
```bash
docker-compose down
```