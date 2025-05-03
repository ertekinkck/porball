using UnityEngine;

public class BallController : MonoBehaviour
{
    public Rigidbody ballRigidbody;
    public float forceMultiplier = 10f;
    private Vector3 startMousePosition;
    private Vector3 endMousePosition;
    private bool isAiming = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isAiming = true;
            startMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0) && isAiming)
        {
            endMousePosition = Input.mousePosition;
            Vector3 dir = (startMousePosition - endMousePosition).normalized;

            float distance = Vector3.Distance(startMousePosition, endMousePosition);
            Vector3 force = dir * distance * forceMultiplier;
            ballRigidbody.isKinematic = false;
            ballRigidbody.AddForce(force);
            isAiming = false;
        }
    }
}
