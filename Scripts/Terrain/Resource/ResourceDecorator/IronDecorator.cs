namespace Terrain{


    public class IronDecorator : TileDecorator {
        public IronDecorator(HexTile tile) : base(tile) {
            this.tile.construction += 2;
        }

    }


}