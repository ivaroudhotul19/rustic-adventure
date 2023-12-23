using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonCtrl : MonoBehaviour
{
    public AudioSource buttonAudio;
    public ButtonAction buttonAction; // Enum untuk menentukan tindakan tombol
    public string sceneName; // Nama scene yang akan dipindahkan

    public enum ButtonAction 
    {
        LoadScene,
        ExitGame
    }

    // Metode ini dipanggil saat tombol diklik
    public void OnButtonClick()
    {
        // Putar suara klik tombol
        buttonAudio.Play();

        // Berdasarkan jenis tindakan tombol, lakukan yang sesuai
        switch (buttonAction)
        {
            case ButtonAction.LoadScene:
                // Panggil metode LoadScene setelah durasi suara klik tombol selesai
                Invoke("LoadScene", buttonAudio.clip.length);
                break;
            case ButtonAction.ExitGame:
                // Panggil metode ExitGame setelah durasi suara klik tombol selesai
                Invoke("ExitGame", buttonAudio.clip.length);
                break;
        }
    }

    // Metode ini akan dipanggil setelah suara klik tombol selesai diputar
    private void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    // Metode ini akan dipanggil setelah suara klik tombol selesai diputar
    private void ExitGame()
    {
        Debug.Log("Exiting the game...");
        Application.Quit();
    }
}
