﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatTalkingManager : MonoBehaviour
{
    [Header("Dialogues")]
    public CatDialogue[] catDialogues;
    private CatDialogueManager catDialogueManager;

    // Start is called before the first frame update
    void Start()
    {
        catDialogueManager = FindObjectOfType<CatDialogueManager>();
        if(catDialogues.Length > 0)
        {
            TriggerDialogue(0);
        } 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) {
            CatDialogueManager.TriggerAction();
        }
    }
    public void TriggerDialogue(int catDialogueCount)
    {
        catDialogueManager.StartDialogue(catDialogues[catDialogueCount]);
    }
}
