using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Strategy.Assets.Game.Scripts.Terrain;
using Terrain;
using TMPro;
using UnityEngine;



public static class DebugHandler
{

    public static void InitializeDebugHexComponents(List<Hex> hex_list){


        foreach(Hex hex in hex_list){
            GameObject hex_go = TerrainHandler.hex_to_hex_go[hex];
            hex_go.isStatic = true; // Hex Empty Object 
            foreach (Transform child in hex_go.transform)
            {
                // Set each child object to static
                child.gameObject.isStatic = true;
            }

            //hex_go.transform.GetChild(1).GetComponent<TextMeshPro>().text = string.Format("{0},{1}" , hex.GetColRow().x, hex.GetColRow().y);
            //hex_go.transform.GetChild(2).GetComponent<TextMeshPro>().text = string.Format("{0}" , hex.GetPosition().y);
            //hex_go.transform.GetChild(3).GetComponent<TextMeshPro>().text = string.Format("{0}" , hex.elevation_type);
            //hex_go.transform.GetChild(4).GetComponent<TextMeshPro>().text = string.Format("{0}" , hex.region_type);

            // Set the name of the hex game object for Unity
            hex_go.name = "Hex - " + hex.GetColRow().x + "_" + hex.GetColRow().y;
        }    
    }

    public static void ShowOceanTypes(List<Hex> hex_list){
        foreach(Hex hex in hex_list){
            GameObject hex_go = TerrainHandler.hex_to_hex_go[hex];
            if(hex.land_type == EnumHandler.LandType.Water){
                hex_go.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color32(0, 0, 255, 255);
            }
        }
        
    }

    public static void ShowElevationTypes(List<Hex> hex_list){

        foreach(Hex hex in hex_list){
            GameObject hex_go = TerrainHandler.hex_to_hex_go[hex];
            if(hex.GetElevationType() == EnumHandler.HexElevation.Canyon){
                hex_go.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color32(195, 143, 2, 255);
            }
            else if(hex.GetElevationType() == EnumHandler.HexElevation.Valley){
                hex_go.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color32(255, 209, 81, 255);
            }
            else if(hex.GetElevationType() == EnumHandler.HexElevation.Flatland){
                hex_go.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color32(21, 195, 2, 255);
            }
            else if(hex.GetElevationType() == EnumHandler.HexElevation.Hill){
                hex_go.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color32(17, 155, 2, 255);
            }
            else if(hex.GetElevationType() == EnumHandler.HexElevation.Large_Hill){
                hex_go.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color32(11, 105, 0, 255);
            }
            else if(hex.GetElevationType() == EnumHandler.HexElevation.Mountain){
                hex_go.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color32(160, 160, 160, 255);
            }
        }
        
    }


    public static void ShowRegionTypes(List<Hex> hex_list){
        foreach(Hex hex in hex_list){
            GameObject hex_go = TerrainHandler.hex_to_hex_go[hex];
            if(hex.GetLandType() != EnumHandler.LandType.Water){
                if(hex.GetRegionType() == EnumHandler.HexRegion.Desert){
                    hex_go.transform.GetChild(0).GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Desert");
                }

                if(hex.GetRegionType() == EnumHandler.HexRegion.Plains){
                    hex_go.transform.GetChild(0).GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Savannah");
                }

                if(hex.GetRegionType() == EnumHandler.HexRegion.Grassland){
                    hex_go.transform.GetChild(0).GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Grassland");
                }
                
                if(hex.GetRegionType() == EnumHandler.HexRegion.Tundra){
                    hex_go.transform.GetChild(0).GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Tundra");
                }
                if(hex.GetRegionType() == EnumHandler.HexRegion.Highlands){
                    hex_go.transform.GetChild(0).GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Highlands");
                }
                if(hex.GetRegionType() == EnumHandler.HexRegion.Jungle){
                    hex_go.transform.GetChild(0).GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Jungle");
                }
                if(hex.GetRegionType() == EnumHandler.HexRegion.Swamp){
                    hex_go.transform.GetChild(0).GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Swamp");
                }


                // if(hex.GetElevationType() == TerrainUtils.HexElevation.Mountain){
                //     Color hex_go_color = hex_go.transform.GetChild(0).GetComponent<MeshRenderer>().material.color;
                //     hex_go.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color(hex_go_color.r * .90f, hex_go_color.g * .90f, hex_go_color.b * .90f, 255f);
                // }
                // if(hex.GetElevationType() == TerrainUtils.HexElevation.Large_Hill){
                //     Color hex_go_color = hex_go.transform.GetChild(0).GetComponent<MeshRenderer>().material.color;
                //     hex_go.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color(hex_go_color.r * .85f, hex_go_color.g * .85f, hex_go_color.b * .85f, 255);
                // }
                // if(hex.GetElevationType() == TerrainUtils.HexElevation.Hill){
                //     Color hex_go_color = hex_go.transform.GetChild(0).GetComponent<MeshRenderer>().material.color;
                //     hex_go.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color(hex_go_color.r * 0.75f, hex_go_color.g * 0.75f, hex_go_color.b * 0.75f, 255);
                // }
                // if(hex.GetElevationType() == TerrainUtils.HexElevation.Flatland){
                //     Color hex_go_color = hex_go.transform.GetChild(0).GetComponent<MeshRenderer>().material.color;
                //     hex_go.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color(hex_go_color.r * .65f, hex_go_color.g * 0.65f, hex_go_color.b * 0.65f, 255);
                // }
                // if(hex.GetElevationType() == TerrainUtils.HexElevation.Valley){
                //     Color hex_go_color = hex_go.transform.GetChild(0).GetComponent<MeshRenderer>().material.color;
                //     hex_go.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color(hex_go_color.r * 0.55f, hex_go_color.g * 0.55f, hex_go_color.b * 0.55f, 255);
                // }
                // if(hex.GetElevationType() == TerrainUtils.HexElevation.Canyon){
                //     Color hex_go_color = hex_go.transform.GetChild(0).GetComponent<MeshRenderer>().material.color;
                //     hex_go.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color(hex_go_color.r * 0.45f, hex_go_color.g * 0.45f, hex_go_color.b * 0.45f, 255);
                // }

            }

        }
    }



    public static void PrintMapDebug(string title,  List<List<float>> map){
        string message = title + "\n";
        foreach(List<float> row in map){
            foreach(float value in row){
                message += value + " ";
            }
            message += "\n";
        }

        Debug.Log(message);
    }


    public static void SpawnPerlinViewers(Vector2 map_size,  List<List<float>> map, string name){
            PerlinViewer pv = new PerlinViewer(map_size, map, name);

    }

    public static void SetHexAsChildren(MapGeneration map_generation){
    foreach(Hex i in TerrainHandler.GetHexList()){
        GameObject hex_go = TerrainHandler.hex_to_hex_go[i];
        hex_go.transform.SetParent(map_generation.transform);
        hex_go.name = "Hex - " + i.GetColRow().x + "_" + i.GetColRow().y + " - " + i.GetRegionType() + " - " + i.GetElevationType();
        }   
    }

}
