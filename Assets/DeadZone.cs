using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Target" || collision.tag == "Dino")
        {
            UI.instance.OpenScene(); //this will open restart scene
        }
    }
}
