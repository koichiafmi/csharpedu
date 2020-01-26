using System.Collections.Generic;
using System.Linq;

namespace CalcBowlingScore
{
    public class ScoreController
    {
        private List<ScoreData> scoreDataList;

        public ScoreController()
        {
            this.createScoreDataList();
        }

        private void createScoreDataList()
        {
            this.scoreDataList = new List<ScoreData>()
            {
                new ScoreData(1, Common.CreatePinsPerFlame()),
                new ScoreData(2, Common.CreatePinsPerFlame()),
                new ScoreData(3, Common.CreatePinsPerFlame()),
                new ScoreData(4, Common.CreatePinsPerFlame()),
                new ScoreData(5, Common.CreatePinsPerFlame()),
                new ScoreData(6, Common.CreatePinsPerFlame()),
                new ScoreData(7, Common.CreatePinsPerFlame()),
                new ScoreData(8, Common.CreatePinsPerFlame()),
                new ScoreData(9, Common.CreatePinsPerFlame()),
                new ScoreData(10, Common.CreatePinsPerFlame())
            };
        }

        public void Initialize()
        {
            this.scoreDataList.ForEach(e => e.InitializePins());
        }

        public void SetPins(int index, ThrowData throwData)
        {
            this.GetScoreInFlame(index).SetScore(throwData);
        }

        public int GetTotalPinsInFlame(int index)
        {
            return this.GetScoreInFlame(index).Pins.Where(e => e >= 0).Sum();
        }

        private ScoreData GetScoreInFlame(int index)
        {
            return this.scoreDataList.Find(e => e.Index == index);
        }

        public List<string> GetScores()
        {
            return this.calculateScores();
        }

        private List<string> calculateScores()
        {
            var scoreArray = new int[21]
            {
                this.scoreDataList[0].Pins[0],
                this.scoreDataList[0].Pins[1],
                this.scoreDataList[1].Pins[0],
                this.scoreDataList[1].Pins[1],
                this.scoreDataList[2].Pins[0],
                this.scoreDataList[2].Pins[1],
                this.scoreDataList[3].Pins[0],
                this.scoreDataList[3].Pins[1],
                this.scoreDataList[4].Pins[0],
                this.scoreDataList[4].Pins[1],
                this.scoreDataList[5].Pins[0],
                this.scoreDataList[5].Pins[1],
                this.scoreDataList[6].Pins[0],
                this.scoreDataList[6].Pins[1],
                this.scoreDataList[7].Pins[0],
                this.scoreDataList[7].Pins[1],
                this.scoreDataList[8].Pins[0],
                this.scoreDataList[8].Pins[1],
                this.scoreDataList[9].Pins[0],
                this.scoreDataList[9].Pins[1],
                this.scoreDataList[9].Pins[2]
            };

            var scoreTextList = new List<string>();
            int currentScore = 0;
            int updatedScore = 0;
            for(var i = 0; i < 10; i++)
            {
                string scoreText = string.Empty;
                if (scoreArray[i * 2] >= 0)
                {
                    updatedScore = getScore(scoreArray, scoreDataList[i].Index, currentScore);
                    scoreText = updatedScore.ToString();
                }

                scoreTextList.Add(scoreText);
                currentScore = updatedScore;
            }

            return scoreTextList;           
        }

        private static int getScore(int[] scoreArray, int flameIndex, int currentScore)
        {
            int index = (2 * (flameIndex - 1));
            int score = currentScore;

            // 1投目
            if (scoreArray[index] >= 0)
            {
                score += scoreArray[index];
            }

            // 2投目
            if (scoreArray[index + 1] >= 0)
            {
                score += scoreArray[index + 1];
            }

            // 3投目（フレーム10のみ）
            if (flameIndex == 10)
            {
                if (scoreArray[index + 2] >= 0)
                {
                    score += scoreArray[index + 2];
                }
            }

            // フレーム1 ～ フレーム9
            if (flameIndex < 10)
            {
                // 1投目がストライク
                if (scoreArray[index] == 10)
                {
                    // 次フレームの1投目
                    if (scoreArray[index + 2] >= 0)
                    {
                        score += scoreArray[index + 2];
                    }

                    if (scoreArray[index + 2] == 10)
                    {
                        // 次次フレームの1投目
                        if (scoreArray[index + 4] >= 0)
                        {
                            score += scoreArray[index + 4];
                        }
                    }
                    else
                    {
                        // 次フレームの2投目
                        if (scoreArray[index + 3] >= 0)
                        {
                            score += scoreArray[index + 3];
                        }
                    }
                }
                // 2投目がスペア
                else if (scoreArray[index] + scoreArray[index + 1] == 10)
                {
                    // 次フレームの1投目
                    if (scoreArray[index + 2] >= 0)
                    {
                        score += scoreArray[index + 2];
                    }
                }
            }

            return score;
        }
    }
}
