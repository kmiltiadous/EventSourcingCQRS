namespace Mobile.Client.Behaviors
{
    public class TargetPropertyChanged
    {
        public dynamic Value { get; } 
        public string Name { get; }
        public TargetPropertyChanged(dynamic value, string name)
        {
            Value = value;
            Name = name;
        }
    }
}
