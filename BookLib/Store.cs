using BookLib.Enums;
using BookLib.Models;
using DbService;
using System.Collections;
using System.Collections.ObjectModel;
using System.Data.SqlTypes;
using System.Globalization;

namespace BookLib {
    public class Store {

        /// <summary>
        /// instace - single tone
        /// </summary>
        public static Store Instace { get; } = new Store();

        /// <summary>
        /// list of all items in the store
        /// </summary>
        public List<Item> Items { get; } = new List<Item>();

        /// <summary>
        /// jsonSave instance for save all items 
        /// </summary>
        private readonly JsonSave<Item> LogItemsList = new JsonSave<Item>("LogItemsList.json");

        /// <summary>
        /// TextSave instance for save error in the program
        /// </summary>
        public TextSave LogError { get; } = new TextSave("LogError.txt");

        #region enum lists - convert enum to list
        public List<Genre> GenreList { get; } = Enum.GetValues(typeof(Genre)).Cast<Genre>().ToList();
        public List<DiscountBy> DiscountByList { get; } = Enum.GetValues(typeof(DiscountBy)).Cast<DiscountBy>().ToList();
        public List<ItemType> ItemTypeList { get; } = Enum.GetValues(typeof(ItemType)).Cast<ItemType>().ToList();
        public List<FilterBy> FilterByList { get; } = Enum.GetValues(typeof(FilterBy)).Cast<FilterBy>().ToList();
        #endregion

        #region author/ name/ publisher lists - convert props from all items to list
        public List<string> AllAuthor {
            get {
                List<string> temp = new List<string>();
                foreach (var item in Items) {
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
                foreach (var item in Items) {
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
                foreach (var item in Items) {
                    if (item is null || item.Publisher is null) throw new ArgumentNullException();
                    if (!temp.Contains(item.Publisher)) {
                        temp.Add(item.Publisher);
                    }
                }
                return temp;
            }
        }
        #endregion

        /// <summary>
        /// bool to know if admin is logged in or not. if false - user
        /// </summary>
        public bool IsAdmin { get; set; }

        private Store() {
            //CreateTempList();
            Items = LogItemsList.GetData();
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
            LogItemsList.SaveData(Items);
        }

        /// <summary>
        /// get predicta and filter the items list
        /// </summary>
        /// <param name="predicate">bool quastion</param>
        /// <returns>filterd list</returns>
        public List<Item> FilterList(Predicate<Item> predicate) {
            return (from x in Items where predicate(x) select x).ToList();
        }


        public Item this[int index] { get => ((IList<Item>)Items)[index]; set => ((IList<Item>)Items)[index] = value; }
        public int Count => ((ICollection<Item>)Items).Count;

        /// <summary>
        /// add new item to items list and save.
        /// </summary>
        /// <param name="item"></param>
        public void Add(Item item) {
            ((ICollection<Item>)Items).Add(item);
            Save();
        }

        /// <summary>
        /// remove item from items list and save
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(Item item) {
            bool a = ((ICollection<Item>)Items).Remove(item);
            Save();
            return a;
        }
    }
}