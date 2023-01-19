using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioEventManager : MonoBehaviour
{
    private float bgm_volume = 0.0f;

    //MUSIC EVENTS
    [FMODUnity.EventRef]
    public string mus_graveyard_ambient_loop = "event:/MUSIC/mus_graveyard_ambient_loop";


    private FMOD.Studio.EventInstance curr_bgm;

    public void Start()
    {
        curr_bgm = FMODUnity.RuntimeManager.CreateInstance(mus_graveyard_ambient_loop);
        curr_bgm.setVolume(bgm_volume);
        curr_bgm.start();
    }

    //SFX EVENTS

    public void MuteBGM()
    {
        bgm_volume = (bgm_volume == 0.0f) ? 1.0f : 0.0f;
        curr_bgm.setVolume(bgm_volume);
    }

    //TODO: fix
    public void PauseBGM()
    {
        FMOD.Studio.PLAYBACK_STATE bgm_playing;
        curr_bgm.getPlaybackState(out bgm_playing);

        if (bgm_playing == FMOD.Studio.PLAYBACK_STATE.STOPPED)
        {
            curr_bgm.setPaused(false);
        } else { 
            curr_bgm.setPaused(true); 
        }
    }
}
