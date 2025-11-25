using UnityEngine;

public class Spike : MonoBehaviour
{
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

            player.Knockback(dir, knockbackForce);
        }
    }
}
