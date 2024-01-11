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
            if(hex.structure_type == EnumHandler.StructureType.Capital){
                hex = new CapitalDecorator(hex);
            }
        }

        private static void SetElevationDecorator(HexTile hex)  // Sets Elevation Decorator
        {
            if(hex.elevation_type == EnumHandler.HexElevation.Mountain){
                hex = new MountainDecorator(hex);
            }
            if(hex.elevation_type == EnumHandler.HexElevation.Small_Hill){
                hex = new SmallHillDecorator(hex);
            }
            if(hex.elevation_type == EnumHandler.HexElevation.Canyon){
                hex = new CanyonDecorator(hex);
            }
            if(hex.elevation_type == EnumHandler.HexElevation.Valley){
                hex = new ValleyDecorator(hex);
            }
            if(hex.elevation_type == EnumHandler.HexElevation.Large_Hill){
                hex = new LargeHillDecorator(hex);
            }
            if(hex.elevation_type == EnumHandler.HexElevation.Flatland){
                hex = new FlatlandDecorator(hex);
            }

        }

        private static void SetResourceDecorator(HexTile hex)   // Sets Resource Decorator
        {
            if(hex.resource_type == EnumHandler.HexResource.Bananas){
                hex = new BananasDecorator(hex);
            }
            if(hex.resource_type == EnumHandler.HexResource.Cattle){
                hex = new CattleDecorator(hex);
            }
            if(hex.resource_type == EnumHandler.HexResource.Gems){
                hex = new GemsDecorator(hex);
            }
            if(hex.resource_type == EnumHandler.HexResource.Incense){
                hex = new IncenseDecorator(hex);
            }
            if(hex.resource_type == EnumHandler.HexResource.Iron){
                hex = new IronDecorator(hex);
            }
            if(hex.resource_type == EnumHandler.HexResource.Stone){
                hex = new StoneDecorator(hex);
            }
            


        }

        private static void SetRegionDecorator(HexTile hex) // Sets Region Decorator
        {
            if(hex.region_type == EnumHandler.HexRegion.Plains){
                hex = new PlainDecorator(hex);
            }
            if(hex.region_type == EnumHandler.HexRegion.Desert){
                hex = new DesertDecorator(hex);
            }
            if(hex.region_type == EnumHandler.HexRegion.Grassland){
                hex = new GrasslandDecorator(hex);
            }
            if(hex.region_type == EnumHandler.HexRegion.Highlands){
                hex = new HighlandsDecorator(hex);
            }
            if(hex.region_type == EnumHandler.HexRegion.Jungle){
                hex = new JungleDecorator(hex);
            }
            if(hex.region_type == EnumHandler.HexRegion.Swamp){
                hex = new SwampDecorator(hex);
            }
            if(hex.region_type == EnumHandler.HexRegion.Tundra){
                hex = new TundraDecorator(hex);
            }

        }

        private static void SetLandDecorator(HexTile hex)   // Sets Land Decorator
        {
            if(hex.land_type == EnumHandler.LandType.Water){
                hex = new WaterDecorator(hex);
            }
            if(hex.land_type == EnumHandler.LandType.Land){
                hex = new LandDecorator(hex);
            }
        }

        private static void SetFeatureDecorators(HexTile hex){  // Sets Feature Decorator

            if(hex.feature_type == EnumHandler.HexNaturalFeature.Forest){
                hex = new ForestDecorator(hex);
            }
            if(hex.feature_type == EnumHandler.HexNaturalFeature.Rocks){
                hex = new RockDecorator(hex);
            }
            if(hex.feature_type == EnumHandler.HexNaturalFeature.Jungle){
                hex = new JungleDecorator(hex);
            }
            if(hex.feature_type == EnumHandler.HexNaturalFeature.Oasis){
                hex = new OasisDecorator(hex);
            }
            if(hex.feature_type == EnumHandler.HexNaturalFeature.Swamp){
                hex = new SwampDecorator(hex);
            }
            if(hex.feature_type == EnumHandler.HexNaturalFeature.Heavy_Vegetation){
                hex = new WheatDecorator(hex);
            }
        }

    }
}