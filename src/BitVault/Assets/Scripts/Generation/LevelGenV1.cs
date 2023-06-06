using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class LevelGenV1
{
    public static LevelMap Generate(int minMoves)
    {
        var maxX = 8;
        var maxY = 14;
        var lb = new LevelMapBuilder(Guid.NewGuid().ToString(), maxX, maxY);

        var knownPieces = new Dictionary<TilePoint, MapPiece>();
        var emptyFloors = new HashSet<TilePoint>();
        var serverLoc = new TilePoint(Rng.Int(0, maxX), Rng.Int(0, maxY));
        lb.WithPieceAndFloor(serverLoc, MapPiece.Root);
        knownPieces[serverLoc] = MapPiece.Root;

        var adjacents = serverLoc.GetAdjacents().Where(x => x.IsInBounds(maxX, maxY)).ToArray();
        var rootKeyLoc = adjacents.Random();
        lb.WithPieceAndFloor(rootKeyLoc, MapPiece.RootKey);

        var hasPlacedDataCube = false;
        var knownMoves = 0;
        var isFinished = false;
        
        Debug.Log(new AsciiLevelMap(lb.Build()).ToString());
        while (!isFinished)
        {
            var selectedPiece = knownPieces.Count < 1 || Rng.Dbl() < 0.4
                ? new KeyValuePair<TilePoint, MapPiece>(rootKeyLoc, MapPiece.RootKey) 
                : knownPieces.Random();

            var move = selectedPiece.Key.GetCardinals(2).Where(x => x.IsInBounds(maxX, maxY)).ToArray().Random();
            var inBetweenLoc = selectedPiece.Key.InBetween(move).First();
            if (knownPieces.ContainsKey(move) || knownPieces.ContainsKey(inBetweenLoc) ||
                selectedPiece.Value == MapPiece.Root)
            {
                Debug.Log("Wasted Cycle. Invalid Pick");
                continue;
            }

            lb.WithPieceAndFloor(inBetweenLoc, MapPiece.Routine);
            lb.MovePieceAndAddFloor(selectedPiece.Key, move, selectedPiece.Value);
            knownPieces[inBetweenLoc] = MapPiece.Routine;
            knownPieces.Remove(selectedPiece.Key);
            knownPieces[move] = selectedPiece.Value;
            emptyFloors.Add(selectedPiece.Key);
            emptyFloors.Remove(move);
            emptyFloors.Remove(inBetweenLoc);
            if (selectedPiece.Value == MapPiece.RootKey)
                rootKeyLoc = move;
            knownMoves++;

            if (!hasPlacedDataCube && Rng.Dbl() < 0.08)
            {
                var dataCubeLoc = emptyFloors.ToArray().Random();
                lb.WithPiece(dataCubeLoc, MapPiece.DataCube);
                emptyFloors.Remove(dataCubeLoc);
                hasPlacedDataCube = true;
            }

            isFinished = knownMoves >= minMoves && hasPlacedDataCube && Rng.Dbl() < 0.3;
            
            if (knownPieces.Count(x => x.Value == MapPiece.RootKey) > 1)
                Debug.LogWarning("More than 1 Root Key");
            
            if (knownPieces.Count(x => x.Value == MapPiece.Root) < 1)
                Debug.LogWarning("Less than 1 Root");

            Debug.Log(new AsciiLevelMap(lb.Build()).ToString());
        }

        return lb.Build();
    }
    
    // TODO: Add some random floors?
    // Try in the App
    
}
