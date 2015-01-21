using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShortHorn.DataTransferObjects;
using System.ComponentModel;
using ShortHorn.Desktop.Services;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;

namespace ShortHorn.Desktop.ViewModels.UserControls
{
    /// <summary>
    /// View model for TodoItemDetails.xaml
    /// </summary>
    public class TodoItemDetailsViewModel : BaseViewModel, INotifyPropertyChanged
    {
        #region constructors

        public TodoItemDetailsViewModel()
        {
        }

        /// <summary>
        /// Initializes view model with values from DTO passed in argument.
        /// </summary>
        /// <param name="item"></param>
        public TodoItemDetailsViewModel(TodoItemDTO item)
        {
            this.Id = item.Id;
            this.Name = item.Name;
            this.IsFinished = item.IsFinished;
            this.isFavourite = item.IsFavourite;
            this.DateFinish = item.DateFinish;

            this.todoItemsService = new TodoItemsService(ConfigurationManager.GetApiBaseAddress(), AppState.ApiLoginToken);
        }

        #endregion

        #region private methods

        private new async void onPropertyChanged(object sender, string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(sender, new PropertyChangedEventArgs(propertyName));
                await updateItem();
                
            }
        }

        private void noWeatherVisible()
        {
            this.CloudyVisible = Visibility.Collapsed;
            this.SunVisible = Visibility.Collapsed;
            this.RainVisible = Visibility.Collapsed;
            this.SnowVisible = Visibility.Collapsed;
        }

        private async Task updateItem()
        {
            bool result = await this.todoItemsService.UpdateItem(this.ToDTO());

            if (!result)
            {
                System.Windows.MessageBox.Show("Couldn't update item details!");
            }
            else
            {
                if (this.ItemUpdateCompleted != null)
                    this.ItemUpdateCompleted.Invoke();   
            }
        }

        #endregion

        #region public methods

        public TodoItemDTO ToDTO()
        {
            return new TodoItemDTO()
            {
                Id = this.Id,
                Name = this.Name,
                IsFinished = this.IsFinished,
                IsFavourite = this.IsFavourite,
                DateFinish = this.DateFinish
            };
        }

        public async Task<bool> SetWeather()
        {
            DateTime today = DateTime.Today;
            if (today > this.dateFinish.Value)
            {
                noWeatherVisible();
                return false;
            }
            else
            {
                int timeDiff = (this.dateFinish.Value - today).Days;
                if (timeDiff <= 14)
                {
                    noWeatherVisible();
                    string weather = await this.todoItemsService.FetchWeather(this.DateFinish.Value, AppState.City, AppState.Country);
                    weather = weather.ToLower();
                    if (weather == "snow")
                    {
                        this.SnowVisible = Visibility.Visible;
                    }
                    else if (weather == "rain")
                    {
                        this.RainVisible = Visibility.Visible;
                    }
                    else if (weather == "clouds")
                    {
                        this.CloudyVisible = Visibility.Visible;
                    }
                    else
                    {
                        this.SunVisible = Visibility.Visible;
                    }
                    return true;
                }
                else
                {
                    noWeatherVisible();
                    return false;
                }
            }
            
            
        }

        #endregion

        #region properties

        private TodoItemsService todoItemsService;

        public new event PropertyChangedEventHandler PropertyChanged;

        public delegate void itemUpdateCompleted();
        public event itemUpdateCompleted ItemUpdateCompleted = null;

        public Visibility CloudyVisible
        {
            get { return cloudyVisible; }
            set { cloudyVisible = value; onPropertyChanged(this, "CloudyVisible"); }
        }

        private Visibility cloudyVisible;

        public Visibility RainVisible
        {
            get { return rainVisible; }
            set { rainVisible = value; onPropertyChanged(this, "RainVisible"); }
        }

        private Visibility rainVisible;

        public Visibility SnowVisible
        {
            get { return snowVisible; }
            set { snowVisible = value; onPropertyChanged(this, "SnowVisible"); }
        }

        private Visibility snowVisible;

        public Visibility SunVisible
        {
            get { return sunVisible; }
            set { sunVisible = value; onPropertyChanged(this, "SunVisible"); }
        }

        private Visibility sunVisible;

        /// <summary>
        /// Todo item ID.
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; onPropertyChanged(this, "Id"); }
        }

        private int id;

        /// <summary>
        /// Todo item name.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; onPropertyChanged(this, "Name"); }
        }

        private string name;

        /// <summary>
        /// Todo item completion status.
        /// </summary>
        public bool IsFinished
        {
            get { return isFinished; }
            set { isFinished = value; onPropertyChanged(this, "IsFinished"); }
        }

        private bool isFinished;

        /// <summary>
        /// Todo item favourite flag.
        /// </summary>
        public bool IsFavourite
        {
            get { return isFavourite; }
            set { isFavourite = value; onPropertyChanged(this, "IsFavourite"); }
        }

        private bool isFavourite;

        /// <summary>
        /// Todo item deadline.
        /// </summary>
        public DateTime? DateFinish
        {
            get { return dateFinish; }
            set { dateFinish = value; onPropertyChanged(this, "DateFinish"); }
        }

        private DateTime? dateFinish;

        #endregion
    }
}
