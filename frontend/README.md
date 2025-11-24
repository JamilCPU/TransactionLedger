# TransactionAnalytics Frontend

A modern React-based frontend for TransactionAnalytics, a transaction analysis application with AI-powered insights. Built with TypeScript, Vite, and Tailwind CSS.

## Features

- 📊 **Dashboard Overview** - Real-time statistics and recent activity
- 👥 **User Management** - Create, view, and manage users
- 💳 **Account Management** - Handle different types of accounts
- 💰 **Transaction Tracking** - Monitor deposits, withdrawals, and transfers
- 🤖 **AI-Powered Analysis** - Intelligent transaction insights and analytics
- 🎨 **Modern UI** - Clean, responsive design with Tailwind CSS
- ⚡ **Fast Development** - Hot module replacement with Vite
- 🔄 **Data Fetching** - React Query for efficient API calls
- 🧭 **Routing** - React Router for navigation

## Tech Stack

- **React 19** - Latest React with hooks and modern patterns
- **TypeScript** - Type-safe development
- **Vite** - Fast build tool and dev server
- **Tailwind CSS** - Utility-first CSS framework
- **React Router** - Client-side routing
- **React Query** - Data fetching and caching
- **Axios** - HTTP client for API calls
- **Lucide React** - Beautiful icons
- **clsx + tailwind-merge** - Conditional styling utilities

## Project Structure

```
src/
├── components/
│   ├── ui/           # Reusable UI components
│   │   ├── Button.tsx
│   │   ├── Input.tsx
│   │   └── Card.tsx
│   └── layout/       # Layout components
│       ├── Header.tsx
│       └── Layout.tsx
├── pages/            # Page components
│   └── Dashboard.tsx
├── services/         # API services
│   └── api.ts
├── types/            # TypeScript type definitions
│   └── index.ts
├── utils/            # Utility functions
│   └── cn.ts
├── App.tsx           # Main app component
└── main.tsx          # App entry point
```

## Getting Started

### Prerequisites

- Node.js 18+ 
- npm or yarn
- Backend API running on `http://localhost:5000`

### Installation

1. Install dependencies:
   ```bash
   npm install
   ```

2. Start the development server:
   ```bash
   npm run dev
   ```

3. Open your browser and navigate to `http://localhost:5173`

### Available Scripts

- `npm run dev` - Start development server
- `npm run build` - Build for production
- `npm run preview` - Preview production build
- `npm run lint` - Run ESLint

## API Integration

The frontend is configured to communicate with the backend API at `http://localhost:5000/api`. The API service layer includes:

- **User Management** - CRUD operations for users
- **Account Management** - Handle bank accounts
- **Transaction Management** - Process financial transactions

## Styling

The application uses Tailwind CSS with a custom design system:

- **Primary Colors** - Blue theme for main actions
- **Success Colors** - Green for positive actions
- **Danger Colors** - Red for destructive actions
- **Custom Components** - Reusable styled components

## Development

### Adding New Pages

1. Create a new component in `src/pages/`
2. Add the route in `src/App.tsx`
3. Update navigation in `src/components/layout/Header.tsx`

### Adding New Components

1. Create reusable components in `src/components/ui/`
2. Use the `cn()` utility for conditional styling
3. Follow the existing component patterns

### API Integration

1. Add new API methods in `src/services/api.ts`
2. Use React Query for data fetching
3. Handle loading and error states

## Contributing

1. Follow the existing code structure and patterns
2. Use TypeScript for all new code
3. Add proper error handling
4. Test your changes thoroughly

## License

This project is part of TransactionAnalytics - AI-powered transaction analysis platform.
