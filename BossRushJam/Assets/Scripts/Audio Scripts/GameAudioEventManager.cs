using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioEventManager : MonoBehaviour
{
    private float bgm_volume = 1.0f;
    private bool bgm_paused = false;

    //MUSIC EVENTS
    private string menu_bgm_path = "event:/MUSIC/mus_graveyard_ambient_loop";
    private string zombie_bgm_path = "event:/MUSIC/mus_zombie_bgm";
    private string skeleton_bgm_path = "event:/MUSIC/mus_skeleton_bgm";
    private string necromancer_bgm_path = "event:/MUSIC/mus_necromancer_bgm";
    private FMOD.Studio.EventInstance menu_bgm;
    private FMOD.Studio.EventInstance zombie_bgm;
    private FMOD.Studio.EventInstance skeleton_bgm;
    private FMOD.Studio.EventInstance necromancer_bgm;
    private FMOD.Studio.EventInstance curr_bgm;

    //SFX EVENTS
    private string sfx_button_click_path = "event:/SFX/sfx_button_click";
    private string sfx_change_equipped_item_path = "event:/SFX/sfx_change_equipped_item";
    private string sfx_enemy_damaged_path = "event:/SFX/sfx_enemy_damaged";
    private string sfx_enemy_die_path = "event:/SFX/sfx_enemy_die";
    private string sfx_item_broken_path = "event:/SFX/sfx_item_broken";
    private string sfx_item_crafted_path = "event:/SFX/sfx_item_crafted";
    private string sfx_player_damaged_path = "event:/SFX/sfx_player_damaged";
    private string sfx_player_dash_path = "event:/SFX/sfx_player_dash";
    private string sfx_player_weapon_slash_path = "event:/SFX/sfx_player_weapon_slash";
    private string sfx_projectile_collision_path = "event:/SFX/sfx_projectile_collision";
    private string sfx_zombie_bite_path = "event:/SFX/sfx_zombie_bite";
    private string sfx_zombie_smash_wave_path = "event:/SFX/sfx_zombie_smash_wave";
    private string sfx_zombie_tombstone_toss_land_path = "event:/SFX/sfx_zombie_tombstone_toss_land";
    private FMOD.Studio.EventInstance sfx_button_click;
    private FMOD.Studio.EventInstance sfx_change_equipped_item;
    private FMOD.Studio.EventInstance sfx_enemy_damaged;
    private FMOD.Studio.EventInstance sfx_enemy_die;
    private FMOD.Studio.EventInstance sfx_item_broken;
    private FMOD.Studio.EventInstance sfx_item_crafted;
    private FMOD.Studio.EventInstance sfx_player_damaged;
    private FMOD.Studio.EventInstance sfx_player_dash;
    private FMOD.Studio.EventInstance sfx_player_weapon_slash;
    private FMOD.Studio.EventInstance sfx_projectile_collision;
    private FMOD.Studio.EventInstance sfx_zombie_bite;
    private FMOD.Studio.EventInstance sfx_zombie_smash_wave;
    private FMOD.Studio.EventInstance sfx_zombie_tombstone_toss_land;
    
    private enum BOSS_TYPE
    {
        ZOMBIE = 1,
        SKELETON = 2,
        NECROMANCER = 3
    };

    public void Start()
    {
        InitializeAudioInstances();
        curr_bgm.setVolume(bgm_volume);
        bgm_paused = false;
    }

    public void InitializeAudioInstances()
    {
        menu_bgm = FMODUnity.RuntimeManager.CreateInstance(menu_bgm_path);
        zombie_bgm = FMODUnity.RuntimeManager.CreateInstance(zombie_bgm_path);
        skeleton_bgm = FMODUnity.RuntimeManager.CreateInstance(skeleton_bgm_path);
        necromancer_bgm = FMODUnity.RuntimeManager.CreateInstance(necromancer_bgm_path);

        sfx_button_click = FMODUnity.RuntimeManager.CreateInstance(sfx_button_click_path);
        sfx_change_equipped_item = FMODUnity.RuntimeManager.CreateInstance(sfx_change_equipped_item_path); //TODO
        sfx_enemy_die = FMODUnity.RuntimeManager.CreateInstance(sfx_enemy_die_path);
        sfx_item_broken = FMODUnity.RuntimeManager.CreateInstance(sfx_item_broken_path); //TODO
        sfx_item_crafted = FMODUnity.RuntimeManager.CreateInstance(sfx_item_crafted_path); //TODO
        sfx_player_damaged = FMODUnity.RuntimeManager.CreateInstance(sfx_player_damaged_path);
        sfx_player_dash = FMODUnity.RuntimeManager.CreateInstance(sfx_player_dash_path);
        sfx_player_weapon_slash = FMODUnity.RuntimeManager.CreateInstance(sfx_player_weapon_slash_path);
        sfx_projectile_collision = FMODUnity.RuntimeManager.CreateInstance(sfx_projectile_collision_path); //TODO
    }

    //Audio Functions
    public void PlayBackgroundMusicByType(int bgm_type)
    {
        StopBGM();

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

    //SFX Play Functions
    public void PlayButtonClick()
    {
        sfx_button_click.start();
    }

    public void PlayZombieBite()
    {
        sfx_zombie_bite = FMODUnity.RuntimeManager.CreateInstance(sfx_zombie_bite_path);
        sfx_zombie_bite.start();
    }

    public void PlayZombieShockwave()
    {
        sfx_zombie_smash_wave = FMODUnity.RuntimeManager.CreateInstance(sfx_zombie_smash_wave_path);
        sfx_zombie_smash_wave.start();
    }

    public void PlayZombieTomstoneToss()
    {
        sfx_zombie_tombstone_toss_land = FMODUnity.RuntimeManager.CreateInstance(sfx_zombie_tombstone_toss_land_path);
        sfx_zombie_tombstone_toss_land.start();
    }

    public void PlayPlayerDash()
    {
        sfx_player_dash.start();
    }

    public void PlayPlayerWeaponSlash()
    {
        sfx_player_weapon_slash.start();
    }

    public void PlayPlayerDamaged()
    {
        sfx_player_damaged.start();
    }

    public void PlayEnemyDamaged()
    {
        sfx_enemy_damaged = FMODUnity.RuntimeManager.CreateInstance(sfx_enemy_damaged_path);
        sfx_enemy_damaged.start();
    }

    public void PlayEnemyDie()
    {
        sfx_enemy_die.start();
    }

    public void PlayChangeEquippedItem()
    {
        sfx_change_equipped_item.start();
    }

    public void PlayItemBroken()
    {
        sfx_item_broken.start();
    }

    public void PlayItemCrafted()
    {
        sfx_item_crafted.start();
    }

    public void PlayProjectileCollision()
    {
        sfx_projectile_collision.start();
    }
}

//Audio TODO:
/*
 * add Skeleton attacks
 * Add vampire attacks
 * add boss death SFX
 * add player death SFX
 * set BGM players for each boss
 * add win/lose music
 */