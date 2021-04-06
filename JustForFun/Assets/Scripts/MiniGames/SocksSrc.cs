using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocksSrc : MonoBehaviour
{
    public int ID = 0;
    public GameObject SocksObj;
    public Rigidbody MainBone;
    public bool onDrag = false;

    Material SocksMt;
    int row = 3, col = 5;
    void Start()
    {
        int _row = Mathf.FloorToInt(ID / col);
        int _col = ID - _row * col;
        Vector2 _offset = new Vector2(1f / col * _col, 1f / row * _row);

        SocksMt = SocksObj.GetComponent<SkinnedMeshRenderer>().material;
        SocksMt.SetTextureOffset("_MainTex", _offset);
            
    }
}
