namespace Terrain{


    public class  MountainDecorator: TileDecorator {
        public MountainDecorator(HexTile tile) : base(tile) {
            this.tile.construction += 3;
        }

    }


}