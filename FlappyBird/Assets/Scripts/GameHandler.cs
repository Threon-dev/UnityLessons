﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;

public class GameHandler : MonoBehaviour
{
    void Start()
    {

        int count = 0;
        FunctionPeriodic.Create(() =>
        {
            //CMDebug.TextPopupMouse("Ding! " + count);
            count++;
        },.300f);
    }
}
