using System.Collections;
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

        CMDebug.ButtonUI(new Vector2(100,100),"damage",() => {
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

        vertices[0] = new Vector3(0,1);
        vertices[1] = new Vector3(1,1);
        vertices[2] = new Vector3(0,0);
        vertices[3] = new Vector3(1,0);

        uv[0] = new Vector2(0,1);
        uv[1] = new Vector2(1,1);
        uv[2] = new Vector2(0,0);
        uv[3] = new Vector2(1,0);

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
    }

    public Vector2 ConvertPixelsToUVCoordinates(int x, int y , int textureWidth,int textureHeight)
    {
        return new Vector2((float)x/textureWidth,(float)y/textureHeight);
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

