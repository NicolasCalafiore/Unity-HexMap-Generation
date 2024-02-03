namespace Terrain{
    public abstract class TileDecorator : HexTile {

        /*
            TileDecorator is used to wrap around HexTile objects
            Used to add additional properties to HexTile objects
        */
        
        protected HexTile tile;

        protected TileDecorator(HexTile tile) : base(tile.column, tile.row) {
            this.tile = tile;
        }

        public override float MovementCost {
            get { return tile.MovementCost; }
            set { tile.MovementCost = value; }
        }

        // You can override other HexTile methods or add new ones
    }
}