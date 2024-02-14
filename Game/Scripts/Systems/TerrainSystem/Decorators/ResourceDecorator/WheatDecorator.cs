namespace Terrain{


    public class WheatDecorator : TileDecorator {
        public WheatDecorator(HexTile tile) : base(tile) {
            this.tile.nourishment += 2;
        }

    }


}