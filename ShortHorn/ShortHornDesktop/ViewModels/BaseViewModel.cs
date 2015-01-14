using System.ComponentModel;

namespace ShortHorn.Desktop.ViewModels
{
    /// <summary>
    /// Base viewmodel from which implements logic reusable in other viewmodels.
    /// </summary>
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        // Declare the PropertyChanged event
        public event PropertyChangedEventHandler PropertyChanged;

        // OnPropertyChanged will raise the PropertyChanged event passing the
        // source property that is being updated.
        protected virtual void onPropertyChanged(object sender, string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(sender, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
