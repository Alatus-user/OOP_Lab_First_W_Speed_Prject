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

    // เพิ่ม currentHealth สำหรับ HealthBar
    [HideInInspector] public int currentHealth;

    [Header("Health Drain Over Time")]
    public float healthDrainRate = 1f;
    public float healthDrainInterval = 2f;
    private float healthTimer = 0f;
    public HealthBar healthBar;

    
    //set ground cheack 
    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    public bool grounded = false;
    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
    }
   

    // ชนกับไอเท็มหรือสิ่งกีดขวาง
    public void OnTriggerEnter2D(Collider2D other)
    {
        // ไอเท็ม เช่น Coin, Hearth
        Item item = other.GetComponent<Item>();
        if (item)
        {
            item.PickUp(this);
            return;
        }
    }
    

    // ฟังก์ชัน player
    public void AddCoin(int value)
    {
        Coin += value;
        Debug.Log($"Coin +{value} | Current Coin = {Coin}");
        UI.instance.AddScore(value);
    }

    public void Heal(int value)
    {
        Health += value;
        if (Health > 100) Health = 100; // สมมติ MaxHealth = 50

        // อัปเดต HealthBar
        currentHealth = Health;
        if (healthBar != null)
            healthBar.SetHealth(currentHealth);

        Debug.Log($"Heal +{value} | Current Health = {currentHealth}");
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health < 0) Health = 0;

        // อัปเดต HealthBar
        currentHealth = Health;
        if (healthBar != null)
            healthBar.SetHealth(currentHealth);
    }

    //knock back player after hit 
    public void Knockback(Vector2 direction, float force)
    {
        if (rb)
        {
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(direction * force, ForceMode2D.Impulse);
        }
    }

    
    // ลด HP ตามเวลา
    private void UpdateHealthDrain()
    {
        healthTimer += Time.deltaTime;

        if (healthTimer >= healthDrainInterval)
        {
            healthTimer = 0f;
            TakeDamage((int)healthDrainRate);
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

        // กำหนด currentHealth = Health
        currentHealth = Health;

        if (healthBar != null)
            healthBar.SetMaxHealth(Health, currentHealth);
    }


    void Update()
    {
        UpdateHealthDrain();

        transform.Translate(Vector2.right * currentSpeed * Time.deltaTime);

        if (currentSpeed < maxSpeed)
            currentSpeed += rampUpSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

    }
}