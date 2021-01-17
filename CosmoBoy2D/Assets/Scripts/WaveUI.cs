using UnityEngine.UI;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    [SerializeField]
    WaveSpawner spawner;

    [SerializeField]
    Animator waveAnimator;

    [SerializeField]
    Text waveCountdownObject;

    [SerializeField]
    Text waveCount;

    private WaveSpawner.SpawnState previousState;
    private void Start()
    {
        if (spawner == null)
        {
            Debug.LogError("No spawner referenced");
            this.enabled = false;
        }

        if (waveAnimator == null)
        {
            Debug.LogError("No waveAnimator referenced");
            this.enabled = false;
        }

        if (waveCountdownObject == null)
        {
            Debug.LogError("No waveCountdownObjet referenced");
            this.enabled = false;
        }

        if (waveCount == null)
        {
            Debug.LogError("No waveCount referenced");
            this.enabled = false;
        }
    }
    private void Update()
    {
        switch (spawner.State)
        {
            case WaveSpawner.SpawnState.COUNTING:
                UpdateCountingUI();
                break;
            case WaveSpawner.SpawnState.SPAWNING:
                UpdateSpawningUI();
                break;

        }

        previousState = spawner.State;
    }
    void UpdateCountingUI()
    {
        if(previousState!= WaveSpawner.SpawnState.COUNTING)
        {
            waveAnimator.SetBool("WaveIncoming", false);
            waveAnimator.SetBool("WaveCoundown", true);
            Debug.Log("Counting");
        }

        waveCountdownObject.text = ((int)spawner.WaveCountdown).ToString();      
    }
    void UpdateSpawningUI()
    {
        if (previousState != WaveSpawner.SpawnState.SPAWNING)
        {
            waveAnimator.SetBool("WaveCoundown", false);
            waveAnimator.SetBool("WaveIncoming", true);

            waveCount.text = spawner.NextWave.ToString();

            Debug.Log("Spawning");
        }
    }
}
