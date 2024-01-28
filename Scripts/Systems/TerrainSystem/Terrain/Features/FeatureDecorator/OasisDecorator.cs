namespace Terrain{


    public class OasisDecorator : TileDecorator {
        public OasisDecorator(HexTile tile) : base(tile) {
            this.tile.nourishment += 1;
        }

    }


}