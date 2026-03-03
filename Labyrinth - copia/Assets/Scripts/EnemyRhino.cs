using UnityEngine;

public class EnemyRhino : MonoBehaviour
{

    public float speed = 2f;
    public Transform[] points;
    private int i;


    public bool canCharge = true;
    public float chargeSpeed = 7f;
    public float sightDistance = 4f;
    public float loseDistance = 6f;
    public float maxChargeTime = 2f;
    public float chargeCooldown = 3f;
    public LayerMask playerLayer;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Transform player;
    private bool isCharging;
    private float chargeTimer;
    private float cooldownTimer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (cooldownTimer > 0) cooldownTimer -= Time.deltaTime;

        if (!isCharging && cooldownTimer <= 0 && DetectPlayer())
        {
            StartCharge();
        }

        if (isCharging) ChargePlayer();
        else Patrol();

        SetAnimation();
    }

    private void SetAnimation()
    {
        if (animator == null) return;
        if (isCharging) animator.Play("Enemy_Charge");
        else animator.Play("Enemy_Run");
    }

    void StartCharge()
    {
        isCharging = true;
        chargeTimer = maxChargeTime;
    }

    void StopCharge()
    {
        isCharging = false;
        player = null;
        cooldownTimer = chargeCooldown;
    }

    void Patrol()
    {
        if (points.Length == 0) return;
        if (Vector2.Distance(transform.position, points[i].position) < 0.25f)
        {
            i++;
            if (i == points.Length) i = 0;
        }
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);


        spriteRenderer.flipX = (points[i].position.x < transform.position.x);
    }

    bool DetectPlayer()
    {

        Vector2 direction = spriteRenderer.flipX ? Vector2.left : Vector2.right;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, sightDistance, playerLayer);

        Debug.DrawRay(transform.position, direction * sightDistance, Color.red);

        if (hit.collider != null)
        {
            player = hit.transform;
            return true;
        }
        return false;
    }

    void ChargePlayer()
    {
        if (player == null) { StopCharge(); return; }

        chargeTimer -= Time.deltaTime;
        float distance = Vector2.Distance(transform.position, player.position);

        if (chargeTimer <= 0 || distance > loseDistance)
        {
            StopCharge();
            return;
        }

        Vector2 target = new Vector2(player.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, target, chargeSpeed * Time.deltaTime);

        spriteRenderer.flipX = (player.position.x < transform.position.x);
    }
}