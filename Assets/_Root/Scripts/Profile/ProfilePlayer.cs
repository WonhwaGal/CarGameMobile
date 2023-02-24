using Tool;
using Game;
using Game.Car;
using Features.Inventory;
using Features.Rewards.Currency;

namespace Profile
{
    internal class ProfilePlayer
    {
        public readonly SubscriptionProperty<GameState> CurrentState;
        public readonly InventoryModel Inventory;
        public readonly CarModel CurrentCar;
        public readonly CurrencyModel Currency;
        public readonly PauseMenuModel PauseModel;

        public ProfilePlayer(float speedCar, float jumpHeight, GameState initialState) : 
            this(speedCar, jumpHeight)
        {
            CurrentState.Value = initialState;
        }

        public ProfilePlayer(float speedCar, float jumpHeight)
        {
            CurrentState = new SubscriptionProperty<GameState>();

            CurrentCar = new CarModel(speedCar, jumpHeight);
            Inventory = new InventoryModel();
            Currency = new CurrencyModel();
            PauseModel = new PauseMenuModel();
        }
    }
}
