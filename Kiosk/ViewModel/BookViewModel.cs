using Kiosk.Api;
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
    public class BookViewModel : ViewModelBase
    {

        public BookViewModel()
        {
            //SearchBooksCommand = new RelayCommand<string>(GetBooks);
        }

        private ICommand _SearchBooksCommand { get; set; }
        public ICommand SearchBooksCommand
        {
            get
            {
                if (_SearchBooksCommand == null)
                {
                    _SearchBooksCommand = new RelayCommand<string>(GetBooks);
                }
                return _SearchBooksCommand;
            }
        }

        private string _SearchText;
        public string SearchText
        {
            get { return _SearchText; }
            set
            {
                if(value != _SearchText)
                {
                    _SearchText = value;
                    this.OnPropertyChanged("SearchText");
                }
            }
        }

        private ObservableCollection<Book> _BookList;
        public ObservableCollection<Book> BookList
        {
            get { return _BookList; }
            set
            {
                if(value != _BookList)
                {
                    _BookList = value;                 
                    this.OnPropertyChanged("BookList");
                }
            }
        }
    
        private string _BooksListVisible;
        public string BooksListVisible
        {
            get { return _BooksListVisible; }
            set
            {
                if (value != _BooksListVisible)
                {
                    if(BookList.ToList().Count > 0)
                    {
                        value = "Visible";
                    }
                    else
                    {
                        value = "Hidden";
                    }
                    _BooksListVisible = value;
                    this.OnPropertyChanged("BooksListVisible");
                }
            }
        }

        private bool _SearchByTitle;
        public bool SearchByTitle
        {
            get { return _SearchByTitle; }
            set
            {
                if(value != _SearchByTitle)
                {
                    _SearchByTitle = value;
                    this.OnPropertyChanged("SearchByTitle");
                }
            }
        }

        public void GetBooks(string SearchString)
        {
            //var retVal = SearchByTitle ? BooksApi.Search(SearchString) : BooksApi.SearchByAuthor(SearchString);
            //var retVal = BooksApi.Search( SearchString);
            //
            var retVal = BooksApi.Search(this.SearchText);
            BookList = new ObservableCollection<Book>(retVal);
            //BooksApi.SearchWithUrl();
        }

        public async void GetBooks2(string SearchString)
        {

            var result = await BooksApi.SearchWithUrl();
            BookList = new ObservableCollection<Book>(result);

            //await Task.Run(() => 
            //     BooksApi.SearchWithUrl() 
            //);
        }

    }
}