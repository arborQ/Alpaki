﻿namespace SyncPartyShopSearchIndex.Models
{
    public class PartyItem
    {
        public string name { get; set; }

        public string code { get; set; }

        public string ean { get; set; }

        public string description { get; set; }

        public string photos { get; set; }

        public string category_path { get; set; }

        public string category_name { get; set; }

        public string price_net { get; set; }

        public string tax { get; set; }

        public string price_gross { get; set; }

        public string currency { get; set; }

        public string is_available { get; set; }

        public string stock { get; set; }

        public string availability_date { get; set; }

        public string base_unit_of_measure { get; set; }

        public string items_in_package_count { get; set; }

        public string has_carton_packaging { get; set; }

        public string base_units_in_carton_count { get; set; }

        public string hs_code { get; set; }

        public string country_of_origin { get; set; }

        public string width_cm { get; set; }

        public string height_cm { get; set; }

        public string depth_cm { get; set; }

        public string weight_kg { get; set; }

        public string remarks { get; set; }
    }
}
