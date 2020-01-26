namespace CalcBowlingScore
{
    public class ThrowData
    {
        public int ThrowCount { get; private set; }
        public int Pins { get; private set; }

        public ThrowData(int count, int pins)
        {
            this.ThrowCount = count;
            this.Pins = pins;
        }
    }
}
