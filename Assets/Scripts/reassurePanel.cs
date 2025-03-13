using UnityEngine;

public class reassurePanel : MonoBehaviour
{
    [SerializeField] private GameObject ReassurePanel;

    void Start()
    {
        ReassurePanel.SetActive(false);
    }

    public void AreYouSure()
    {
        // Aktifkan panel ketika tombol Run ditekan
        ReassurePanel.SetActive(true);
    }

    public void ClosePanel()
    {
        // Nonaktifkan panel ketika tombol Yes atau No ditekan
        ReassurePanel.SetActive(false);
    }
}
