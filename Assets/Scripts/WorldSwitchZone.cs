using UnityEngine;

public class WorldSwitchZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            WorldShifter switchScript = other.GetComponent<WorldShifter>();
            if (switchScript != null)
            {
                switchScript.canSwitchWorld = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            WorldShifter switchScript = other.GetComponent<WorldShifter>();
            if (switchScript != null)
            {
                switchScript.canSwitchWorld = false;
            }
        }
    }
}