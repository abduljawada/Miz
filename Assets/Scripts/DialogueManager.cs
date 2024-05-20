using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance; 
    Queue<string> sentences = new Queue<string>();
    public bool inDialogue = false;
    [SerializeField] GameObject dialogueUI;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] StateMachine playerStateMachine;
    [SerializeField] Missions missions;
    private void Awake()
    {
        instance = this;
    }

    public void StartDialogue(List<string> dialogue)
    {
        inDialogue = true;
        dialogueUI.SetActive(true);
        playerStateMachine.ChangeState(StateMachine.State.Stun);
        sentences.Clear();

        foreach (string sentence in dialogue)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        dialogueUI.SetActive(false);
        playerStateMachine.ChangeState(StateMachine.State.Default);
        inDialogue = false;
        if (!missions.talkedAboutCurrentMission)
        {
            missions.missions[missions.missionNumber].unityEvent?.Invoke();
            missions.talkedAboutCurrentMission = true;
        }
        
    }
}
