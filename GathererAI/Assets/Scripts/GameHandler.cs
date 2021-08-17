using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CodeMonkey;
using CodeMonkey.Utils;
public class GameHandler : MonoBehaviour
{
    private static GameHandler instance;

    public static GameHandler GetInstance()
    {
        return instance;
    }
    [SerializeField] private GathererAI[] gathererAIArray;
    private GathererAI selectedGathererAI;

    [SerializeField] private Transform[] goldNodeTransformArray;
    [SerializeField] private Transform[] treeTransformArray;

    [SerializeField] private Transform storageTransform;

    [SerializeField] private Transform pfTower;
    [SerializeField] private Transform pfUnit;

    private List<ResourceNode> resourceNodeList;
    private void Awake()
    {
        instance = this;

        GameResources.Init();
        resourceNodeList = new List<ResourceNode>();
        foreach (Transform goldNodeTransform in goldNodeTransformArray)
        {
            resourceNodeList.Add(new ResourceNode(goldNodeTransform, GameResources.ResourceType.Gold));
        }

        foreach (Transform treeTransform in treeTransformArray)
        {
            resourceNodeList.Add(new ResourceNode(treeTransform, GameResources.ResourceType.Wood));
        }

        ResourceNode.OnResourceNodeClicked += ResourceNode_OnResourceNodeClicked;
        GathererAI.onUnitSelected += GathererAI_onUnitSelected;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (Tower.TrySpendResourcesCost())
            {
                Vector3 towerSpawnPosition = UtilsClass.GetMouseWorldPosition();
                Tower.Create(pfTower, towerSpawnPosition, () => {
                    SpawnGathererUnit(towerSpawnPosition + new Vector3(0, -2));
                });
            }
        }
    }

    private void SpawnGathererUnit(Vector3 position)
    {
         Instantiate(pfUnit, position, Quaternion.identity);
    }
    private void GathererAI_onUnitSelected(object sender, EventArgs e)
    {
        GathererAI gathererAI = sender as GathererAI;
        if(selectedGathererAI != null)
        {
            selectedGathererAI.HideSelectedSprite();
        }
        selectedGathererAI = gathererAI;
        selectedGathererAI.ShowSelectedSprite();
    }

    private void ResourceNode_OnResourceNodeClicked(object sender, EventArgs e)
    {
        ResourceNode resourceNode = sender as ResourceNode;
        //order the selected gatherer
        if(selectedGathererAI != null)
        {
            selectedGathererAI.SetResourceNode(resourceNode);
        }
        else
        {
            // no one is selected
        }
    }

    private ResourceNode GetNodeTransform()
    {
        List<ResourceNode> tmpResourceNodeList = new List<ResourceNode>(resourceNodeList);
        for (int i = 0; i < tmpResourceNodeList.Count; i++)
        {
            if (!tmpResourceNodeList[i].HasResources())
            {
                tmpResourceNodeList.RemoveAt(i);
                i--;
            }
        }

        return tmpResourceNodeList[UnityEngine.Random.Range(0, tmpResourceNodeList.Count)];         
    }
    public static ResourceNode GetNodeTransform_Static()
    {
        return instance.GetNodeTransform();
    }

    private ResourceNode GetNodeTransformNearPosition(Vector3 position, GameResources.ResourceType resourceType)
    {
        float maxDistance = 20f;
        List<ResourceNode> tmpResourceNodeList = new List<ResourceNode>(resourceNodeList);
        for (int i = 0; i < tmpResourceNodeList.Count; i++)
        {
            if (!tmpResourceNodeList[i].HasResources() ||
                Vector3.Distance(position,tmpResourceNodeList[i].GetPosition()) > maxDistance || 
                tmpResourceNodeList[i].GetResourceType() != resourceType)
            {
                tmpResourceNodeList.RemoveAt(i);
                i--;
            }
        }
        if(tmpResourceNodeList.Count > 0)
        {
            return tmpResourceNodeList[UnityEngine.Random.Range(0, tmpResourceNodeList.Count)];
        }
        else
        {
            return null;
        }
    }
    public static ResourceNode GetNodeTransformNearPosition_Static(Vector3 position,GameResources.ResourceType resourceType)
    {
        return instance.GetNodeTransformNearPosition(position,resourceType);
    }

    private Transform GetStorageTransform()
    {
        return storageTransform;
    }
    public static Transform GetStorageTransform_Static()
    {
        return instance.GetStorageTransform();
    }
}
