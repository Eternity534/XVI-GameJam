using UnityEngine;

public class BossZoneTrigger : MonoBehaviour
{
    private bool hasTriggered = false;
    public BossController bossController;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;

            GameObject bossObject = GameObject.Find("BossTuto");
            if (bossObject != null)
            {
                bossObject.SetActive(true);
                BossHealthUI.Instance.Show();
                BossController bossController = bossObject.GetComponent<BossController>();
                bossController?.ActivateBoss();
            }
            else
            {
                Debug.LogWarning("Boss not found!");
            }
        }
    }
}
