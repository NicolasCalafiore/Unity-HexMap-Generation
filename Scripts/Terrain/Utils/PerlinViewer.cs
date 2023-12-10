using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Terrain;
using UnityEngine;

namespace Strategy.Assets.Game.Scripts.Terrain
{
    public class PerlinViewer : MonoBehaviour
    {
        private Vector2 map_size = new Vector2();
        [SerializeField] public int UpdateBool = 0;
        private static GameObject perlin_map_object;
        private static int spawn_distance = 50;
        private static Vector2 position = new Vector2(- spawn_distance, 0);
        
        public PerlinViewer(Vector2 map_size, List<List<float>> map, GameObject perlin_map_object, string name)
        {
            PerlinViewer.perlin_map_object = perlin_map_object;
            GameObject perlin_map_go = Instantiate(perlin_map_object);
            perlin_map_go.transform.eulerAngles = new Vector3(0, 90, 0);
            perlin_map_go.transform.position = new Vector3(position.x, 15, position.y - spawn_distance);
            position.x += spawn_distance;
            this.map_size = map_size;
            perlin_map_go.name = name;

            
            Renderer renderer = perlin_map_go.transform.GetChild(0).GetComponent<Renderer>();
            renderer.material.mainTexture = ListToTexture(map);

        }




        Texture2D ListToTexture(List<List<float>> list)
        {
            float width = map_size.x;
            float height = map_size.y;
            Texture2D texture = new Texture2D((int) width, (int) height);

            Color[] colors = new Color[(int) width * (int) height + 1];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++){
                    colors[i * (int) width + j] = new Color(list[i][j], list[i][j], list[i][j]);
                }
            }


            texture.SetPixels(colors);
            texture.Apply();
            return texture;
        }

    }
}