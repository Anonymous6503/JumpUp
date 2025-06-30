using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace negProt.Utils
{ 
    public class StartMenu : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private RectTransform menuContainer;

        [Header("Smooth")] 
        [SerializeField] private bool smooth = true;
        [SerializeField] private float smoothSpeed = 0.1f;
        [SerializeField] private Vector3 desiredPosition;

        //Logic
        private Vector3[] _menuPositions;

        private void Awake()
        {
            if(menuContainer==null)
                menuContainer = new RectTransform();
        }

        private void Start()
        {
            _menuPositions = new Vector3[menuContainer.childCount];
            Vector3 halfScreen = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
            for (int i = 0; i < _menuPositions.Length; i++)
            {
                _menuPositions[i] = menuContainer.GetChild(i).position - halfScreen;
            }
        }

        private void Update()
        {
            if (smooth)
            {
                menuContainer.anchoredPosition =
                    Vector3.Lerp(menuContainer.anchoredPosition, desiredPosition, smoothSpeed);
            }
            else
            {
                menuContainer.anchoredPosition = desiredPosition;
            }
        }

        public void ChangeScene(int sceneId)
        {
            SceneManager.LoadScene(sceneId);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void MoveMenu(int id)
        {
            desiredPosition = -_menuPositions[id];
        }
    }
}