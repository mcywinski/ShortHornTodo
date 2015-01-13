using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShortHorn.Desktop.Services;

namespace ShortHorn.Desktop.ViewModels
{
    /// <summary>
    /// View model for creating todo lists.
    /// </summary>
    public class CreateTodoListViewModel : BaseViewModel
    {
        private string todoListName;

        /// <summary>
        /// Name of todo list.
        /// </summary>
        public string TodoListName
        {
            get { return todoListName; }
            set { todoListName = value; onPropertyChanged(this, "Location"); }
        }

        /// <summary>
        /// Creates a todo list and saves it on the server.
        /// </summary>
        /// <returns></returns>
        public async Task CreateTodoList()
        {
            TodoListService listService = new TodoListService(ConfigurationManager.GetApiBaseAddress(), AppState.ApiLoginToken);
            await listService.CreateList(this.todoListName);
        }
    }
}
