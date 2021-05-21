﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using V_AnimationSystem;
using CodeMonkey;
using CodeMonkey.Utils;

public class GameHandler : MonoBehaviour {

    public Material material;

    public Transform pfHealthBar;
    public CameraFollow cameraFollow;
    public Transform playerTransform;

    private float zoom = 40f;

    private Vector2[] headDownUV;
    private Vector2[] headUpUV;
    private Vector2[] headLeftUV;
    private Vector2[] headRightUV;

    private Vector2[] bodyDownUV;
    private Vector2[] bodyUpUV;
    private Vector2[] bodyLeftUV;
    private Vector2[] bodyRightUV;

    private Vector2[] swordUV;
    private Vector2[] shieldUV;

    private Vector2[] handUV;
    private Vector2[] footUV;

    void Start()
    {

        cameraFollow.Setup(() => playerTransform.position, () => zoom);
        cameraFollow.SetGetCameraFollowPositionFunc(() => playerTransform.position);

        HealthSystem healthSystem = new HealthSystem(100);

        Transform healthBarTransform = Instantiate(pfHealthBar, new Vector3(0, 10), Quaternion.identity);
        HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();
        healthBar.Setup(healthSystem);

        Debug.Log("Health:" + healthSystem.GetHealthPercent());
        healthSystem.Damage(10);
        Debug.Log("Health:" + healthSystem.GetHealthPercent());

        CMDebug.ButtonUI(new Vector2(100, 100), "damage", () => {
            healthSystem.Damage(10);
            Debug.Log("Damaged:" + healthSystem.GetHealth());
        });

        CMDebug.ButtonUI(new Vector2(-100, 100), "heal", () => {
            healthSystem.Heal(10);
            Debug.Log("Healed:" + healthSystem.GetHealth());
        });

        CMDebug.ButtonUI(new Vector2(570, 90), "ZoomIn", ZoomIn);


        CMDebug.ButtonUI(new Vector2(570, 30), "ZoomOut", ZoomOut);

        Vector3[] vertices = new Vector3[4];
        Vector2[] uv = new Vector2[4];
        int[] triangles = new int[6];

        vertices[0] = new Vector3(0, 1);
        vertices[1] = new Vector3(1, 1);
        vertices[2] = new Vector3(0, 0);
        vertices[3] = new Vector3(1, 0);
        /*    
        uv[0] = new Vector2(0, 1);
        uv[1] = new Vector2(1, 1);
        uv[2] = new Vector2(0, 0);
        uv[3] = new Vector2(1, 0);
        /*
        int headX = 0;
        int headY = 380;
        int headWidth = 128;
        int headHeight = 128;
        int textureWidth = 512;
        int textureHeaight = 512;

        uv[0] = ConvertPixelsToUVCoordinates(headX, headY + headHeight, textureWidth, textureHeaight);
        uv[1] = ConvertPixelsToUVCoordinates(headX + headWidth, headY + headHeight, textureWidth, textureHeaight);
        uv[2] = ConvertPixelsToUVCoordinates(headX, headY, textureWidth, textureHeaight);
        uv[3] = ConvertPixelsToUVCoordinates(headX + headWidth, headY, textureWidth, textureHeaight);
        **/
        headDownUV = GetUVRectangleFromPixels(0, 380, 128, 128, 512, 512);
        headUpUV = GetUVRectangleFromPixels(256, 380, 128, 128, 512, 512);
        headLeftUV = GetUVRectangleFromPixels(256, 380, -128, 128, 512, 512);
        headRightUV = GetUVRectangleFromPixels(128, 380, 128, 128, 512, 512);

        bodyDownUV = GetUVRectangleFromPixels(0, 256, 128, 128, 512, 512);
        bodyUpUV = GetUVRectangleFromPixels(256, 256, 128, 128, 512, 512);
        bodyLeftUV = GetUVRectangleFromPixels(256, 256, -128, 128, 512, 512);
        bodyRightUV = GetUVRectangleFromPixels(128, 256, 128, 128, 512, 512);

        swordUV = GetUVRectangleFromPixels(0, 128, 128, 128, 512, 512);
        shieldUV = GetUVRectangleFromPixels(128, 128, 128, 128, 512, 512);

        handUV = GetUVRectangleFromPixels(384, 448, 64, 64, 512, 512);
        footUV = GetUVRectangleFromPixels(448, 448, 64, 64, 512, 512);

        ApplyUVToUVArray(headLeftUV, ref uv);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        triangles[3] = 2;
        triangles[4] = 1;
        triangles[5] = 3;

        Mesh mesh = new Mesh();

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        GameObject gameObject = new GameObject("Mesh", typeof(MeshFilter), typeof(MeshRenderer));
        gameObject.transform.localScale = new Vector3(30, 30, 1);

        gameObject.GetComponent<MeshFilter>().mesh = mesh;
        gameObject.GetComponent<MeshRenderer>().material = material;

        CMDebug.ButtonUI(new Vector2(-200, 100), "Head Down", () =>
        {
            ApplyUVToUVArray(headDownUV, ref uv);
            mesh.uv = uv;
        });
        CMDebug.ButtonUI(new Vector2(-200, 30), "Body Down", () =>
        {
            ApplyUVToUVArray(bodyDownUV, ref uv);
            mesh.uv = uv;
        });
        CMDebug.ButtonUI(new Vector2(-200, -40), "Sword Down", () =>
        {
            ApplyUVToUVArray(swordUV, ref uv);
            mesh.uv = uv;
        });
    }

    private Vector2 ConvertPixelsToUVCoordinates(int x, int y, int textureWidth, int textureHeight)
    {
        return new Vector2((float)x / textureWidth, (float)y / textureHeight);
    }

    private Vector2[] GetUVRectangleFromPixels(int x, int y, int width, int height, int textureWidth, int textureHeight)
    {
        /*0,1
         * 1,1
         * 0,0
         * 1,0
         * */
        return new Vector2[]
        {
            ConvertPixelsToUVCoordinates(x,y+height,textureWidth,textureHeight),
            ConvertPixelsToUVCoordinates(x+width,y+height,textureWidth,textureHeight),
            ConvertPixelsToUVCoordinates(x,y,textureWidth,textureHeight),
            ConvertPixelsToUVCoordinates(x+width,y,textureWidth,textureHeight),
        };
    }

    private void ApplyUVToUVArray(Vector2[] uv, ref Vector2[] mainUV)
    {
        if (uv == null || uv.Length < 4 || mainUV == null || mainUV.Length < 4) throw new System.Exception();
        

        
        mainUV[0] = uv[0];
        mainUV[1] = uv[1];
        mainUV[2] = uv[2];
        mainUV[3] = uv[3];
    }
    void ZoomIn()
    {
        zoom -= 40f;

        if (zoom < 40f) zoom = 40f;
    }
    void ZoomOut()
    {
        zoom += 40f;
        if (zoom > 200f) zoom = 200f;
    }
}

