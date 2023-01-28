using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioEventManager : MonoBehaviour
{
    private float bgm_volume = 0.0f;
    private bool bgm_paused = false;

    //MUSIC EVENTS
    [FMODUnity.EventRef]
    public string mus_graveyard_ambient_loop = "event:/MUSIC/mus_graveyard_ambient_loop";
    //public string mus_zombie_boss_loop = "event:/MUSIC/mus_zombie_boss_loop";
    //public string mus_necromancer_boss_loop = "event:/MUSIC/mus_necromancer_boss_loop";
    //public string mus_vampire_boss_loop = "event:/MUSIC/mus_vampire_boss_loop";

    private FMOD.Studio.EventInstance curr_bgm;

    public void Start()
    {
        curr_bgm = FMODUnity.RuntimeManager.CreateInstance(mus_graveyard_ambient_loop);
        curr_bgm.setVolume(bgm_volume);
        curr_bgm.start();
    }

    //SFX EVENTS

    //Audio Functions
    public void PlayBGM()
    {
        curr_bgm.start();
        bgm_paused = false;
    }

    public void MuteBGM()
    {
        bgm_volume = (bgm_volume == 0.0f) ? 1.0f : 0.0f;
        curr_bgm.setVolume(bgm_volume);
    }

    public void PauseBGM()
    {
        if (bgm_paused) {
            curr_bgm.setPaused(false);
            bgm_paused = false;
        } else { 
            curr_bgm.setPaused(true);
            bgm_paused = true;
        }
    }
}

/*Audio TODO:
-Stop() BGM function
-Music volume slider
-SFX volume slider
*/