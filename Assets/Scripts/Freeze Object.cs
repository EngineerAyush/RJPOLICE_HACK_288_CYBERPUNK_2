using UnityEngine;

public class FreezeObjectPosition : MonoBehaviour
{
    void Start()
    {
        // Freeze the position of the object
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }
}
