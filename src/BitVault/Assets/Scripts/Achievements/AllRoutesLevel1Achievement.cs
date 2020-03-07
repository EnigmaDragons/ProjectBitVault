using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AllRoutesLevel1Achievement : OnMessage<LevelReset, PieceMoved, EndingLevelAnimationFinished>
{
    [SerializeField] private CurrentLevelMap map;
    [SerializeField] private CurrentLevelStars stars;
    [SerializeField] private SaveStorage storage;
    [SerializeField] private Achievements achievements;
    [SerializeField] private CurrentLevel level;

    private List<TilePoint> _route = new List<TilePoint>();

    protected override void Execute(LevelReset msg)
    {
        _route = new List<TilePoint>();
    }

    protected override void Execute(PieceMoved msg)
    {
        if (msg.Piece == map.Hero)
            _route.Add(msg.To);
    }

    protected override void Execute(EndingLevelAnimationFinished msg)
    {
        var routesTaken = storage.SaveData.Achievements.RoutesTakenOnLevel1;
        if (stars.Count == 3 && level.ZoneNumber == 0 && level.LevelNumber == 0 && !routesTaken.Any(x =>
        {
            if (x.Count != _route.Count)
                return false;
            for (var i = 0; i < x.Count; i++)
                if (!x[i].Equals(_route[i]))
                    return false;
            return true;
        }))
        {
            routesTaken.Add(_route);
            achievements.SetStat(StatType.DifferentPathsTakenLevel1, routesTaken.Count);
            if (routesTaken.Count >= 5)
                achievements.UnlockAchievement(AchievementType.AllRoutesLevel1);
        }
    }
}
