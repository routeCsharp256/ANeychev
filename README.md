# ANeychev
Нейчев Александр

## Workshop 1

[описание workshop 1](https://c_route256.tilda.ws/workshop_1)

### Merchandise service

#### Запуск 

```shell
docker-compose -p "OzonEdu" up -d --build
```

#### Остановка

```shell
docker-compose -p "OzonEdu" down
```

#### Очистка

```shell
docker system prune  -a
```

## Workshop 2

### Задачи

[описание workshop 2](https://c_route256.tilda.ws/workshop_2)

#### Часть 1
 - [x] Добавить проект библиотеки **OzonEdu.Infrastructure**, в которую должны быть реализованы все настройки инфраструктуры сервиса.
 - [x] Добавить **middleware**, который будет логгировать **request** и **response**.
 - [x] Добавить **middleware**, который будет возвращать версию приложения по пути: _"/version"_.
 - [x] Добавить **2 middlewares**, которые будут возвращать **200 Ок** по путям: _"/live"_ и _"/ready"_.
 - [x] Добавить глобальный **exception filter**, который будет отлавливать все необработанные исключения и выдавать response с json, в котором будет наименование исключения (его тип) и стэк-трейс.
 - [x] Добавить **Swagger middleware**.
 - [x] Добавить **interceptor**, который будет логгировать **request** и **response** обычных **unary** вызовов.
 - [x] Подключить библиотеку **OzonEdu.Infrastructure** к проекту сервиса **OzonEdu.MerchandiseService**.

#### Часть 2
 - [x] Добавить проект HTTP API
 - [x] Добавить проект gRPC API