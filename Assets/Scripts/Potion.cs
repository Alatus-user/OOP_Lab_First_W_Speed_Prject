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

    //เล่นเสียงเก็บเหรียญ
    public AudioClip pickupSound;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //set potion item value
    public PotionSize potionSize;

    public void Init(PotionSize size)
    {
        potionSize = size;

        switch (size)
        {
            case PotionSize.Small: 
                itemValue = 25; 
                break;

            case PotionSize .Big:
                itemValue = 50;
                break;
        }
    }


    public override void Use(CharacterMovement player)
    {
        if (player)
        {
            player.Heal(itemValue);
            if (pickupSound && audioSource)
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            Destroy(gameObject); // เก็บแล้วลบออกจากฉาก
        }
    }
}
