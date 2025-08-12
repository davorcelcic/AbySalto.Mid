using System.Text.Json.Serialization;

namespace AbySalto.Mid.WebApi.Models
{
    public class Product
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("category")]
        public string? Category { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("discountPercentage")]
        public double DiscountPercentage { get; set; }

        [JsonPropertyName("rating")]
        public double Rating { get; set; }

        [JsonPropertyName("stock")]
        public int Stock { get; set; }

        [JsonPropertyName("tags")]
        public List<string>? Tags { get; set; }

        [JsonPropertyName("brand")]
        public string? Brand { get; set; }

        [JsonPropertyName("sku")]
        public string? Sku { get; set; }

        [JsonPropertyName("weight")]
        public double Weight { get; set; }

        [JsonPropertyName("dimensions")]
        public Dimensions? Dimensions { get; set; }

        [JsonPropertyName("warrantyInformation")]
        public string? WarrantyInformation { get; set; }

        [JsonPropertyName("shippingInformation")]
        public string? ShippingInformation { get; set; }

        [JsonPropertyName("availabilityStatus")]
        public string? AvailabilityStatus { get; set; }

        [JsonPropertyName("reviews")]
        public List<Review>? Reviews { get; set; }

        [JsonPropertyName("returnPolicy")]
        public string? ReturnPolicy { get; set; }

        [JsonPropertyName("minimumOrderQuantity")]
        public int MinimumOrderQuantity { get; set; }

        [JsonPropertyName("meta")]
        public Meta? Meta { get; set; }

        [JsonPropertyName("images")]
        public List<string>? Images { get; set; }

        [JsonPropertyName("thumbnail")]
        public string? Thumbnail { get; set; }
    }

    public class Dimensions
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("width")]
        public double Width { get; set; }

        [JsonPropertyName("height")]
        public double Height { get; set; }

        [JsonPropertyName("depth")]
        public double Depth { get; set; }
    }

    public class Review
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("rating")]
        public int Rating { get; set; }

        [JsonPropertyName("comment")]
        public string? Comment { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("reviewerName")]
        public string? ReviewerName { get; set; }

        [JsonPropertyName("reviewerEmail")]
        public string? ReviewerEmail { get; set; }
    }

    public class Meta
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("barcode")]
        public string? Barcode { get; set; }

        [JsonPropertyName("qrCode")]
        public string? QrCode { get; set; }
    }
}
