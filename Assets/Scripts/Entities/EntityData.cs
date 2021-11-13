using UnityEngine;


[CreateAssetMenu(fileName ="New Entity Data", menuName ="Entity Data")]
public class EntityData : ScriptableObject
{
    [SerializeField]
    private float _movementSpeed = 5;

    public float MovementSpeed
    {
        get => _movementSpeed;
    }
}
