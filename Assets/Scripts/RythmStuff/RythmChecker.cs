using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RythmChecker : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "RythmBlock")
            return;

        RythmManager.Instance.CurrentBeat = OnBeat.BEAT;
        EventManager.Instance.Beat();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "RythmBlock")
            return;

        RythmManager.Instance.CurrentBeat = OnBeat.OFFBEAT;
    }
}
