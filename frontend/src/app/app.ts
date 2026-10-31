import { Component } from '@angular/core';
import { DatePipe } from '@angular/common';

@Component({
  imports: [DatePipe],
  selector: 'app-root',
  styleUrl: './app.scss',
  templateUrl: './app.html',
})
export class App {
  protected readonly transactions = [
    {
      title: 'Grocery Shopping',
      category: 'Food',
      date: 'Oct 28, 2026',
      amount: 85.50,
      status: 'Completed'
    },
    {
      title: 'Electricity Bill',
      category: 'Utilities',
      date: 'Oct 26, 2026',
      amount: 120.00,
      status: 'Completed'
    },
    {
      title: 'Restaurant',
      category: 'Food',
      date: 'Oct 24, 2026',
      amount: 45.75,
      status: 'Completed'
    },
    {
      title: 'Internet Bill',
      category: 'Utilities',
      date: 'Oct 22, 2026',
      amount: 75.00,
      status: 'Completed'
    }
  ];
  protected readonly totalExpenses = this.transactions.reduce(
    (total, transaction) => total + transaction.amount,
    0
  );

  protected readonly totalTransactions = this.transactions.length;

  protected readonly averageExpense =
    this.totalTransactions > 0
      ? this.totalExpenses / this.totalTransactions
      : 0;
  protected readonly monthlyExpenses = this.transactions
    .filter(transaction => {
      const transactionDate = new Date(transaction.date);
      const today = new Date();

      return (
        transactionDate.getMonth() === today.getMonth() &&
        transactionDate.getFullYear() === today.getFullYear()
      );
    })
    .reduce((total, transaction) => total + transaction.amount, 0);

  protected readonly categoryExpenses = this.transactions.reduce(
    (categories, transaction) => {
      categories[transaction.category] =
        (categories[transaction.category] || 0) + transaction.amount;

      return categories;
    },
    {} as Record<string, number>
  );
  protected readonly categoryPercentages = Object.entries(
    this.categoryExpenses
  ).map(([category, amount]) => ({
    category,
    amount,
    percentage: this.totalExpenses > 0
      ? (amount / this.totalExpenses) * 100
      : 0
  }));

  protected readonly currentDate = new Date();

  protected readonly greeting = this.getGreeting();

  private getGreeting(): string {
    const hour = new Date().getHours();

    if (hour < 12) {
      return 'Good morning';
    } else if (hour < 17) {
      return 'Good afternoon';
    } else {
      return 'Good evening';
    }
  }
}
