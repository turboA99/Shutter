using JetBrains.Annotations;
using UnityEngine;

namespace Interactions
{
    [CreateAssetMenu(fileName = "Interaction", menuName = "Scriptable Objects/Interaction")]
    public class Interaction : ScriptableObject
    {
        public InteractableObject interactedObject;
        public Sprite newSprite;
        public Sprite newOutline;
        public Sprite inventorySprite;
        public string characterThoughts;
        [CanBeNull] public InventoryItem newItem;
        public bool takeItem;
        public NoiseLevel noiseLevel;
        public AudioClip clipToPlay;
        public NoiseReduced reductionLevel;
        public ReductionDuration duration;
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
}