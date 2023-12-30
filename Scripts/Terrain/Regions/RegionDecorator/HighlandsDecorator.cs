namespace Terrain{


    public class HighlandsDecorator : TileDecorator {
        public HighlandsDecorator(HexTile tile) : base(tile) {
            this.MovementCost *= 1.25f;
        }

    }


}