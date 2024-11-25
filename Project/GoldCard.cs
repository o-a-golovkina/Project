namespace Project
{
    public class GoldCard : DiscountCard
    {
        public override string CardType => "GOLD";

        public override int Percent { get; } = 22;

        public override double CalculateDiscount(double amount) => amount * 0.22;
    }
}
