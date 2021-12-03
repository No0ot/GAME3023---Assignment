using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxFootstepId : MonoBehaviour
{
    [SerializeField] private int footstep_id_ = 0;

    public int GetFootstepId()
    {
        return footstep_id_;
    }
}
