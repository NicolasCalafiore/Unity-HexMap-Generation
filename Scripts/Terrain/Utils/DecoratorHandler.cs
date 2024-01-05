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
            }
        }

        private static void SetElevationDecorator(HexTile hex)  // Sets Elevation Decorator
        {
            if(hex.GetElevationType() == EnumHandler.HexElevation.Mountain){
                hex = new MountainDecorator(hex);
            }
            if(hex.GetElevationType() == EnumHandler.HexElevation.Small_Hill){
                hex = new SmallHillDecorator(hex);
            }
            if(hex.GetElevationType() == EnumHandler.HexElevation.Canyon){
                hex = new CanyonDecorator(hex);
            }
            if(hex.GetElevationType() == EnumHandler.HexElevation.Valley){
                hex = new ValleyDecorator(hex);
            }
            if(hex.GetElevationType() == EnumHandler.HexElevation.Large_Hill){
                hex = new LargeHillDecorator(hex);
            }
            if(hex.GetElevationType() == EnumHandler.HexElevation.Flatland){
                hex = new FlatlandDecorator(hex);
            }

        }

        private static void SetResourceDecorator(HexTile hex)   // Sets Resource Decorator
        {
            if(hex.GetResourceType() == EnumHandler.HexResource.Bananas){
                hex = new BananasDecorator(hex);
            }
            if(hex.GetResourceType() == EnumHandler.HexResource.Cattle){
                hex = new CattleDecorator(hex);
            }
            if(hex.GetResourceType() == EnumHandler.HexResource.Gems){
                hex = new GemsDecorator(hex);
            }
            if(hex.GetResourceType() == EnumHandler.HexResource.Incense){
                hex = new IncenseDecorator(hex);
            }
            if(hex.GetResourceType() == EnumHandler.HexResource.Iron){
                hex = new IronDecorator(hex);
            }
            if(hex.GetResourceType() == EnumHandler.HexResource.Stone){
                hex = new StoneDecorator(hex);
            }
            


        }

        private static void SetRegionDecorator(HexTile hex) // Sets Region Decorator
        {
            if(hex.GetRegionType() == EnumHandler.HexRegion.Plains){
                hex = new PlainDecorator(hex);
            }
            if(hex.GetRegionType() == EnumHandler.HexRegion.Desert){
                hex = new DesertDecorator(hex);
            }
            if(hex.GetRegionType() == EnumHandler.HexRegion.Grassland){
                hex = new GrasslandDecorator(hex);
            }
            if(hex.GetRegionType() == EnumHandler.HexRegion.Highlands){
                hex = new HighlandsDecorator(hex);
            }
            if(hex.GetRegionType() == EnumHandler.HexRegion.Jungle){
                hex = new JungleDecorator(hex);
            }
            if(hex.GetRegionType() == EnumHandler.HexRegion.Swamp){
                hex = new SwampDecorator(hex);
            }
            if(hex.GetRegionType() == EnumHandler.HexRegion.Tundra){
                hex = new TundraDecorator(hex);
            }

        }

        private static void SetLandDecorator(HexTile hex)   // Sets Land Decorator
        {
            if(hex.GetLandType() == EnumHandler.LandType.Water){
                hex = new WaterDecorator(hex);
            }
            if(hex.GetLandType() == EnumHandler.LandType.Land){
                hex = new LandDecorator(hex);
            }
        }

        private static void SetFeatureDecorators(HexTile hex){  // Sets Feature Decorator

            if(hex.GetFeatureType() == EnumHandler.HexNaturalFeature.Forest){
                hex = new ForestDecorator(hex);
            }
            if(hex.GetFeatureType() == EnumHandler.HexNaturalFeature.Rocks){
                hex = new RockDecorator(hex);
            }
            if(hex.GetFeatureType() == EnumHandler.HexNaturalFeature.Jungle){
                hex = new JungleDecorator(hex);
            }
            if(hex.GetFeatureType() == EnumHandler.HexNaturalFeature.Oasis){
                hex = new OasisDecorator(hex);
            }
            if(hex.GetFeatureType() == EnumHandler.HexNaturalFeature.Swamp){
                hex = new SwampDecorator(hex);
            }
            if(hex.GetFeatureType() == EnumHandler.HexNaturalFeature.WheatField){
                hex = new WheatDecorator(hex);
            }
        }

    }
}