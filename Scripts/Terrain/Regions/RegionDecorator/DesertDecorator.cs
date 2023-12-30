namespace Terrain{


    public class DesertDecorator : TileDecorator {
        public DesertDecorator(HexTile tile) : base(tile) {
            this.MovementCost *= .5f;
        }

    }


}