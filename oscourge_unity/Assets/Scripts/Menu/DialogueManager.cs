using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{

    public GameObject player1, player2;
    public GameObject loadingScene, introCanvas;


    public Text dialogueText;

    [TextArea(3, 10)]
    public string[] sentences;


    private Queue<string> sentencesQueue;




    private bool sceneIsLoading = false;
    private AsyncOperation currentLoadingOperation = null;
    void Update()
    {


        if (sceneIsLoading)
        {
            introCanvas.SetActive(false);
            loadingScene.SetActive(true);
           
        }

    }
        void Start()
    {
        sentencesQueue = new Queue<string>();
   

        foreach (string sentence in sentences)
        {
            sentencesQueue.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentencesQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentencesQueue.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(2);
        DisplayNextSentence();
    }

    void EndDialogue()
    {
        player1.GetComponent < Animator > ().SetBool("switch",true);
        player2.GetComponent<Animator>().SetBool("switch", true);
        StartCoroutine(NextLevel());
    }


    IEnumerator NextLevel()
    {
            yield return new WaitForSeconds(3);


        currentLoadingOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        sceneIsLoading = true;

    }

}