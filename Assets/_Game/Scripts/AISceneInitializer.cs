using UnityEngine;

// Bu script'i AI_scene'deki bir y�netici GameObject'e (�rn: GameManager) ekleyin.
public class AISceneInitializer : MonoBehaviour
{
    public GameObject ballPrefab; // Inspector'dan top prefab'�n� buraya s�r�kleyin!

    void Start()
    {
        // Sahne ge�i� verisi var m� diye kontrol et
        if (SceneTransitionData.NextSpawnPosition.HasValue && SceneTransitionData.NextSpawnVelocity.HasValue)
        {
            Debug.Log($"AI Scene Start: Found transition data. Pos: {SceneTransitionData.NextSpawnPosition.Value}, Vel: {SceneTransitionData.NextSpawnVelocity.Value}");

            // E�er sahnede zaten bir top varsa (belki test ama�l�), onu yok edebilir veya
            // sadece yeni bir tane olu�turabilirsiniz. Burada yeni olu�turuyoruz:
            GameObject existingBall = GameObject.FindGameObjectWithTag("Ball"); // BallController'daki tag ile ayn� olmal�
            if (existingBall != null)
            {
                Debug.LogWarning("AI Scene Start: Found an existing ball, destroying it before spawning new one.");
                Destroy(existingBall);
            }


            if (ballPrefab != null)
            {
                // 1. Yeni topu olu�tur (Instantiate)
                GameObject newBall = Instantiate(ballPrefab,
                                                 SceneTransitionData.NextSpawnPosition.Value,
                                                 Quaternion.identity); // Ba�lang�� rotasyonu

                Debug.Log($"Ball instantiated at {newBall.transform.position}");


                // 2. Topun Rigidbody'sini al ve h�z�n� ayarla
                Rigidbody2D rb = newBall.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.linearVelocity = SceneTransitionData.NextSpawnVelocity.Value;
                    Debug.Log($"Ball velocity set to {rb.linearVelocity}");

                    // Opsiyonel: H�z�n �ok d���k olmas�n� engelle
                    // float minSpeed = 1f;
                    // if (rb.velocity.magnitude < minSpeed && rb.velocity.magnitude > 0.01f)
                    // {
                    //     rb.velocity = rb.velocity.normalized * minSpeed;
                    // }
                }
                else
                {
                    Debug.LogError("Spawned Ball Prefab does not have a Rigidbody2D!", newBall);
                }

                // 3. BallController script'indeki hasShot flag'ini ayarla (gerekirse)
                BallController bc = newBall.GetComponent<BallController>();
                if (bc != null)
                {
                    // bc.SetAsShot(); // BallController'a b�yle bir public metot ekleyebilirsiniz
                }


            }
            else
            {
                Debug.LogError("Ball Prefab is not assigned in AISceneInitializer!", this.gameObject);
            }

            // 4. Veriyi temizle ki sahne tekrar y�klendi�inde yanl��l�kla kullan�lmas�n
            SceneTransitionData.Clear();
        }
        else
        {
            Debug.Log("AI Scene Start: No transition data found. Starting normally.");
            // Burada normal ba�lang�� senaryosu i�in kod olabilir (�rn: topu ba�lang�� noktas�na koyma)
        }
    }
}