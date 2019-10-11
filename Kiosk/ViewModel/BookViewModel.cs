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
            SearchBooksCommand2 = new RelayCommand<string>(GetBooks2);
            SearchByTitle = true;
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

        private ICommand _SearchBooksCommand2 { get; set; }
        public ICommand SearchBooksCommand2
        {
            get
            {
                if (_SearchBooksCommand2 == null)
                {
                    _SearchBooksCommand2 = new RelayCommand<string>(GetBooks2);
                }
                return _SearchBooksCommand2;
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
    
        private bool _BooksListVisible;
        public bool BooksListVisible
        {
            get { return _BooksListVisible; }
            set
            {
                if (value != _BooksListVisible)
                {
                    if(BookList.ToList().Count > 0)
                    {
                        value = true;
                    }
                    else
                    {
                        value = false;
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

        // SearchByAuthor
        private bool _SearchByAuthor;
        public bool SearchByAuthor
        {
            get { return _SearchByAuthor; }
            set
            {
                if (value != _SearchByAuthor)
                {
                    _SearchByAuthor = value;
                    this.OnPropertyChanged("SearchByAuthor");
                }
            }
        }

        public void GetBooks(string SearchString)
        {
            var retVal = BooksApi.Search(this.SearchText);
            BookList = new ObservableCollection<Book>(retVal);
        }

        public async void GetBooks2(string SearchString)
        {
            List<Book> result = new List<Book>();
            if( SearchByTitle)
            {
                 result = await BooksApi.SearchByTitle(this.SearchText);               
            }
            if(SearchByAuthor)
            {
                 result = await BooksApi.SearchByAuthor(this.SearchText);
                
            }
            BookList = new ObservableCollection<Book>(result);
        }

    }
}