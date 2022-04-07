using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public GameObject camera;
    Vector3 positionPlayer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        positionPlayer = player.transform.position;
        camera.transform.position = new Vector3(positionPlayer.x, positionPlayer.y + 200, positionPlayer.z);
    }
}
