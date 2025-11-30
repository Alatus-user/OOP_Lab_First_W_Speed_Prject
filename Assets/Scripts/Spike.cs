using UnityEngine;

public class Spike : MonoBehaviour
{
    public AudioClip pickupSound;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    [SerializeField] public int damage = 1;
    [SerializeField] private float knockbackForce = 10f;

    void OnCollisionEnter2D(Collision2D other)
    {


        
        CharacterMovement player = other.gameObject.GetComponent<CharacterMovement>();

        if (player)
        {
            Debug.Log("Player hit Spike!");

            // ลด HP
            player.TakeDamage(damage);
            
            // reset speed
            player.ResetSpeed();

            // เด้งไปทางซ้ายเสมอ
            Vector2 dir = Vector2.left;
            if (pickupSound && audioSource)
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            player.Knockback(dir, knockbackForce);
        }
    }
}
