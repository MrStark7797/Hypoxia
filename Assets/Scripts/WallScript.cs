using System;
using UnityEngine;

public class WallScript : MonoBehaviour {

	#region properties
	public GameObject prefabGrass;
	public GameObject prefabBad1;
	public GameObject prefabBad2;
	public GameObject prefabSafe;
	public GameObject prefabBad1ice;
	public GameObject prefabBad2ice;
	public GameObject prefabSafeice;
    public GameObject prefabOxygen;
	public bool isIce = false;
	#endregion

	public void Start() {
		for (int i = 0; i < 4; i++)
			AddRow(i);
		DrawGrass();
	}

	public void SetIce() {
		isIce = true;
	}

    public void DrawGrass() {
		var parentRow = new GameObject();
		parentRow.name = "rowGrass";
		parentRow.transform.position = new Vector3(0,0,0);
		GameObject pref = prefabGrass;
		for (int i = -3; i <= 3; i++) {
			Debug.Log("creating at (" + i.ToString() + ",-1,0)");
			Instantiate(pref,new Vector3(i,-1,0),Quaternion.identity,parentRow.transform);
			Instantiate(pref,new Vector3(i,-2,0),Quaternion.identity,parentRow.transform);
		}
	}

    public void AddRow(int height=-1) {
		var player = GameObject.Find("Player");
		Vector3 playerPos = player.transform.position;

		var parentRow = new GameObject();
		float yPos;
		if (height == -1) {
			yPos = playerPos.y + 2;
		} else {
			yPos = (float)height;
		}

		if (yPos >= 50 && !isIce)
			SetIce();
		if (yPos >= 100) {
			GeneratePeak(yPos+1);
			player.GetComponent<PlayerScript>().ReachPeak(yPos);
		}
		parentRow.name = "row" + yPos.ToString();
		parentRow.transform.position = new Vector3(0,yPos,0);
        for (int i = -3; i <= 3; i++) {
            GameObject pref;
            if (yPos != 0) {
                pref = RandomPrefab(i, yPos, parentRow.transform);
            } else {
                pref = isIce ? prefabSafeice : prefabSafe;
            }
            Instantiate(pref, new Vector3(i, yPos, 0), Quaternion.Euler(RandomEuler()), parentRow.transform);
        }
    }

	private GameObject RandomPrefab(float x, float y, Transform parent) {
        double randNormal = RandomNormal();
        GameObject tileToReturn;
        if (randNormal >= 0.66) {
            tileToReturn = isIce ? prefabBad2ice : prefabBad2;
        } else if (randNormal <= -0.66) {
            tileToReturn = isIce ? prefabBad1ice : prefabBad1;
        } else {
            tileToReturn = isIce ? prefabSafeice : prefabSafe;
        }
        GameObject spawnedTile = Instantiate(tileToReturn, new Vector3(x, y, 0), Quaternion.Euler(RandomEuler()), parent);
        if (System.Math.Abs(randNormal) <= 0.12566) {
            if (prefabOxygen != null) {
                GameObject o2 = Instantiate(prefabOxygen, new Vector3(x, y, -0.1f), Quaternion.identity, spawnedTile.transform);
                o2.name = "Oxygen_Unlooted";
            }
        }
        return spawnedTile;
    }

	private void GeneratePeak(float yPos)
	{
		//Generate sky and peak...
		for (float y = yPos; y <= yPos + 1f; y += 1f) {
            GameObject parentRow = new GameObject();
            parentRow.name = "row" + yPos.ToString();
            parentRow.transform.position = new Vector3(0, yPos, 0);
            for (int i = -3; i <= 3; i++) {
				GameObject pref = isIce ? prefabSafeice : prefabSafe;
				Instantiate(pref, new Vector3(i, y, 0), Quaternion.Euler(RandomEuler()), parentRow.transform);
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
