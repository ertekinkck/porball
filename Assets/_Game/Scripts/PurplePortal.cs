using UnityEngine;
using DG.Tweening;

public class PurplePortal : MonoBehaviour
{
    public Transform[] theyWillSwapPlaces;
    public float moveDuration = 0.6f;
    public float jumpPower = 0.5f;
    public int jumpCount = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out BallController ballController))
        {
            ShuffleWithTween();
            ballController.ChangeTrailColor(Color.purple);
        }
    }

    private void ShuffleWithTween()
    {
        Vector3[] originalPositions = new Vector3[theyWillSwapPlaces.Length];
        Collider2D[] colliders = new Collider2D[theyWillSwapPlaces.Length];

        for (int i = 0; i < theyWillSwapPlaces.Length; i++)
        {
            originalPositions[i] = theyWillSwapPlaces[i].position;
            colliders[i] = theyWillSwapPlaces[i].GetComponent<Collider2D>();
            if (colliders[i] != null)
                colliders[i].enabled = false;
        }

        for (int i = 0; i < originalPositions.Length; i++)
        {
            int randomIndex = Random.Range(i, originalPositions.Length);
            Vector3 temp = originalPositions[i];
            originalPositions[i] = originalPositions[randomIndex];
            originalPositions[randomIndex] = temp;
        }

        for (int i = 0; i < theyWillSwapPlaces.Length; i++)
        {
            int index = i;

            theyWillSwapPlaces[i]
                .DOJump(originalPositions[i], jumpPower, jumpCount, moveDuration)
                .SetEase(Ease.InOutQuad)
                .OnComplete(() =>
                {
                    if (colliders[index] != null)
                        colliders[index].enabled = true;
                });
        }
    }
}
