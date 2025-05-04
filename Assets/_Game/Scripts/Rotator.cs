using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Vector3 rotateVector;
    void Update()
    {
        transform.Rotate(rotateVector);
    }
}
