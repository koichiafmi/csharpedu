namespace CalcBowlingScore
{
    public static class Common
    {
        public const string Image1 = @"..\..\View\Images\1.png";
        public const string Image2 = @"..\..\View\Images\2.png";
        public const string Image3 = @"..\..\View\Images\3.png";
        public const string Image4 = @"..\..\View\Images\4.png";
        public const string Image5 = @"..\..\View\Images\5.png";
        public const string Image6 = @"..\..\View\Images\6.png";
        public const string Image7 = @"..\..\View\Images\7.png";
        public const string Image8 = @"..\..\View\Images\8.png";
        public const string Image9 = @"..\..\View\Images\9.png";
        public const string ImageBar = @"..\..\View\Images\Bar.png";
        public const string ImageGutter = @"..\..\View\Images\Gutter.png";
        public const string ImageSpare = @"..\..\View\Images\Spare.png";
        public const string ImageStrike = @"..\..\View\Images\Strike.png";

        public static string[] CreatePinTexts()
        {
            return new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
        }

        public static int[] CreatePinsPerFrame()
        {
            return new int[] { -1, -1, -1 };
        }
    }
}
