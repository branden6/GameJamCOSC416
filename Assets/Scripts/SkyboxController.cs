using UnityEngine;

public class SkyboxController : MonoBehaviour {
    [SerializeField] private float rotationSpeed = 0.4f;
    private static SkyboxController instance;

    void Awake(){
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
            }
        else{
            Destroy(gameObject);
            }
    }

    void Update(){
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);
    }
}
