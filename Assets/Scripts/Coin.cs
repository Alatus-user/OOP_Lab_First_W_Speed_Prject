using System;
using UnityEngine;

// ขนาดเหรียญ พร้อมค่าที่จะให้
[SerializeField] public enum CoinSize
{
    Small,
    Medium,
    Large
}


public class Coin : Item
{
    public CoinSize coinSize { get; private set; }



    //เล่นเสียงเก็บเหรียญ
    public AudioClip pickupSound;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    

    // เซ็ตค่าเหรียญตามขนาด
    public void Init(CoinSize size)
    {
        coinSize = size;

        switch (size)
        {
            case CoinSize.Small:  // ให้ 1
                itemValue = 1;
                break;

            case CoinSize.Medium: // ให้ 3
                itemValue = 3;
                break;

            case CoinSize.Large:  // ให้ 5
                itemValue = 5;
                break;
        }
    }

    // เมื่อชน player จะเพิ่มเหรียญ
    public override void Use(CharacterMovement player)
    {
        if (player)
        {
            player.AddCoin(itemValue);
            if (pickupSound && audioSource)
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            Destroy(gameObject); // เก็บแล้วให้หายไป
        }
    }
}
