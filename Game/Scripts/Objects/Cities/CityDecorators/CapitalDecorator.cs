namespace Terrain{


    public class CapitalDecorator : TileDecorator {
        public CapitalDecorator(HexTile tile) : base(tile) {
            this.tile.MovementCost = 0;
        }

    }


}