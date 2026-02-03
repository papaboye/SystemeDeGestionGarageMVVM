using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;
using TravailPratique2.Models;

namespace TravailPratique2.ViewModels
{
    class ImportationCSV
    {
        public static List<Voiture> ImporterVoiture()
        {
            List<Voiture> voiture = new List<Voiture>(); //
            var chemin = (@"C:\Users\HP\source\repos\TravauxPratiques\vehicules_db.csv");
            //var destination = (@"C:\Users\HP\source\repos\TravauxPratiques\vehicules_db.json");
            var csvconfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                Encoding = Encoding.UTF8,
                HasHeaderRecord = true,
                HeaderValidated = null,
                MissingFieldFound = null

            };

            using (var reader = new StreamReader(chemin))
            using (var csv = new CsvHelper.CsvReader(reader, csvconfig))
            {
                //csv.Context.RegisterClassMap<VoitureMap>();
                voiture = csv.GetRecords<Voiture>().ToList();

            }

            
            return voiture;

        }
        public static List<Reparation> ImporterReparation()
        {
            List<Reparation> reparation = new List<Reparation>();
            var chemin = Path.Combine(Environment.CurrentDirectory, @"C:\Users\HP\source\repos\TravauxPratiques\reparations_db.csv");

            using (var stre = new StreamReader(chemin))
            {
                var csvconfig = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ",",
                    Encoding = Encoding.UTF8,
                    HasHeaderRecord = true,
                    HeaderValidated = null,
                    MissingFieldFound = null
                };
                using (var csv = new CsvReader(stre, csvconfig))
                {

                    return csv.GetRecords<Reparation>().ToList();

                }

            }
        }
    }
}
