using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource AS, Music;
    public AudioClip MenuOpen, MenuConfirm, MenuCancel, MenuChange, MenuClose, MenuClick, 
        MetalImpact2;

    public float maxVol;

    public void PlaySoundClip(int clip)
    {
        switch(clip)
        {
            case 0:
                AS.PlayOneShot(MenuOpen);
                break;
            case 1:
                AS.PlayOneShot(MenuConfirm);
                break;
            case 2:
                AS.PlayOneShot(MenuCancel);
                break;
            case 3:
                AS.PlayOneShot(MenuChange);
                break;
            case 4:
                AS.PlayOneShot(MenuClose);
                break;
            case 5:
                AS.PlayOneShot(MenuClick);
                break;
            case 6:
                AS.PlayOneShot(MetalImpact2);
                break;
        }
    }

    public void PlaySpecificClip(AudioClip ac)
    {
        AS.PlayOneShot(ac);
    }

    public void ChangeMusic(AudioClip ac)
    {
        Music.Stop();
        Music.clip = ac;
        Music.PlayDelayed(0.5f);
    }

    public void BattleTransition(AudioClip intro, AudioClip loop)
    {
        Music.Stop();
        Music.PlayOneShot(intro);
        Music.clip = loop;
        Music.PlayDelayed(intro.length - 0.25f);
    }
}
