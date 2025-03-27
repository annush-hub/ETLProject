using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETLProject
{
    public class CsvUtility
    {
        private readonly CsvConfiguration _config;
        
        public CsvUtility(CsvConfiguration config)
        {
            _config = config;
        }

        public List<T> ReadRecords<T>(string inputFilePath)
        {
            List<T> records = new List<T>();

            using (var reader = new StreamReader(inputFilePath))
            using (var csvReader = new CsvReader(reader, _config))
            {
                csvReader.Context.RegisterClassMap<TripDataMap>();
                records = csvReader.GetRecords<T>().ToList();
            }
            return records;
        }

        public void WriteRecords<T> (List<T> values, string outputFilePath)
        {
            using (var writer = new StreamWriter(outputFilePath))
            using (var csvWriter = new CsvWriter(writer, _config))
            {
                csvWriter.Context.RegisterClassMap<TripDataMap>();
                csvWriter.WriteRecords(values);
            }
        }
    }
}
