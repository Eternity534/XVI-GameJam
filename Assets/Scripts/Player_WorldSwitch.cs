using UnityEngine;

public class Player_WorldSwitch : MonoBehaviour
{
    private bool canSwitchWorld = false;
    private Transform switchTarget;

    void Update()
    {
        if (canSwitchWorld && Input.GetKeyDown(KeyCode.E))
        {
            SwitchWorld();
        }
    }

    public void EnableWorldSwitch(Transform target)
    {
        canSwitchWorld = true;
        switchTarget = target;
    }

    public void DisableWorldSwitch()
    {
        canSwitchWorld = false;
        switchTarget = null;
    }

    void SwitchWorld()
    {
        if (switchTarget != null)
        {
            transform.position = switchTarget.position;
            Debug.Log("Switched to " + switchTarget.name);
        }
    }
}
