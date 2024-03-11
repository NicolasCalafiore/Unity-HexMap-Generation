namespace Terrain{


    public class WaterDecorator : TileDecorator {
        public WaterDecorator(HexTile tile) : base(tile) {
            this.tile.elevation_type = 0;
            this.tile.nourishment += .5f;
        }

    }


}