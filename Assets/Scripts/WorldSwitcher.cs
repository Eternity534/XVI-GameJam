using UnityEngine;

public class WorldShifter : MonoBehaviour
{
    public float worldYOffset = 100f; // Décalage entre les mondes
    private bool isInWorldA = true;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ShiftWorld();
        }
    }

    void ShiftWorld()
    {
        Vector3 newPosition = transform.position;
        newPosition.y += isInWorldA ? -worldYOffset : worldYOffset;
        transform.position = newPosition;

        isInWorldA = !isInWorldA;
    }
}
