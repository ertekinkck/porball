using UnityEngine;
using UnityEngine.SceneManagement;

public class BluePortal : MonoBehaviour
{
    public string sceneToLoad = "AI_scene"; // Y�klenecek sahnenin ad� 
    public Transform spawnPoint;            // Topun yeni sahnede do�aca�� noktan�n Transform'u

    private void OnTriggerEnter2D(Collider2D other)
    {
        // �arpan nesnenin top olup olmad���n� kontrol
        if (other.TryGetComponent(out BallController ballController))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null && spawnPoint != null) // Rigidbody ve spawnPoint oldu�undan emin ol
            {
                // Yeni sahnedeki ba�lang�� pozisyonunu ve mevcut h�z� kaydet
                SceneTransitionData.NextSpawnPosition = spawnPoint.position; // Belirlenen spawn noktas�n�n pozisyonu
                SceneTransitionData.NextSpawnVelocity = rb.linearVelocity; // Topun mevcut h�z�

                Debug.Log($"Portal Entered. Saving Pos: {SceneTransitionData.NextSpawnPosition.Value}, Vel: {SceneTransitionData.NextSpawnVelocity.Value}. Loading scene: {sceneToLoad}");

                // Yeni sahneyi y�kle
                SceneManager.LoadScene(sceneToLoad);
            }
            else if (spawnPoint == null)
            {
                Debug.LogError("BluePortal'da Spawn Point atanmam��!", this.gameObject);
            }
        }
    }
}