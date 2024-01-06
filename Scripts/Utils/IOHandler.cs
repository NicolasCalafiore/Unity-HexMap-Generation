using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

public static class IOHandler{
    public static List<string> ReadCityNames(string filePath, string region)
    {
       XDocument doc = XDocument.Load(filePath);
        List<string> cityNames = doc.Descendants("Region")
                                    .Where(r => r.Attribute("name").Value.Equals(region, StringComparison.OrdinalIgnoreCase))
                                    .Descendants("City")
                                    .Select(c => c.Value)
                                    .ToList();

        return cityNames;
    }


    public static List<string> ReadPrefixNames(string filePath, string region)
    {
       XDocument doc = XDocument.Load(filePath);
        List<string> cityNames = doc.Descendants("Government")
                                    .Where(r => r.Attribute("name").Value.Equals(region, StringComparison.OrdinalIgnoreCase))
                                    .Descendants("Prefix")
                                    .Select(c => c.Value)
                                    .ToList();

        return cityNames;
    }

    public static List<string> ReadStateNames(string filePath, string region)
    {
       XDocument doc = XDocument.Load(filePath);
        List<string> cityNames = doc.Descendants("States")
                                    .Where(r => r.Attribute("name").Value.Equals(region, StringComparison.OrdinalIgnoreCase))
                                    .Descendants("name")
                                    .Select(c => c.Value)
                                    .ToList();

        return cityNames;
    }

    
    
}