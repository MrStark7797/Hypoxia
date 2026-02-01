using UnityEngine;

public class RopeScript : MonoBehaviour {
	#region properties
	private LineRenderer lineRenderer;
	#endregion

	public void Start() {
		lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.startWidth = 0.2f;
		lineRenderer.endWidth = 0.2f;
		lineRenderer.positionCount = 3;
		MoveLineRender();
	}
	public void Update() {
		MoveLineRender();
	}

	#region LineRenderer
	private void MoveLineRender() {
		Vector3 perryPos = transform.position;
		Vector3 checkpointPos = gameObject.GetComponent<PlayerScript>().currentCheckpoint;
		Vector3 lastCheckpointPos = gameObject.GetComponent<PlayerScript>().lastCheckpoint;
		lineRenderer.SetPosition(0, perryPos);
		lineRenderer.SetPosition(1, checkpointPos);
		lineRenderer.SetPosition(2, lastCheckpointPos);
	}

	//private void ColourLineRender() {}
	#endregion
