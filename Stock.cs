using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using TravailPratique2.Models;

namespace TravailPratique2
{
    class Stock
    {
        public static List<Voiture> ImporterVoiture()
        {
            List<Voiture> voiture = new List<Voiture>(); //
            var chemin = (@"C:\Users\HP\source\repos\TravauxPratiques\vehicules_db.csv");
            
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

                return csv.GetRecords<Voiture>().ToList();

            }
           
        }
        public static List<Reparation> ImporterReparation()
        {
            List<Reparation> reparation = new List<Reparation>(); //
            
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
                using (var reader = new StreamReader(chemin))
                using (var csv = new CsvHelper.CsvReader(stre, csvconfig))
                {

                    return csv.GetRecords<Reparation>().ToList();

                }

            }

        }
        //
        public static List<Piece> ImporterPiece()
        {
            List<Piece> reparation = new List<Piece>(); //

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
                using (var reader = new StreamReader(chemin))
                using (var csv = new CsvHelper.CsvReader(stre, csvconfig))
                {

                    return csv.GetRecords<Piece>().ToList();

                }

            }

        }
    }
}
