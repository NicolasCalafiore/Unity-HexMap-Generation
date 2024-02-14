using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public static class IOHandler{
    public static List<string> ReadCityNamesRegionSpecified(string filePath, string region)
    {
       XDocument doc = XDocument.Load(filePath);
        List<string> cityNames = doc.Descendants("Region")
                                    .Where(r => r.Attribute("name").Value.Equals(region, StringComparison.OrdinalIgnoreCase))
                                    .Descendants("City")
                                    .Select(c => c.Value)
                                    .ToList();

        return cityNames;
    }


    public static List<string> ReadPrefixNamesRegionSpecified(string region)
    {

        string filePath = "C:\\Users\\Nico\\Desktop\\Projects\\Strategy\\Assets\\Game\\Resources\\Data\\GovernmentPrefixes.xml";
       XDocument doc = XDocument.Load(filePath);
        List<string> cityNames = doc.Descendants("Government")
                                    .Where(r => r.Attribute("name").Value.Equals(region, StringComparison.OrdinalIgnoreCase))
                                    .Descendants("Prefix")
                                    .Select(c => c.Value)
                                    .ToList();

        return cityNames;
    }

    public static List<string> ReadStateNamesRegionSpecified(string region)
    {
    
    string filePath = "C:\\Users\\Nico\\Desktop\\Projects\\Strategy\\Assets\\Game\\Resources\\Data\\StateNames.xml";
    XDocument doc = XDocument.Load(filePath);
    List<string> countryNames = doc.Descendants("Regions")
                                   .FirstOrDefault(r => r.Attribute("name")?.Value.Equals(region, StringComparison.OrdinalIgnoreCase) == true)
                                   ?.Descendants("Country")
                                   .Select(c => c.Value)
                                   .ToList();

    return countryNames ?? new List<string>(); // Return an empty list if the region is not found
    }

    public static List<string> ReadFirstNamesRegionSpecified(string filePath, string region, string gender)
    {
        XDocument doc = XDocument.Load(filePath);
        
        var firstNames = doc.Descendants("Regions")
                                    .FirstOrDefault(r => r.Attribute("name")?.Value.Equals(region, StringComparison.OrdinalIgnoreCase) == true)
                                    ?.Element("FirstName")
                                    ?.Elements("Name");

        if (gender != null)
        {
            firstNames = firstNames?.Where(c => c.Attribute("gender")?.Value.Equals(gender, StringComparison.OrdinalIgnoreCase) == true);
        }

        List<string> first_names_final = firstNames?.Select(c => c.Value)?.ToList();


        return first_names_final ?? new List<string>(); // Return an empty list if the region is not found or no first names are available
    }

    public static List<string> ReadLastNamesRegionSpecified(string filePath, string region)
    {
        XDocument doc = XDocument.Load(filePath);
        
        var lastNames = doc.Descendants("Regions")
                                    .FirstOrDefault(r => r.Attribute("name")?.Value.Equals(region, StringComparison.OrdinalIgnoreCase) == true)
                                    ?.Element("LastName")
                                    ?.Elements("Name");


        List<string> last_names_final = lastNames?.Select(c => c.Value)?.ToList();


        return last_names_final ?? new List<string>(); // Return an empty list if the region is not found or no first names are available
    }


    

    
    
}