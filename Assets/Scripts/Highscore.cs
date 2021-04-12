using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Highscore : MonoBehaviour
{
    private void Awake()
    {
    DontDestroyOnLoad(gameObject);
    }

    public int score = 0;

    public static void SaveFile(int currentScore)
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);

        Highscoredata data = new Highscoredata(currentScore);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }

    public static int LoadFile()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            Debug.LogError("File not found");
            return 0;
        }

        BinaryFormatter bf = new BinaryFormatter();
        Highscoredata data = (Highscoredata)bf.Deserialize(file);
        file.Close();

        return data.highscore;
    }
}