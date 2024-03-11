using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Character;
using Terrain;
using Unity.VisualScripting;
using UnityEngine;

namespace Terrain
{
    public class DecoratorHandler
    {
        /*
            DecoratorHandler is used to set HexTile decorators for each HexTile
        */
        public DecoratorHandler()
        {
        }

        public void SetHexDecorators(List<HexTile> hex_list){    // Wraps each Hex Object with a Decorator Object for each HexTile - called from MapGeneration
            foreach(HexTile hex in hex_list){
                SetFeatureDecorators(hex);
                SetLandDecorator(hex);
                SetRegionDecorator(hex);
                SetResourceDecorator(hex);
                SetElevationDecorator(hex);
                SetStructureDecorator(hex);
            }
        }

        private void SetStructureDecorator(HexTile hex)
        {
            switch (hex.structure_type)
            {
                case StructureEnums.StructureType.Capital:
                    hex = new CapitalDecorator(hex);
                    break;
            }
        }

        private void SetElevationDecorator(HexTile hex)  // Sets Elevation Decorator
        {
            switch (hex.elevation_type)
            {
                case ElevationEnums.HexElevation.Mountain:
                    hex = new MountainDecorator(hex);
                    break;
                case ElevationEnums.HexElevation.Small_Hill:
                    hex = new SmallHillDecorator(hex);
                    break;
                case ElevationEnums.HexElevation.Canyon:
                    hex = new CanyonDecorator(hex);
                    break;
                case ElevationEnums.HexElevation.Valley:
                    hex = new ValleyDecorator(hex);
                    break;
                case ElevationEnums.HexElevation.Large_Hill:
                    hex = new LargeHillDecorator(hex);
                    break;
                case ElevationEnums.HexElevation.Flatland:
                    hex = new FlatlandDecorator(hex);
                    break;
            }
        }

        private void SetResourceDecorator(HexTile hex)   // Sets Resource Decorator
        {
            switch (hex.resource_type)
            {
                case ResourceEnums.HexResource.Bananas:
                    hex = new BananasDecorator(hex);
                    break;
                case ResourceEnums.HexResource.Cattle:
                    hex = new CattleDecorator(hex);
                    break;
                case ResourceEnums.HexResource.Gems: 
                    hex = new GemsDecorator(hex);
                    break;
                case ResourceEnums.HexResource.Spices:
                    hex = new IncenseDecorator(hex);
                    break;
                case ResourceEnums.HexResource.Iron:
                    hex = new IronDecorator(hex);
                    break;
                case ResourceEnums.HexResource.Stone:
                    hex = new StoneDecorator(hex);
                    break;
                case ResourceEnums.HexResource.Pigs:
                    hex = new PigsDecorator(hex);
                    break;
            }
        }

        private void SetRegionDecorator(HexTile hex) // Sets Region Decorator
        {
            switch (hex.region_type)
            {
                case RegionsEnums.HexRegion.Plain:
                    hex = new PlainDecorator(hex);
                    break;
                case RegionsEnums.HexRegion.Desert:
                    hex = new DesertDecorator(hex);
                    break;
                case RegionsEnums.HexRegion.Grassland:
                    hex = new GrasslandDecorator(hex);
                    break;
                case RegionsEnums.HexRegion.Highland:
                    hex = new HighlandsDecorator(hex);
                    break;
                case RegionsEnums.HexRegion.Jungle:
                    hex = new JungleDecorator(hex);
                    break;
                case RegionsEnums.HexRegion.Swamp:
                    hex = new SwampDecorator(hex);
                    break;
                case RegionsEnums.HexRegion.Tundra:
                    hex = new TundraDecorator(hex);
                    break;
            }
        }

        private void SetLandDecorator(HexTile hex)   // Sets Land Decorator
        {
            switch (hex.land_type)
            {
                case LandEnums.LandType.Water:
                    hex = new WaterDecorator(hex);
                    break;
                case LandEnums.LandType.Land:
                    hex = new LandDecorator(hex);
                    break;
            }
        }

        private void SetFeatureDecorators(HexTile hex)  // Sets Feature Decorator
        {
            switch (hex.feature_type)
            {
                case FeaturesEnums.HexNaturalFeature.Forest:
                    hex = new ForestDecorator(hex);
                    break;
                case FeaturesEnums.HexNaturalFeature.Rocks:
                    hex = new RockDecorator(hex);
                    break;
                case FeaturesEnums.HexNaturalFeature.Jungle:
                    hex = new JungleDecorator(hex);
                    break;
                case FeaturesEnums.HexNaturalFeature.Oasis:
                    hex = new OasisDecorator(hex);
                    break;
                case FeaturesEnums.HexNaturalFeature.Swamp:
                    hex = new SwampDecorator(hex);
                    break;
                case FeaturesEnums.HexNaturalFeature.Heavy_Vegetation:
                    hex = new WheatDecorator(hex);
                    break;
            }
        }

    }
}