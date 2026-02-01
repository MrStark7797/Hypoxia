using UnityEngine;

public class RopeScript : MonoBehaviour {
	#region properties
	private LineRenderer lineRenderer;
	#endregion

	public void Start() {
		lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.startWidth = 0.2f;
		lineRenderer.endWidth = 0.2f;
		lineRenderer.positionCount = 2;
		MoveLineRender();
	}
	public void Update() {
		MoveLineRender();
	}

	#region LineRenderer
	private void MoveLineRender() {
		Vector3 perryPos = transform.position;
		Vector3 checkpointPos = gameObject.GetComponent<PlayerScript>().currentCheckpoint;
		lineRenderer.SetPosition(0, perryPos);
		lineRenderer.SetPosition(1, checkpointPos);
	}

	//private void ColourLineRender() {}
	#endregion
