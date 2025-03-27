using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETLProject
{
    public static class TripFormatter
    {
        public static List<Trip> UpdateFlags(List<Trip> trips) {

            return trips.Select(t =>
            {
                t.StoreAndFwdFlag = t.StoreAndFwdFlag == null ? null : (t.StoreAndFwdFlag == "N" ? "No" : "Yes");
                return t;
            }).ToList();
        }

        public static List<Trip> UpdateTimeZone(List<Trip> trips)
        {
            var estTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

            return trips.Select(t =>
            {
                t.PickUpDateTime = TimeZoneInfo.ConvertTimeToUtc(t.PickUpDateTime, estTimeZone);
                t.DropOffDateTime = TimeZoneInfo.ConvertTimeToUtc(t.DropOffDateTime, estTimeZone);
                return t;
            }).ToList();        
        }
    }
}
