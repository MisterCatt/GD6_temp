using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OnBeat { BEAT, OFFBEAT};

public class RythmManager : MonoBehaviour
{
    public static RythmManager Instance;

    public OnBeat CurrentBeat = OnBeat.OFFBEAT;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
