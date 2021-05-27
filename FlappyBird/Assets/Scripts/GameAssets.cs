using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    public static GameAssets instanse;

    public static GameAssets GetInstanse()
    {
        return instanse;
    }

    private void Awake()
    {
        instanse = this;
    }


    public Sprite pipeHeadSprite;

    public Transform pfPipeHead;
    public Transform pfPipeBody;
}
