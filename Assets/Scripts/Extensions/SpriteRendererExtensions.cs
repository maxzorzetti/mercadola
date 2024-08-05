using UnityEngine;

public static class SpriteRendererExtension
{
        public static bool AreFacingEachOther(this SpriteRenderer us, SpriteRenderer them)
        {
            var positionA = us.transform.position;
            var positionB = them.transform.position;
            var flipX = us.flipX;
            
            return flipX != them.flipX 
                   && (positionA.x < positionB.x && !flipX 
                   || positionA.x > positionB.x && flipX);
        }
}