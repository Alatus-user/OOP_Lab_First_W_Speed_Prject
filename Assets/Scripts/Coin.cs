using UnityEngine;
public enum CoinSize
{
    Small, //0
    Medium, //1
    Large //2
}
public class Coin : Item
{
    public CoinSize coinSize { get; private set; }

    public void init(CoinSize size)
    {
        switch(size)
        {
            case CoinSize.Small:
                itemValue = 1;
                break;
            case CoinSize.Medium:
                itemValue = 3;
                break;
            case CoinSize.Large:
                itemValue = 5;
                break;
        }
    }

    public override void Use(CharacterMovement player)
    {
        {
            if (player)
            {
                player.AddCoin(itemValue);
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
