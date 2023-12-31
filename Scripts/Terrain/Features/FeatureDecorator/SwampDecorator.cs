namespace Terrain{


    public class SwampFeatureDecorator : TileDecorator {
        public SwampFeatureDecorator(HexTile tile) : base(tile) {
            this.tile.MovementCost *= 2f; 
        }

    }


}