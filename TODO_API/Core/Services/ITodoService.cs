using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TODO_API.Controllers.Resources;

namespace TODO_API.Core.Services
{
    public interface ITodoService
    {

        Task<bool> AddTodoItem_Details(TodoItem_Details TodoItem_Details);
        Task <List<TodoItem_Details>> FetchAllTodoItem_Details();
        Task<TodoItem_Details> FetchTodoItem_Details(long id);

        Task<bool> ModifyTodoItem_Details(TodoItem_Details TodoItem_Details);
        Task<bool> DeleteTododItem(long id);
    }
}
