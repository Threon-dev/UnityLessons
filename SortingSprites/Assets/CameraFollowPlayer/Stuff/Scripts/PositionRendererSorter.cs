using UnityEngine;
using System.Collections;

public class PositionRendererSorter : MonoBehaviour {

    public int sortingOrderBase = 5000;
    public int offset = 0;
    public bool runOnlyOnce = false;
    
    private Renderer myRenderer;
    private float timer;
    private const float timerMax = .1f;

    void Awake() {
        myRenderer = transform.GetComponent<Renderer>();
    }
	void LateUpdate () {
        timer -= Time.deltaTime;
        if (timer <= 0f) {
            timer = timerMax;
            myRenderer.sortingOrder = (int)(sortingOrderBase - transform.position.y + offset);
            if (runOnlyOnce) Destroy(this);
        }
	}
}
