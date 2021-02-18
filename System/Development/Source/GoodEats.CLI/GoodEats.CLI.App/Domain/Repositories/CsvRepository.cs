using GoodEats.CLI.Configurations;
using GoodEats.CLI.Core;
using GoodEats.CLI.Domain.Entities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GoodEats.CLI.Domain.Repositories
{
    public interface ICsvRepository: IRepository
    {
        List<TruckDetailEntity> GetCsvData();
    }
    public class CsvRepository: ICsvRepository
    {
        private readonly DataConfig _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvRepository"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public CsvRepository(IOptions<DataConfig> configuration)
        {
            _configuration = configuration.Value;
        }

        /// <summary>
        /// Gets the CSV data.
        /// </summary>
        /// <returns></returns>
        public List<TruckDetailEntity> GetCsvData()
        {
            var filePath = _configuration.DataPath;
            if (!File.Exists(filePath))
            {
                return new List<TruckDetailEntity>();
            }

            var csvLines = new List<string>();
            using var reader = new StreamReader(File.OpenRead(filePath));
            while (!reader.EndOfStream)
            {
                csvLines.Add(reader.ReadLine());
            }
            var header = csvLines.First();
            var records = csvLines.Skip(1).ToArray();
            var jArray = GetJsonArray(header, records);

            return jArray.ToObject<List<TruckDetailEntity>>();
        }
        private static JArray GetJsonArray(string header, string[] csvLines, char delimiter = ',')
        {
            if (!csvLines.Any())
            {
                return new JArray();
            }

            if (string.IsNullOrEmpty(header))
            {
                // unable to continue
                throw new ArgumentException(paramName: nameof(header), message: "Cannot be null or empty.");
            }

            var headerParts = header.Split(delimiter);

            if (headerParts.GroupBy(part => part).Where(partGroup => partGroup.Count() > 1).Any())
            {
                // unable to continue
                throw new InvalidOperationException($"There are repeating headers in '{header}'");
            }

            var jsonArray = new JArray();

            foreach (var line in csvLines)
            {
                var csvParts = line.Split(delimiter);

                if (csvParts.Count() == headerParts.Count())
                {
                    jsonArray.Add(GetJObject(headerParts, csvParts));
                }
            }

            return jsonArray;
        }

        private static JObject GetJObject(string[] headerParts, string[] rowParts)
        {
            var jsonObject = new JObject();

            for (var i = 0; i < headerParts.Count(); i++)
            {
                jsonObject.Add(headerParts[i], rowParts[i]);
            }

            return jsonObject;
        }
    }
}
