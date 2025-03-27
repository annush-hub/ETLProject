using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETLProject
{
    public class TripDataMap : ClassMap<Trip>
    {
        public TripDataMap()
        {
            Map(m => m.PULocationId).Name("PULocationID");
            Map(m => m.DOLocationId).Name("DOLocationID");
            Map(m => m.FareAmount).Name("fare_amount");
            Map(m => m.TipAmount).Name("tip_amount");
           Map(m => m.PassengerCount).Name("passenger_count");
            Map(m => m.TripDistance).Name("trip_distance");
            Map(m => m.PickUpDateTime).Name("tpep_pickup_datetime");
            Map(m => m.DropOffDateTime).Name("tpep_dropoff_datetime");
            Map(m => m.StoreAndFwdFlag).Name("store_and_fwd_flag");
        }
    }
}
