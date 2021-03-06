﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastController : MonoBehaviour {

	public const float skinWidth = 0.015f; 
	public int horizontalRayCount = 4;
	public int verticalRayCount = 4;

	public float horizontalRaySpacing;
	public float verticalRaySpacing;

	public BoxCollider2D collider;
	public RaycastOrigins raycastOrigins;
	public CollisionInfo collisions;

	public virtual void Start () {
		collider = GetComponent<BoxCollider2D>(); 
		CalculateRaySpacing();
	}

	public void UpdateRaycastOrigins() {
		Bounds bounds = collider.bounds; 
		bounds.Expand(skinWidth * -2);
		raycastOrigins.topLeft     = new Vector2(bounds.min.x, bounds.max.y);
		raycastOrigins.topRight    = new Vector2(bounds.max.x, bounds.max.y);
		raycastOrigins.bottomLeft  = new Vector2(bounds.min.x, bounds.min.y);
		raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
	}

	public void CalculateRaySpacing() {
		Bounds bounds = collider.bounds; 
		bounds.Expand(skinWidth * -2);

		horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
		verticalRayCount   = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

		horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
		verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
	}

	public struct CollisionInfo {
		public bool above, below; 
		public bool left, right;
		public bool climbingSlope;
		public bool descendingSlope;
		public float slopeAngle, slopeAnglePrev;
		public Vector3 velocityPrev;
		public void Reset() {
			above = below = left = right = climbingSlope = descendingSlope = false;
			slopeAnglePrev = slopeAngle; 
			slopeAngle = 0;
		}
	}
	
	
	public struct RaycastOrigins {
		public Vector2 topLeft, 
					   topRight, 
					   bottomLeft, 
					   bottomRight; 
	}

}
