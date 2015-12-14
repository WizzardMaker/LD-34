using System;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Rendering/Screen Space Ambient Occlusion")]
public class SSAO : MonoBehaviour
{
    public enum SSAOSamples
	{
        Low = 0,
        Medium = 1,
        High = 2,
    }

    [Range(0.05f, 1.0f)]
    public float radius = 0.4f;
    public SSAOSamples sampleCount = SSAOSamples.Medium;
    [Range(0.5f, 4.0f)]
    public float occlusionIntesity = 1.5f;
    [Range(0, 4)]
    public int blur = 2;
    [Range(1,6)]
    public int donwlsampling = 2;
    [Range(0.2f, 2.0f)]
    public float occlusionAttenuation = 1.0f;
    [Range(0.00001f, 0.5f)]
    public float mininumZ = 0.01f;

    public Shader SSAOShader;
    Material SSAOMaterial;

    bool isSupported;

    static Material MakeMaterial (Shader shader)
    {
        if (!shader)
            return null;
        Material m = new Material (shader);
        m.hideFlags = HideFlags.HideAndDontSave;
        return m;
    }

    static void DestroyMaterial (Material mat)
    {
        if (mat)
        {
            DestroyImmediate (mat);
            mat = null;
        }
    }


    void OnDisable()
    {
        DestroyMaterial (SSAOMaterial);
    }

    void Start(){
        if (!SystemInfo.supportsImageEffects || !SystemInfo.SupportsRenderTextureFormat (RenderTextureFormat.Depth)){
            isSupported = false;
            enabled = false;
            return;
        }

        CreateMaterials ();

        if (!SSAOMaterial || SSAOMaterial.passCount != 5){
            isSupported = false;
            enabled = false;
            return;
        }

        //CreateRandomTable (26, 0.2f);

        isSupported = true;
    }

    void OnEnable () {
        GetComponent<Camera>().depthTextureMode |= DepthTextureMode.DepthNormals;
    }

    void CreateMaterials (){
        if (!SSAOMaterial && SSAOShader.isSupported){
            SSAOMaterial = MakeMaterial (SSAOShader);
			SSAOMaterial.SetTexture("_RandomTexture", new Texture2D(1,1));
		}
    }

    [ImageEffectOpaque]
    void OnRenderImage (RenderTexture source, RenderTexture destination){
        if (!isSupported || !SSAOShader.isSupported) {
            enabled = false;
            return;
        }

        CreateMaterials ();

        donwlsampling = Mathf.Clamp (donwlsampling, 1, 6);
        radius = Mathf.Clamp (radius, 0.05f, 1.0f);
        mininumZ = Mathf.Clamp (mininumZ, 0.00001f, 0.5f);
        occlusionIntesity = Mathf.Clamp (occlusionIntesity, 0.5f, 4.0f);
        occlusionAttenuation = Mathf.Clamp (occlusionAttenuation, 0.2f, 2.0f);
        blur = Mathf.Clamp (blur, 0, 4);

        // Render SSAO term into a smaller texture
        RenderTexture rtAO = RenderTexture.GetTemporary (source.width / donwlsampling, source.height / donwlsampling, 0);

		float fovY = GetComponent<Camera>().fieldOfView;
        float far = GetComponent<Camera>().farClipPlane;
        float y = Mathf.Tan (fovY * Mathf.Deg2Rad * 0.5f) * far;
        float x = y * GetComponent<Camera>().aspect;
        SSAOMaterial.SetVector ("_FarCorner", new Vector3(x,y,far));

		int noiseWidth, noiseHeight;
			noiseWidth = 1; noiseHeight = 1;

		SSAOMaterial.SetVector ("_NoiseScale", new Vector3 ((float)rtAO.width / noiseWidth, (float)rtAO.height / noiseHeight, 0.0f));
		SSAOMaterial.SetVector("_Params", new Vector4(radius, mininumZ, 1.0f / occlusionAttenuation, occlusionIntesity));

        bool doBlur = blur > 0;
        Graphics.Blit (doBlur ? null : source, rtAO, SSAOMaterial, (int)sampleCount);

        if (doBlur){
            // Blur SSAO horizontally
            RenderTexture rtBlurX = RenderTexture.GetTemporary (source.width, source.height, 0);
            SSAOMaterial.SetVector ("_TexelOffsetScale",
                                        new Vector4 ((float)blur / source.width, 0,0,0));
            SSAOMaterial.SetTexture ("_SSAO", rtAO);
            Graphics.Blit (null, rtBlurX, SSAOMaterial, 3);
            RenderTexture.ReleaseTemporary (rtAO); // original rtAO not needed anymore

            // Blur SSAO vertically
            RenderTexture rtBlurY = RenderTexture.GetTemporary (source.width, source.height, 0);
            SSAOMaterial.SetVector ("_TexelOffsetScale",
                                        new Vector4 (0, (float)blur/source.height, 0,0));
            SSAOMaterial.SetTexture ("_SSAO", rtBlurX);
            Graphics.Blit (source, rtBlurY, SSAOMaterial, 3);
            RenderTexture.ReleaseTemporary (rtBlurX); // blurX RT not needed anymore

            rtAO = rtBlurY; // AO is the blurred one now
        }

        // Modulate scene rendering with SSAO
        SSAOMaterial.SetTexture ("_SSAO", rtAO);
        Graphics.Blit (source, destination, SSAOMaterial, 4);

        RenderTexture.ReleaseTemporary (rtAO);
    }

    /*
	void CreateRandomTable (int count, float minLength){
		Random.seed = 1337;
		Vector3[] samples = new Vector3[count];
		// initial samples
		for (int i = 0; i < count; ++i)
			samples[i] = Random.onUnitSphere;
		// energy minimization: push samples away from others
		int iterations = 100;
		while (iterations-- > 0) {
			for (int i = 0; i < count; ++i) {
				Vector3 vec = samples[i];
				Vector3 res = Vector3.zero;
				// minimize with other samples
				for (int j = 0; j < count; ++j) {
					Vector3 force = vec - samples[j];
					float fac = Vector3.Dot (force, force);
					if (fac > 0.00001f)
						res += force * (1.0f / fac);
				}
				samples[i] = (samples[i] + res * 0.5f).normalized;
			}
		}
		// now scale samples between minLength and 1.0
		for (int i = 0; i < count; ++i) {
			samples[i] = samples[i] * Random.Range (minLength, 1.0f);
		}

		string table = string.Format ("#define SAMPLE_COUNT {0}\n", count);
		table += "const float3 RAND_SAMPLES[SAMPLE_COUNT] = {\n";
		for (int i = 0; i < count; ++i) {
			Vector3 v = samples[i];
			table += string.Format("\tfloat3({0},{1},{2}),\n", v.x, v.y, v.z);
		}
		table += "};\n";
		Debug.Log (table);
	}
	*/
}