using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemFade : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var ps = GetComponent<ParticleSystem>();
        var minGradient = ps.colorOverLifetime.color.gradientMax.alphaKeys[0].alpha;
        Debug.Log(minGradient);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
