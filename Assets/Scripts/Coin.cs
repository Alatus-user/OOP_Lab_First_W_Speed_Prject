using UnityEngine;

public class Coin : Item
{
    public override void Use(CharacterMovement player)
    {
        {
            if (player)
            {
                player.AddCoin(ItemValue);
                Destroy(gameObject);
            }
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
