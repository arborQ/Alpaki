using Newtonsoft.Json;

namespace SyncPartyShopSearchIndex.Models
{
    public class PartyShopIndexItem
    {
        [JsonProperty("objectID")]
        public string Key { get; set; }

        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public string CategoryPath { get; set; }

        public decimal FromPriceNet { get; set; }

        public decimal ToPriceNet { get; set; }

        public decimal FromPriceGross { get; set; }

        public decimal ToPriceGross { get; set; }

        public string[] ImageUrls { get; set; }

        public PartyShopIndexItemVariant[] Variants { get; set; }

        public class PartyShopIndexItemVariant
        {
            public string VariantName { get; set; }

            public decimal PriceNet { get; set; }

            public decimal PriceGross { get; set; }
        }
    }
}
