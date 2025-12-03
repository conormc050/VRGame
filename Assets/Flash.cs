using UnityEngine;

public class LightningFlashSimple : MonoBehaviour
{
    public Light lightningLight;
    public float flashDuration = 2f;    
    public float minTimeBetweenFlashes = 4f;
    public float maxTimeBetweenFlashes = 12f;

    private float nextFlashTime;
    private float timer;
    private bool flashing = false;

    void Start()
    {
        lightningLight.intensity = 0f;
        nextFlashTime = Random.Range(minTimeBetweenFlashes, maxTimeBetweenFlashes);
    }

    void Update()
    {
        if (!flashing)
        {
            nextFlashTime -= Time.deltaTime;
            if (nextFlashTime <= 0f)
            {
                flashing = true;
                timer = 0f;
                nextFlashTime = Random.Range(minTimeBetweenFlashes, maxTimeBetweenFlashes);
            }
        }
        else
        {
            timer += Time.deltaTime;
            float t = timer / flashDuration;

            if (t < 0.2f)
                lightningLight.intensity = Mathf.Lerp(0f, 3.5f, t * 5f);
            else
                lightningLight.intensity = Mathf.Lerp(3.5f, 0f, (t - 0.2f) * 2f);

            if (t >= 1f)
            {
                lightningLight.intensity = 0f;
                flashing = false;
            }
        }
    }
}
