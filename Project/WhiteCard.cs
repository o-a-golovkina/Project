namespace Project
{
    public class WhiteCard : DiscountCard
    {
        public override string CardType => "WHITE";

        public override int Percent { get; } = 9;

        public override double CalculateDiscount(double amount) => amount * 0.09;
    }
}
