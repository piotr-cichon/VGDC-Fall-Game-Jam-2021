using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    public void Awake()
    {
        Slider slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(VolumeChange);
        slider.value = 0.75f;
    }

    public void VolumeChange(float volume)
    {
        audioMixer.SetFloat("volume", Mathf.Max(80 * (volume - 1), -80));
    }
}
