using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioEventManager : MonoBehaviour
{
    private float bgm_volume = 1.0f;
    private bool bgm_paused = false;

    //MUSIC EVENTS
    //[FMODUnity.EventRef]
    public string mus_menu_bgm = "event:/MUSIC/mus_graveyard_ambient_loop";
    public string mus_zombie_bgm = "event:/MUSIC/mus_zombie_bgm";
    public string mus_skeleton_bgm = "event:/MUSIC/mus_skeleton_bgm";
    public string mus_necromancer_bgm = "event:/MUSIC/mus_necromancer_bgm";

    
    private FMOD.Studio.EventInstance menu_bgm;
    private FMOD.Studio.EventInstance zombie_bgm;
    private FMOD.Studio.EventInstance skeleton_bgm;
    private FMOD.Studio.EventInstance necromancer_bgm;
    private FMOD.Studio.EventInstance curr_bgm;


    private enum BOSS_TYPE
    {
        ZOMBIE = 1,
        SKELETON = 2,
        NECROMANCER = 3
    };

    public void Start()
    {
        menu_bgm = FMODUnity.RuntimeManager.CreateInstance(mus_menu_bgm);
        zombie_bgm = FMODUnity.RuntimeManager.CreateInstance(mus_zombie_bgm);
        skeleton_bgm = FMODUnity.RuntimeManager.CreateInstance(mus_skeleton_bgm);
        necromancer_bgm = FMODUnity.RuntimeManager.CreateInstance(mus_necromancer_bgm);

        curr_bgm = skeleton_bgm;
        curr_bgm.setVolume(bgm_volume);
        bgm_paused = false;
    }

    //SFX EVENTS

    //Audio Functions
    public void PlayBackgroundMusicByType(int bgm_type)
    {
        switch ((BOSS_TYPE)bgm_type)
        {
            case BOSS_TYPE.ZOMBIE:
                curr_bgm = zombie_bgm;
                break;
            case BOSS_TYPE.SKELETON:
                curr_bgm = skeleton_bgm;
                break;
            case BOSS_TYPE.NECROMANCER:
                curr_bgm = necromancer_bgm;
                break;
            default:
                curr_bgm = zombie_bgm;
                break;
        }

        curr_bgm.setVolume(bgm_volume);
        curr_bgm.start();
        bgm_paused = false;
    }

    public void MuteBGM()
    {
        bgm_volume = (bgm_volume == 0.0f) ? 1.0f : 0.0f;
        curr_bgm.setVolume(bgm_volume);
    }

    public void StopBGM()
    {
        bgm_paused = true;
        curr_bgm.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
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

    public void TransitionBossMusicPhase(int boss_phase)
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("BossMusicPhase", boss_phase);
    }
}

//Audio TODO:
/* remove debug audio UI
 * add volume slider
 * set BGM players for each boss
 * set BGM phase triggers
 */