using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TODO_API.Controllers.Resources;
using TODO_API.Core.Services;
using TODO_API.Persistence;

namespace TODO_API.Services
{
    public class TodoService:ITodoService
    {
        private readonly TodoDBContext _context;

        public TodoService(TodoDBContext context)
        {
            _context = context;
        }
        public async Task<bool> AddTodoItem_Details(TodoItem_Details TodoItem_Details)

        {

            try
            {
                await _context.TodoItem_Details.AddAsync(TodoItem_Details);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new TODOCustomError($"Erorr While adding Todod item : {ex.Message}");
                
            }
        }
        private bool TodoItem_DetailsExists(long id)
        {
            return _context.TodoItem_Details.Any(e => e.Item_id == id);
        }

        public async Task<List<TodoItem_Details>> FetchAllTodoItem_Details()
        {
            try
            {
                return await _context.TodoItem_Details.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new TODOCustomError($"Erorr While fetching all TODO item details : {ex.Message}");
            }

        }
        public async Task<TodoItem_Details> FetchTodoItem_Details(long id)
        {
            try 
            { 
            var TodoItem_Details = await _context.TodoItem_Details.FindAsync(id);

           
            return TodoItem_Details;
            }
            catch (Exception ex)
            {
               
                    throw new TODOCustomError($"Erorr While fetching TODO item details by id : {ex.Message}");
             
            }
}

        public async Task<bool> ModifyTodoItem_Details(TodoItem_Details TodoItem_Details)
        {
            _context.Entry(TodoItem_Details).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new TODOCustomError($"Erorr While modifying Todod item : {ex.Message}");
            }
        }
        public async Task<bool> DeleteTododItem(long id)
        {
            try 
            { 
            var TodoItem_Details = await _context.TodoItem_Details.FindAsync(id);
            if (TodoItem_Details == null)
            {
                return false;
            }

            _context.TodoItem_Details.Remove(TodoItem_Details);
            await _context.SaveChangesAsync();
            return true;
            }

             catch (Exception ex)
            {
                throw new TODOCustomError($"Erorr While deleting Todod item : {ex.Message}");
            }

        }
    }
}
