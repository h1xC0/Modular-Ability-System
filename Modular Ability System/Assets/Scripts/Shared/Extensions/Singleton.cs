using UnityEngine;

namespace Shared.Extensions
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField]
        private bool dontDestroy = false;

        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;
                
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    GameObject singleton = new GameObject(typeof(T).Name);
                    _instance = singleton.AddComponent<T>();
                }
                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                if (dontDestroy)
                {
                    transform.parent = null;
                    DontDestroyOnLoad(this.gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
            }

            AwakeInit();
        }

        public virtual void AwakeInit()
        {

        }    


    }
}