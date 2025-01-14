using UnityEngine;

namespace _project.Scripts.VFX
{
    public class VfxElement : MonoBehaviour
    { 
        [SerializeField] private Animator animator;
        private readonly int _explode = Animator.StringToHash("Explode");

        private void OnValidate()
        {
            animator ??= GetComponent<Animator>();
        }

        public void PlayAnimation(Vector2 dataPosition)
        {
            transform.position = dataPosition;
            gameObject.SetActive(true);
            animator.SetTrigger(_explode);
        }
    }
}