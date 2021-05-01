using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IRoomGame;

public class DoubleGameSrc : MonoBehaviour
{
    public Transform RootObject;
    public GameObject SocksObject;
    public bool isDemo = true;
    public Transform BucketObj;
    public GameObject WallCollider;
    public float ProgressValue => _ProgressValue;
    float _ProgressValue = 0;
    public GamePoolSrc.GameState _state;
    Vector3 IdlePos;
    Quaternion IdleRot;
    float StartSpeed = 0.5f;
    Transform MoveObj;
    Transform GamePosObj;
    Plane Ground;
    float CurPos;
    int SocksCount = 10;
    SocksSrc[] socksSrcs;

    void Start()
    {
        Ground = new Plane(Vector3.up, RootObject.position);
        MoveObj = Camera.main.transform;
        GamePosObj = RootObject.GetChild(0);

        IdleRot = MoveObj.rotation;
        IdlePos = MoveObj.position;

        socksSrcs = new SocksSrc[SocksCount];
        for (int _id = 0; _id < SocksCount; _id++)
        {
            socksSrcs[_id] = Instantiate(SocksObject, RootObject).GetComponent<SocksSrc>();
            socksSrcs[_id].ID = _id + 1 > SocksCount / 2 ? _id - SocksCount / 2 : _id;
            float _angle = Random.Range(0, 360) * Mathf.Deg2Rad;
            socksSrcs[_id].transform.position = RootObject.position + new Vector3(Mathf.Sin(_angle), Random.Range(3f, 5f), Mathf.Cos(_angle)) * Random.Range(5, 10);
        }
    }

    Ray InputRay;
    RaycastHit InputHit;
    bool InputHook = false;
    void Update()
    {
        if (_state == GamePoolSrc.GameState.START)
        {
            CurPos += Time.deltaTime;
            MoveObj.position = Vector3.Lerp(IdlePos, GamePosObj.position, CurPos / StartSpeed);
            MoveObj.rotation = Quaternion.Lerp(IdleRot, GamePosObj.rotation, CurPos / StartSpeed);

            if (CurPos / StartSpeed >= 1f)
            {
                SetState(GamePoolSrc.GameState.PLAY);
            }
        }

        if (_state == GamePoolSrc.GameState.STOP)
        {
            CurPos -= Time.deltaTime;
            MoveObj.position = Vector3.Lerp(IdlePos, GamePosObj.position, CurPos / StartSpeed);
            MoveObj.rotation = Quaternion.Lerp(IdleRot, GamePosObj.rotation, CurPos / StartSpeed);

            if (CurPos / StartSpeed <= 0f)
            {
                SetState(GamePoolSrc.GameState.IDLE);
            }
        }

        if (_state == GamePoolSrc.GameState.PLAY)
        {
            if (Input.GetMouseButtonDown(0))
            {
                InputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                InputHook = Physics.Raycast(InputRay, out InputHit, 300f, LayerMask.GetMask("Socks"));
            } else if (Input.GetMouseButtonUp(0))
            {
                InputHook = false;
            } else if (Input.GetMouseButton(0) && InputHook)
            {
                InputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                float _dist;
                Ground.Raycast(InputRay, out _dist);
                if (InputHit.rigidbody != null) InputHit.rigidbody.velocity = (InputRay.GetPoint(_dist * 0.7f) - InputHit.transform.position) * 3f;
            }
        }
    }

    public void SetState(GamePoolSrc.GameState state)
    {
        _state = state;
        if (state == GamePoolSrc.GameState.START || state == GamePoolSrc.GameState.IDLE)
        {
            MoveObj.rotation = IdleRot;
            MoveObj.position = IdlePos;
            
            CurPos = 0;
        }

        if (state == GamePoolSrc.GameState.STOP)
        {
            WallCollider.SetActive(false);
            for (int _id = 0; _id < SocksCount; _id++)
            {
                if (socksSrcs[_id] != null) Destroy(socksSrcs[_id].gameObject);
            }
            CurPos = StartSpeed;
        }

        if (state == GamePoolSrc.GameState.PLAY)
        {
            WallCollider.SetActive(true);
            if (isDemo)
            {
                BucketObj.GetChild(2).gameObject.SetActive(true);
                BucketObj.GetChild(3).gameObject.SetActive(true);
            }
            MoveObj.position = GamePosObj.position;
            MoveObj.rotation = GamePosObj.rotation;
        }
    }

    public void UpdateProg()
    {
        _ProgressValue += 1f / ((float)SocksCount / 2f);
    }
}
