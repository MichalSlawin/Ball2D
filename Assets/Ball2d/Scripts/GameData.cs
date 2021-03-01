using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class GameData
{
    private const string filename = "/gameData.dat";
    private const int levelsCount = 9;
    private static FileData fileData = new FileData();
    
    public static FileData FileData { get => fileData; set => fileData = value; }
    
    public static int GetLevelsCount()
    {
        return levelsCount;
    }

    public static void SaveFile()
    {
        string destination = Application.persistentDataPath + filename;
        FileStream file;

        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, fileData);
        file.Close();
    }

    public static FileData LoadFile()
    {
        string destination = Application.persistentDataPath + filename;
        FileStream file;

        if (File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            return null;
        }

        BinaryFormatter bf = new BinaryFormatter();
        fileData = (FileData)bf.Deserialize(file);
        file.Close();

        return fileData;
    }
}
