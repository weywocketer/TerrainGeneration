using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WFC : MonoBehaviour
{
    public Vector2Int GridSize = new Vector2Int(5, 5);
    public WfcCell[,] CellGrid;
    public List<WfcCell> NextStepCandidates;

    [SerializeField] List<GameObject> UsedTiles;
    [SerializeField] float _stepDelay = 0.1f;

    void Start()
    {
        NextStepCandidates = new();
        CellGrid = new WfcCell[GridSize.x, GridSize.y];

        // Create cell grid
        for (int j = 0; j < CellGrid.GetLength(1); j++)
        {
            for (int i = 0; i < CellGrid.GetLength(0); i++)
            {
                CellGrid[i, j] = new WfcCell(new Vector2Int(i, j), this, UsedTiles);
            }
        }


        StartCoroutine(GenerateTerrain());
    }

    IEnumerator GenerateTerrain()
    {
        // First step
        CellGrid[Random.Range(0, GridSize.x), Random.Range(0, GridSize.y)].Collapse();
        yield return new WaitForSeconds(_stepDelay);

        while (NextStepCandidates.Count > 0)
        {
            // From candidates with the lowest entropy select one ramdomly and collapse it.
            float minValue = NextStepCandidates.Min(c => c.Entropy);
            List<WfcCell> lowestEntropyCandidates = NextStepCandidates.Where(c => c.Entropy == minValue).ToList();
            lowestEntropyCandidates[Random.Range(0, lowestEntropyCandidates.Count)].Collapse();
            yield return new WaitForSeconds(_stepDelay);
        }
    }

    public void CreateTile(GameObject chosenTile, Vector3 position)
    {
        Instantiate(chosenTile, position, Quaternion.identity);
    }
}
