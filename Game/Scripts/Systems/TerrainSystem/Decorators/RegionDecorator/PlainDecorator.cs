namespace Terrain{


    public class PlainDecorator : TileDecorator {
        public PlainDecorator(HexTile tile) : base(tile) {
            this.tile.elevation_type =  0;
            this.MovementCost *= .75f;
            this.tile.nourishment += 1;
        }

    }


}