using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mobile.Client.Behaviors
{
    public class AnimateBehavior : IAnimate
    {
        public async void Toggle<T>(T element) where T : TargetPropertyChanged
        {
            if (!element.Value.GetType().IsSubclassOf(typeof(VisualElement)) || element.Name != nameof(VisualElement.IsVisible)) return;

            var visualElement = (VisualElement) element.Value;
            if (visualElement.IsVisible)
            {
                visualElement.AnchorX = 1;
                visualElement.AnchorY = 1;

                var scaleAnimation = new Animation(
                    f => visualElement.Scale = f,
                    0.5,
                    1,
                    Easing.SinInOut);

                var fadeAnimation = new Animation(
                    f => visualElement.Opacity = f,
                    0.2,
                    1,
                    Easing.SinInOut);

                scaleAnimation.Commit(visualElement, "popupScaleAnimation", 250);
                fadeAnimation.Commit(visualElement, "popupFadeAnimation", 250);
            }
            else
            {
                await Task.WhenAny<bool>
                (
                    visualElement.FadeTo(0, 50, Easing.SinInOut)
                );
            }
        }
    }

    public interface IAnimate
    {
        void Toggle<T>(T element) where T : TargetPropertyChanged;
    }
}