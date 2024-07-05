using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Terrain;
using UnityEngine;
using static Character.CharacterEnums;
using static Terrain.GovernmentEnums;

public static class IOHandler{
    private static string feautures_path = "Prefab/Natural_Features/";
    private static string city_names_path = ".\\Assets\\Game\\Resources\\Data\\CityNames.xml";
    private static string government_prefixes_path = ".\\Assets\\Game\\Resources\\Data\\GovernmentPrefixes.xml";
    private static string state_names_path = ".\\Assets\\Game\\Resources\\Data\\StateNames.xml";
    private static string character_names_path = ".\\Assets\\Game\\Resources\\Data\\CharacterNames.xml";
    private static string government_titles_path = ".\\Assets\\Game\\Resources\\Data\\GovernmentTitles.xml";
    private static string continent_names_path = ".\\Assets\\Game\\Resources\\Data\\Continents.xml";

    public static List<string> ReadCityNamesRegionSpecified(HexTile hex)
    {
        string region = hex.region_type.ToString();
    
        XDocument doc = XDocument.Load(city_names_path);
        List<string> cityNames = doc.Descendants("Region")
                                    .Where(r => r.Attribute("name").Value.Equals(region, StringComparison.OrdinalIgnoreCase))
                                    .Descendants("City")
                                    .Select(c => c.Value)
                                    .ToList();

        return cityNames;
    }


    public static List<string> ReadPrefixNamesRegionSpecified(string region)
    {

        XDocument doc = XDocument.Load(government_prefixes_path);
    
        List<string> prefixes = doc.Descendants("Government")
                                    .Where(r => r.Attribute("name").Value.Equals(region, StringComparison.OrdinalIgnoreCase))
                                    .Descendants("Identifier")
                                    .Where(p => p.Attribute("loc")?.Value == "beg")
                                    .Select(c => c.Value)
                                    .ToList();
        return prefixes;
    }

    public static List<string> ReadSuffixNamesRegionSpecified(string region)
    {

        XDocument doc = XDocument.Load(government_prefixes_path);
        List<string> suffixes = doc.Descendants("Government")
                                    .Where(r => r.Attribute("name").Value.Equals(region, StringComparison.OrdinalIgnoreCase))
                                    .Descendants("Identifier")
                                    .Where(p => p.Attribute("loc")?.Value == "end")
                                    .Select(c => c.Value)
                                    .ToList();

        return suffixes;
    }

    public static List<string> ReadStateNamesRegionSpecified(string region)
    {
    
        XDocument doc = XDocument.Load(state_names_path);
        List<string> countryNames = doc.Descendants("Regions")
                                    .FirstOrDefault(r => r.Attribute("name")?.Value.Equals(region, StringComparison.OrdinalIgnoreCase) == true)
                                    ?.Descendants("Country")
                                    .Select(c => c.Value)
                                    .ToList();

        return countryNames ?? new List<string>(); // Return an empty list if the region is not found
    }

    public static List<string> ReadFirstNamesRegionSpecified(string region, string gender)
    {
        XDocument doc = XDocument.Load(character_names_path);
        
        var firstNames = doc.Descendants("Regions")
                                    .FirstOrDefault(r => r.Attribute("name")?.Value.Equals(region, StringComparison.OrdinalIgnoreCase) == true)
                                    ?.Element("FirstName")
                                    ?.Elements("Name");

        if (gender != null)
            firstNames = firstNames?.Where(c => c.Attribute("gender")?.Value.Equals(gender, StringComparison.OrdinalIgnoreCase) == true);
        
        List<string> first_names_final = firstNames?.Select(c => c.Value)?.ToList();

        return first_names_final ?? new List<string>(); // Return an empty list if the region is not found or no first names are available
    }

    public static List<string> ReadLastNamesRegionSpecified(string region)
    {
        XDocument doc = XDocument.Load(character_names_path);
        
        var lastNames = doc.Descendants("Regions")
                                    .FirstOrDefault(r => r.Attribute("name")?.Value.Equals(region, StringComparison.OrdinalIgnoreCase) == true)
                                    ?.Element("LastName")
                                    ?.Elements("Name");


        List<string> last_names_final = lastNames?.Select(c => c.Value)?.ToList();


        return last_names_final ?? new List<string>(); // Return an empty list if the region is not found or no first names are available
    }

    public static List<string> ReadTitles(RoleType characterType, GovernmentType governmentType)
    {
        XDocument doc = XDocument.Load(government_titles_path);

        var titles = doc.Descendants("Government")
                        .FirstOrDefault(r => r.Attribute("name")?.Value.Equals(governmentType.ToString(), StringComparison.OrdinalIgnoreCase) == true)
                        ?.Elements("Title")
                        .Where(t => t.Attribute("type")?.Value.Equals(characterType.ToString(), StringComparison.OrdinalIgnoreCase) == true)
                        .Select(t => t.Value)
                        .ToList();

        return titles ?? new List<string>(); // Return an empty list if the government type or character type is not found
    }

    public static List<string> ReadContinentNames()
    {
        XDocument doc = XDocument.Load(continent_names_path);

        var names = doc.Descendants("Name")
                    .Select(n => n.Value)
                    .ToList();

        return names;
    }

    public static GameObject LoadFeature(string feature_name)
    {
        return Resources.Load<GameObject>(feautures_path + feature_name);
    }

    public static GameObject LoadPrefab(string path)
    {
        return Resources.Load<GameObject>("Prefab/" + path);
    }
}