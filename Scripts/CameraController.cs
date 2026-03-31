using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float minHeight = 5f;
    public float maxHeight = 30f;
    public float zoomSpeed = 5f;

    private float heightAbovePlayer;

    void Start()
    {
        heightAbovePlayer = (minHeight + maxHeight) / 2f;
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 playerPosition = player.transform.position;

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            heightAbovePlayer -= scroll * zoomSpeed * Time.deltaTime;
            heightAbovePlayer = Mathf.Clamp(heightAbovePlayer, minHeight, maxHeight);

            transform.position = new Vector3(playerPosition.x, playerPosition.y + heightAbovePlayer, playerPosition.z);
        }
    }
}