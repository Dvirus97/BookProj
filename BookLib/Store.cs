using BookLib.Models;
using DbService;
using System.Collections;
using System.Collections.ObjectModel;
using System.Data.SqlTypes;
using System.Globalization;

namespace BookLib {
    public class Store {

        public static Store Instace { get; } = new Store();

        public List<Item> items { get; } = new List<Item>();
        private readonly JsonSave<Item> LogItemsList = new JsonSave<Item>("LogItemsList.json");
        public TextSave TextSave { get; } = new TextSave("LogError.txt");

        #region enum lists
        public List<Genre> GenreList { get => Enum.GetValues(typeof(Genre)).Cast<Genre>().ToList(); }
        public List<DiscountBy> DiscountByList { get => Enum.GetValues(typeof(DiscountBy)).Cast<DiscountBy>().ToList(); }
        public List<itemType> ItemTypeList { get => Enum.GetValues(typeof(itemType)).Cast<itemType>().ToList(); }
        public List<FilterBy> FilterByList { get => Enum.GetValues(typeof(FilterBy)).Cast<FilterBy>().ToList(); }
        #endregion

        #region author/ name/ publisher lists
        public List<string> AllAuthor {
            get {
                List<string> temp = new List<string>();
                foreach (var item in items) {
                    if (item is null || item.Author is null) throw new ArgumentNullException();
                    if (!temp.Contains(item.Author)) {
                        temp.Add(item.Author);
                    }
                }
                return temp;
            }
        }
        public List<string> AllName {
            get {
                List<string> temp = new List<string>();
                foreach (var item in items) {
                    if (item is null || item.Name is null) throw new ArgumentNullException();
                    if (!temp.Contains(item.Name)) {
                        temp.Add(item.Name);
                    }
                }
                return temp;
            }
        }
        public List<string> AllPublisher {
            get {
                List<string> temp = new List<string>();
                foreach (var item in items) {
                    if (item is null || item.Publisher is null) throw new ArgumentNullException();
                    if (!temp.Contains(item.Publisher)) {
                        temp.Add(item.Publisher);
                    }
                }
                return temp;
            }
        }
        #endregion
        public bool IsAdmin { get; set; } = true;

        private Store() {
            //CreateTempList();
            items = LogItemsList.GetData();
        }

        void CreateTempList() {
            Add(new Book() { Name = "Dvir", Author = "aaa", Publisher = "zzz", Edition = 3, Amount = 7, Price = 30, });
            Add(new Book() { Name = "Berta", Author = "bbb", Publisher = "xxx", Edition = 1, Amount = 8, Price = 40, });
            Add(new Book() { Name = "Ori", Author = "ccc", Publisher = "vvv", Edition = 5, Amount = 9, Price = 70, });
            Add(new Book() { Name = "Ofri", Author = "ddd", Publisher = "nnn", Edition = 2, Amount = 10, Price = 60, });
            Add(new Book() { Name = "Stav", Author = "eee", Publisher = "mmm", Edition = 7, Amount = 11, Price = 50, });
            Add(new Journal() { Name = "Kobi", Author = "fff", Publisher = "sss", Edition = 4, Amount = 12, Price = 20, });
            Add(new Journal() { Name = "Nisan", Author = "ggg", Publisher = "rrr", Edition = 9, Amount = 13, Price = 10, });

            Save();
        }

        public void Save() {
            LogItemsList.SaveData(items);
        }

        public List<Item> FilterList(Predicate<Item> predicate) {
            return (from x in items where predicate(x) select x).ToList();
        }


        public Item this[int index] { get => ((IList<Item>)items)[index]; set => ((IList<Item>)items)[index] = value; }
        public int Count => ((ICollection<Item>)items).Count;
        public void Add(Item item) {
            ((ICollection<Item>)items).Add(item);
            Save();
        }
        public bool Remove(Item item) {
            bool a = ((ICollection<Item>)items).Remove(item);
            Save();
            return a;
        }
    }
}