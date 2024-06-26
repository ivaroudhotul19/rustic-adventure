using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameData
{
  public int coinCount;
  public int score;
  public double lives;
  public bool isFirstBoot;
  public LevelData[] levelData;
  public bool keyFound;

  public bool playSound;       
  public bool playMusic;

  public bool harmonyKeyFound;
}
