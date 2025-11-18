using UnityEngine;

public abstract class Item : MonoBehaviour
{

    [field: SerializeField] public int ItemValue { get; set; }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public abstract void Use(CharacterMovement player);
    public void PickUp(CharacterMovement player)
    {
        Use(player);
        
    }
}
