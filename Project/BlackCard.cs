namespace Project
{
    public class BlackCard : DiscountCard
    {
        public override string CardType => "BLACK";

        public override int Percent { get; } = 13;

        public override double CalculateDiscount(double amount) => amount * 0.13;
    }
}
