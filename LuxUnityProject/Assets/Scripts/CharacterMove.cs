using UnityEngine;
using System.Collections;

public class CharacterMove : MonoBehaviour {
    public float Speed;
    public float JumpPower;
    public float FootSensorHeight;

    BoxCollider2D mySensor, footSensor;
    int jumpEnabled = 0;
	// Use this for initialization
	void Start () {
        mySensor = transform.GetComponent<BoxCollider2D>();
        Vector2 mySize = mySensor.size;
        Vector2 footSize = new Vector2(mySize.x, mySize.y * FootSensorHeight);
        footSensor = gameObject.AddComponent<BoxCollider2D>();
        footSensor.size = footSize;
        footSensor.offset = new Vector2(0, mySize.y * -0.5f);
        footSensor.isTrigger = true;
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
        if (Input.GetButtonDown("Jump") && jumpEnabled > 0)
        {
            transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, JumpPower), ForceMode2D.Impulse);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (footSensor.IsTouching(other) && other.gameObject.layer == 8) jumpEnabled++;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!footSensor.IsTouching(other) && other.gameObject.layer == 8) jumpEnabled--;
    }
}
