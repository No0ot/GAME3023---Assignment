using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject main_player_;
    [SerializeField] private Vector3 cam_offset_; //only for side view, to make camera off-centered for platforming
    [SerializeField] private float cam_lag_ = 0.125f;

    private void Awake()
    {
        main_player_ = GameObject.Find("Player");
    }

    private void FixedUpdate()
    {
        Vector3 target_pos = new Vector3(main_player_.transform.position.x + cam_offset_.x,
                                            main_player_.transform.position.y + cam_offset_.y,
                                            cam_offset_.z);
        transform.position = Vector3.Lerp(transform.position, target_pos, cam_lag_); //smoothen movement
    }
}
