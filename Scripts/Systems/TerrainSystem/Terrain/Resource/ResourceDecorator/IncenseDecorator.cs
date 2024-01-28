namespace Terrain{


    public class IncenseDecorator : TileDecorator {
        public IncenseDecorator(HexTile tile) : base(tile) {
            this.tile.nourishment += 1;
            this.tile.construction += 1;
        }

    }


}