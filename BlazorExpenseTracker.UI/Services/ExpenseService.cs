using BlazorExpenseTracker.Model;
using BlazorExpenseTracker.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorExpenseTracker.UI.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly HttpClient _httpClient;

        public ExpenseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task DeleteExpense(int id)
        {
            await _httpClient.DeleteAsync($"api/expense/{id}");
        }

        public async Task<IEnumerable<Expense>> GetAllExpenses()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Expense>>(
                await _httpClient.GetStreamAsync($"api/expense"),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Expense> GetExpenseDetails(int id)
        {
            return await JsonSerializer.DeserializeAsync<Expense>(
                await _httpClient.GetStreamAsync($"api/expense/{id}"),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task SaveExpense(Expense expense)
        {
            var expenseJson = new StringContent(JsonSerializer.Serialize(expense), Encoding.UTF8, "application/json");

            if (expense.Id == 0)
                await _httpClient.PostAsync("api/expense", expenseJson); //Insert Expense
            else
                await _httpClient.PutAsync("api/expense", expenseJson); //Update Expense
        }
    }
}
