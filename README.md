# WPF CRUD Приложение с GUID идентификаторами

Современное WPF приложение для управления товарами с использованием GUID идентификаторов, Entity Framework Core, Dependency Injection и WPF-UI.

## Технологии

- **.NET 8.0** - Основная платформа
- **WPF** - Пользовательский интерфейс
- **Entity Framework Core 9.0** - ORM для работы с базой данных
- **Microsoft SQL Server** - База данных
- **WPF-UI** - Современные UI компоненты
- **Dependency Injection** - Внедрение зависимостей
- **GUID (Globally Unique Identifier)** - Уникальные идентификаторы

## Возможности

- **Создание товаров** - Добавление новых товаров с автоматической генерацией GUID
- **Просмотр товаров** - Отображение списка всех товаров в DataGrid с UID
- **Редактирование товаров** - Обновление информации о существующих товарах
- **Удаление товаров** - Удаление товаров из базы данных по UID
- **Валидация данных** - Проверка корректности введенных данных
- **Современный UI** - Использование WPF-UI для красивого интерфейса
- **Безопасность** - GUID обеспечивают непредсказуемые идентификаторы

## Блок-схема работы приложения

```
    ┌─────────────┐
    │   Начало    │
    └─────┬───────┘
          │
          ▼
    ┌─────────────┐
    │ Инициализация│
    │ приложения   │
    └─────┬───────┘
          │
          ▼
    ┌─────────────┐
    │ Настройка DI │
    │ контейнера   │
    └─────┬───────┘
          │
          ▼
    ┌─────────────┐
    │ Подключение  │
    │ к БД         │
    └─────┬───────┘
          │
          ▼
    ┌─────────────┐
    │ Создание БД  │
    │ (если нужно) │
    └─────┬───────┘
          │
          ▼
    ┌─────────────┐
    │ Загрузка     │
    │ главного окна│
    └─────┬───────┘
          │
          ▼
    ┌─────────────┐
    │ Загрузка     │
    │ товаров из БД│
    └─────┬───────┘
          │
          ▼
    ┌─────────────┐
    │ Отображение  │
    │ в DataGrid   │
    └─────┬───────┘
          │
          ▼
    ┌─────────────┐
    │ Ожидание     │
    │ действий     │
    │ пользователя │
    └─────┬───────┘
          │
    ┌─────┴─────┬─────────┬─────────┬─────────┐
    │           │         │         │         │
    ▼           ▼         ▼         ▼         ▼
┌─────────┐ ┌─────────┐ ┌─────────┐ ┌─────────┐ ┌─────────┐
│Создание │ │Выбор    │ │Редакти- │ │Удаление │ │Обновле- │
│товара   │ │товара   │ │рование  │ │товара   │ │ние      │
│         │ │         │ │товара   │ │         │ │списка   │
└─────┬───┘ └─────┬───┘ └─────┬───┘ └─────┬───┘ └─────┬───┘
      │           │           │           │           │
      ▼           ▼           ▼           ▼           │
┌─────────┐ ┌─────────┐ ┌─────────┐ ┌─────────┐       │
│Генерация│ │Заполне- │ │Валида-  │ │Подтвер- │       │
│GUID UID │ │ние формы│ │ция      │ │ждение   │       │
└─────┬───┘ └─────┬───┘ └─────┬───┘ └─────┬───┘       │
      │           │           │           │           │
      ▼           │           ▼           ▼           │
┌─────────┐       │     ┌─────────┐ ┌─────────┐       │
│Валида-  │       │     │Сохране- │ │Удаление │       │
│ция      │       │     │ние в БД │ │из БД    │       │
└─────┬───┘       │     └─────┬───┘ └─────┬───┘       │
      │           │           │           │           │
      ▼           │           ▼           ▼           │
┌─────────┐       │     ┌─────────┐ ┌─────────┐       │
│Сохране- │       │     │Обновле- │ │Обновле- │       │
│ние в БД │       │     │ние      │ │ние      │       │
└─────┬───┘       │     │списка   │ │списка   │       │
      │           │     └─────┬───┘ └─────┬───┘       │
      ▼           │           │           │           │
┌─────────┐       │           ▼           ▼           │
│Обновле- │       │     ┌─────────┐ ┌─────────┐       │
│ние      │       │     │Очистка  │ │Очистка  │       │
│списка   │       │     │формы    │ │формы    │       │
└─────┬───┘       │     └─────┬───┘ └─────┬───┘       │
      │           │           │           │           │
      ▼           │           ▼           ▼           │
┌─────────┐       │           └─────┬─────┘           │
│Очистка  │       │                 │                 │
│формы    │       │                 │                 │
└─────┬───┘       │                 │                 │
      │           │                 │                 │
      └─────┬─────┘                 │                 │
            │                       │                 │
            └───────────────────────┼─────────────────┘
                                    │
                                    ▼
                              ┌─────────┐
                              │Продол-  │
                              │жение    │
                              │работы?  │
                              └─────┬───┘
                                    │
                               ┌────┴────┐
                               │ Да │ Нет │
                               ▼    │    ▼
                         ┌─────────┐│┌─────────┐
                         │Возврат  │││ Конец   │
                         │к ожида- │││         │
                         │нию      │││         │
                         └─────┬───┘│└─────────┘
                               │    │
                               └────┘
```

