namespace Terrain{


    public class SwampDecorator : TileDecorator {
        public SwampDecorator(HexTile tile) : base(tile) {
            this.tile.elevation_type = 0f;
            this.MovementCost *= 2f;
            this.tile.nourishment += 1;
        }

    }


}