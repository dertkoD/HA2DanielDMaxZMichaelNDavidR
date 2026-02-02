using UnityEngine;

public class AgentRoot : MonoBehaviour
{
    [SerializeField] private int agentId;
    [SerializeField] private Transform handSocket;
    [SerializeField] private Animator animator; 
    public int AgentId => agentId; 
    public Transform HandSocket => handSocket; 
    public Animator Animator => animator;
}
