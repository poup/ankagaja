﻿using Assets.Scripts.PlayerManagement;
using UnityEngine;

public class Player
{
  private readonly Color _color;
  private readonly PlayerInput _input;

  private int _value;

  public Color Color
  {
    get { return _color; }
  }

  public PlayerInput Input
  {
    get { return _input; }
  }

  public Player(Color color, PlayerInput input)
  {
    _color = color;
    _input = input;
  }
  
  


  //private int _id;
//  private bool _active = false;

//  public int Id
//  {
//    get { return _id; }
//    set { _id = value; }
//  }

//  void Update()
//  {
//
//  }
  public void AddReward(int value)
  {
    _value += value;
  }

  public int Value
  {
    get { return _value; }
  }

  public void Reset()
  {
    _value = 0;
  }
}