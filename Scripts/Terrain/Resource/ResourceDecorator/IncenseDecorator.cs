namespace Terrain{


    public class IncenseDecorator : TileDecorator {
        public IncenseDecorator(HexTile tile) : base(tile) {
            this.tile.food += 1;
            this.tile.production += 1;
        }

    }


}