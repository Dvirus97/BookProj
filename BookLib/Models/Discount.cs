using BookLib.Enums;
using DbService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BookLib.Models {


    public class Discount {

        /// <summary>
        /// enum of discount by options
        /// </summary>
        public DiscountBy DiscountBy { get; set; }
        /// <summary>
        /// name of the discount by prop
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// double number of the amount of the discount
        /// </summary>
        public double Price { get; set; }

        public override string ToString() {
            return $"DiscountBy: {DiscountBy}, Name: {Name}, Price: {Price}%";
        }

        public Discount(DiscountBy discountBy, string name, double price) {
            DiscountBy = discountBy;
            Name = name;
            Price = price;
        }

    }
}
