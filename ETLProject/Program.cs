using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using ETLProject;

string rootDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
string inputFilePath = Path.Combine(rootDirectory, "Data", "sample-cab-data.csv");
string outputFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "duplicates.csv");

List<Trip> records = new List<Trip>();
List<Trip> uniqueTrips = new List<Trip>();
List<Trip> duplicates = new List<Trip>();


var config = new CsvConfiguration(CultureInfo.InvariantCulture)
{
    TrimOptions = TrimOptions.Trim,
    IgnoreBlankLines = true
};

var csv = new CsvUtility(config);
records = csv.ReadRecords<Trip>(inputFilePath);

uniqueTrips = records.GroupBy(r => new { r.PickUpDateTime, r.DropOffDateTime, r.PassengerCount })
                         .Select(g => g.First())
                         .ToList();

duplicates = records.Except(uniqueTrips).ToList();

csv.WriteRecords<Trip>(duplicates, outputFilePath);

var flagUpdatedTrips = TripFormatter.UpdateFlags(uniqueTrips);
var timeUpdatedTrips = TripFormatter.UpdateTimeZone(flagUpdatedTrips);

var connectionString = "Server=localhost;Database=ETLDatabase;Trusted_Connection=True;";

var dbWriter = new DatabaseWriter(connectionString);
dbWriter.WriteTrips(timeUpdatedTrips);

