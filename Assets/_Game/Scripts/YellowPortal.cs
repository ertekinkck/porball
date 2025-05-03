using UnityEngine;

public class YellowPortal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out BallController ballController))
        {
            var balls = FindObjectsByType<BallController>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            if (balls.Length < 3)
                Instantiate(ballController.gameObject, ballController.transform.position, ballController.transform.rotation);
        }
    }
}
