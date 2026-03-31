using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechToText : MonoBehaviour
{
    public Manager gameManager;
    private KeywordRecognizer recognizer;
    private readonly Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
    
    void Start()
    {
        // Add keywords and corresponding actions to the dictionary
        keywords.Add("play", gameManager.openPlay);
        keywords.Add("credits", gameManager.openCredits);
        keywords.Add("quit", gameManager.Quit);

        keywords.Add("options", gameManager.openSettings);
        keywords.Add("settings", gameManager.openSettings);

        keywords.Add("return", gameManager.openMenu);
        keywords.Add("back", gameManager.openMenu);

        keywords.Add("pause", gameManager.Pause);
        keywords.Add("stop", gameManager.Pause);

        // Create a new KeywordRecognizer with the keywords and a recognition threshold of ConfidenceLevel.Low
        recognizer = new KeywordRecognizer(keywords.Keys.ToArray(), ConfidenceLevel.Low);

        // Register a callback for the KeywordRecognizer
        recognizer.OnPhraseRecognized += RecognizedSpeech;

        // Start the KeywordRecognizer
        recognizer.Start();
    }

    public void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        System.Action keywordAction;

        // Check if the recognized speech is in the dictionary
        if (keywords.TryGetValue(speech.text, out keywordAction) && gameManager.isOn)
        {
            // If the speech is in the dictionary, invoke the corresponding action
            keywordAction.Invoke();
        }
        else
        {
            // If the speech is not in the dictionary, print "Keyword not recognized" to the console
            Debug.Log("Keyword not recognised");
            //gameManager.StartCoroutine(SpeechFeedback());
            gameManager.SpeechFeedback();
        }
    }
}