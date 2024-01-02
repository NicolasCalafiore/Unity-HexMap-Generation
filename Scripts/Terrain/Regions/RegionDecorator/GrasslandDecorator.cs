namespace Terrain{


    public class GrasslandDecorator : TileDecorator {
        public GrasslandDecorator(HexTile tile) : base(tile) {
            this.tile.food += 1;
        }

    }


}