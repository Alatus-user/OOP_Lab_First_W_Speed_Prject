using UnityEngine;

// ไอเท็มเพิ่มพลังชีวิต
public class Hearth : Item
{
    public override void Use(CharacterMovement player)
    {
        if (player)
        {
            player.Heal(itemValue);
            Destroy(gameObject); // เก็บแล้วลบออกจากฉาก
        }
    }
}
