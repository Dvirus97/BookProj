namespace BookLib.Models {
    public enum Genre {
        Undefine, Fantasy, Sci_Fi, Mystery, Romance, Kids
    }

    public enum itemType {
        Book, Journal
    }

    public enum DiscountBy {
        Author, Publisher, Genre, AllStore
    }

    public enum FilterBy {
        Undefine, All, Name, Author, Publisher, Genre, Item_Type
    }

}
