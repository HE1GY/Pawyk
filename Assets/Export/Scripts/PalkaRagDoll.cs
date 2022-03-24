using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace ShitPalka
{
    public class PalkaRagDoll
    {
        private readonly RigBuilder _rigBuilder;
        private readonly LegAnimation[] _legAnimations;
        private readonly Rigidbody[] _legRigidbodies;
        private readonly Animator _mainAnimator;

        public PalkaRagDoll(RigBuilder rigBuilder, Rigidbody[] legRigidbodies,Animator mainAnimator,params LegAnimation[] legAnimations)
        {
            _rigBuilder = rigBuilder;
            _legAnimations = legAnimations;
            _legRigidbodies = legRigidbodies;
            _mainAnimator = mainAnimator;
        }

        public void SetRagDoll(bool on)
        {
            foreach (var legAnimation in _legAnimations)
            {
                legAnimation.SetEnableAnimator(!on);
            }

            foreach (var legRigidbody in _legRigidbodies)
            {
                legRigidbody.isKinematic = !on;
            }

            _mainAnimator.enabled = !on;
        }
    }
}