## Архитектурные преимущества GUID

### Почему GUID вместо INT ID?

1. **Глобальная уникальность** - GUID гарантированно уникальны во всех системах
2. **Безопасность** - Непредсказуемые идентификаторы предотвращают атаки перебора
3. **Генерация на клиенте** - Можно создавать идентификаторы без обращения к БД
4. **Масштабируемость** - Подходят для распределенных систем
5. **Конфиденциальность** - Не раскрывают информацию о количестве записей

### Структура GUID
```
xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx
```
- 128-битный идентификатор
- 36 символов в строковом представлении
- Пример: `11111111-1111-1111-1111-111111111111`

## Структура проекта

```
WpfApp1/
├── Data/
│   ├── ApplicationDbContext.cs          # DbContext с настройкой GUID
│   └── ApplicationDbContextFactory.cs   # Фабрика для EF Tools
├── Models/
│   └── Product.cs                       # Модель товара с Guid Uid
├── Services/
│   ├── IProductService.cs               # Интерфейс с методами для GUID
│   └── ProductService.cs                # Реализация сервиса с GUID
├── Windows/
│   ├── MainWindow.xaml                  # UI с привязкой к Uid
│   └── MainWindow.xaml.cs               # Логика с генерацией GUID
├── Migrations/                          # Миграции с GUID схемой
├── App.xaml                            # Конфигурация приложения
├── App.xaml.cs                         # Настройка DI и запуск
└── README.md                           # Документация
```

## Настройка базы данных

Приложение использует **SQL Server LocalDB** с GUID в качестве первичного ключа:

```csharp
// В ApplicationDbContext.cs
modelBuilder.Entity<Product>(entity =>
{
    entity.HasKey(e => e.Uid);  // GUID как первичный ключ
    entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
    entity.Property(e => e.Description).HasMaxLength(500);
    entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
    entity.Property(e => e.CreatedDate).HasDefaultValueSql("GETDATE()");
});
```

### Строка подключения

```csharp
options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=WpfApp1Db;Trusted_Connection=true;");
```

## Запуск приложения

1. **Клонируйте репозиторий**
2. **Откройте проект** в Visual Studio или VS Code
3. **Восстановите пакеты NuGet**:
   ```bash
   dotnet restore
   ```
4. **Создайте и примените миграцию**:
   ```bash
   dotnet ef database update
   ```
5. **Запустите приложение**:
   ```bash
   dotnet run
   ```

## Использование

### Добавление товара
1. Заполните поля: Название, Описание, Цена, Количество
2. Нажмите **"Сохранить"**
3. Система автоматически сгенерирует уникальный GUID UID
4. Товар появится в списке с новым UID

### Редактирование товара
1. Выберите товар в таблице (по UID)
2. Данные загрузятся в форму
3. Измените нужные поля
4. Нажмите **"Обновить"**

### Удаление товара
1. Выберите товар по UID
2. Нажмите **"Удалить"**
3. Подтвердите удаление

