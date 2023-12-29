namespace Terrain{
    public abstract class TileDecorator : HexTile {
        protected HexTile tile;

        protected TileDecorator(HexTile tile) : base(tile.Q, tile.R) {
            this.tile = tile;
        }

        public override float MovementCost {
            get { return tile.MovementCost; }
            set { tile.MovementCost = value; }
        }

        // You can override other HexTile methods or add new ones
    }
}