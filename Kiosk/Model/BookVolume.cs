using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Model
{
    public class BookVolume
    {
        public BookVolume()
        {
            volumeInfo = new VolumeInfo();
            saleInfo = new SaleInfo();
            accessInfo = new AccessInfo();
            searchInfo = new SearchInfo();
        }
        public string kind;
        public string id;
        public string etag;
        public string selfLink;
        public VolumeInfo volumeInfo;
        public SaleInfo saleInfo;
        public AccessInfo accessInfo;
        public SearchInfo searchInfo;

    }
    public class VolumeInfo
    {
        public VolumeInfo()
        {
            industryIdentifiers = new List<IndustryIdentifier>();
            panelizationSummary = new PanelizationSummary();
            imageLinks = new ImageLinks();
        }
        public string title;
        public string[] authors;
        public string publisher;
        public string publishedDate;
        public string description;
        public List<IndustryIdentifier> industryIdentifiers;
        public int? pageCount;
        public string printType;
        public string[] categories;
        public long averageRating;
        public int ratingsCount; 
        public string maturityRating;
        public bool allowAnonLogging;
        public string contentVersion;
        public PanelizationSummary panelizationSummary;
        public  ImageLinks  imageLinks;
        public string language;
        public string previewLink;
        public string infoLink;
        public string canonicalVolumeLink;
    }
    public class ImageLinks
    {
        public string smallThumbnail;
        public string thumbnail;
    }
    public class PanelizationSummary
    {
        public bool containsEpubBubbles;
        public bool containsImageBubbles;
    }
    public class IndustryIdentifier
    {
        public string type;
        public string identifier;       
    }
    public  class ReadingModes
    {
        public bool text;
        public bool image;
    }

    public class SaleInfo
    {
      public SaleInfo()
        {
            listPrice = new Price();
            retailPrice = new Price();
            offers = new List<Offer>();
        }
        public string country;
        public string saleability;
        public bool isEbook;
        public Price listPrice;
        public Price retailPrice;
        public string buyLink;
        public List<Offer> offers;
    }
    public class Offer
    {
        public Offer()
        {
            listPrice = new Price2();
            retailPrice = new Price2();
        }
        public int finskyOfferType;
        public Price2 listPrice;
        public Price2 retailPrice;
        public bool giftable;
    }
    public class Price2
    {
        public long amountInMicros;
        public string currencyCode;
    }
    public class Price
    {
        public long amount;
        public string currencyCode;

    }

    public class AccessInfo
    {
        public AccessInfo()
        {
            epub = new Epub();
            pdf = new Pdf();
        }
        public string country;
        public string viewability;
        public bool embeddable;
        public bool publicDomain;    
        public string textToSpeechPermission;
        public Epub epub;
        public Pdf pdf;
        public string webReaderLink;
        public string accessViewStatus;
        public bool quoteSharingAllowed;
    }
    public class Epub
    {
        public bool isAvailable;
        public string acsTokenLink;
    }
    public class Pdf
    {
        public bool isAvailable;
    }

    public class SearchInfo
    {
        public string textSnippet;
    }
}