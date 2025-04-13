using UnityEngine;

public class WorldSwitchPortal : MonoBehaviour
{
    public Transform teleportTarget;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player_WorldSwitch switchScript = other.GetComponent<Player_WorldSwitch>();
            if (switchScript != null)
            {
                switchScript.EnableWorldSwitch(teleportTarget);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player_WorldSwitch switchScript = other.GetComponent<Player_WorldSwitch>();
            if (switchScript != null)
            {
                switchScript.DisableWorldSwitch();
            }
        }
    }
}