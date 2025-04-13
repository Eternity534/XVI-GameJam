using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public TextMeshProUGUI cinematicText;
    public float delayBetweenLines = 2.5f;
    public string[] lines;
    public string nextSceneName = "Tuto";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(PlayIntro());
    }

    IEnumerator PlayIntro()
    {
        foreach(string line in lines)
        {
            cinematicText.text = line;
            yield return new WaitForSeconds(delayBetweenLines);
        }
        SceneManager.LoadScene(nextSceneName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
