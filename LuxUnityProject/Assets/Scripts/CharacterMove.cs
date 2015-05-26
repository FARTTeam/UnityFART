using UnityEngine;
using System.Collections;

public class CharacterMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update() {
        if (Input.GetAxis("Horizontal") < -0.5)
        {
            gameObject.transform.Translate(Vector3.left * Time.deltaTime * 2.0f);
            gameObject.transform.localScale = new Vector3(-1, 1);
        }
        else if (Input.GetAxis("Horizontal") > 0.5)
        {
            gameObject.transform.Translate(Vector3.right * Time.deltaTime * 2.0f);
            gameObject.transform.localScale = new Vector3(1,1);
        }
    }
}
