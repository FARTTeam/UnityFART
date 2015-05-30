/// Character Control: Creates a platformer character
/// By Saeed Afshari (2015)
/// 
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
    void Update()
    {
        if (Input.GetAxis("Horizontal") < -0.5)
        {
            transform.Translate(Vector3.left * Time.deltaTime * Speed);
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y,1);
            GetComponent<Animator>().Play("Moving");
        }
        else if (Input.GetAxis("Horizontal") > 0.5)
        {
            transform.Translate(Vector3.right * Time.deltaTime * Speed);
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y,1);
            GetComponent<Animator>().Play("Moving");
        }
        else
        {
            GetComponent<Animator>().Play("Idle");
        }
        if (Input.GetButtonDown("Jump") && jumpEnabled > 0)
        {
            float impulse = -Mathf.Sign(Physics2D.gravity.y) * JumpPower;
            transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, impulse), ForceMode2D.Impulse);
        }
        if (Input.GetButtonDown("Fire2"))
        {
            //Only allow gravity reverse when player is standing
            //but if player is backwards, allow gravity fix at any point.
            if ((transform.localScale.y > 0 && jumpEnabled > 0) || transform.localScale.y < 0)
            {
                transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, 1);
                //Physics2D.gravity *= -1.0f;
                GetComponent<Rigidbody2D>().gravityScale *= -1.0f;
            }
        }
        if (jumpEnabled <= 0)
        {
            if (transform.GetComponent<Rigidbody2D>().velocity.y > 0)
                GetComponent<Animator>().Play("JumpUp");
            else if (transform.GetComponent<Rigidbody2D>().velocity.y < 0)
                GetComponent<Animator>().Play("JumpDown");
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
