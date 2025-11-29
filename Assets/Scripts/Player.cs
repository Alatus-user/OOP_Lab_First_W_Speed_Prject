using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rampUpSpeed = 0.2f;
    public float maxSpeed = 12f;
    private float currentSpeed;
    
    private Rigidbody2D rb;
    private Animator anim;

    [Header("Jump")]
    [SerializeField] public float jumpForce = 9f;

    [Header("Player Stats")]
    [field: SerializeField] public int Coin { get; set; } = 0;
    [field: SerializeField] public int Health { get; set; } = 50;

    [HideInInspector] public int currentHealth;

    [Header("Health Drain Over Time")]
    public float healthDrainRate = 1f;
    public float healthDrainInterval = 2f;
    private float healthTimer = 0f;
    public HealthBar healthBar;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    public bool grounded = false;
    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
    }

    // เสียง
    public AudioClip jumpSound;
    public AudioClip GameoverSound;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // ========== NEW: Animation State ==========
    private enum PlayerState { Run, Jump, Hurt, Idle, Die }
    private PlayerState currentState = PlayerState.Run;

    private void ChangeState(PlayerState newState)
    {
        if (currentState == newState) return;
        currentState = newState;

        switch (newState)
        {
            case PlayerState.Run:
                anim.Play("Run_Animation");
                break;

            case PlayerState.Jump:
                anim.Play("Jump_Animation");
                break;

            case PlayerState.Hurt:
                anim.Play("Hurt_Animation");
                break;

            case PlayerState.Idle:
                anim.Play("Run_Animation");
                break;
            case PlayerState.Die:
                anim.Play("Die_Animation");
                break;
        }
    }
    // ===========================================

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Item item = collision.GetComponent<Item>();
        if (item)
        {
            item.PickUp(this);
            return;
        }

        if (collision.gameObject.tag == "Dead Zone") 
        {
            Health = 0;
            return;
        }
    }

    public void AddCoin(int value)
    {
        Coin += value;
        Debug.Log($"Coin +{value} | Current Coin = {Coin}");
        UI.instance.AddScore(value);
    }

    public void Heal(int value)
    {
        Health += value;
        if (Health > 100) Health = 100;

        currentHealth = Health;
        if (healthBar != null)
            healthBar.SetHealth(currentHealth);

        Debug.Log($"Heal +{value} | Current Health = {currentHealth}");
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health < 0) Health = 0;
        ChangeState(PlayerState.Hurt);
        currentHealth = Health;
        if (healthBar != null)
            healthBar.SetHealth(currentHealth);

        // ========== PLAY HURT ANIMATION ==========
        
    }

    public void Knockback(Vector2 direction, float force)
    {
        if (rb)
        {
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(direction * force, ForceMode2D.Impulse);
        }
    }

    private void UpdateHealthDrain()
    {
        healthTimer += Time.deltaTime;

        if (healthTimer >= healthDrainInterval)
        {
            healthTimer = 0f;
            Health -= (int)healthDrainRate;
            Debug.Log($"Health Drain -{healthDrainRate} | Current Health = {currentHealth}");
        }
    }

    public void ResetSpeed()
    {
        currentSpeed = moveSpeed;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentSpeed = moveSpeed;

        currentHealth = Health;

        if (healthBar != null)
            healthBar.SetMaxHealth(Health, currentHealth);
    }

    void Update()
    {
        UpdateHealthDrain();
        IsDead();

        transform.Translate(Vector2.right * currentSpeed * Time.deltaTime);

        if (currentSpeed < maxSpeed)
            currentSpeed += rampUpSpeed * Time.deltaTime;

        // ========== JUMP ==========
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            audioSource.PlayOneShot(jumpSound, 0.5f);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

            ChangeState(PlayerState.Jump);
        }

        // ========== RUN ANIMATION ==========
        if (grounded && currentState != PlayerState.Hurt)
        {
            ChangeState(PlayerState.Run);
        }
    }

    public void IsDead()
    {
        if (Health <= 0)
        {
            audioSource.PlayOneShot(GameoverSound, 0.2f);
            UI.instance.OpenScene();
            currentSpeed = 0f;
            
            anim.Play("Die_Animation");
        }
    }
}
