using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    public Transform[] backgrounds;
    public float[] parallaxScales;
    public float smoothing=1f;
    public Transform cam;
    public Vector3 PreviousCamPos;
    private void Awake()
    {
        cam = Camera.main.transform;
    }
    void Start()
    {
        PreviousCamPos = cam.position;
        parallaxScales = new float[backgrounds.Length];
        
        for(int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
    }
    void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float parallax = (PreviousCamPos.x - cam.position.x) * parallaxScales[i];

            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        PreviousCamPos = cam.position;
    }
}
