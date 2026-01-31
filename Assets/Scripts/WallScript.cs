
using UnityEngine;

public class WallScript : MonoBehaviour {

	#region properties
	public GameObject prefabBad1;
	public GameObject prefabBad2;
	public GameObject prefabSafe;
	public GameObject prefabBad1ice;
	public GameObject prefabBad2ice;
	public GameObject prefabSafeice;
	public bool isIce = false;
	#endregion

	public void Start() {
		for (int i = 0; i < 4; i++)
			AddRow(i);
	}

	public void SetIce() {
		isIce = true;
	}

    public void AddRow(int height=-1) {
		// find player
		var player = GameObject.Find("Player");
		Vector3 playerPos = player.transform.position;
		// create parent row
		var parentRow = new GameObject();
		float yPos;
		if (height == -1) {
			yPos = playerPos.y + 2;
		} else {
			yPos = (float)height;
		}
		if (yPos >= 50 && !isIce)
			SetIce();
		parentRow.name = "row" + yPos.ToString();
		parentRow.transform.position = new Vector3(0,yPos,0);
		for (int i = -3; i <= 3; i++) {
			//choose a prefab
			GameObject pref;
			if (yPos != 0) {
				pref = RandomPrefab();
			} else {
				pref = isIce ? prefabSafeice : prefabSafe;
			}
			Instantiate(pref,new Vector3(i,yPos,0),Quaternion.Euler(RandomEuler()),parentRow.transform);
		}
    }

	private GameObject RandomPrefab() {
		double randNormal = RandomNormal();
		if (randNormal >= 0.66) {
			if (isIce) {
				return prefabBad2ice;
			} else {
				return prefabBad2;
			}
		} else if (randNormal <= -0.66) {
			if (isIce) {
				return prefabBad1ice;
			} else {
				return prefabBad1;
			}
		} else {
			if (isIce) {
				return prefabSafeice;
			} else {
				return prefabSafe;
			}
		}
	}

	private double RandomNormal() {
		// Source - https://stackoverflow.com/a/218600␍
		// Posted by yoyoyoyosef, modified by community. See post 'Timeline' for change history␍
		// Retrieved 2026-01-31, License - CC BY-SA 3.0␍
		System.Random rand = new System.Random(); //reuse this if you are generating many
		double u1 = 1.0-rand.NextDouble(); //uniform(0,1] random doubles
		double u2 = 1.0-rand.NextDouble();
		double randStdNormal = System.Math.Sqrt(-2.0 * System.Math.Log(u1)) *
			System.Math.Sin(2.0 * System.Math.PI * u2); //random normal(0,1)
		return randStdNormal;
	}

	private Vector3 RandomEuler() {
		int zRot = UnityEngine.Random.Range(0,3);
		return new Vector3(0,0,zRot * 90);
	}
}
