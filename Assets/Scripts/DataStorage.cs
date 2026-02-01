using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class DataStorage : MonoBehaviour
{
    public bool endlessMode = false;
    public TextAsset standardScores;
    public TextAsset endlessScores;
    public float finalTime;

    private void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("Data").Length > 1)
        {
            Destroy(gameObject);
        }

        finalTime = 0f;

        DontDestroyOnLoad(this);
    }


    public string DisplayStandardScores()
    {
        (string, float)[] scores = ReadStandardScores();

        string r = "";

        for (int i = 0; i < 10; i++) 
        { 
            (string, float) s = scores[i];
            if (s.Item2 != Mathf.Infinity)
            {
                float time = s.Item2;
                r += (i+1).ToString() + ". " + s.Item1.PadRight(10) + "| " + ((int)time / 60).ToString() + ":" + (time % 60).ToString("00.00") + "\n";
            }
        }

        return r;
    }

    public (string, float) [] ReadStandardScores()
    {
        (string, float)[] t = new (string, float)[10];

        StreamReader reader = new StreamReader("Assets/StandardScores.txt");

        string[] lines = reader.ReadToEnd().Split("\n");

        reader.Close();
       
        for (int i = 0; i < 10; i++ )
        {
            string name = "None";
            float timeFloat = Mathf.Infinity;

            if (i < lines.Length)
            {
                name = lines[i].Split(",")[0];
                timeFloat = (float)Convert.ToDouble(lines[i].Split(",")[1]);
            }
            

            t[i] = (name, timeFloat);
        }

        return t;
    }

    public void WriteStandardScores(string name)
    {

        (string, float)[] currentLeaderboard = ReadStandardScores();

        if (finalTime < currentLeaderboard[9].Item2)
        {
            currentLeaderboard[9] = (name, finalTime);
            currentLeaderboard = currentLeaderboard.OrderByDescending(x => x.Item2).Reverse().ToArray();
            StreamWriter writer = new StreamWriter("Assets/StandardScores.txt", false);
            foreach ((string, float) entry in currentLeaderboard)
            {
                writer.WriteLine(entry.Item1 + "," + entry.Item2.ToString("0.00"));
            }
            writer.Close();
        }
    }

}
