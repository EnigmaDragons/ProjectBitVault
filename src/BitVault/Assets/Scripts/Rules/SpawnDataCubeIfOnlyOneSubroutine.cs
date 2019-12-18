using UnityEngine;

public class SpawnDataCubeIfOnlyOneSubroutine : OnMessage<LevelStateChanged>
{
    [SerializeField] private GameObject dataCube;
    [SerializeField] private CurrentLevelMap map;

    private bool _spawned;

    protected override void Execute(LevelStateChanged msg)
    {
        if (map.NumOfJumpables == 1 && !_spawned)
        {
            var heroTile = new TilePoint(map.Hero.gameObject);
            if (map.IsJumpable(new TilePoint(heroTile.X - 1, heroTile.Y)) && map.IsWalkable(new TilePoint(heroTile.X - 2, heroTile.Y)))
                SpawnDataCube(new TilePoint(heroTile.X - 2, heroTile.Y));
            if (map.IsJumpable(new TilePoint(heroTile.X + 1, heroTile.Y)) && map.IsWalkable(new TilePoint(heroTile.X + 2, heroTile.Y)))
                SpawnDataCube(new TilePoint(heroTile.X + 2, heroTile.Y));
            if (map.IsJumpable(new TilePoint(heroTile.X, heroTile.Y - 1)) && map.IsWalkable(new TilePoint(heroTile.X, heroTile.Y - 2)))
                SpawnDataCube(new TilePoint(heroTile.X, heroTile.Y - 2));
            if (map.IsJumpable(new TilePoint(heroTile.X, heroTile.Y + 1)) && map.IsWalkable(new TilePoint(heroTile.X, heroTile.Y + 2)))
                SpawnDataCube(new TilePoint(heroTile.X, heroTile.Y + 2));
        }
    }

    private void SpawnDataCube(TilePoint position)
    {
        var cube = Instantiate(dataCube);
        cube.transform.localPosition = new Vector3(position.X, position.Y, cube.transform.localPosition.z);
        _spawned = true;
    }
}
