/// Character Control: Creates a platformer character
/// By Saeed Afshari (2015)
/// 
using UnityEngine;
using System.Collections.Generic;

public class CharacterMove : MonoBehaviour {
    public float MoveForce;
    public Vector2 MaxSpeed;
    public float JumpPower;
    public float FootSensorWidth = 0.8f;
    public float FootSensorHeight;
    public GameObject DebugText;

    HashSet<object> platforms = new HashSet<object>();
    BoxCollider2D mySensor, footSensor;
    bool jumpEnabled { get { return platforms.Count > 0; } }

	// Use this for initialization
	void Start()
    {
        mySensor = transform.GetComponent<BoxCollider2D>();
        Vector2 mySize = mySensor.size;
        Vector2 footSize = new Vector2(mySize.x * FootSensorWidth, mySize.y * FootSensorHeight);
        footSensor = gameObject.AddComponent<BoxCollider2D>();
        footSensor.size = footSize;
        footSensor.offset = new Vector2(0.5f - (mySize.x * FootSensorWidth) * 0.5f, mySize.y * -FootSensorHeight);
        footSensor.isTrigger = true;
	}

    // Update is called once per frame
    void Update()
    {        
        Vector2 velocity = transform.GetComponent<Rigidbody2D>().velocity;

        if (Input.GetAxis("Horizontal") < -0.5)
        {
            //transform.Translate(Vector3.left * Time.deltaTime * Speed);
            transform.GetComponent<Rigidbody2D>().AddForce(Vector3.left * Time.deltaTime * MoveForce, ForceMode2D.Force);
            //velocity.x -= Time.deltaTime * MoveForce;
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
            GetComponent<Animator>().Play("Moving");
        }
        else if (Input.GetAxis("Horizontal") > 0.5)
        {
            //transform.Translate(Vector3.right * Time.deltaTime * Speed);
            transform.GetComponent<Rigidbody2D>().AddForce(Vector3.right * Time.deltaTime * MoveForce, ForceMode2D.Force);
            //velocity.x += Time.deltaTime * MoveForce;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
            GetComponent<Animator>().Play("Moving");
        }
        else
        {
            GetComponent<Animator>().Play("Idle");
        }


        if (velocity.x < -MaxSpeed.x) velocity.x = -MaxSpeed.x;
        else if (velocity.x > MaxSpeed.x) velocity.x = MaxSpeed.x;
        if (velocity.y < -MaxSpeed.y) velocity.y = -MaxSpeed.y;
        else if (velocity.y > MaxSpeed.y) velocity.y = MaxSpeed.y;

        transform.GetComponent<Rigidbody2D>().velocity = velocity;

        if (Input.GetButtonDown("Jump") && jumpEnabled)
        {
            float impulse = GetComponent<Rigidbody2D>().gravityScale * JumpPower;
            transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, impulse), ForceMode2D.Impulse);
        }
        
        if (Input.GetButtonDown("Fire2"))
        {
            //Only allow gravity reverse when player is standing
            //but if player is backwards, allow gravity fix at any point.
            if ((transform.localScale.y > 0 && jumpEnabled) || transform.localScale.y < 0)
            {
                transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, 1);
                //Physics2D.gravity *= -1.0f;
                GetComponent<Rigidbody2D>().gravityScale *= -1.0f;
            }
        }
        
        if (jumpEnabled)
        {
            if (velocity.y > 0) GetComponent<Animator>().Play("JumpUp");
            else if (velocity.y < 0) GetComponent<Animator>().Play("JumpDown");
        }
        
        if (DebugText != null)
        {
            DebugText.GetComponent<UnityEngine.UI.Text>().text = jumpEnabled.ToString();
                //transform.GetComponent<Rigidbody2D>().velocity.ToString();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (footSensor.IsTouching(other) && other.gameObject.layer == 8)
        {
            platforms.Add(other);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        List<object> platformsToRemove = new List<object>(platforms.Count);
        foreach (var platform in platforms)
        {
            if (!footSensor.IsTouching((Collider2D)platform)) platformsToRemove.Add(platform);
        }
        foreach (var platform in platformsToRemove)
            platforms.Remove(platform);
    }
}
