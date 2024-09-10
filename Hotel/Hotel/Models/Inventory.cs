namespace Hotel.Models
{
    public class Inventory
    {
        public int InventoryId { get; set; }

        public string? ItemName { get; set; }

        public string? ItemDescription { get; set; }

        public int Quantity { get; set; }

        public double? UnitPrice { get; set; }
    }
}
