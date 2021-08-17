using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;
using CodeMonkey;

public class ResourceNode
{
    public static event EventHandler OnResourceNodeClicked;
    private int resourcesAmount;
    private int resourcesAmountMax;
    private GameResources.ResourceType resourceType;

    private Transform resourceNodeTransform;
    public ResourceNode(Transform resourceNodeTransform,GameResources.ResourceType resourceType)
    {
        this.resourceNodeTransform = resourceNodeTransform;
        this.resourceType = resourceType;
        resourcesAmountMax = 3;
        resourcesAmount = resourcesAmountMax;
        resourceNodeTransform.GetComponent<Button_Sprite>().ClickFunc = () =>
        {
            OnResourceNodeClicked?.Invoke(this,EventArgs.Empty);
        };

        FunctionPeriodic.Create(RegenerateSingleResourceAmount,2f);
    }

    public Vector3 GetPosition()
    {
        return resourceNodeTransform.position;
    }

    public GameResources.ResourceType GetResourceType()
    {
        return resourceType;
    }
    public GameResources.ResourceType GrabResource()
    {
        resourcesAmount -= 1;
        if(resourcesAmount == 0)
        {
            UpdateSprite();
            //FunctionTimer.Create(ResetResourceAmount, 5f);
        }
        return resourceType;
    }

    public bool HasResources()
    {
        return resourcesAmount > 0;
    }
    private void ResetResourceAmount()
    {
        resourcesAmount = resourcesAmountMax;
        UpdateSprite();
    }

    private void RegenerateSingleResourceAmount()
    {
        if (resourcesAmount < resourcesAmountMax)
        {
            resourcesAmount++;
            UpdateSprite();
        } 
    }
    private void UpdateSprite()
    {
        if(resourcesAmount > 0)
        {
            switch (resourceType)
            {
                default:
                case GameResources.ResourceType.Gold:
                    resourceNodeTransform.GetComponent<SpriteRenderer>().sprite = GameAssets.i.goldNodeSprite;
                    break;
                case GameResources.ResourceType.Wood:
                    resourceNodeTransform.GetComponent<SpriteRenderer>().sprite = GameAssets.i.treeSprite;
                    break;
            }
        }
        else
        {
            switch (resourceType)
            {
                default:
                case GameResources.ResourceType.Gold:
                    resourceNodeTransform.GetComponent<SpriteRenderer>().sprite = GameAssets.i.goldNodeDepletedSprite;
                    break;
                case GameResources.ResourceType.Wood:
                    resourceNodeTransform.GetComponent<SpriteRenderer>().sprite = GameAssets.i.treeDepletedSprite;
                    break;
            }
        }
    }
}
