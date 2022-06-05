namespace Interactions
{
    public class OrderGate : BaseGate
    {
        public override void UseGatePower()
        {
            base.UseGatePower();
            StackSystem.Instance.OrderColors();

        }
    }
}