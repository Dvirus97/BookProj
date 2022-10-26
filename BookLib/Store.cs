using BookLib.Models;
using DbService;
using System.Collections;
using System.Collections.ObjectModel;
using System.Globalization;

namespace BookLib {
    public class Store : IList<Item>, IEnumerable<Item> {

        public static Store store = new Store();

        public List<Item> items = new List<Item>();
        JsonSave<Item> LogItemsList = new JsonSave<Item>("LogItemsList.json");


        public List<Genre> GenreList { get => Enum.GetValues(typeof(Genre)).Cast<Genre>().ToList(); }
        public List<DiscountBy> DiscountByList { get => Enum.GetValues(typeof(DiscountBy)).Cast<DiscountBy>().ToList(); }
        public List<itemType> ItemTypeList { get => Enum.GetValues(typeof(itemType)).Cast<itemType>().ToList(); }
        public List<FilterBy> FilterByList { get => Enum.GetValues(typeof(FilterBy)).Cast<FilterBy>().ToList(); }

        public List<string> AllAuthor { get => (from x in items select x.Author).ToList(); }
        public List<string> AllName { get => (from x in items select x.Name).ToList(); }
        public List<string> AllPublisher { get => (from x in items select x.Publisher).ToList(); }
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
        }
        public bool Remove(Item item) {
            return ((ICollection<Item>)items).Remove(item);
        }

        #region Ilist

        public bool IsReadOnly => ((ICollection<Item>)items).IsReadOnly;


        public void Clear() {
            ((ICollection<Item>)items).Clear();
        }

        public bool Contains(Item item) {
            return ((ICollection<Item>)items).Contains(item);
        }

        public void CopyTo(Item[] array, int arrayIndex) {
            ((ICollection<Item>)items).CopyTo(array, arrayIndex);
        }

        public IEnumerator<Item> GetEnumerator() {
            return ((IEnumerable<Item>)items).GetEnumerator();
        }

        public int IndexOf(Item item) {
            return ((IList<Item>)items).IndexOf(item);
        }

        public void Insert(int index, Item item) {
            ((IList<Item>)items).Insert(index, item);
        }


        public void RemoveAt(int index) {
            ((IList<Item>)items).RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return ((IEnumerable)items).GetEnumerator();
        }
        #endregion
    }
}