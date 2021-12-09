using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxFootstepController : MonoBehaviour
{
    [SerializeField] private List<AudioClip> footstep_sfx_list_;
    private AudioClip curr_footstep_;
    private AudioSource audio_;

    void Awake()
    {
        audio_ = GetComponent<AudioSource>();
        curr_footstep_ = footstep_sfx_list_[0];
    }

    private void PlayFootstepSfx() //used by anim events
    {
        audio_.PlayOneShot(curr_footstep_);
    }

    public void SetCurrFootstepSfx(int id)
    {
        if (id < footstep_sfx_list_.Count)
        {
            curr_footstep_ = footstep_sfx_list_[id];
        }
        else
        {
            Debug.Log(">>> id >= footstep_sfx_list_.Count");
        }
    }

    public void ResetFootstepSfx()
    {
        SetCurrFootstepSfx(0);
    }
}
