using System;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Nsu.HackathonProblem.Contracts;

namespace Nsu.HackathonProblem.Utils
{
    public static class EmployeesReader
    {
        public static IEnumerable<Junior> ReadJuniors(string filePath)
        {
            using var streamReader = new StreamReader(filePath);
            using var csvReader = new CsvReader(streamReader, new CsvConfiguration(CultureInfo.CurrentCulture) { Delimiter = ";" });
            return csvReader.GetRecords<Junior>().ToList();
        }

        public static IEnumerable<TeamLead> ReadTeamLeads(string filePath)
        {
            using var streamReader = new StreamReader(filePath);
            using var csvReader = new CsvReader(streamReader, new CsvConfiguration(CultureInfo.CurrentCulture) { Delimiter = ";" });
            return csvReader.GetRecords<TeamLead>().ToList();
        }
    }
}