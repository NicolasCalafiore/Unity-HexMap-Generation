namespace Terrain{


    public class  LargeHillDecorator: TileDecorator {
        public LargeHillDecorator(HexTile tile) : base(tile) {
            this.tile.construction += 2;
        }

    }


}