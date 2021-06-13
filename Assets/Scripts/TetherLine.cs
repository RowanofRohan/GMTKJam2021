using UnityEngine;

public class TetherLine : MonoBehaviour
{
	[SerializeField] private Texture[] textures;
	[SerializeField] private float fps = 30f;

	private LineRenderer line;
	private float fpsCounter;
	private int animationStep;


    private void Awake()
    {
		line = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        fpsCounter += Time.deltaTime;
        if (fpsCounter >= 1f / fps)
        {
            animationStep++;
            if (animationStep == textures.Length)
            {
                animationStep = 0;
            }

            line.material.SetTexture("_MainTex", textures[animationStep]);

            fpsCounter = 0f;
        }
    }

}
