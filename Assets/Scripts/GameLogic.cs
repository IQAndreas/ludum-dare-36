using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour {

	public static GameLogic instance;

	public bool waiting = true;
	public bool playing = false;
	public string exitKey = "escape";
	
	public GameObject sign;
	// How many seconds to wait before destroying objects (gives the camera time to pan up or down and not see them disappear)
	public float cooldownTime = 6.0f;
	public float soundDelay = 2.0f;
	
	public float lockInputUntil;
	
	//public Transform menuCamera;
	//public Transform playerCamera;
	public Camera menuCamera;
	public Camera playerCamera;
	public Transform playerCameraLocation;
	
	public string MAIN_CAMERA_TAG = "MainCamera";
	public string DISABLED_CAMERA_TAG = "DisabledCamera";

	public GameObject LevelPickupsPrefab;
	//public Transform levelPickupsLocation;
	public Transform levelPickupsContainer;
	
	public GameObject PlayerPrefab;
	//public Transform playerLocation;
	public Transform playerContainer;
	public Collider playerBounds;
	
	private float startTime;
	
	private PlayerLogic player = null;
	private GameObject playerGO;
	private GameObject levelPickupsGO;
	
	public int startLives = 3;
	
	public int numPickups;
	public int collectedPickups = 0;
	
	
	// ------- sounds --------------
	private DialogSounds dialogSounds;
	public AudioClip audio_intro;
	public AudioClip audio_startGame;
	public AudioClip audio_tryAgain1;
	public AudioClip audio_tryAgain2;
	public AudioClip audio_youWin;
	public AudioClip audio_youLose;
	

	public GameLogic() {
		if (instance) {
			Debug.LogError("For some bizarre reason, there is more than one game logic! Fix it!");
		} else {
			instance = this;
		}
	}
	
	// Use this for initialization
	void Start () {
		
		this.dialogSounds = this.gameObject.AddComponent<DialogSounds>();
		this.dialogSounds.Play(audio_intro);
		
		this.MainMenu();
		
		
	}
	
	private void MainMenu() {
	
		if (this.playing) {
			Destroy(playerGO);
			Destroy(levelPickupsGO);
			player = null;
		}
	
		this.playing = false;
		this.waiting = false;
		
		menuCamera.tag = MAIN_CAMERA_TAG;
		menuCamera.enabled = true;
		playerCamera.tag = DISABLED_CAMERA_TAG;
		playerCamera.enabled = false;
	}
	
	private void InitGame() {
		this.playing = true;
		this.waiting = true;
		
		this.dialogSounds.Play(audio_startGame);
		
		Component[] rotators = sign.GetComponentsInChildren<Rotator>( );
		foreach( Rotator rotator in rotators ) {
			rotator.running = true;
		}
		
		Component[] audioSources = sign.GetComponentsInChildren<AudioSource>( );
		foreach( AudioSource audioSource in audioSources ) {
			audioSource.PlayDelayed(soundDelay);
		}
		
		// ---- PLAYER ----
		playerGO = Instantiate(PlayerPrefab, playerContainer, false) as GameObject;
		
		player = playerGO.GetComponent<PlayerLogic>();
		player.startLocation = playerContainer.transform;
		player.bounds = playerBounds;
		player.lives = startLives;
		
		playerCamera.transform.position = playerCameraLocation.position;
		playerCamera.transform.rotation = playerCameraLocation.rotation;
		FollowTarget cameraFollowTarget = playerCamera.GetComponent<FollowTarget>() as FollowTarget;
		cameraFollowTarget.SetTarget(playerGO, 0.5f);
		
		// ---- PICKUPS ----
		levelPickupsGO = Instantiate(LevelPickupsPrefab, levelPickupsContainer, false) as GameObject;
		Component[] pickups = levelPickupsGO.GetComponentsInChildren<Pickup>( );
		this.numPickups = pickups.Length;
		this.collectedPickups = 0;
		
		// Don't start immediately. Wait a couple seconds for the song to play.
		this.waiting = true;
		this.startTime = Time.time + cooldownTime;
		
	}
	
	private void PlayGame() {
		this.waiting = false;
		
		menuCamera.tag = MAIN_CAMERA_TAG;
		menuCamera.enabled = false;
		playerCamera.tag = DISABLED_CAMERA_TAG;
		playerCamera.enabled = true;
		
		player.ResetPlayer();
	}
	
	
	void Update () {
	
		if (Input.GetKeyDown(exitKey)) {
			if (this.playing) {
				this.MainMenu();
			}
			else {
				// At the main menu. Goodbye.
				Application.Quit();
			}
			
			// No other keys allowed
			return;
		}
		
		// Only key allowed is escape
		if (Time.time < this.lockInputUntil) return;
		
		// Any OTHER key that is
		if (Input.anyKeyDown && !this.playing) {
			this.InitGame();
		}
		
		if (this.waiting) {
			//Debug.Log(this.startTime - Time.time);
			if (Time.time > this.startTime) {
				this.PlayGame();
				return;
			}
		}
		else if (this.playing) {
			
			if (player.pickupCount >= this.numPickups) {
				this.PlayerHasWon();
				return;
			}
			
			if (player.outOfBounds) {
				Debug.Log("Out of bounds");
				player.outOfBounds = false; // Reset flag
				player.lives--;
				
				if (player.lives <= 0) {
					this.PlayerHasDied();
					return;
				}
				else {
					this.ResetPlayer();
					
					if      (player.lives == 2) { this.dialogSounds.Play(audio_tryAgain1); }
					else if (player.lives == 1) { this.dialogSounds.Play(audio_tryAgain2); }
					
					return;
				}
			}
			
			if (player.hitEnemy) {
				player.hitEnemy = false; // Reset flag
				Debug.Log("Hit enemy");
				player.lives--;
				
				if (player.lives <= 0) {
					this.PlayerHasDied();
					return;
				}
				else {
					this.ResetPlayer();
					return;
				}
			}
		}
		
		
	}
	
	
	void ResetPlayer() {
		player.ResetPlayer();
	}
	
	void PlayerHasWon() {
		Debug.Log("You win!");
		this.lockInputUntil = Time.time + cooldownTime;
		this.dialogSounds.Play(audio_youWin);
		this.MainMenu();
	}
	
	void PlayerHasDied() {
		Debug.Log("You lose...");
		this.lockInputUntil = Time.time + cooldownTime;
		this.dialogSounds.Play(audio_youLose);
		this.MainMenu();
	}
	
	
	
}










