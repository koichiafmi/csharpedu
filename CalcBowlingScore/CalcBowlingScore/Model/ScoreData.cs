namespace CalcBowlingScore
{
    public class ScoreData
    {
        public int Index { get; private set; }
        public int[] Pins { get; private set; }

        public ScoreData(int index, int[] pins)
        {
            this.Index = index;
            this.Pins = pins;
        }

        public void InitializePins()
        {
            this.Pins = Common.CreatePinsPerFrame();
        }

        public void SetScore(ThrowData throwData)
        {
            this.Pins[throwData.ThrowCount - 1] = throwData.Pins;
        }
    }
}