using UnityEngine; // Mengimpor library UnityEngine untuk menggunakan API Unity
using UnityEngine.UI;// Mengimpor library UnityEngine.UI untuk menggunakan komponen UI
using System.Collections; //Mengimpor library System.Collections untuk menggunakan koleksi dan coroutine

public class BtnTutorial : MonoBehaviour 
{
    public GameObject[] tutorialScreens; // Array GameObject untuk menampung tampilan tutorial
    public AudioSource buttonAudio; // Komponen AudioSource untuk audio tombol
    public AudioSource backgroundAudio; // Komponen AudioSource untuk audio latar belakang
    public Button btnNext;
    public Button btnBack;

    private int currentTutorialIndex = 0; // Indeks tutorial yang sedang ditampilkan

    private void Start() {
        ShowTutorial(currentTutorialIndex);  // Menampilkan tampilan tutorial pada indeks awal saat aplikasi dimulai
        UpdateButtonVisibility();
    }

    public void OnButtonClick() { 
        if (buttonAudio != null) {
            buttonAudio.Play();
        }
        //Unity coroutine adalah metode yang dapat menghentikan eksekusi dan mengembalikan kontrol ke Unity, namun kemudian melanjutkan dari tempat terakhir pada frame berikutnya.
        StartCoroutine(PlayBackgroundAudioWithDelay());  //  menunggu sejenak sebelum memainkan audio latar belakang dengan penundaan
    }

    public void NextTutorial() { 
        // Menampilkan tampilan tutorial berikutnya
        currentTutorialIndex = (currentTutorialIndex + 1) % tutorialScreens.Length;
        ShowTutorial(currentTutorialIndex);
        UpdateButtonVisibility();
    }

    public void PreviousTutorial() {
        // Menampilkan tampilan tutorial sebelumnya
        currentTutorialIndex--;
        if (currentTutorialIndex < 0) {
            currentTutorialIndex = tutorialScreens.Length - 1;
        }
        ShowTutorial(currentTutorialIndex);
        UpdateButtonVisibility();
    }

    private void UpdateButtonVisibility() {
        // Menyembunyikan tombol "Previous" saat di tampilan pertama
        if (currentTutorialIndex == 0) {
            btnBack.gameObject.SetActive(false);
        } else {
            btnBack.gameObject.SetActive(true);
        }

        // Menyembunyikan tombol "Next" saat di tampilan terakhir
        if (currentTutorialIndex == tutorialScreens.Length - 1) {
            btnNext.gameObject.SetActive(false);
        } else {
            btnNext.gameObject.SetActive(true);
        }
    }

    private void ShowTutorial(int index) {
        // Menampilkan tampilan tutorial yang sesuai dan menyembunyikan yang lain
        for (int i = 0; i < tutorialScreens.Length; i++) {
            if (i == index) {
                tutorialScreens[i].SetActive(true);
            } else {
                tutorialScreens[i].SetActive(false);
            }
        }
    }

    private IEnumerator PlayBackgroundAudioWithDelay() {
        //untuk membuat coroutines
        yield return new WaitForSeconds(buttonAudio.clip.length);// Menunggu sejenak sebelum memainkan audio latar belakang
        if (backgroundAudio != null) {
            backgroundAudio.Play();
        }
    }
}
