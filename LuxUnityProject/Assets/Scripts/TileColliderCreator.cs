using UnityEngine;
using System.Collections;

public class TileColliderCreator : MonoBehaviour {
    public PhysicsMaterial2D SidePhysics = null;
    public PhysicsMaterial2D TopPhysics = null;
    public bool BottomHasSideMaterial = true;
    public Vector2 sideOffset = new Vector2(0.008f, 0.0f);
	// Use this for initialization
	void Start () {
        var sprite = GetComponent<SpriteRenderer>();
        var bounds = sprite.bounds;

        var center = transform.InverseTransformPoint(bounds.center);
        var size = //transform.InverseTransformPoint
            (bounds.size);

        var left = center.x - size.x * 0.5f;
        var right = center.x + size.x * 0.5f;
        var bottom = center.y - size.y * 0.5f;
        var top = center.y + size.y * 0.5f;

        var leftE = gameObject.AddComponent<EdgeCollider2D>();
        var rightE = gameObject.AddComponent<EdgeCollider2D>();
        var topE = gameObject.AddComponent<EdgeCollider2D>();
        var bottomE = gameObject.AddComponent<EdgeCollider2D>();

        leftE.sharedMaterial = SidePhysics;
        rightE.sharedMaterial = SidePhysics;
        if (BottomHasSideMaterial) bottomE.sharedMaterial = SidePhysics;
        else bottomE.sharedMaterial = TopPhysics;
        topE.sharedMaterial = TopPhysics;

        leftE.points = new Vector2[] { new Vector2(left - size.x * sideOffset.x, bottom), new Vector2(left - size.x * sideOffset.x, top) };
        rightE.points = new Vector2[] { new Vector2(right + size.x * sideOffset.x, bottom), new Vector2(right + size.x * sideOffset.x, top) };
        bottomE.points = new Vector2[] { new Vector2(left, bottom - size.y * sideOffset.y), new Vector2(right, bottom - size.y * sideOffset.y) };
        topE.points = new Vector2[] { new Vector2(left, top + size.y * sideOffset.y), new Vector2(right, top + size.y * sideOffset.y) };
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
