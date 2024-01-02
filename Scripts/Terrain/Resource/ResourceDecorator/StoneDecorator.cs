namespace Terrain{


    public class StoneDecorator : TileDecorator {
        public StoneDecorator(HexTile tile) : base(tile) {
            this.tile.production += 2;
        }

    }


}