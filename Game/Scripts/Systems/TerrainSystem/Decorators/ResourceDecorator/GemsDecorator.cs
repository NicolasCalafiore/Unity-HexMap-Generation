namespace Terrain{


    public class GemsDecorator : TileDecorator {
        public GemsDecorator(HexTile tile) : base(tile) {
            this.tile.construction += 2;
        }

    }


}