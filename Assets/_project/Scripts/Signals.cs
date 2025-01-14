using _project.Scripts.GameEntities;
using UnityEngine;

namespace _project.Scripts
{
    public class Signals
    {
        //В целом сейчас это просто взрыв, но если нам понадобится, мы делаем сигнал
        //для общей анимации, туда мы будем вставлять айди и позицию и сигнал будет в библиотеке смотреть
        //анимацию, и в случае ее присуствия - проигрывать ее в определенном месте
        public struct OnExplosion
        {
            public Vector2 Position;
        }

        public struct OnActionPerformed
        {
            public string ActionDescription;
        }
        
        public struct OnCubeAdded
        {
            public string CubeId;
            public float X;
            public float Y;
            public Color Color;
        }
        
        public struct OnCubeRemoved
        {
            public string CubeId;
        } 
        
    }
}

