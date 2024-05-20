using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Missions : MonoBehaviour
{
    [System.Serializable]
    public struct Mission
    {
        public List<GameObject> targets;

        [TextArea(3, 10)]
        public string[] missionText;
        public UnityEvent unityEvent;
    }

    public bool talkedAboutCurrentMission = false;
    public int missionNumber = 0;
    [SerializeField] public Mission[] missions;
    public string nextMissionSentence;
    public string missionDoneSentence;

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject target in missions[missionNumber].targets)
        {
            if (!target)
            {
                missions[missionNumber].targets.Remove(target);
            }
        }
    }

    public void NextMission()
    {
        missionNumber++;
        talkedAboutCurrentMission = false;
    }
}
