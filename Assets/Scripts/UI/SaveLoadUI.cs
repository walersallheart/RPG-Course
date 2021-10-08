using System.Collections;
using System.Collections.Generic;
using RPG.SceneManagement;
using TMPro;
using UnityEngine;

    namespace RPG.UI {
    public class SaveLoadUI : MonoBehaviour
    {
        [SerializeField] Transform contentRoot;
        [SerializeField] GameObject buttonPrefab;

        private void OnEnable() {
            print("Loading Saves...");
            foreach (Transform child in contentRoot)
            {
                Destroy(child.gameObject);
            }

            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();

            foreach (string save in savingWrapper.ListSaves())
            {
                GameObject buttonInstance = Instantiate(buttonPrefab, contentRoot);
                TMP_Text textComp = buttonInstance.GetComponentInChildren<TMP_Text>();
                textComp.text = save;
            }
        }

    }
}
