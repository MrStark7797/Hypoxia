using UnityEditor.Tilemaps;
using UnityEngine;

public class WallScript : MonoBehaviour {

	#region properties
	public GameObject prefabBad1;
	public GameObject prefabBad2;
	public GameObject prefabSafe;
	#endregion

	public void Start() {
		for (int i = 0; i < 4; i++)
			AddRow();
	}

    public void AddRow() {
		// find player
		var player = GameObject.Find("Player");
		Vector3 playerPos = player.transform.position;
		// create parent row
		var parentRow = new GameObject();
		parentRow.name = "row" + playerPos.y.ToString();
		for (int i = -3; i < =3; i++) {
			//choose a prefab
			var pref = RandomPrefab();
			Instantiate(pref,parentRow);
		}
    }

	private int RandomPrefab() {
		double randNormal = RandomNormal();
		if (randNormal >= 0.66) {
			return prefabBad2;
		} else if (randNormal <= 0.33) {
			return prefabBad1;
		} else {
			return prefabSafe;
		}
	}

	private double RandomNormal() {
		// Source - https://stackoverflow.com/a/218600␍
		// Posted by yoyoyoyosef, modified by community. See post 'Timeline' for change history␍
		// Retrieved 2026-01-31, License - CC BY-SA 3.0␍
		Random rand = new Random(); //reuse this if you are generating many
		double u1 = 1.0-rand.NextDouble(); //uniform(0,1] random doubles
		double u2 = 1.0-rand.NextDouble();
		double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
			Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
		return randStdNormal;
	}
}
