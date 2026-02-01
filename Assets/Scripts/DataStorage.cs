using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class DataStorage : MonoBehaviour
{
    public bool endlessMode = false;
    public TextAsset standardScores;
    public TextAsset endlessScores;
    public float finalTime;
    public int height;

    private void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("Data").Length > 1)
        {
            Destroy(gameObject);
        }

        finalTime = 0f;
        height = 0;

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

        string[] lines = File.ReadAllLines("standardScores.txt");
       
        for (int i = 0; i < 10; i++ )
        {
            string name = "None";
            float timeFloat = Mathf.Infinity;

            if (i < lines.Length)
            {
                if (lines[i] != "")
                {
                    name = lines[i].Split(",")[0];
                    timeFloat = (float)Convert.ToDouble(lines[i].Split(",")[1]);
                }
            }
            

            t[i] = (name, timeFloat);
        }

        return t;
    }

    public void WriteStandardScores(string name)
    {

        (string, float)[] currentLeaderboard = ReadStandardScores();

        string finalString = "";

        if (finalTime < currentLeaderboard[9].Item2)
        {
            currentLeaderboard[9] = (name, finalTime);
            currentLeaderboard = currentLeaderboard.OrderByDescending(x => x.Item2).Reverse().ToArray();
            foreach ((string, float) entry in currentLeaderboard)
            {
                finalString += entry.Item1 + "," + entry.Item2.ToString("0.00") + "\n";
            }
        }

        File.WriteAllText("standardScores.txt", finalString);
    }


    public string DisplayEndlessScores()
    {
        (string, int)[] scores = ReadEndlessScores();

        string r = "";

        for (int i = 0; i < 10; i++)
        {
            (string, int) s = scores[i];
            if (s.Item2 != 0)
            {
                int heightVal = s.Item2;
                r += (i + 1).ToString() + ". " + s.Item1.PadRight(10) + "| " + heightVal.ToString() + "\n";
            }
        }

        return r;
    }

    public (string, int)[] ReadEndlessScores()
    {
        (string, int)[] t = new (string, int)[10];

        string[] lines = File.ReadAllLines("endlessScores.txt");

        for (int i = 0; i < 10; i++)
        {
            string name = "None";
            int heightVal = 0;

            if (i < lines.Length)
            {
                if (lines[i] != "")
                {
                    name = lines[i].Split(",")[0];
                    heightVal = (int)Convert.ToDouble(lines[i].Split(",")[1]);
                }
            }


            t[i] = (name, heightVal);
        }

        return t;
    }

    public void WriteEndlessScores(string name)
    {

        (string, int)[] currentLeaderboard = ReadEndlessScores();

        string finalString = "";

        if (finalTime < currentLeaderboard[9].Item2)
        {
            currentLeaderboard[9] = (name, height);
            currentLeaderboard = currentLeaderboard.OrderByDescending(x => x.Item2).ToArray();
            foreach ((string, int) entry in currentLeaderboard)
            {
                finalString += entry.Item1 + "," + entry.Item2.ToString() + "\n";
            }
        }

        File.WriteAllText("endlessScores.txt", finalString);
    }

}
