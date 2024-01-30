using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulser : MonoBehaviour
{
    [SerializeField]
    float pulseSize = 1.15f, returnSpeed = 5f;
    private Vector3 startSize;

    public bool ShouldPulse = false;

    // Start is called before the first frame update
    void Start()
    {
        startSize = transform.localScale;

        EventManager.Instance.OnBeat += Pulse;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, startSize, Time.deltaTime * returnSpeed);
    }

    void Pulse()
    {
        if(ShouldPulse)
        transform.localScale = startSize * pulseSize;
    }
}
