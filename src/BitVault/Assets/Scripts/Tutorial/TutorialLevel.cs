using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TutorialLevel : MonoBehaviour
{
    [SerializeField] private List<TutorialLine> lines;
    [SerializeField] private CurrentLevelMap map;
    [SerializeField] private CurrentSelectedPiece selectedPiece;

    private TutorialUI _ui;

    private List<GameObject> _elements;
    private TutorialLine _currentLine;

    private void Start()
    {
        _ui = FindObjectOfType<TutorialUI>();
        _elements = new List<GameObject>();
    }

    private void Update()
    {
        for (var i = 0; i < lines.Count; i++)
        {
            var line = lines[i];
            if (ConditionsAreMet(line.Conditions))
            {
                if (line == _currentLine)
                    return;
                _currentLine = line;
                _ui.SetLine(line);
                Clear();
                line.Elements.ForEach(x =>
                {
                    var element = Instantiate(x.Element, transform);
                    element.transform.localPosition = x.Position;
                    _elements.Add(element);
                });
                return;
            }
        }
        _ui.Clear();
        Clear();
    }

    private bool ConditionsAreMet(List<TutorialCondition> conditions) => conditions.All(IsConditionMet);

    private bool IsConditionMet(TutorialCondition condition)
    {
        if (condition.Type == TutorialConditonType.RootPresent)
            return new TilePoint(map.Hero).Equals(condition.TilePoint) == condition.IsTrue;
        if (condition.Type == TutorialConditonType.SubroutinePresent)
            return map.IsJumpable(condition.TilePoint) == condition.IsTrue;
        if (condition.Type == TutorialConditonType.PieceSelected)
            return condition.IsTrue 
                ? selectedPiece.Selected.IsPresentAnd(x => new TilePoint(x).Equals(condition.TilePoint)) 
                : !selectedPiece.Selected.IsPresent || !new TilePoint(selectedPiece.Selected.Value).Equals(condition.TilePoint);
        return false;
    }

    private void Clear()
    {
        _elements.ForEach(Destroy);
        _elements.Clear();
    }
}
