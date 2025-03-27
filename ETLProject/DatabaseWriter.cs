using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETLProject
{
    public class DatabaseWriter
    {
        private readonly string _connectionString;

        public DatabaseWriter(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void WriteTrips(List<Trip> trips)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using var bulkCopy = new SqlBulkCopy(_connectionString);
                bulkCopy.DestinationTableName = "Trips";
                MapColumns(bulkCopy);

                var dataTable = CreateDataTable();
                AddTripsToTable(trips, dataTable);

                bulkCopy.WriteToServer(dataTable);
            }
        }

        private void MapColumns (SqlBulkCopy bulkCopy)
        {
            bulkCopy.ColumnMappings.Add("PULocationID", "PULocationID");
            bulkCopy.ColumnMappings.Add("DOLocationID", "DOLocationID");
            bulkCopy.ColumnMappings.Add("fare_amount", "fare_amount");
            bulkCopy.ColumnMappings.Add("tip_amount", "tip_amount");
            bulkCopy.ColumnMappings.Add("passenger_count", "passenger_count");
            bulkCopy.ColumnMappings.Add("trip_distance", "trip_distance");
            bulkCopy.ColumnMappings.Add("tpep_pickup_datetime", "tpep_pickup_datetime");
            bulkCopy.ColumnMappings.Add("tpep_dropoff_datetime", "tpep_dropoff_datetime");
            bulkCopy.ColumnMappings.Add("store_and_fwd_flag", "store_and_fwd_flag");
        }

        private DataTable CreateDataTable()
        {
            var dataTable = new DataTable();

            dataTable.Columns.Add("PULocationID", typeof(int));
            dataTable.Columns.Add("DOLocationID", typeof(int));
            dataTable.Columns.Add("fare_amount", typeof(decimal));
            dataTable.Columns.Add("tip_amount", typeof(decimal));
            dataTable.Columns.Add("passenger_count", typeof(int));
            dataTable.Columns.Add("trip_distance", typeof(decimal));
            dataTable.Columns.Add("tpep_pickup_datetime", typeof(DateTime));
            dataTable.Columns.Add("tpep_dropoff_datetime", typeof(DateTime));
            dataTable.Columns.Add("store_and_fwd_flag", typeof(string));

            return dataTable;
        }

        private void AddTripsToTable(List<Trip> trips, DataTable table)
        {
            foreach (var trip in trips)
            {
                table.Rows.Add(trip.PULocationId, trip.DOLocationId,
                                    trip.FareAmount, trip.TipAmount, trip.PassengerCount,
                                    trip.TripDistance, trip.PickUpDateTime, trip.DropOffDateTime, trip.StoreAndFwdFlag);
            }
        }
    }
}
