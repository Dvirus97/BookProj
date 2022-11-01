using BookLib.Models;
using DbService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLib {

    public class DiscountManager {
        /// <summary>
        /// instance - single tone
        /// </summary>
        public static DiscountManager Instance { get; } = new DiscountManager();

        /// <summary>
        /// ObservableCollection - list of all the discount in the store
        /// </summary>
        public ObservableCollection<Discount> AllDiscounts { get; set; } = new ObservableCollection<Discount>();

        /// <summary>
        /// jsonSave instance to save all discounts
        /// </summary>
        JsonSave<Discount> LogDiscountsList = new JsonSave<Discount>("LogDiscountsList.json");

        private DiscountManager() {
            Load();
        }

        public void Add(Discount dis) {
            AllDiscounts.Add(dis);
            Save();
        }

        public void Remove(Discount dis) {
            AllDiscounts.Remove(dis);
            Save();
        }

        public void Save() {
            LogDiscountsList.SaveData(AllDiscounts);
        }
        public void Load() {
            AllDiscounts = new ObservableCollection<Discount>(LogDiscountsList.GetData());
        }
    }
}
