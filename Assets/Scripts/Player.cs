using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float maxSpeed;

    [field: SerializeField]public int Coin {  get;  set; } = 0;
    [field: SerializeField]public int Health { get; set; } = 10;


    Animator anim;
    private Rigidbody2D rb;

    public bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveInput = 0f;
        // Move forward continuously
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);


        // ���������ǡ������͹���
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // ���ⴴ���ʹ (����Ǩ�ͺ���)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void FixedUpdate()
    {

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", grounded);

        anim.SetFloat("vSpeed", rb.linearVelocity.y);

        float move = Input.GetAxis("Horizontal");

        anim.SetFloat("Speed", Mathf.Abs(move));

        rb.linearVelocity = new Vector2(move * maxSpeed, rb.linearVelocity.y);
    }




    public void OnTriggerEnter2D(Collider2D other)
    {
        Item item = other.GetComponent<Item>();
        if(item)
        {
            item.PickUp(this);
        }
    }

    public void AddCoin(int value)
    {
        Coin += value;
        Debug.Log("Coin +1 CurrentCoin: " +  Coin);
    }

    public void Heal(int value)
    {
        Health += value;
        Debug.Log("Heal + 10 Health  CurrentHealth: "+ Health);

    }

    public void TakeDamage(int value)
    {
        Health -= value;
        Debug.Log("Heal - 10 Health  CurrentHealth: " + Health);

    }

    public void Knockback(Vector2 direction, float force)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb)
        {
            rb.linearVelocity = Vector2.zero; // ��૵�������������������͹
            rb.AddForce(direction.normalized * force, ForceMode2D.Impulse);
        }
    }
}
