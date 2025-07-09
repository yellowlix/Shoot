using QFramework;

namespace Model
{
    public class LevelModel:AbstractModel
    {
        public BindableProperty<int> CurrentExp { get; } = new(0);
        public BindableProperty<int> CurrentLevel{ get; } = new(0);
        public BindableProperty<int> ExpToNextLevel{ get; } = new(50);
        protected override void OnInit()
        {
            var storage = this.GetUtility<Storage>();
            CurrentExp.SetValueWithoutEvent(storage.LoadInt(nameof(CurrentExp),0));
            CurrentLevel.SetValueWithoutEvent(storage.LoadInt(nameof(CurrentLevel),0));
            ExpToNextLevel.SetValueWithoutEvent(storage.LoadInt(nameof(ExpToNextLevel),50));
            CurrentExp.Register(newValue => storage.SaveInt(nameof(CurrentExp), newValue));
            CurrentLevel.Register(newValue => storage.SaveInt(nameof(CurrentLevel), newValue));
            ExpToNextLevel.Register(newValue => storage.SaveInt(nameof(ExpToNextLevel), newValue));
        }
        public void Load()
        {
            
        }

        public void Save()
        {
            
        }
    }
}