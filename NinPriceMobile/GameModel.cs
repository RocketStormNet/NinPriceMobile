using System;
namespace NinPriceMobile
{
    public class GameModel
    {
        public string Sorting_Title { get; set; }
        public bool price_has_discount_b { get; set; }
        public float price_discount_percentage_f { get; set; }
        public float price_lowest_f { get; set; }
        public string imageUrl;
        public string Image_Url {
            get
            {
                return imageUrl;
            }
            set
            {
                imageUrl = "http:" + value;
            }
        }
    }
}
