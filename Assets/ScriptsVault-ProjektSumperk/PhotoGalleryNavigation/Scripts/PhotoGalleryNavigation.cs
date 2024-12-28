﻿using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace ProjektSumperk
{
    public class PhotoGalleryNavigation : MonoBehaviour
    {
        string path;
        public GameObject FullScreenPanel;
        public Sprite[] imagesSprites;
        private int currentImageIndex = 0; // To keep track of the current image index.

        void Start()
        {
            path = Application.dataPath + "/ScriptsVault-ProjektSumperk/PhotoGallery/Photos";
            LoadImageGallery();
            Debug.Log(path);

            // Initialize the gallery with the first image.
            if (imagesSprites.Length > 0)
            {
                FullScreenPanel.GetComponent<Image>().sprite = imagesSprites[currentImageIndex];
            }
        }

        public void GetPhotoImages(Sprite sprite)
        {
            FullScreenPanel.SetActive(true);
            FullScreenPanel.GetComponent<Image>().sprite = sprite;
        }

        void LoadImageGallery()
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo d = new DirectoryInfo(path);

                foreach (var extension in new string[] { "*.png", "*.jpg", "*.jpeg" })
                {
                    foreach (var file in d.GetFiles(extension))
                    {
                        string fileURL = "file:///" + file.FullName;
                        StartCoroutine(LoadImage(fileURL));
                    }
                }
            }
            else
            {
                Directory.CreateDirectory(path);
                return;
            }
        }

        IEnumerator LoadImage(string url)
        {
            string extension = Path.GetExtension(url).ToLower();

            UnityWebRequest www;

            if (extension == ".png")
            {
                www = UnityWebRequestTexture.GetTexture(url);
            }
            else if (extension == ".jpg" || extension == ".jpeg")
            {
                www = UnityWebRequestTexture.GetTexture(url, true);
            }
            else
            {
                Debug.LogError("Unsupported image format: " + extension);
                yield break;
            }

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Texture2D textur = DownloadHandlerTexture.GetContent(www);
                Vector2 pivot = new Vector2(0.5f, 0.5f);
                Sprite sprite = Sprite.Create(textur, new Rect(0.0f, 0.0f, textur.width, textur.height), pivot, 100.0f);

                // Add the loaded sprite to the array.
                System.Array.Resize(ref imagesSprites, imagesSprites.Length + 1);
                imagesSprites[imagesSprites.Length - 1] = sprite;

                // If this is the first image loaded, set it as the current image.
                if (imagesSprites.Length == 1)
                {
                    FullScreenPanel.GetComponent<Image>().sprite = sprite;
                }
            }
            else
            {
                Debug.LogError("Error loading image: " + www.error);
            }
        }

        public void NextImage()
        {
            if (imagesSprites.Length > 0)
            {
                currentImageIndex = (currentImageIndex + 1) % imagesSprites.Length;
                FullScreenPanel.GetComponent<Image>().sprite = imagesSprites[currentImageIndex];
            }
        }

        public void PreviousImage()
        {
            if (imagesSprites.Length > 0)
            {
                currentImageIndex = (currentImageIndex - 1 + imagesSprites.Length) % imagesSprites.Length;
                FullScreenPanel.GetComponent<Image>().sprite = imagesSprites[currentImageIndex];
            }
        }
    }
}