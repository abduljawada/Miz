using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    DialogueManager dialogueManager => DialogueManager.instance;
    Missions missions => GetComponentInParent<Missions>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            if (missions.talkedAboutCurrentMission)
            {
                if (missions.missions[missions.missionNumber].targets.Count == 0)
                {
                    missions.NextMission();

                    List<string> sentences = new List<string>();

                    sentences.Add(missions.nextMissionSentence);
                    sentences.AddRange(missions.missions[missions.missionNumber].missionText);

                    dialogueManager.StartDialogue(sentences);
                }
                else
                {
                    List<string> sentences = new List<string>();

                    sentences.AddRange(missions.missions[missions.missionNumber].missionText);

                    dialogueManager.StartDialogue(sentences);
                }
            }
            else
            {
                if (missions.missions[missions.missionNumber].targets.Count == 0)
                {
                    List<string> sentences = new List<string>();

                    sentences.AddRange(missions.missions[missions.missionNumber].missionText);
                    sentences.Add(missions.missionDoneSentence);

                    missions.NextMission();

                    sentences.AddRange(missions.missions[missions.missionNumber].missionText);

                    dialogueManager.StartDialogue(sentences);
                }
                else
                {
                    List<string> sentences = new List<string>();

                    sentences.AddRange(missions.missions[missions.missionNumber].missionText);

                    dialogueManager.StartDialogue(sentences);
                }
            }
        }
    }
}
