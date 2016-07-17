using UnityEngine;
using System.Collections;

public class ReplaySystem : MonoBehaviour {

	private const int bufferFrames = 1000;
	private MyKeyFrame[] keyFrames = new MyKeyFrame[bufferFrames];
	private Rigidbody rigidBody;
	private GameManager gameManager;
	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
		gameManager = GameObject.FindObjectOfType<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameManager.recording == true) {
			Record ();
		} else {
			PlayBack ();
		}

	}

	public void PlayBack(){
		rigidBody.isKinematic = true;
		int frame = Time.frameCount % bufferFrames;
		print ("Reading frame" + frame);
		transform.position = keyFrames [frame].position;
		transform.rotation = keyFrames [frame].rotation;

	}
	public void Record ()
	{
		rigidBody.isKinematic = false;
		int frame = Time.frameCount % bufferFrames;
		float time = Time.time;
		print ("Writing frame" + frame);
		keyFrames [frame] = new MyKeyFrame (time, transform.position, transform.rotation);

	}

}
/// <summary>
/// A structure for storing time and position for replays.
/// </summary>
public struct MyKeyFrame{
	public float frameTime;
	public Vector3 position;
	public Quaternion rotation;

	public MyKeyFrame(float time, Vector3 pos, Quaternion rot){
		frameTime = time;
		position = pos;
		rotation = rot;

	}
		
}
