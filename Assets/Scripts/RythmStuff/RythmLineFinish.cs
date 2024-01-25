using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RythmLineFinish : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "RythmBlock")
            return;

        collision.GetComponent<RythmBlock>().Reset();
    }
}
