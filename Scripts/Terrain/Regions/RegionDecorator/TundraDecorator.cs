namespace Terrain{


    public class TundraDecorator : TileDecorator {
        public TundraDecorator(HexTile tile) : base(tile) {
            this.MovementCost *= 1.5f;
            this.tile.production += 1;
        }

    }


}