using UnityEngine;
public class Knife : MonoBehaviour
{
    public float distance = 10f;

    public float spinSpeed = 10f;

    private float spinSpeedMultiply = 10f;

    GameManager gm;
    Vector3 startPos;

    public GameObject instruction;

    private void Start()
    {
        gm = GameManager.instance;
        startPos = new Vector3(transform.position.x,transform.position.y,transform.position.z);
    }

    void OnMouseDrag()
    {
        instruction.SetActive(false);

        if (!gm.levelComplete)
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = objPosition;
            transform.Rotate(0, 0, spinSpeed * spinSpeedMultiply * Time.deltaTime);          
        }
    }
    private void Update()
    {
        if (Input.touchCount == 0)
        {
            transform.position = startPos;
            transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        }
    }
}