### Просмотр UID
- В таблице отображается полный GUID UID
- Ширина колонки увеличена для отображения полного идентификатора
- UID используется для всех операций CRUD

## Особенности реализации с GUID

### Генерация GUID в коде
```csharp
// При создании нового товара
var newProduct = new Product
{
    Uid = Guid.NewGuid(),  // Автоматическая генерация GUID
    Name = NameTextBox.Text,
    // ... другие свойства
};
```

### Поиск по GUID
```csharp
// В сервисе
public async Task<Product?> GetProductByUidAsync(Guid uid)
{
    return await _context.Products.FindAsync(uid);
}
```

### Тестовые данные
```csharp
// Статические GUID для тестовых данных
new Product { 
    Uid = new Guid("11111111-1111-1111-1111-111111111111"), 
    Name = "Ноутбук", 
    // ...
}
```

## Решение проблем

### Ошибки миграций с GUID
- Используйте статические GUID в `HasData()`
- Избегайте `Guid.NewGuid()` в миграциях
- Используйте `new Guid("строка")` для фиксированных значений

### Проблемы с отображением GUID
- Увеличьте ширину колонки в DataGrid
- GUID отображаются в формате: `xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx`

### Производительность с GUID
- GUID занимают больше места (16 байт vs 4 байта для int)
- Используйте индексы для оптимизации поиска
- Рассмотрите Sequential GUID для лучшей производительности

## Пакеты NuGet

- `Microsoft.Extensions.DependencyInjection` - Dependency Injection
- `Microsoft.EntityFrameworkCore.SqlServer` - EF Core для SQL Server
- `Microsoft.EntityFrameworkCore.Design` - EF Core Tools
- `WPF-UI` - Современные UI компоненты

## Команды Entity Framework с GUID

```bash
# Создание миграции
dotnet ef migrations add InitialCreateWithGuid

# Применение миграции
dotnet ef database update

# Удаление миграции
dotnet ef migrations remove

# Пересоздание базы данных
dotnet ef database drop --force
dotnet ef database update
```

## Автор

Создано с использованием современных практик разработки WPF приложений и архитектурных принципов с GUID идентификаторами для обеспечения масштабируемости и безопасности. 

## 🔄 Современная стратегия обновления данных

Приложение использует `ExecuteUpdateAsync` для максимальной производительности:

```
┌─────────────────────────────────────────────────────────────────┐
│                    ОБНОВЛЕНИЕ ДАННЫХ                            │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│  ExecuteUpdateAsync                                            │
│  ┌─────────────────────────────────────────────────────────┐   │
│  │ • Прямое SQL UPDATE в базе данных                      │   │
│  │ • Не загружает сущность в память                       │   │
│  │ • Максимальная производительность                      │   │
│  │ • Минимальный трафик между приложением и БД            │   │
│  └─────────────────────────────────────────────────────────┘   │
│                                                                 │
└─────────────────────────────────────────────────────────────────┘
```

### Преимущества ExecuteUpdateAsync

1. **🚀 Производительность**: Выполняет UPDATE напрямую в БД без загрузки данных
2. **📈 Масштабируемость**: Минимальное использование памяти приложения
3. **🔒 Атомарность**: Одна транзакция, одна операция
4. **🌐 Сетевая эффективность**: Минимальный трафик между приложением и БД

### Код ExecuteUpdateAsync

```csharp
var rowsAffected = await _context.Products
    .Where(p => p.Uid == uid)
    .ExecuteUpdateAsync(setters => setters
        .SetProperty(p => p.Name, newName)
        .SetProperty(p => p.Description, newDescription)
        .SetProperty(p => p.Price, newPrice)
        .SetProperty(p => p.Quantity, newQuantity)
    );
```

## ⚡ Оптимизации производительности

**Кэширование данных:**
- Данные загружаются из БД только один раз
- Повторные операции используют локальный кэш
- Мгновенный отклик при выборе строк в таблице

**Локальное обновление кэша:**
- CRUD операции обновляют кэш без запросов к БД
- Синхронизация с базой данных через ExecuteUpdateAsync
- Минимальный сетевой трафик

## 📊 Архитектура CRUD операций