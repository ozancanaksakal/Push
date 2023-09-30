using UnityEngine;


public enum PowerType { None=-1, Push, Rocket, Hammer };

public class Powerup : MonoBehaviour
{
    public ParticleSystem effect;
    public PowerType myPower;
    [SerializeField] float rotSpeed = 60;
    public float powerDuration;
    private void Start()
    {
        transform.Translate(0, 0.20f, 1);
    }

    void Update()
    {
        transform.Rotate(0, rotSpeed * Time.deltaTime, 0);
    }
}
