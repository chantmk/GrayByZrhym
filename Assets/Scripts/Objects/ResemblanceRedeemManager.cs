﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResemblanceRedeemManager : Interactable
{
    public Text resemblanceCountText;
    public Text redeemAmountText;
    public Button unlockButton;
    public GameObject resemblanceUI;


    private int[] checkpointAmount = { 1, 2, 3};
    private GameObject player;
    private PlayerInventory playerInventory;

    private int playerResemblanceCount;
    private int requiredResemblanceCount;

    private bool isOpen;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        playerInventory = player.GetComponent<PlayerInventory>();
        //resemblanceUI = transform.Find("ResemblanceUI");

        playerResemblanceCount = playerInventory.GetResemblanceCount();

        if (playerResemblanceCount < checkpointAmount[0])
        {
            requiredResemblanceCount = checkpointAmount[0];
        }
        else if (playerResemblanceCount >= checkpointAmount[0] && playerResemblanceCount < checkpointAmount[1])
        {
            requiredResemblanceCount = checkpointAmount[0];
        }
        else if (playerResemblanceCount >= checkpointAmount[1] && playerResemblanceCount < checkpointAmount[2])
        {
            requiredResemblanceCount = checkpointAmount[1];
        }
        else
        {
            requiredResemblanceCount = checkpointAmount[2];
        }

        resemblanceCountText.text = $"Resemblance Count {playerResemblanceCount}";
        redeemAmountText.text = $"Redeem with {requiredResemblanceCount} resemblances";

        isOpen = false;
        //resemblanceUI.SetActive(isOpen);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        playerResemblanceCount = playerInventory.GetResemblanceCount();
        //resemblanceCountText.text = $"Resemblance Count {playerResemblanceCount}";

        if (playerResemblanceCount < checkpointAmount[0])
        {
            requiredResemblanceCount = checkpointAmount[0];
            unlockButton.interactable = false;
        }
        else if (playerResemblanceCount >= checkpointAmount[0] && playerResemblanceCount < checkpointAmount[1])
        {
            requiredResemblanceCount = checkpointAmount[0];
            unlockButton.interactable = true;
        }
        else if (playerResemblanceCount >= checkpointAmount[1] && playerResemblanceCount < checkpointAmount[2])
        {
            requiredResemblanceCount = checkpointAmount[1];
            unlockButton.interactable = true;
        }
        else
        {
            requiredResemblanceCount = checkpointAmount[2];
            unlockButton.interactable = true;
        }
        redeemAmountText.text = $"Redeem with {requiredResemblanceCount} resemblances";
    }

    public override void OnInteract()
    {
        //show ui
        //Debug.Log("Interact with resemblance redemption");
        //render ui according to the amount of resemblance player has
        isOpen = !isOpen;
        resemblanceUI.SetActive(isOpen);

        if (isOpen)
        {
            PauseManager.PauseTime();
        }
        else
        {
            PauseManager.ResumeTime();
        }
        
        //render redeem button
        

    }
}
