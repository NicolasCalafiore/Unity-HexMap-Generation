namespace Terrain{


    public class  MountainDecorator: TileDecorator {
        public MountainDecorator(HexTile tile) : base(tile) {
            this.tile.construction += 3;
            this.tile.defense += 4;
            this.tile.MovementCost += 4;
        }

    }


}