using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float forceMultiplier = 5f;
    public float maxForce = 10f;
    public float maxSpeed = 20f;
    public LineRenderer aimLine;
    public TrailRenderer trailRenderer;

    private Vector2 startMouseWorldPos;
    private bool isAiming = false;
    private bool hasShot = false;
    public ParticleSystem hitParticleSystem;
    public ParticleSystem[] portalHitParticleSystems;
    public AudioClip hitClip;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void SetHasShot(bool newValue)
    {
        hasShot = newValue;
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
            Vector2 dragVector = startMouseWorldPos - currentMouseWorldPos;
            float dragDistance = Mathf.Min(dragVector.magnitude * forceMultiplier, maxForce);

            Vector2 direction = dragVector.normalized;
            Vector2 endPosition = (Vector2)transform.position + direction * dragDistance;
            aimLine.SetPosition(0, transform.position);
            aimLine.SetPosition(1, endPosition);
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

    public void ChangeTrailColor(Color color)
    {
        trailRenderer.material.color = color;
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
        hitClip.PlayClip2D(this, 0.2f, Random.Range(0.95f, 1.05f));
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BlackPortal blackPortal))
            Instantiate(portalHitParticleSystems[0], collision.transform.position, Quaternion.identity);
        if (collision.TryGetComponent(out BluePortal bluePortal))
            Instantiate(portalHitParticleSystems[1], collision.transform.position, Quaternion.identity);
        if (collision.TryGetComponent(out GreenPortal greenPortal))
            Instantiate(portalHitParticleSystems[2], collision.transform.position, Quaternion.identity);
        if (collision.TryGetComponent(out PurplePortal purplePortal))
            Instantiate(portalHitParticleSystems[3], collision.transform.position, Quaternion.identity);
        if (collision.TryGetComponent(out RedPortal redPortal))
            Instantiate(portalHitParticleSystems[4], collision.transform.position, Quaternion.identity);
        if (collision.TryGetComponent(out YellowPortal yellowPortal))
            Instantiate(portalHitParticleSystems[5], collision.transform.position, Quaternion.identity);
    }
}
