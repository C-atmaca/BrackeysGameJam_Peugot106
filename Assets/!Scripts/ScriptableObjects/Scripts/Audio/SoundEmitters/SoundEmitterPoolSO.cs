using Mech.Pool;
using Mech.Factory;
using UnityEngine;


[CreateAssetMenu(fileName = "SoundEmitterPoolSO", menuName = "Scriptable Objects/SoundEmitterPoolSO")]
public class SoundEmitterPoolSO : ComponentPoolSO<SoundEmitter>
{
    [SerializeField] private SoundEmitterFactorySO _factory;

    public override IFactory<SoundEmitter> Factory
    {
        get
        {
            return _factory;
        }
        set
        {
            _factory = value as SoundEmitterFactorySO;
        }
    }
    
}
