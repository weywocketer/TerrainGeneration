using System.Collections.Generic;
using UnityEngine;

public class WfcTile : MonoBehaviour
{
    public int Weight = 1;
    public List<GameObject> LegalNeighboursNorth;
    public List<GameObject> LegalNeighboursEast;
    public List<GameObject> LegalNeighboursSouth;
    public List<GameObject> LegalNeighboursWest;
}