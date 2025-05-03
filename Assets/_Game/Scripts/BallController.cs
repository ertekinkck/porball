using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float forceMultiplier = 5f;
    public float maxForce = 10f;
    public float maxSpeed = 20f;
    public LineRenderer aimLine;

    private Vector2 startMouseWorldPos;
    private bool isAiming = false;
    private bool hasShot = false;
    public ParticleSystem hitParticleSystem;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (hasShot) return;

        if (Input.GetMouseButtonDown(0))
        {
            isAiming = true;
            startMouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            aimLine.positionCount = 2;
        }

        if (Input.GetMouseButton(0) && isAiming)
        {
            Vector2 currentMouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            aimLine.SetPosition(0, transform.position);
            aimLine.SetPosition(1, transform.position + (Vector3)(startMouseWorldPos - currentMouseWorldPos).normalized);
        }

        if (Input.GetMouseButtonUp(0) && isAiming)
        {
            Vector2 endMouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (startMouseWorldPos - endMouseWorldPos).normalized;
            float distance = Vector2.Distance(startMouseWorldPos, endMouseWorldPos);

            float finalForce = Mathf.Min(distance * forceMultiplier, maxForce);
            Vector2 force = direction * finalForce;

            rb.linearVelocity = Vector2.zero;
            rb.AddForce(force, ForceMode2D.Impulse);

            aimLine.positionCount = 0;
            isAiming = false;
            hasShot = true;
        }
    }

    void FixedUpdate()
    {
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(hitParticleSystem, collision.GetContact(0).point, Quaternion.identity);
    }
}
