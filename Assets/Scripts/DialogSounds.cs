using UnityEngine;
using System.Collections.Generic;

public class DialogSounds : MonoBehaviour {

	private AudioSource currentAudioSource;
	private Dictionary<AudioClip, AudioSource> audioSources;
	
	public AudioSource AddAudio(AudioClip clip, float vol) { 
		AudioSource audioSource = this.gameObject.AddComponent<AudioSource>();
		audioSource.clip = clip; 
		audioSource.loop = false;
		audioSource.playOnAwake = false;
		audioSource.volume = vol; 
		
		audioSources[clip] = audioSource;
		return audioSource; 
	}

	public void Play(AudioClip clip) {
	
		if (clip == null) {
			Debug.LogError("DialogSounds: Cannot play a clip that doesn't exist, dummy!");
		}
		
		if (audioSources == null) {
			audioSources = new Dictionary<AudioClip, AudioSource>();
		}
		
		if (!audioSources.ContainsKey(clip)) {
			this.AddAudio(clip, 1.0f);
		}
		
		if (currentAudioSource != null) {
			currentAudioSource.Stop();
		}
		
		currentAudioSource = audioSources[clip];
		currentAudioSource.Play();
	}
}
