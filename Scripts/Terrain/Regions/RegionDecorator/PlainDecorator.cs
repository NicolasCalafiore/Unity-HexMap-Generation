namespace Terrain{


    public class PlainDecorator : TileDecorator {
        public PlainDecorator(HexTile tile) : base(tile) {
            this.tile.elevation =  0f;
            this.MovementCost *= .75f;
            this.tile.food += 1;
        }

    }


}