namespace Terrain{


    public class WheatDecorator : TileDecorator {
        public WheatDecorator(HexTile tile) : base(tile) {
            this.tile.food += 2;
        }

    }


}