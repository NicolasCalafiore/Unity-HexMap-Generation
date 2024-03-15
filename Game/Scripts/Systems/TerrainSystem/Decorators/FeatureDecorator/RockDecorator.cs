namespace Terrain{


    public class RockDecorator : TileDecorator {
        public RockDecorator(HexTile tile) : base(tile) {
            this.tile.MovementCost *= 3; // For example, doubling the movement cost
            this.tile.construction += 3;
            this.tile.defense += 3;
        }

    }


}