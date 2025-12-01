# Procure Risk Analyzer - React Web Application

React.js веб-додаток для аналізу державних закупівель та виявлення корупційних ризиків.

## Технології

- **React 18** - UI бібліотека
- **TypeScript** - Типізація
- **Vite** - Build tool
- **React Router** - Маршрутизація
- **Axios** - HTTP клієнт
- **Recharts** - Графіки та візуалізація даних

## Встановлення

1. Встановіть залежності:
```bash
npm install
```

2. Запустіть backend API (http://localhost:5019)

3. Запустіть React додаток:
```bash
npm run dev
```

Додаток буде доступний на http://localhost:3000

## Функціонал

- ✅ Аутентифікація через Okta (JWT токени)
- ✅ Управління Suppliers (Постачальники)
- ✅ Управління Tenders (Тендери)
- ✅ Управління Categories (Категорії)
- ✅ Статистика з графіками
- ✅ Профіль користувача
- ✅ About сторінка

## Облікові дані

- **Логін**: `olena_skakunn`
- **Пароль**: `A0982935531a`

## API Endpoints

Додаток використовує той самий backend API:
- `/api/authapi/login` - Аутентифікація
- `/api/suppliers` - Постачальники
- `/api/tenders` - Тендери
- `/api/categories` - Категорії
- `/api/tenders/statistics` - Статистика

## Структура проекту

```
src/
  ├── pages/          # Сторінки додатку
  ├── services/       # API сервіси
  ├── types/          # TypeScript типи
  ├── App.tsx         # Головний компонент
  └── main.tsx        # Точка входу
```

