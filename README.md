# 🚚 DeliveryService

Десктопное приложение для управления курьерской службой. Разработано на C# WPF.

## Возможности

- **Диспетчерская** — интерактивная карта с позициями курьеров и точками доставки в реальном времени
- **Управление заказами** — создание, назначение курьера, отслеживание статусов
- **Курьеры** — список, статус онлайн/офлайн, статистика по выполненным заказам
- **Симуляция доставки** — визуализация движения курьера по маршруту на карте

## Технологии

| Слой | Технология |
|---|---|
| UI | WPF (.NET 8), XAML |
| Паттерн | MVVM |
| БД | PostgreSQL + Entity Framework Core |
| Карта | GMap.NET (OpenCycleMap) |
| DI | Microsoft.Extensions.DependencyInjection |

## Структура проекта

```
DeliveryService/
├── Models/                  # Сущности БД
│   ├── Order.cs
│   ├── Courier.cs
│   ├── OrderStatusHistory.cs
│   └── RoutePoint.cs
├── Data/                    # EF Core
│   ├── AppDbContext.cs
│   └── Migrations/
├── Repositories/            # Слой доступа к данным
│   ├── OrderRepository.cs
│   └── CourierRepository.cs
├── Services/                # Бизнес-логика
│   ├── OrderService.cs
│   ├── CourierService.cs
│   └── SimulationService.cs
├── ViewModels/              # MVVM
│   ├── BaseViewModel.cs
│   ├── DispatcherViewModel.cs
│   ├── OrderListViewModel.cs
│   └── CourierViewModel.cs
├── Views/                   # XAML экраны
│   ├── DispatcherView.xaml
│   ├── OrderListView.xaml
│   ├── NewOrderView.xaml
│   └── CourierView.xaml
└── App.xaml.cs              # DI регистрация
```

## Запуск

### Требования

- Visual Studio 2022
- .NET 8 SDK
- PostgreSQL 15+

### Установка

1. Клонировать репозиторий:
   ```bash
   git clone https://github.com/aurusxd/DeliveryService
   cd DeliveryService
   ```

2. Создать базу данных в PostgreSQL:
   ```sql
   CREATE DATABASE delivery_db;
   ```

3. Настроить строку подключения в `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "Default": "Host=localhost;Port=5432;Database=delivery_db;Username=postgres;Password=secretpassword"
     }
   }
   ```

4. Применить миграции:
   ```bash
   dotnet ef database update
   ```

5. Открыть `DeliveryService.sln` в Visual Studio и запустить (F5)

## Команда

| Участник | Зона ответственности |
|---|---|
| aurusxd | БД, модели, EF Core, миграции + остальное|
| DanilKozlov1 | Сервисный слой, бизнес-логика, симуляция |
| s-k-1-n-1 | Экраны заказов и создания заказа (UI + ViewModel) |
| romchik-ww | Карта, экран диспетчерской |


---
