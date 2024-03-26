using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interactable : MonoBehaviour
{
    [SerializeField] TMP_Text prompt;

    // Start is called before the first frame update
    void Start()
    {
        resetPromptText();
    }

    public void interact()
    {
        
        hideInteractPrompt();

        this.gameObject.SendMessage("onInteract");
    }

    public void showInteractPrompt()
    {
        prompt.gameObject.SetActive(true);

        //Debug.Log("Show Prompt.");
    }

    public void hideInteractPrompt()
    {
        prompt.gameObject.SetActive(false);

        //Debug.Log("Hide Prompt.");
    }

    public void showInteractPrompt(string newPrompt)
    {
        prompt.text = newPrompt;

        showInteractPrompt();
    }

    public void hideInteractPrompt(float delay, bool resetText = true)
    {
        Invoke("hideInteractPrompt", delay);

        if(resetText) 
            Invoke("resetPromptText", delay);
    }

    void resetPromptText()
    {
        prompt.text = $"[E] {gameObject.name}";
    }








    ///////////
    //CONTROL
    ///////////

    void OnEnable()
    {
        tag = "Interactable";
    }

    void OnDisable()
    {
        tag = "Untagged";
    }

    public void enable()
    {
        enabled = true;
    }

    public void disable()
    {
        enabled = false;
    }
}
