# Проєкт
Програма є системою для управління замовленнями клієнтів у контексті онлайн-магазину продуктів. Вона дозволяє створювати, переглядати, оплачувати та видаляти замовлення. Огляд основних функцій та структури системи:

## Основні компоненти
---
### 1. Інтерфейс `ICustomer`
Визначає базовий функціонал для клієнтів, зокрема методи для:
- Створення замовлень.
- Перегляду наявних замовлень.
- Здійснення покупки.
- Видалення замовлень.

Клієнти мають такі властивості, як ім'я та баланс, який використовується для оплати замовлень.

### 2. Клас `Customer` (Клієнт)
Реалізує інтерфейс `ICustomer`. Клієнт має ім'я та баланс (суму грошей на рахунку). Клієнт може:
- Створювати замовлення.
- Купувати замовлення.
- Видаляти замовлення.


При створенні клієнта вказуються його ім'я та початковий баланс.

### 3. Клас `Order` (Замовлення)
Містить інформацію про замовлення, таку як:
- Дата.
- Номер замовлення.
- Статус (новий, в обробці, виконано або закрито).
- Клієнт, що створив замовлення.

Клієнт може додавати продукти до замовлення з вказанням кількості, видаляти продукти та обчислювати загальну суму замовлення. Також можна очистити замовлення, якщо це необхідно.

### 4. Клас `Product` (Продукт)
Представляє товар із такими властивостями:
- Назва.
- Ціна.
- Тип продукту (наприклад, молочні продукти, хлібобулочні вироби, м'ясо тощо).

Кожен продукт належить до однієї з категорій, визначених у переліку `ProductType`.

### 5. Перелік `ProductType` (Тип продукту)
Містить перелік можливих категорій продуктів:
- Молочні продукти.
- Хлібобулочні вироби.
- М'ясо.
- Морепродукти.
- Напої.
- Засоби для прибирання.

Цей перелік допомагає категоризувати товари для зручності.

### 6. Перелік `Status` (Статус замовлення)
Визначає можливі статуси для замовлень:
- Новий.
- В обробці.
- Виконано.
- Закрито.

Це дозволяє відслідковувати прогрес замовлення.

## Основні можливості
---
- Створення клієнтів з індивідуальними даними та балансом.
- Додавання замовлень та продуктів до них, встановлення кількості продуктів.
- Видалення продуктів із замовлення та повне очищення замовлення.
- Підрахунок загальної суми замовлення на основі продуктів та їх кількості.
- Контроль статусу замовлення (наприклад, "новий", "в обробці" тощо).

## Діаграма класів
---
Програма відповідає наступній діаграмі классів:
![Діаграма](https://github.com/o-a-golovkina/Project/blob/master/Part%20A/Diagram-2.jpg)
