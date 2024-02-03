using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Terrain;
using Unity.VisualScripting;
using UnityEngine;

namespace Terrain
{
    public static class DecoratorHandler
    {
        /*
            DecoratorHandler is used to set HexTile decorators for each HexTile
        */

        public static void SetHexDecorators(List<HexTile> hex_list){    // Wraps each Hex Object with a Decorator Object for each HexTile - called from MapGeneration
            foreach(HexTile hex in hex_list){
                SetFeatureDecorators(hex);
                SetLandDecorator(hex);
                SetRegionDecorator(hex);
                SetResourceDecorator(hex);
                SetElevationDecorator(hex);
                SetStructureDecorator(hex);
            }
        }

        private static void SetStructureDecorator(HexTile hex)
        {
            switch (hex.structure_type)
            {
                case EnumHandler.StructureType.Capital:
                    hex = new CapitalDecorator(hex);
                    break;
            }
        }

        private static void SetElevationDecorator(HexTile hex)  // Sets Elevation Decorator
        {
            switch (hex.elevation_type)
            {
                case EnumHandler.HexElevation.Mountain:
                    hex = new MountainDecorator(hex);
                    break;
                case EnumHandler.HexElevation.Small_Hill:
                    hex = new SmallHillDecorator(hex);
                    break;
                case EnumHandler.HexElevation.Canyon:
                    hex = new CanyonDecorator(hex);
                    break;
                case EnumHandler.HexElevation.Valley:
                    hex = new ValleyDecorator(hex);
                    break;
                case EnumHandler.HexElevation.Large_Hill:
                    hex = new LargeHillDecorator(hex);
                    break;
                case EnumHandler.HexElevation.Flatland:
                    hex = new FlatlandDecorator(hex);
                    break;
            }
        }

        private static void SetResourceDecorator(HexTile hex)   // Sets Resource Decorator
        {
            switch (hex.resource_type)
            {
                case EnumHandler.HexResource.Bananas:
                    hex = new BananasDecorator(hex);
                    break;
                case EnumHandler.HexResource.Cattle:
                    hex = new CattleDecorator(hex);
                    break;
                case EnumHandler.HexResource.Gems: 
                    hex = new GemsDecorator(hex);
                    break;
                case EnumHandler.HexResource.Spices:
                    hex = new IncenseDecorator(hex);
                    break;
                case EnumHandler.HexResource.Iron:
                    hex = new IronDecorator(hex);
                    break;
                case EnumHandler.HexResource.Stone:
                    hex = new StoneDecorator(hex);
                    break;
                case EnumHandler.HexResource.Pigs:
                    hex = new PigsDecorator(hex);
                    break;
            }
        }

        private static void SetRegionDecorator(HexTile hex) // Sets Region Decorator
        {
            switch (hex.region_type)
            {
                case EnumHandler.HexRegion.Plain:
                    hex = new PlainDecorator(hex);
                    break;
                case EnumHandler.HexRegion.Desert:
                    hex = new DesertDecorator(hex);
                    break;
                case EnumHandler.HexRegion.Grassland:
                    hex = new GrasslandDecorator(hex);
                    break;
                case EnumHandler.HexRegion.Highland:
                    hex = new HighlandsDecorator(hex);
                    break;
                case EnumHandler.HexRegion.Jungle:
                    hex = new JungleDecorator(hex);
                    break;
                case EnumHandler.HexRegion.Swamp:
                    hex = new SwampDecorator(hex);
                    break;
                case EnumHandler.HexRegion.Tundra:
                    hex = new TundraDecorator(hex);
                    break;
            }
        }

        private static void SetLandDecorator(HexTile hex)   // Sets Land Decorator
        {
            switch (hex.land_type)
            {
                case EnumHandler.LandType.Water:
                    hex = new WaterDecorator(hex);
                    break;
                case EnumHandler.LandType.Land:
                    hex = new LandDecorator(hex);
                    break;
            }
        }

        private static void SetFeatureDecorators(HexTile hex)  // Sets Feature Decorator
        {
            switch (hex.feature_type)
            {
                case EnumHandler.HexNaturalFeature.Forest:
                    hex = new ForestDecorator(hex);
                    break;
                case EnumHandler.HexNaturalFeature.Rocks:
                    hex = new RockDecorator(hex);
                    break;
                case EnumHandler.HexNaturalFeature.Jungle:
                    hex = new JungleDecorator(hex);
                    break;
                case EnumHandler.HexNaturalFeature.Oasis:
                    hex = new OasisDecorator(hex);
                    break;
                case EnumHandler.HexNaturalFeature.Swamp:
                    hex = new SwampDecorator(hex);
                    break;
                case EnumHandler.HexNaturalFeature.Heavy_Vegetation:
                    hex = new WheatDecorator(hex);
                    break;
            }
        }

    }
}