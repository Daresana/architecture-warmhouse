# Приложение для работы с датчиками

Нужны Docker и Docker Compose. Порты 8080 и 8081 должны быть свободны.

Из папки `apps`:

```bash
docker compose up --build -d
docker compose ps
```

Монолит: `http://localhost:8080`. Сервис температуры: `http://localhost:8081`.
PostgreSQL использует именованный том. Скрипт `smart_home/init.sql` создаёт базу `smarthome` и таблицы при первом запуске с пустым томом.

```bash
curl "http://localhost:8081/temperature?location=Bedroom"
curl "http://localhost:8081/temperature?sensorId=1"
curl "http://localhost:8081/temperature/2"
```

Комнаты и ID: `Living Room` — `1`, `Bedroom` — `2`, `Kitchen` — `3`. Для неизвестной комнаты используется ID `0`, для неизвестного ID — комната `Unknown`.

Импортировать `smarthome-api.postman_collection.json` в Postman. Вызвать `Create Sensor`, затем повторить `Get All Sensors`. Создание сохраняет датчик; чтение запрашивает новое показание у сервиса температуры.

```bash
curl -X POST "http://localhost:8080/api/v1/sensors" -H "Content-Type: application/json" -d '{"name":"Bedroom sensor","type":"temperature","location":"Bedroom","unit":"C"}'
curl "http://localhost:8080/api/v1/sensors"
```

Просмотр журналов и остановка:

```bash
docker compose logs app temperature-api postgres
docker compose down
```

Команда `down` сохраняет том PostgreSQL. Учётные данные в Compose предназначены для локального учебного запуска.
