using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class OutroManager : MonoBehaviour
{
    public TextMeshProUGUI cinematicText;
    public GameObject menuButton;
    public float delayBetweenLines = 2.5f;
    public string[] lines;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menuButton.SetActive(false);
        StartCoroutine(PlayOutro());
    }

    IEnumerator PlayOutro()
    {
        foreach(string line in lines)
        {
            cinematicText.text = line;
            yield return new WaitForSeconds(delayBetweenLines);
        }
        menuButton.SetActive(true);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
