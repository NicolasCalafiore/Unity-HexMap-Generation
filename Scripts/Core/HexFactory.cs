using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Players;
using Strategy.Assets.Scripts.Objects;
using Terrain;
using UnityEngine;


public class HexFactory{

    public HexFactory(){

    }

    public HexTile GenerateHex(float elevation_type, float structure_type, float feature_type, float land_type, float region_type, float resource_type, float owner_id, float col, float row){ 
       HexTile hex = new((int) col, (int) row);

        hex.SetElevation(EnumHandler.GetElevationType(elevation_type));
        hex.SetStructureType(EnumHandler.GetStructureType(structure_type));
        hex.SetFeatureType(EnumHandler.GetNaturalFeatureType(feature_type));
        hex.SetLandType(EnumHandler.GetLandType(land_type));
        hex.SetRegionType(EnumHandler.GetRegionType(region_type));
        hex.SetResourceType(EnumHandler.GetResourceType(resource_type));

        if(owner_id == -1){
            hex.SetOwnerPlayer(null);
        }
        else{
            hex.SetOwnerPlayer(GameManager.player_id_to_player[owner_id]);
        }

        return hex;

    }
}



