using Unity.VisualScripting;
using UnityEngine;
public enum PotionSize
{ 
    Small,
    Big
}

// ไอเท็มเพิ่มพลังชีวิต
public class Hearth : Item
{
    //set potion item value
    public PotionSize potionSize;

    public void Init(PotionSize size)
    {
        potionSize = size;

        switch (size)
        {
            case PotionSize.Small: 
                itemValue = 10; //heal 10 point
                break;

            case PotionSize .Big:
                itemValue = 25; //heal 25 point
                break;
        }
    }


    public override void Use(CharacterMovement player)
    {
        if (player)
        {
            player.Heal(itemValue);
            Destroy(gameObject); // เก็บแล้วลบออกจากฉาก
        }
    }
}
