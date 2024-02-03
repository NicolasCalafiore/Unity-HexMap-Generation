namespace Terrain{


    public class PigsDecorator : TileDecorator {
        public PigsDecorator(HexTile tile) : base(tile) {
            this.tile.nourishment += 2;
        }

    }


}