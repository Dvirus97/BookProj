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

        public static DiscountManager Instance { get; } = new DiscountManager();
        public ObservableCollection<Discount> AllDiscounts { get; set; } = new ObservableCollection<Discount>();
        JsonSave<Discount> LogDiscountsList = new JsonSave<Discount>("LogDiscountsList.json");

        private DiscountManager() {
            Load();
        }

        public void Save() {
            LogDiscountsList.SaveData(AllDiscounts);
        }
        public void Load() {
            AllDiscounts = new ObservableCollection<Discount>(LogDiscountsList.GetData());
        }
    }
}
