using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

namespace TerrainGeneration{
    public class HexMapHandler : MonoBehaviour
    {
        [SerializeField] private GameObject hex_prefab;
        [SerializeField] public Vector2 map_size;
        [SerializeField] private int TerrainStrategy;

        void Start()
        {
            // Create a list of Hex objects
            List<Hex> HEX_LIST = CreateHexObjects();

            // Elevate the terrain of the Hex objects based on the selected strategy
            ElevateHexTerrain(HEX_LIST);

            // Spawn the terrain by instantiating hex game objects and setting their properties
            SpawnTerrain(HEX_LIST);
        }

        private void SpawnTerrain(List<Hex> HEX_LIST){
            foreach (Hex hex in HEX_LIST){
                // Instantiate a hex game object
                GameObject hex_go = Instantiate(hex_prefab, hex.GetPosition(), Quaternion.identity, this.transform);

                // Set the text of the child TextMeshPro component to the hex's column and row, and elevation
                hex_go.transform.GetChild(1).GetComponent<TextMeshPro>().text = string.Format("{0},{1}" , hex.GetColRow().x, hex.GetColRow().y);
                hex_go.transform.GetChild(2).GetComponent<TextMeshPro>().text = string.Format("{0}" , hex.GetPosition().y);

                // Set the name of the hex game object for Unity
                hex_go.name = "Hex - " + hex.GetColRow().x + "_" + hex.GetColRow().y;

                // Set the parent of the hex game object this empty gameobject
                hex_go.transform.SetParent(this.transform);

            }
        }

        private void ElevateHexTerrain(List<Hex> HEX_LIST){
            ElevationStrategy elevationStrategy = null;

            // Select the elevation strategy based on the TerrainStrategy value
            switch(TerrainStrategy){
                case 2:
                    elevationStrategy = new GroupingsElevations();
                    break;
                case 1:
                    elevationStrategy = new RandomElevation();
                    break;
                default:
                    elevationStrategy = new RandomElevation();
                    break;
            }

            // Elevate the terrain of the Hex objects using the selected elevation strategy
            elevationStrategy.ElevateHexTerrain(HEX_LIST, map_size);
        }

        private  List<Hex> CreateHexObjects(){
            List<Hex> HEX_LIST = new List<Hex>();

            // Create Hex objects for each column and row in the map
            for(int column = 0; column < map_size.x; column++)
            {
                for(int row = 0; row < map_size.y; row++)
                {
                    Hex hex = new Hex(column, row);
                    HEX_LIST.Add(hex);
                }
            }

            return HEX_LIST;
        }

        // Update is called once per frame
        void Update()
        {
            
        }
                
    }
}