using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    [field: SerializeField]public int Coin {  get;  set; } = 0;
    [field: SerializeField]public int Health { get; set; } = 10;



    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveInput = 0f;

        // เดินซ้ายด้วย A
        if (Input.GetKey(KeyCode.A))
        {
            moveInput = -1f;
        }
        // เดินขวาด้วย D
        else if (Input.GetKey(KeyCode.D))
        {
            moveInput = 1f;
        }

        // ใส่ความเร็วการเคลื่อนที่
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // กระโดดได้ตลอด (ไม่ตรวจสอบพื้น)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
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
            rb.velocity = Vector2.zero; // รีเซตความเร็วเพื่อให้เด้งแน่นอน
            rb.AddForce(direction.normalized * force, ForceMode2D.Impulse);
        }
    }
}
