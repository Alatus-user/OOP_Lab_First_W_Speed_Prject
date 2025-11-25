using UnityEngine;

public abstract class Item : MonoBehaviour
{
    // ค่าที่ส่งผล เช่น coinValue, healValue, damageValue
    [field: SerializeField] public int itemValue { get; protected set; }

    // คลาสลูกต้อง implement เองว่าไอเท็มนี้ทำอะไรกับผู้เล่น
    public abstract void Use(CharacterMovement player);

    // ฟังก์ชันถูกเรียกเมื่อ player เก็บไอเท็ม
    public void PickUp(CharacterMovement player)
    {
        Use(player);
    }
}
