using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
   public List<Building> buildings;
    public List<Resource> resources;
    public int alliveNinja;
    public bool isMusicOn;
    public int currentLevel;
    public GameData()
    {
        buildings = new List<Building>();
        resources = new List<Resource>();
    }
}

[Serializable]
public class Building
{
    public string buildingName;
    public bool isBuild;
    //public int level;
}

[Serializable]
public class Resource
{
    public string resourceName;
    public int quantity;
}