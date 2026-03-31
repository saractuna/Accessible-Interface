using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public GameObject speechSprite;
    public GameObject speechFeedback;
    public GameObject Canvas;
    public bool isOn = true;
    public bool isActive;

    private static Manager instance;

    private void Start()
    {
        speechFeedback.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isActive) 
        {
            Canvas.SetActive(false);
            isActive = false;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !isActive)
        {
            Canvas.SetActive(true);
            isActive = true;
        }
    }

    public IEnumerator SpeechFeedback()
    {
        speechFeedback.SetActive(true);
        yield return new WaitForSecondsRealtime(1);

        float fadeDuration = 1.0f;
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            speechFeedback.GetComponent<CanvasGroup>().alpha = alpha;
            yield return null;
        }

        speechFeedback.GetComponent<CanvasGroup>().alpha = 1.0f;
        speechFeedback.SetActive(true);
        Time.timeScale = 0f;
    }

    public void toggleSpeech()
    {
        isOn = !isOn;
    }

    public void openMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void openCredits()
    {
        SceneManager.LoadScene(1);
    }

    public void openSettings()
    {
        SceneManager.LoadScene(2);
    }

    public void openPlay()
    {
        SceneManager.LoadScene(3);
        Canvas.SetActive(false);
        isActive = false;
    }

    public void Pause()
    {
        Canvas.SetActive(false);
        isActive = false;
    }

    public void Quit()
    {
        Application.Quit();
    }
}