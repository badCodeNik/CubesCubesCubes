using System;
using _project.Scripts.Tools;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace _project.Scripts.GameEntities
{
    public class Cube : MonoBehaviour, IDraggable, IDestroyable
    {
        [SerializeField] private GameObject cubeChecker;
        [SerializeField] private float checkDistance;
        private SpriteRenderer _spriteRenderer;
        private int _cubeLayerMask;
        private Tweener _moveTween;
        private Signal _signal;
        
        public string Id { get; private set; }
        public bool IsInTower { get; private set; }
        public void GetFromTower() => IsInTower = false;
        public float GetWidth() => _spriteRenderer.bounds.size.x;
        public float GetHeight() => _spriteRenderer.bounds.size.y;
        public Vector2 GetPosition() => transform.position;
        public void SetParent(Transform parent) => transform.SetParent(parent);
        public void SetId(string id) =>  Id = id;


        [Inject]
        public void Construct(Signal signal)
        {
            _signal = signal;
        }
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _cubeLayerMask = (1 << LayerMask.NameToLayer("Cube")) | ~(1 << LayerMask.NameToLayer("Tower"));
            Id = Guid.NewGuid().ToString();
            DOTween.SetTweensCapacity(1000, 50);
            
        }
        
        public bool Raycast()
        {
            RaycastHit2D hit = Physics2D.Raycast( cubeChecker.transform.position, Vector2.down, checkDistance * 1.5f, _cubeLayerMask);
            return hit.collider != null && hit.collider.gameObject != gameObject;
        }
        
        public void SetPosition(Vector2 position)
        {
            IsInTower = true;
            _moveTween?.Kill();
            _moveTween = transform.DOMoveY(transform.position.y + 1f, 0.3f).OnComplete(() =>
            {
                transform.DOMove(position, 0.3f);
            });
        }
        
        public void Destroy()
        {
            _signal.RegistryRaise(new Signals.OnCubeRemoved
            {
                CubeId = Id
            });
            _moveTween?.Kill();
            _moveTween = transform
                .DORotate(new Vector3(0, 0, 360), 0.7f, RotateMode.FastBeyond360)
                .OnUpdate(() => _spriteRenderer.DOFade(0, 0.7f))
                .OnComplete(() => Destroy(gameObject, 0.75f));
        }
    }
}