using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class botón_cambioescena: MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clicksound;
    public string nombre_escena;

    private Button btn;

    void Start()
    {
        btn = GetComponent<Button>();
        if (btn != null)
        {
            cambiar_escena();
        }
        cambiar_escena();
    }
    void PlayClickSound()
    {
        audioSource.PlayOneShot(clicksound);
    }
    void cambiar_escena()
    {
        // esto que hicimos con el btn.onclick es una lambda
        // (() => { ... }); 0.3f es el tiempo de retraso que tendrá para que se 
        // pueda reproducir bien el playclicksound()
        // la función Invoke sirve para llamar a otra función después 
        // de un tiempo determinado
        btn.onClick.AddListener(() =>
        {
            PlayClickSound();
            Invoke($"changescene", 0.8f);
        });
        

    }
    private void changescene()
    {
        SceneManager.LoadScene(nombre_escena);
    }

}
