using UnityEngine;
using UnityEngine.SceneManagement;

public class BluePortal : MonoBehaviour
{
    public string sceneToLoad = "AI_scene"; // Yüklenecek sahnenin adý 
    public Transform spawnPoint;            // Topun yeni sahnede doðacaðý noktanýn Transform'u

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Çarpan nesnenin top olup olmadýðýný kontrol
        if (other.TryGetComponent(out BallController ballController))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null && spawnPoint != null) // Rigidbody ve spawnPoint olduðundan emin ol
            {
                // Yeni sahnedeki baþlangýç pozisyonunu ve mevcut hýzý kaydet
                SceneTransitionData.NextSpawnPosition = spawnPoint.position; // Belirlenen spawn noktasýnýn pozisyonu
                SceneTransitionData.NextSpawnVelocity = rb.linearVelocity; // Topun mevcut hýzý

                Debug.Log($"Portal Entered. Saving Pos: {SceneTransitionData.NextSpawnPosition.Value}, Vel: {SceneTransitionData.NextSpawnVelocity.Value}. Loading scene: {sceneToLoad}");

                // Yeni sahneyi yükle
                SceneManager.LoadScene(sceneToLoad);
            }
            else if (spawnPoint == null)
            {
                Debug.LogError("BluePortal'da Spawn Point atanmamýþ!", this.gameObject);
            }
        }
    }
}