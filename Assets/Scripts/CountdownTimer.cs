using UnityEngine;
using UnityEngine.Events;

public class CountdownTimer : MonoBehaviour
{
    [Header("Timer")]
    public float durationSeconds = 180f;
    public float remainingTime { get; private set; }
    public bool isRunning { get; private set; }

    [Header("Events")]
    public UnityEvent onTimerFinished;

    private void Update()
    {
        if (!isRunning)
            return;

        remainingTime -= Time.deltaTime;
        if (remainingTime <= 0f)
        {
            remainingTime = 0f;
            isRunning = false;
            Debug.Log("Tiempo terminado!");
            onTimerFinished?.Invoke();
        }
    }

    public void Play()
    {
        remainingTime = durationSeconds;
        isRunning = true;
    }

    public void Stop()
    {
        isRunning = false;
    }

    public void ResetTimer()
    {
        remainingTime = durationSeconds;
    }
}