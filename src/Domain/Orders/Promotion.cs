namespace Domain.Orders
{
    public class Promotion
    {
        public string Id { get; set; }
        public int DiscountedPrice { get; set; }
        public int PriceTrigger { get; set; }
        public bool Active { get; set; }

        public string[] ApplicableIDs { get; set; }
    }
}