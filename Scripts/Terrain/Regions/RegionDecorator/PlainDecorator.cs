namespace Terrain{


    public class PlainDecorator : TileDecorator {
        public PlainDecorator(HexTile tile) : base(tile) {
            this.tile.E = 0;
            this.MovementCost *= .75f;
        }

    }


}