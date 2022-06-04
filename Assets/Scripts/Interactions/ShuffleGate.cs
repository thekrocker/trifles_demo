namespace Interactions
{
    public class ShuffleGate : BaseGate
    {
        public override void UseGatePower()
        {
            base.UseGatePower();
            StackSystem.Instance.ShuffleColors();
        }
    }
}