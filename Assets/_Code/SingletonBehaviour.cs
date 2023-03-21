using UnityEngine;

namespace Tanks 
{
    //=========================================================================
    public class Singleton<T> where T : class, new()
    {
        private static T m_Instance;
       
        //---------------------------------------------------------------------
        public static T Instance {
            get {
                if (m_Instance == null) {
                    CreateInstance();
                }
                return m_Instance;
            }
            private set { }
        }

        //---------------------------------------------------------------------
        public virtual void Init() {
        }

        //---------------------------------------------------------------------
        protected virtual void OnCreateInstance() { }
        private static void CreateInstance() {
            if (m_Instance == null) {
                m_Instance = new T();

                Singleton<T> obj = m_Instance as Singleton<T>;
                obj.OnCreateInstance();                
            }
        }
    }

    //=========================================================================
    public class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T> 
    {
        private static T m_Instance;

        //---------------------------------------------------------------------
        public static T Instance {
            get {
                if (m_Instance == null) {
                    CreateInstance();
                }
                return m_Instance;
            }
            private set { }
        }

        //---------------------------------------------------------------------
        public virtual void Init() {
        }

        //---------------------------------------------------------------------
        protected virtual void OnCreateInstance() { }
        private static void CreateInstance() {
            if (m_Instance == null) {
                m_Instance = FindObjectOfType<T>();
                if (m_Instance == null) {
                    GameObject go = new GameObject();
                    go.name = typeof(T).Name;
                    m_Instance = go.AddComponent<T>();
                }
                SingletonBehaviour<T> obj = m_Instance as SingletonBehaviour<T>;
                obj.OnCreateInstance();

                DontDestroyOnLoad(m_Instance.transform.root.gameObject);
            }
        }
    }
}