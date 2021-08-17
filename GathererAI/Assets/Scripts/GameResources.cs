using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class GameResources
{
    public static event EventHandler onResourceAmountChanged;

    public enum ResourceType
    {
        Gold,
        Wood,
    }

    private static Dictionary<ResourceType, int> resourceAmountDictionary;

    public static void Init()
    {
        resourceAmountDictionary = new Dictionary<ResourceType, int>();

        foreach(ResourceType resourceType in System.Enum.GetValues(typeof(ResourceType)))
        {
            resourceAmountDictionary[resourceType] = 0;
        }
    }
    public static void AddResourceAmount(ResourceType resourceType,int amount)
    {
        resourceAmountDictionary[resourceType] += amount;
        onResourceAmountChanged?.Invoke(null, EventArgs.Empty); 
    }

    public static void RemoveResourceAmount(ResourceType resourceType,int amount)
    {
        resourceAmountDictionary[resourceType] -= amount;
        onResourceAmountChanged?.Invoke(null, EventArgs.Empty);
    }
    public static int GetResourceAmount(ResourceType resourceType)
    {
        return resourceAmountDictionary[resourceType];
    }
}
