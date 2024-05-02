using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WfcCell
{
    Vector2Int _gridPosition;
    public bool IsCollapsed = false;
    public float Entropy;
    public List<GameObject> PossibleTiles;
    WFC _wfc;

    public WfcCell(Vector2Int gridPosition, WFC wfc, List<GameObject> possibleTiles)
    {
        _gridPosition = gridPosition;
        _wfc = wfc;
        PossibleTiles = new List<GameObject>(possibleTiles);
    }

    public void Collapse()
    {
        int randomValue = Random.Range(0, PossibleTiles.Sum(c => c.GetComponent<WfcTile>().Weight));

        GameObject chosenTile = null;
        int currentTreshold = 0;
        foreach (GameObject tile in PossibleTiles)
        {
            currentTreshold += tile.GetComponent<WfcTile>().Weight;
            if (randomValue < currentTreshold)
            {
                chosenTile = tile;
                break;
            }
        }

        PossibleTiles.Clear();
        Entropy = 1;
        IsCollapsed = true;

        if (_wfc.NextStepCandidates.Contains(this))
        {
            _wfc.NextStepCandidates.Remove(this);
        }
        PossibleTiles.Add(chosenTile);

        // Skip collapse if it's impossible.
        if (chosenTile == null)
        {
            _wfc.CreateTile(_wfc.UsedTiles[0], (Vector3Int)_gridPosition);
            return;
        }

        _wfc.CreateTile(chosenTile, (Vector3Int)_gridPosition);

        // Update all neighbours.
        if (_wfc.GridSize.y > _gridPosition.y + 1)
        {
            UpdateNeighbour(new Vector2Int(_gridPosition.x, _gridPosition.y + 1), Directions.North);
        }
        if (_wfc.GridSize.x > _gridPosition.x + 1)
        {
            UpdateNeighbour(new Vector2Int(_gridPosition.x + 1, _gridPosition.y), Directions.East);
        }
        if (_gridPosition.y - 1 >= 0)
        {
            UpdateNeighbour(new Vector2Int(_gridPosition.x, _gridPosition.y - 1), Directions.South);
        }
        if (_gridPosition.x - 1 >= 0)
        {
            UpdateNeighbour(new Vector2Int(_gridPosition.x - 1, _gridPosition.y), Directions.West);
        }
    }

    void UpdateNeighbour(Vector2Int neighbourPosition, Directions direction)
    {
        if (!_wfc.CellGrid[neighbourPosition.x, neighbourPosition.y].IsCollapsed)
        {
            List<GameObject> legalNeighboursList = new();

            switch (direction)
            {
                case Directions.North:
                    legalNeighboursList = PossibleTiles[0].GetComponent<WfcTile>().LegalNeighboursNorth;
                    break;
                case Directions.East:
                    legalNeighboursList = PossibleTiles[0].GetComponent<WfcTile>().LegalNeighboursEast;
                    break;
                case Directions.South:
                    legalNeighboursList = PossibleTiles[0].GetComponent<WfcTile>().LegalNeighboursSouth;
                    break;
                case Directions.West:
                    legalNeighboursList = PossibleTiles[0].GetComponent<WfcTile>().LegalNeighboursWest;
                    break;
            }
            

            // Remove tiles from neighbour's PossibleTiles which cannot be adjacent to the tile that is being collapsed.
            List<GameObject> tilesToRemove = new();
            foreach (GameObject tile in _wfc.CellGrid[neighbourPosition.x, neighbourPosition.y].PossibleTiles)
            {
                if (!legalNeighboursList.Contains(tile))
                {
                    tilesToRemove.Add(tile);
                }
            }
            foreach (GameObject tileToRemove in tilesToRemove)
            {
                _wfc.CellGrid[neighbourPosition.x, neighbourPosition.y].PossibleTiles.Remove(tileToRemove);
            }


            _wfc.CellGrid[neighbourPosition.x, neighbourPosition.y].UpdateEntropy();


            // Add to NextStepCandidates.
            if (!_wfc.NextStepCandidates.Contains(_wfc.CellGrid[neighbourPosition.x, neighbourPosition.y]))
            {
                _wfc.NextStepCandidates.Add(_wfc.CellGrid[neighbourPosition.x, neighbourPosition.y]);
            }

        }
    }

    void UpdateEntropy()
    {
        //Entropy = PossibleTiles.Count;
        Entropy = PossibleTiles.Sum(c => 1/c.GetComponent<WfcTile>().Weight);
    }
}
