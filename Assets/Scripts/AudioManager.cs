using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSounds, sfxSounds, footsteps, background;
    public AudioSource musicSource, sfxSource, footstepSource, backgroundSource;

    private void Awake(){
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    private void Start(){
        PlayMusic("LiftoffTheme");
        PlayBackground("ForestBirds");
    }

    public void PlayMusic(string name){
    Sound sound = Array.Find(musicSounds, x => x.name == name);
    if(sound == null){
        Debug.Log("Song not found.");
        }
    else {
        musicSource.clip = sound.clip;
        musicSource.Play();
        }
    }
    public void PlaySFX(string name){
    Sound sound = Array.Find(sfxSounds, x => x.name == name);
    if(sound == null){
        Debug.Log("Sound not found.");
        }
    else {
        sfxSource.PlayOneShot(sound.clip);
        }

    }
    public void PlayFootstep(string name){
    Sound sound = Array.Find(footsteps, x => x.name == name);
    if(sound == null){
        Debug.Log("Footstep not found.");
        }
    else {
        footstepSource.PlayOneShot(sound.clip);
        }

    }
    public void PlayBackground(string name){
    Sound sound = Array.Find(background, x => x.name == name);
    if(sound == null){
        Debug.Log("Background Sound not found.");
        }
    else {
        backgroundSource.clip = sound.clip;
        backgroundSource.Play();
        }
    }

    public int getFootstepNumber(){
    return UnityEngine.Random.Range(0, footsteps.Length-1);
}
    public void ToggleMusic(){
        musicSource.mute = !musicSource.mute;
    }
    public void ToggleSFX(){
        sfxSource.mute = !sfxSource.mute;
        footstepSource.mute=!footstepSource.mute;
        backgroundSource.mute = !backgroundSource.mute;
    }
    public void MusicVolume(float volume){
        musicSource.volume = volume;
    }
    public void SFXVolume(float volume){
        sfxSource.volume=volume;
        footstepSource.volume=volume;
        backgroundSource.volume = volume;
    }
    public void ChangeMusicPitch(float pitch){
    musicSource.pitch = pitch;
    }

}
