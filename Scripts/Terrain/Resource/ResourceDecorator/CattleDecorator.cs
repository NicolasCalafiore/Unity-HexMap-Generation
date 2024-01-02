namespace Terrain{


    public class CattleDecorator : TileDecorator {
        public CattleDecorator(HexTile tile) : base(tile) {
            this.tile.food += 2;
            this.tile.production += 2;
        }

    }


}