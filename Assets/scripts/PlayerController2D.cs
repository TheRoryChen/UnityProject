using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float jumpForce = 12f;
    public float fallMultiplier = 2.5f;   // higher = faster fall

    [Header("Combat")]
    public float attackCD = 0.3f;
    public float attackEnableTime = 0.08f;
    public Transform attackPoint;

    [Header("Ground Check")]
    public Transform feet;                        // assign feet point
    public Vector2 feetBoxSize = new Vector2(0.4f, 0.1f); // smaller to avoid ledge-sticking

    [Header("Visuals")]
    public Transform visual;                      // child with sprite/animator
    public Animator animator;                     // Animator on visual

    Rigidbody2D rb;
    bool grounded;
    float atkTimer;
    Collider2D hitbox;
    Vector3 attackOffset;                         // original local pos of attackPoint
    float facing = 1f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // auto grab animator if not set
        if (animator == null && visual != null)
            animator = visual.GetComponent<Animator>();

        if (attackPoint != null)
        {
            hitbox = attackPoint.GetComponent<Collider2D>();
            if (hitbox != null) hitbox.enabled = false;
            attackOffset = attackPoint.localPosition;
        }
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");

        // --- Move (use rb.velocity, not linearVelocity) ---
        rb.linearVelocity = new Vector2(x * moveSpeed, rb.linearVelocity.y);

        // --- Face direction + flip visual & hitbox ---
        if (x != 0f)
        {
            facing = Mathf.Sign(x);

            if (visual != null)
                visual.localScale = new Vector3(facing, 1f, 1f);

            if (attackPoint != null)
            {
                attackPoint.localPosition = new Vector3(
                    Mathf.Abs(attackOffset.x) * facing,
                    attackOffset.y,
                    attackOffset.z
                );
            }
        }

        // --- Animator speed param ---
        if (animator != null)
            animator.SetFloat("speed", Mathf.Abs(x));

        // --- Ground check ---
        if (feet != null)
        {
            grounded = Physics2D.OverlapBox(
                feet.position,
                feetBoxSize,
                0f,
                LayerMask.GetMask("Ground")
            );
        }

        // --- Jump ---
        if (grounded && Input.GetButtonDown("Jump"))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // --- Better fall: faster down, same jump height ---
        if (rb.linearVelocity.y < 0f)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1f) * Time.deltaTime;
        }

        // --- Attack ---
        atkTimer -= Time.deltaTime;
        if (atkTimer <= 0f && Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(Swing());
        }
    }

    System.Collections.IEnumerator Swing()
    {
        atkTimer = attackCD;

        if (animator != null)
            animator.SetTrigger("attack");

        yield return new WaitForSeconds(0.06f);      // wind-up

        if (hitbox != null)
            hitbox.enabled = true;                   // active frames

        yield return new WaitForSeconds(attackEnableTime);

        if (hitbox != null)
            hitbox.enabled = false;                  // end of swing
    }

    void OnDrawGizmosSelected()
    {
        if (feet != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(feet.position, feetBoxSize);
        }
    }
}

