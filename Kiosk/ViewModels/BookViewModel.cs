using Kiosk.ServiceLayer;
using Kiosk.Models;
using Kiosk.Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Kiosk.ViewModels
{
    public class BookViewModel : BaseViewModel
    {
        public BookViewModel()
        {
            SearchByTitle = true;
            BooksListVisible = Visibility.Hidden;
            ClearSearchVisible = Visibility.Hidden;
            ErrorMessageVisible = Visibility.Hidden;
            ErrorMessage = "";

        }

        private string _SearchText;
        public string SearchText
        {
            get { return _SearchText; }
            set
            {
                if (value != _SearchText)
                {
                    _SearchText = value;
                    this.OnPropertyChanged("SearchText");
                }
            }
        }

        private string _ErrorMessage;
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set
            {
                if (value != _ErrorMessage)
                {
                    _ErrorMessage = value;
                    if (_ErrorMessage != null && _ErrorMessage.Trim() == "")
                    {
                        ErrorMessageVisible = Visibility.Hidden;
                        SearchTextBrush = Brushes.Green;
                    }
                    else
                    {
                        ErrorMessageVisible = Visibility.Visible;
                        SearchTextBrush = Brushes.OrangeRed;
                    }

                    this.OnPropertyChanged("ErrorMessage");
                }
            }
        }

        private ObservableCollection<Book> _BookList;
        public ObservableCollection<Book> BookList
        {
            get { return _BookList; }
            set
            {
                if (value != _BookList)
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

        private Visibility _ErrorMessageVisible;
        public Visibility ErrorMessageVisible
        {
            get { return _ErrorMessageVisible; }
            set
            {
                if (value != _ErrorMessageVisible)
                {
                    _ErrorMessageVisible = value;
                    this.OnPropertyChanged("ErrorMessageVisible");
                }
            }
        }

        private SolidColorBrush _SearchTextBrush { get; set; }
        public SolidColorBrush SearchTextBrush
        {
            get { return _SearchTextBrush; }
            set
            {
                if (value != _SearchTextBrush)
                {
                    _SearchTextBrush = value;
                    this.OnPropertyChanged("SearchTextBrush");
                }
            }
        }

        private bool _SearchByTitle;
        public bool SearchByTitle
        {
            get { return _SearchByTitle; }
            set
            {
                if (value != _SearchByTitle)
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
            int validated = ValidationHelper.ValidateSearchString(SearchText);
            List<Book> result = new List<Book>();
            switch (validated)
            {
                case 0:
                    ErrorMessage = "";
                    string searchByType = SearchByTitle ? "title" : SearchByAuthor ? "author" : "title";                    
                    var retVal = await BooksApi.Search(this.SearchText, searchByType);
                    bool ConnectionSuccesful = retVal.Item1;
                    result = retVal.Item2;
                    if (ConnectionSuccesful)
                    {                       
                        if (result.Count == 0)
                        {
                            ErrorMessage = "Your search query resulted 0 records";
                        }
                    }
                    else
                    {
                        ErrorMessage = retVal.Item3;
                    }
                    break;
                case 1:
                    ErrorMessage = "* Search text should not be empty";
                    break;
                case 2:
                    ErrorMessage = "* Search text should not have more than 50 characters (spaces not included)";
                    break;
                case 3:
                    ErrorMessage = "* Search text should be alphnumeric";
                    break;
                case 4:
                    ErrorMessage = "* Search text should be alphanumeric";
                    break;
            }
            BookList = new ObservableCollection<Book>(result);
        }

        public void ClearSearch(string SearchString)
        {
            SearchText = "";
            BookList = new ObservableCollection<Book>();
        }
    }
}