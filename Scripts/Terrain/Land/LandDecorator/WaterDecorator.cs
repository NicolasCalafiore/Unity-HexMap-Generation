namespace Terrain{


    public class WaterDecorator : TileDecorator {
        public WaterDecorator(HexTile tile) : base(tile) {
            this.tile.E = 0;
            
        }

    }


}