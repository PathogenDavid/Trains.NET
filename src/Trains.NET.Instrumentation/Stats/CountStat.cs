namespace Trains.NET.Instrumentation
{
    public class CountStat : IStat
    {
        public int Value { get; private set; }
        public void Add() => this.Value++;
        public void Set(int value) => this.Value = value;
        public string GetDescription() => this.Value.ToString();
        public bool ShouldShow() => true;
    }
}
