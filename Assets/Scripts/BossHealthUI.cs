using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour
{
    public static BossHealthUI Instance;

    public Slider healthSlider;
    void Start()
    {
        gameObject.SetActive(false);
    }
    void Awake()
    {
        Instance = this;
        gameObject.SetActive(true);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void UpdateHealth(int current, int max)
    {
        float value = (float)current / max;
        healthSlider.value = value;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
