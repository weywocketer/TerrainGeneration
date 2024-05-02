using UnityEngine;

public class SetCameraPosition : MonoBehaviour
{
    [SerializeField] WFC _wfc;

    void Start()
    {
        transform.position = new Vector3(_wfc.GridSize.x/2, _wfc.GridSize.y/2, transform.position.z);
    }
}
