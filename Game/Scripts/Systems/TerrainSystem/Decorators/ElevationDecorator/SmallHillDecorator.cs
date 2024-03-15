namespace Terrain{


    public class  SmallHillDecorator: TileDecorator {
        public SmallHillDecorator(HexTile tile) : base(tile) {
            this.tile.construction += 1;
            this.tile.defense += 2;
        }

    }


}