using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip Switch;
    public AudioClip ColumnLose;
    public AudioClip ColumnDone;

    [SerializeField] private AudioSource _sfx;
    [SerializeField] private AudioSource _backGround;

    public void PlayTargetSound(AudioClip clip)
    {
        _sfx.PlayOneShot(clip);
    }
}
