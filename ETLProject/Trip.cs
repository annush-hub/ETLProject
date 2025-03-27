namespace ETLProject
{
    public class Trip
    {
        public int PULocationId { get; set; }
        public int DOLocationId { get; set; }

        public decimal FareAmount { get; set; }
        public decimal TipAmount { get; set;}

        public int? PassengerCount { get; set; }
        public decimal TripDistance { get; set; }

        public DateTime PickUpDateTime { get; set; }
        public DateTime DropOffDateTime  { get; set; }
        public string? StoreAndFwdFlag { get; set; }
    }
}
