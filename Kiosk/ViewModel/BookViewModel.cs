using Kiosk.Model;
using Kiosk.MVVMBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Kiosk.ViewModel
{
    public class BookViewModel
    {
        public ObservableCollection<Book> BookList { get; set; }
        public ICommand SearchBooksCommand { get; set; }
        public BookViewModel()
        {
            //SearchBooksCommand = new RelayCommand<Book>();
            BookList = new ObservableCollection<Book>
            {
                new Book
                {
                    Id = "1",
                    Title = "Full House",
                    Description = "A family with 3 daughters, a dad and 2 uncles",
                    Author = "Hulu",
                    PageCount = 287,
                    Subtitle = "How Rude!!!!"
                },
                new Book
                {
                    Id = "1",
                    Title = "Modern Family",
                    Description = "3 Families tied together",
                    Author = "Online",
                    PageCount = 453,
                    Subtitle = "Alex Rocks"
                }
            };
        }

        //private void SelectedStudentDetails(Book obj)
        //{
        //    if (obj != null)
        //    {
        //        this.SelectedStudentName = obj.Name;
        //        // better to go for full property instead of calling property change here 
        //        this.RaisePropertyChanged("SelectedStudentName");
        //    }
        //}
    }
}
