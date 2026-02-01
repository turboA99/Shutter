using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Interaction", menuName = "Scriptable Objects/Interaction")]
public class Interaction : ScriptableObject
{
    public InteractableObject interactedObject;
    public Sprite newSprite;
    public Sprite newOutline;
    public string characterThoughts;
    public InventoryItem newItem;
    public NoiseMade noiseLevel;
    public AudioClip clipToPlay;
    public NoiseReduced reductionLevel;
    public ReductionDuration duration;
}

public enum InteractableObject
{
    FixableCar,
    GasCan,
    BerryBush,
    TrashCan,
    FirstCar,
    AbandonedCar,
    FrogOne,
    FrogTwo,
    Pond,
    Tree,
    CookingPile,
    Window,
    ToolBox,
    Carjack,
    SpareTire
}

public enum InventoryItem
{
    None,
    Empty,
    BrokenGas,
    FunneledGas,
    SmallBerry,
    MediumBerry,
    LargeBerry,
    FrogOne,
    FrogTwo,
    LongStick,
    SparkPlug,
    Crowbar,
    Funnel,
    Carjack,
    Tire
}

public enum NoiseMade
{
    None,
    VeryLow,
    Low,
    Medium,
    High,
    VeryHigh
}

public enum NoiseReduced
{
    None,
    Low,
    Medium,
    High
}

public enum ReductionDuration
{
    None,
    Short,
    Medium,
    Long
}