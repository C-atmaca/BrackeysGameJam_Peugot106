using UnityEngine;
using Mech.Factory;

[CreateAssetMenu(fileName = "SoundEmitterFactorySO", menuName = "Scriptable Objects/SoundEmitterFactorySO")]
public class SoundEmitterFactorySO : FactorySO<SoundEmitter>
{
    public SoundEmitter prefab = default;

    public override SoundEmitter Create()
    {
        return Instantiate(prefab);
    }
}
