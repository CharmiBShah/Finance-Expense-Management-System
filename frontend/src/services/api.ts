import type{ Expense } from '../types/expense'

const apiUrl = import.meta.env.VITE_API_URL || 'http://localhost:5098/api'

export async function getExpenses(): Promise<Expense[]> {
  const response = await fetch(`${apiUrl}/expenses`)
  return response.json()
}

export async function createExpense(expense: Omit<Expense, 'id'>): Promise<Expense> {
  const response = await fetch(`${apiUrl}/expenses`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(expense),
  })
  return response.json()
}
export async function deleteExpense(id: number): Promise<void> {
  await fetch(`${apiUrl}/expenses/${id}`, {
    method: 'DELETE',
  })
}

export async function updateExpense(expense: Expense): Promise<void> {
  await fetch(`${apiUrl}/expenses/${expense.id}`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(expense),
  })
}