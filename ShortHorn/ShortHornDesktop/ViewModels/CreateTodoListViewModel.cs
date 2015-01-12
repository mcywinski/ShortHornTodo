using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShortHorn.Desktop.Services;

namespace ShortHorn.Desktop.ViewModels
{
    public class CreateTodoListViewModel : BaseViewModel
    {
        private string todoListName;
        public string TodoListName
        {
            get { return todoListName; }
            set { todoListName = value; onPropertyChanged(this, "Location"); }
        }

        public async Task CreateTodoList()
        {
            TodoListService listService = new TodoListService(ConfigurationManager.GetApiBaseAddress(), AppState.ApiLoginToken);
            await listService.CreateList(this.todoListName);
        }
    }
}
