using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HF
{
    public class DamageSystem : MonoBehaviour
    {
        public GameObject m_100Model;
        public GameObject m_80Model;
        public GameObject m_60Model;
        public GameObject m_40Model;
        public GameObject m_20Model;
        public GameObject m_0Model;

        public GameObject m_currentModel;

        void Start()
        {
            //find the current model 
            for (int i = 0; i <= transform.childCount - 1; i++)
            {
                string tempTag = transform.GetChild(i).gameObject.tag;

                if (tempTag == "CarModel")
                {
                    m_currentModel = transform.GetChild(i).gameObject;
                    m_100Model.GetComponent<MeshFilter>().sharedMesh = m_currentModel.GetComponent<MeshFilter>().sharedMesh;
                }
            }
        }

        void Update()
        {
            //if the twist is dissappear, set player mesh to active/inactive
            if (TwistManager.m_instance.m_currentTwist == TwistManager.Twists.dissapear)
            {
                m_currentModel.SetActive(false);
            }
            else
            {
                m_currentModel.SetActive(true);
            }
        }

        public void ChangeMesh(int _damageCounter)
        {
            switch (_damageCounter)
            {
                case 0:
                    Debug.Log("Model has been reset to 100");
                    m_currentModel.GetComponent<MeshFilter>().sharedMesh = m_100Model.GetComponent<MeshFilter>().sharedMesh;
                    break;
                case 1:
                    m_currentModel.GetComponent<MeshFilter>().sharedMesh = m_80Model.GetComponent<MeshFilter>().sharedMesh;
                    break;
                case 2:
                    m_currentModel.GetComponent<MeshFilter>().sharedMesh = m_60Model.GetComponent<MeshFilter>().sharedMesh;
                    break;
                case 3:
                    m_currentModel.GetComponent<MeshFilter>().sharedMesh = m_40Model.GetComponent<MeshFilter>().sharedMesh;
                    break;
                case 4:
                    m_currentModel.GetComponent<MeshFilter>().sharedMesh = m_20Model.GetComponent<MeshFilter>().sharedMesh;
                    break;
                case 5:
                    m_currentModel.GetComponent<MeshFilter>().sharedMesh = m_0Model.GetComponent<MeshFilter>().sharedMesh;
                    break;
                default:
                   // m_currentModel.GetComponent<MeshFilter>().sharedMesh = m_100Model.GetComponent<MeshFilter>().sharedMesh;
                    break;
            }
        }
    }
}
