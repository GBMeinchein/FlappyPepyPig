using UnityEngine;
using System.Collections;

public class ObstaclesBehaviour : MonoBehaviour {

	public float speed;

	private GameController gameController;

	private bool passed;

	// Use this for initialization
	void Start () {
		gameController = FindObjectOfType(typeof(GameController)) as GameController;
	
	}

	void OnEnable(){
		passed = false;
	}
	
	// Update is called once per frame
	void Update () {

		if(gameController.GetCurrentState() != GameStates.INGAME)
			return;


		transform.position += new Vector3(speed,0,0)*Time.deltaTime;
        //transform.position = new Vector3(transform.position.x, transform.position.y - speed * Random.value, transform.position.z);

        if (transform.position.x < -20){
			gameObject.SetActive(false);
		}

		if(transform.position.x < gameController.player.position.x && !passed){
			passed = true;
			gameController.AddScore();
		}

	
	}
}
