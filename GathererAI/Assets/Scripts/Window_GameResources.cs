using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Window_GameResources : MonoBehaviour
{
    private void UpdateResourceTextObject()
    {
        transform.Find("goldAmount").GetComponent<Text>().text = 
            "GOLD: " + GameResources.GetResourceAmount(GameResources.ResourceType.Gold)+"\n"+
            "WOOD: " + GameResources.GetResourceAmount(GameResources.ResourceType.Wood) + "\n";
    }

    private void Awake()
    {
        GameResources.onResourceAmountChanged += delegate (object sender, EventArgs e)
        {
            UpdateResourceTextObject();
        }; 
        UpdateResourceTextObject();        
    }

   
}
