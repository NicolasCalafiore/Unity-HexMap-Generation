namespace Terrain{


    public class BananasDecorator : TileDecorator {
        public BananasDecorator(HexTile tile) : base(tile) {
            this.tile.food += 3;
        }

    }


}