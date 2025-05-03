using UnityEngine;

public class AIGoalkeeperController : MonoBehaviour
{
    public float moveSpeed = 5f; // AI'nin hareket hızı
    public float minY = -4f;     // AI'nin inebileceği minimum Y koordinatı
    public float maxY = 4f;      // AI'nin çıkaibileceği maksimum Y koordinatı
    public string ballTag = "Ball"; // Topun tag'i

    private Rigidbody2D rb;
    private Transform ballTransform;
    private float targetY;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start() 
    { 
    
        FindBall();
    }

    void FindBall()
    {
        GameObject ballObject = GameObject.FindGameObjectWithTag(ballTag);
        if (ballObject != null)
        {
            ballTransform = ballObject.transform;
            Debug.Log("AI Goalkeeper found the ball.");
        }
        else
        {
            Debug.LogWarning("AI Goalkeeper couldn't find the ball with tag: " + ballTag);
        }
    }

    void FixedUpdate() 
    {
        // E�er top referans� kaybolduysa (�rne�in top yok olduysa), tekrar bulmaya �al��
        if (ballTransform == null)
        {
            FindBall();
            // Hala bulunamad�ysa veya rigidbody yoksa bekle
            if (ballTransform == null || rb == null)
            {
                rb.linearVelocity = Vector2.zero; // Hareketsiz kal
                return;
            }
        }

        // 1. Hedef Y Pozisyonunu Belirle (Topun Y'si)
        targetY = ballTransform.position.y;

        // 2. Hedef Y'yi S�n�rlar ��inde Tut
        targetY = Mathf.Clamp(targetY, minY, maxY);

        // 3. Hareketi Hesapla ve Uygula
        // Mevcut pozisyon ile hedef pozisyon aras�ndaki farka bakarak hareket y�n�n� belirle
        float currentY = rb.position.y;
        float differenceY = targetY - currentY;

        // K���k bir fark varsa (hedefe yak�nsa) titremeyi �nlemek i�in dur
        if (Mathf.Abs(differenceY) < 0.1f)
        {
            rb.linearVelocity = new Vector2(0, 0); // Dur
        }
        else
        {
            // Hedefe do�ru hareket et
            float directionY = Mathf.Sign(differenceY); // Yukar� (+1) veya a�a�� (-1)
            rb.linearVelocity = new Vector2(0, directionY * moveSpeed);
        }

        // Ekstra g�venlik: Pozisyonu s�n�rlar i�inde tut (Rigidbody bazen hafif�e d��ar� ��kabilir)
        Vector2 clampedPosition = rb.position;
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);
        // E�er pozisyon farkl�ysa d�zelt (do�rudan atama yerine MovePosition daha p�r�zs�z olabilir)
        if (rb.position != clampedPosition)
        {
            rb.MovePosition(clampedPosition); // Fizik motoruyla uyumlu pozisyon ayarlama
            rb.linearVelocity = Vector2.zero; // S�n�ra �arp�nca h�z� s�f�rla (iste�e ba�l�)
        }

    }

    // (Opsiyonel) Sahne g�r�n�m�nde hareket s�n�rlar�n� g�rmek i�in Gizmo
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        float xPos = transform.position.x; // AI'nin kendi X pozisyonunu kullan
        Gizmos.DrawLine(new Vector3(xPos - 0.5f, minY, 0), new Vector3(xPos + 0.5f, minY, 0));
        Gizmos.DrawLine(new Vector3(xPos - 0.5f, maxY, 0), new Vector3(xPos + 0.5f, maxY, 0));
        Gizmos.DrawLine(new Vector3(xPos, minY, 0), new Vector3(xPos, maxY, 0));
    }
}