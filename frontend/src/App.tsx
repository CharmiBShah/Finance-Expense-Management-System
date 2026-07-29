import { FormEvent, useEffect, useState } from 'react'
import { createExpense, getExpenses } from './services/api'
import type { Expense } from './types/expense'
import './App.css'

function App() {
  const [expenses, setExpenses] = useState<Expense[]>([])
  const [title, setTitle] = useState('')
  const [amount, setAmount] = useState('')
  const [category, setCategory] = useState('Uncategorized')
  const [notes, setNotes] = useState('')

  useEffect(() => {
    loadExpenses()
  }, [])

  const loadExpenses = async () => {
    try {
      const data = await getExpenses()
      setExpenses(data)
    } catch (error) {
      console.error(error)
    }
  }

  const handleSubmit = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault()
    if (!title || !amount) return

    const newExpense = await createExpense({
      title,
      amount: Number(amount),
      category,
      date: new Date().toISOString(),
      notes,
    })

    setExpenses([newExpense, ...expenses])
    setTitle('')
    setAmount('')
    setCategory('Uncategorized')
    setNotes('')
  }

  const totalAmount = expenses.reduce((sum, expense) => sum + expense.amount, 0)
  const categories = Array.from(new Set(expenses.map((expense) => expense.category)))

  return (
    <div className="app-container">
      <header className="app-header">
        <div>
          <p className="eyebrow">AI Powered Finance</p>
          <h1>Personal Expense Management</h1>
          <p>Track spending, categorize expenses, and view financial insights in one place.</p>
        </div>
        <div className="dashboard-card">
          <span>Total spend</span>
          <strong>${totalAmount.toFixed(2)}</strong>
        </div>
      </header>

      <main className="app-main">
        <section className="panel">
          <h2>Add expense</h2>
          <form className="expense-form" onSubmit={handleSubmit}>
            <label>
              Title
              <input value={title} onChange={(e) => setTitle(e.target.value)} placeholder="Coffee at cafe" />
            </label>
            <label>
              Amount
              <input
                type="number"
                step="0.01"
                value={amount}
                onChange={(e) => setAmount(e.target.value)}
                placeholder="24.50"
              />
            </label>
            <label>
              Category
              <select value={category} onChange={(e) => setCategory(e.target.value)}>
                <option>Uncategorized</option>
                <option>Groceries</option>
                <option>Rent</option>
                <option>Transportation</option>
                <option>Dining</option>
                <option>Utilities</option>
                <option>Travel</option>
                <option>Gifts</option>
                <option>Coffee</option>
              </select>
            </label>
            <label>
              Notes
              <textarea value={notes} onChange={(e) => setNotes(e.target.value)} placeholder="Add any contextual details" />
            </label>
            <button type="submit">Save expense</button>
          </form>
        </section>

        <section className="panel">
          <div className="section-header">
            <h2>Expense history</h2>
            <span>{expenses.length} records</span>
          </div>

          {expenses.length === 0 ? (
            <p className="empty-state">No expenses recorded yet. Start by adding your first expense.</p>
          ) : (
            <div className="expense-table-wrapper">
              <table className="expense-table">
                <thead>
                  <tr>
                    <th>Date</th>
                    <th>Title</th>
                    <th>Category</th>
                    <th>Amount</th>
                  </tr>
                </thead>
                <tbody>
                  {expenses.map((expense) => (
                    <tr key={expense.id}>
                      <td>{new Date(expense.date).toLocaleDateString()}</td>
                      <td>{expense.title}</td>
                      <td>{expense.category}</td>
                      <td>${expense.amount.toFixed(2)}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          )}

          <div className="category-summary">
            <h3>Active categories</h3>
            <div className="category-list">
              {categories.map((name) => (
                <span key={name} className="category-pill">
                  {name}
                </span>
              ))}
            </div>
          </div>
        </section>
      </main>
    </div>
  )
}

export default App
