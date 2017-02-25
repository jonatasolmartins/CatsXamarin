using Cats.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Cats.ViewModels
{
    class CatsViewModel : INotifyPropertyChanged
    {
        public CatsViewModel()
        {
            Cats = new ObservableCollection<Cat>();
            GetCatsCommand = new Command(
                async () => await GetCats(),
                () => !IsBusy
                );
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Cat> Cats { get; set; }
        public Command GetCatsCommand { get; set; }
        private bool isBusy;

        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value; OnPropertyChanged(); GetCatsCommand.ChangeCanExecute(); }
        }


        private void OnPropertyChanged(
            [CallerMemberName]
            string propetyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propetyName));

        async Task GetCats()
        {
            if (!IsBusy)
            {
                Exception Error = null;
                try
                {
                    IsBusy = true;
                    var Repository = new Repository();
                    var Items = await Repository.GetCats();
                    Cats.Clear();
                    foreach (var Cat in Items)
                    {
                        Cats.Add(Cat);
                    }
                }
                catch (Exception ex)
                {
                    Error = ex;
                }
                finally
                {
                    IsBusy = false;
                    if (Error != null)
                    {
                        await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Error!", Error.Message, "Ok");
                    }
                }
            }
            return;
        }
    }
}
