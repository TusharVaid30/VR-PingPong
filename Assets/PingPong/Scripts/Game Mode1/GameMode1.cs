using Unity.XR.Oculus.Input;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Random = UnityEngine.Random;
using XRController = UnityEngine.InputSystem.XR.XRController;

public class GameMode1 : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private Transform[] locations;
    
    private int random;
    
    private void Start()
    {
        ChangePosition();
    }

    public void ChangePosition()
    {
        random = Random.Range(0, 10);

        if (random != 10)
        {
            LeanTween.move(gameObject, locations[random].position, 3f * Time.deltaTime);
            LeanTween.scale(gameObject, locations[random].localScale, 3f * Time.deltaTime);
            
            LeanTween.move(particle.gameObject, locations[random].position, 3f * Time.deltaTime);
            
            LeanTween.scale(particle.gameObject, locations[random].localScale, 3f * Time.deltaTime);
        }

        particle.Play();
    }
}
