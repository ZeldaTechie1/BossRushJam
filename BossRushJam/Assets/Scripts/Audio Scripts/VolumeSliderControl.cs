using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSliderControl : MonoBehaviour
{
    FMOD.Studio.Bus master_bus;
    float master_vol = 0.5f;

    void Awake()
    {
        master_bus = FMODUnity.RuntimeManager.GetBus("bus:/MASTER");
    }

    void Update()
    {
        master_bus.setVolume(master_vol);
    }

    public void MasterVolumeLevel(float new_master_vol)
    {
        master_vol = new_master_vol;
    }
}
