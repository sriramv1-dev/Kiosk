using Kiosk.Api;
using Kiosk.Model;
using Kiosk.MVVMBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Kiosk.ViewModel
{
    public class BookViewModel : ViewModelBase
    {
        public BookViewModel()
        {
            SearchByTitle = true;
            BooksListVisible = Visibility.Hidden;
            ClearSearchVisible = Visibility.Hidden;
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
                    if (_BookList != null && _BookList.Count > 0)
                    {
                        BooksListVisible = Visibility.Visible;
                        ClearSearchVisible = Visibility.Visible;
                    }
                    else
                    {
                        BooksListVisible = Visibility.Hidden;
                        ClearSearchVisible = Visibility.Hidden;
                    }
                    this.OnPropertyChanged("BookList");
                }
            }
        }
    
        private Visibility _BooksListVisible;
        public Visibility BooksListVisible
        {
            get { return _BooksListVisible; }
            set
            {
                if (value != _BooksListVisible)
                {
                    _BooksListVisible = value;
                    this.OnPropertyChanged("BooksListVisible");
                }
            }
        }

        private Visibility _ClearSearchVisible;
        public Visibility ClearSearchVisible
        {
            get { return _ClearSearchVisible; }
            set
            {
                if (value != _ClearSearchVisible)
                {
                    _ClearSearchVisible = value;
                    this.OnPropertyChanged("ClearSearchVisible");
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

        private ICommand _ClearSearchCommand { get; set; }
        public ICommand ClearSearchCommand
        {
            get
            {
                if (_ClearSearchCommand == null)
                {
                    _ClearSearchCommand = new RelayCommand<string>(ClearSearch);
                }
                return _ClearSearchCommand;
            }
        }



        public async void GetBooks(string SearchString)
        {
            
            List<Book> result = new List<Book>();
            string searchByType = SearchByTitle ? "title":SearchByAuthor ? "author":"title";
            result = await BooksApi.Search(this.SearchText, searchByType);
            BookList = new ObservableCollection<Book>(result);
        }

        public void ClearSearch(string SearchString)
        {
            SearchText = "";
            BookList = new ObservableCollection<Book>();
        }
    }
}