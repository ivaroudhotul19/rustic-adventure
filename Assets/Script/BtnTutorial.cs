using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BtnTutorial : MonoBehaviour
{
    public GameObject[] tutorialScreens; // Array untuk tampilan tutorial
    public AudioSource buttonAudio;
    public AudioSource backgroundAudio;
    public Button btnNext;
    public Button btnBack;

    private int currentTutorialIndex = 0;

    private void Start()
    {
        ShowTutorial(currentTutorialIndex);
        UpdateButtonVisibility();
    }

    public void OnButtonClick()
    {
        if (buttonAudio != null)
        {
            buttonAudio.Play();
        }

        StartCoroutine(PlayBackgroundAudioWithDelay());
    }

    public void NextTutorial()
    {
        // Menampilkan tampilan tutorial berikutnya
        currentTutorialIndex = (currentTutorialIndex + 1) % tutorialScreens.Length;
        ShowTutorial(currentTutorialIndex);
        UpdateButtonVisibility();
    }

    public void PreviousTutorial()
    {
        // Menampilkan tampilan tutorial sebelumnya
        currentTutorialIndex--;
        if (currentTutorialIndex < 0)
        {
            currentTutorialIndex = tutorialScreens.Length - 1;
        }
        ShowTutorial(currentTutorialIndex);
        UpdateButtonVisibility();
    }

    private void UpdateButtonVisibility()
    {
        // Menyembunyikan tombol "Previous" saat di tampilan pertama
        if (currentTutorialIndex == 0)
        {
            btnBack.gameObject.SetActive(false);
        }
        else
        {
            btnBack.gameObject.SetActive(true);
        }

        // Menyembunyikan tombol "Next" saat di tampilan terakhir
        if (currentTutorialIndex == tutorialScreens.Length - 1)
        {
            btnNext.gameObject.SetActive(false);
        }
        else
        {
            btnNext.gameObject.SetActive(true);
        }
    }

    private void ShowTutorial(int index)
    {
        // Menampilkan tampilan tutorial yang sesuai dan menyembunyikan yang lain
        for (int i = 0; i < tutorialScreens.Length; i++)
        {
            if (i == index)
            {
                tutorialScreens[i].SetActive(true);
            }
            else
            {
                tutorialScreens[i].SetActive(false);
            }
        }
    }

    private IEnumerator PlayBackgroundAudioWithDelay()
    {
        yield return new WaitForSeconds(buttonAudio.clip.length);

        if (backgroundAudio != null)
        {
            backgroundAudio.Play();
        }
    }
}
