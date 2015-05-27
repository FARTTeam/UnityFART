using UnityEngine;
using System.Collections;

public class CharacterMove : MonoBehaviour {
    public float Speed;
    public float JumpPower;
	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update() {
        if (Input.GetAxis("Horizontal") < -0.5)
        {
            transform.Translate(Vector3.left * Time.deltaTime * Speed);
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
        else if (Input.GetAxis("Horizontal") > 0.5)
        {
            transform.Translate(Vector3.right * Time.deltaTime * Speed);
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x),transform.localScale.y);
        }
        else if (Input.GetButton("Jump"))
        {
            transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, JumpPower), ForceMode2D.Impulse);
        }
    }
}
