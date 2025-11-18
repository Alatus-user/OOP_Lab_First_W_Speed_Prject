using UnityEngine;

public class Hearth : Item
{
    public override void Use(CharacterMovement player)
    {
        if (player)
        {
            player.Heal(ItemValue);
            Destroy(gameObject);
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
