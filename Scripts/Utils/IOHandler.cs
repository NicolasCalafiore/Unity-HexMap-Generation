using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

public static class IOHandler{
    public static List<string> ReadXml(string filePath, string region)
    {
       XDocument doc = XDocument.Load(filePath);
        List<string> cityNames = doc.Descendants("Region")
                                    .Where(r => r.Attribute("name").Value.Equals(region, StringComparison.OrdinalIgnoreCase))
                                    .Descendants("City")
                                    .Select(c => c.Value)
                                    .ToList();

        return cityNames;
    }
    
}