using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Player;
    public Vector3 Offset;

    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Offset.x, Offset.y, Player.position.z);
    }
}
