namespace Terrain{


    public class SwampDecorator : TileDecorator {
        public SwampDecorator(HexTile tile) : base(tile) {
            this.tile.elevation = 0f;
            this.tile.elevation = (int) ElevationEnums.HexElevation.Flatland;
            this.MovementCost *= 2f;
            this.tile.nourishment += 1;
        }

    }


}