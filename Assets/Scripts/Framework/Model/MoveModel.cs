using QFramework;

namespace Model
{
    public class MoveModel:AbstractModel
    {
        public BindableProperty<float> MoveSpeed{ get; } = new BindableProperty<float>(300f);
        public BindableProperty<float> DashSpeed{ get; }  = new BindableProperty<float>(10f);
        public BindableProperty<float> DashCoolDown{ get; } = new BindableProperty<float>(5f);
        public BindableProperty<float> DashTime{ get; } = new BindableProperty<float>(0.2f);
        
        protected override void OnInit()
        {
            var storage = this.GetUtility<Storage>();
            MoveSpeed.SetValueWithoutEvent(storage.LoadFloat(nameof(MoveSpeed),300f));
            DashSpeed.SetValueWithoutEvent(storage.LoadFloat(nameof(DashSpeed),10f));
            DashCoolDown.SetValueWithoutEvent(storage.LoadFloat(nameof(DashCoolDown),5f));
            DashTime.SetValueWithoutEvent(storage.LoadFloat(nameof(DashTime),0.2f));
            MoveSpeed.Register((newValue) =>storage.SaveFloat(nameof(MoveSpeed), newValue));
            DashSpeed.Register((newValue) =>storage.SaveFloat(nameof(DashSpeed), newValue));
            DashCoolDown.Register((newValue) =>storage.SaveFloat(nameof(DashCoolDown), newValue));
            DashTime.Register((newValue) =>storage.SaveFloat(nameof(DashTime), newValue));
        }
        public void Load()
        {
            
        }

        public void Save()
        {
            
        }
    }
}