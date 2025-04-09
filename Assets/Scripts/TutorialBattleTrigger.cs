using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBattleTrigger : MonoBehaviour
{
    public OmniDirectionalMovement johnMovement;
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            johnMovement.tutorialBattle = true;
            johnMovement.randomNum = 0;
        }
    }
}
