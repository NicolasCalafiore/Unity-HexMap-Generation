namespace Terrain{


    public class CattleDecorator : TileDecorator {
        public CattleDecorator(HexTile tile) : base(tile) {
            this.tile.nourishment += 2;
            this.tile.construction += 2;
        }

    }


}