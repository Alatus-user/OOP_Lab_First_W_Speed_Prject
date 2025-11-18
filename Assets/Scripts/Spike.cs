using UnityEngine;

public class Spike : Item
{
    public override void Use(CharacterMovement player)
    {
        if (player)
        {
            // ทำดาเมจ
            player.TakeDamage(ItemValue);

            // คำนวณทิศให้กระเด้งออกจากหนาม
            Vector2 knockDir = (player.transform.position - transform.position).normalized;

            // เรียก knockback
            player.Knockback(knockDir, 10f); 
        }
    }

   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
