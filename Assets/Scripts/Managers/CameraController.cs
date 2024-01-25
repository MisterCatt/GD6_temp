using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    GameObject Grid, InterractScreen;

    private void Start()
    {
        positionCamera();
    }

    private void positionCamera()
    {
        Vector3 midPoint = (Grid.transform.position + InterractScreen.transform.position) / 2;

        midPoint = new Vector3 (midPoint.x, midPoint.y, -10);

        Camera.main.transform.position = midPoint;
    }
}
