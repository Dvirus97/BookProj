using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookLib.Enums;

namespace BookLib.Models {


    public abstract class Item {
        public string? Name { get; set; }
        public string? Author { get; set; }
        public string? Publisher { get; set; }
        public DateTime? PublishDate { get; set; }
        public double Price { get; set; }
        public Genre Genre { get; set; }
        public int Edition { get; set; }
        public int Amount { get; set; }
        public int Isbn { get; set; }
        /// <summary>
        /// Discount number $
        /// </summary>
        public double Discount {
            get {
                Discount? temp = BiggestDiscount;
                return temp is null ? 0 : temp.Price;
            }
        }

        /// <summary>
        /// price after Discount $
        /// </summary>
        public double AfterDiscount { get { return Price - ((Price * Discount) / 100); } }
        /// <summary>
        /// counter for shopping cart
        /// </summary>
        public int ShopCount { get; set; }
        public virtual string? PhotoPath { get; set; }

        /// <summary>
        /// the biggest discount from this.DiscountList  type:Discount
        /// </summary>
        private Discount? BiggestDiscount {
            get {
                List<Discount>? temp = DiscountList;
                if (temp is null) return null;
                Discount? biggestDiscount = temp.FirstOrDefault();
                if (biggestDiscount is null) return null;
                foreach (var item in temp) {
                    if (item.Price > biggestDiscount.Price) {
                        biggestDiscount = item;
                    }
                }
                return biggestDiscount;
            }
        }

        /// <summary>
        /// discount list that fit this item. take form all discount
        /// </summary>
        private List<Discount>? DiscountList {
            get {
                List<Discount>? _discountList = new List<Discount>();
                foreach (var item in DiscountManager.Instance.AllDiscounts) {
                    switch (item.DiscountBy) {
                        case DiscountBy.Item_Type:
                            if (this.GetType().Name.Equals(item.Name)) {
                                _discountList?.Add(item);
                            }
                            break;
                        case DiscountBy.Name:
                            if (Name is not null && this.Name.Equals(item.Name)) {
                                _discountList?.Add(item);
                            }
                            break;
                        case DiscountBy.Author:
                            if (Author is not null && this.Author.Equals(item.Name)) {
                                _discountList?.Add(item);
                            }
                            break;
                        case DiscountBy.Publisher:
                            if (Publisher is not null && this.Publisher.Equals(item.Name)) {
                                _discountList?.Add(item);
                            }
                            break;
                        case DiscountBy.Genre:
                            if (this.Genre.ToString().Equals(item.Name)) {
                                _discountList?.Add(item);
                            }
                            break;
                        case DiscountBy.AllStore:
                            _discountList?.Add(item);
                            break;
                        default:
                            break;
                    }
                }
                return _discountList;
            }
        }


        Random Random = new();

        public Item() {
            Isbn = Random.Next(100_000, 100_000_000);
        }


        public override string ToString() {
            return $"type: {this.GetType().Name}, \nname: {Name}, \nauthor: {Author}, \npublisher: {Publisher}, \nprice: {Price:c}, \ngenre: {Genre}, \npublish date: {PublishDate:d}, \nedition: {Edition}, \namount: {Amount}, \ndiscount: {Discount}%, \naftre discount: {AfterDiscount:c}, \nisbn: {Isbn}";
        }

        /// <summary>
        /// main details ot show
        /// </summary>
        public string HeadDetail { get => $"name: {Name}, price: {Price:c}, discount: {Discount}%"; }
    }
}
