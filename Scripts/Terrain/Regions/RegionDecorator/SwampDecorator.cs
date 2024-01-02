namespace Terrain{


    public class SwampDecorator : TileDecorator {
        public SwampDecorator(HexTile tile) : base(tile) {
            this.tile.E = 0f;
            this.tile.elevation_type = EnumHandler.HexElevation.Flatland;
            this.MovementCost *= 2f;
            this.tile.food += 1;
        }

    }


}