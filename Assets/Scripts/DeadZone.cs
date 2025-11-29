using UnityEngine;

public class DeadZone : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterMovement player = collision.gameObject.GetComponent<CharacterMovement>();

        

        if (collision.tag == "Target" || collision.tag == "Dino")
        {
            player.Health = 0;

            UI.instance.OpenScene(); //this will open restart scene
        }
    }
}
