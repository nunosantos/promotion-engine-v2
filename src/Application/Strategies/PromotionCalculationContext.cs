using Application.Interfaces;

namespace Application.Strategies
{
    public class PromotionCalculationContext
    {
        private IPromotionStrategy promotionStrategy;

        public void SetPromotionStrategy(IPromotionStrategy promotionStrategy)
        {
            this.promotionStrategy = promotionStrategy;
        }

        public int CalculateTotal()
        {
            return promotionStrategy.CalculateTotal();
        }
    }
}