using Model;
using QFramework;

namespace Command
{
    public class ChangeGoldCoinCommand:AbstractCommand
    {
        private int value;
        public ChangeGoldCoinCommand(int value)
        {
            this.value = value;
        }
        protected override void OnExecute()
        {
            var model = this.GetModel<GameModel>();
            model.GoldCoinNum.Value += value;
        }
    }
}

