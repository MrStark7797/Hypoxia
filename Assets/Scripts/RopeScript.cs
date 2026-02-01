using UnityEngine;
using UnityEngine.SceneManagement;

public class RopeScript : MonoBehaviour {
	#region properties
	private LineRenderer lineRenderer;
	public float width = 0.05f;
	private Color color = new Color(142/255f, 76/255f, 29/255f);
	public int ropeHealth = 100;
	#endregion

	public void Start() {
		lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.startWidth = width;
		lineRenderer.endWidth = width;
		lineRenderer.positionCount = 3;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = color;
		lineRenderer.endColor = color;
		MoveLineRender();
	}
	public void Update() {
		MoveLineRender();
	}

	private void MoveLineRender() {
		Vector3 perryPos = transform.position;
		Vector3 checkpointPos = gameObject.GetComponent<PlayerScript>().currentCheckpoint;
		Vector3 lastCheckpointPos = gameObject.GetComponent<PlayerScript>().lastCheckpoint;
		lineRenderer.SetPosition(0, perryPos);
		lineRenderer.SetPosition(1, checkpointPos);
		lineRenderer.SetPosition(2, lastCheckpointPos);
	}

	public void DamageRope() {
		ropeHealth -= (int)(transform.position.y - gameObject.GetComponent<PlayerScript>().currentCheckpoint.y);
		if (ropeHealth <= 0) 
			SceneManager.LoadScene("Defeat");
	}
}
