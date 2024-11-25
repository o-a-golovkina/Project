namespace Project
{
    public abstract class DiscountCard
    {
        public abstract string CardType { get; }

        public abstract int Percent { get; }

        public abstract double CalculateDiscount(double amount);
    }
}