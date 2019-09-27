using System;

using Xamarin.Forms;

namespace ourU_NetStandard.Models
{
    public class Book
    {

        [Newtonsoft.Json.JsonProperty("id")]
        public string theID { get; set; }

        [Newtonsoft.Json.JsonProperty("ISBN")]
        public string theISBN { get; set; }

        [Newtonsoft.Json.JsonProperty("Author")]
        public string theAuthor { get; set; }

        [Newtonsoft.Json.JsonProperty("Title")]
        public string theTitle { get; set; }

        [Newtonsoft.Json.JsonProperty("Price")]
        public string thePrice { get; set; }

        [Newtonsoft.Json.JsonProperty("Status")]
        public string theStatus { get; set; }

        [Newtonsoft.Json.JsonProperty("Edition")]
        public string theEdition { get; set; }

        [Newtonsoft.Json.JsonProperty("Class")]
        public string theClass { get; set; }

        [Newtonsoft.Json.JsonProperty("deleted")]
        public bool isDeleted { get; set; }

        [Newtonsoft.Json.JsonProperty("isBook")]
        public bool isBook { get; set; }
    [Newtonsoft.Json.JsonProperty("isListing")]
        public bool isListing { get; set; }
        [Newtonsoft.Json.JsonProperty("Phone")]
        public string phoneNumber { get; set; }
        [Newtonsoft.Json.JsonProperty("Email")]
        public string emailAddress { get; set; }
        [Newtonsoft.Json.JsonProperty("Name")]
        public string userName { get; set; }
        [Newtonsoft.Json.JsonProperty("Comment")]
        public string userComment { get; set; }

    }
}

