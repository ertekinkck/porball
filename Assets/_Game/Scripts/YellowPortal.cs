using UnityEngine;

public class YellowPortal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out BallController ballController))
        {
            var balls = FindObjectsByType<BallController>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            if (balls.Length < 2)
            {
                var spawnedBall = Instantiate(ballController.gameObject, ballController.transform.position - Vector3.left, ballController.transform.rotation);
                spawnedBall.GetComponent<BallController>().SetHasShot(true);
                spawnedBall.GetComponent<Rigidbody2D>().linearVelocity = ballController.GetComponent<Rigidbody2D>().linearVelocity;
            }
        }
    }
}
