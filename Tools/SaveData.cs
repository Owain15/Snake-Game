using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game.Tools
{
    internal class SaveData
    {

        private string HighScorePath = $"C:\\Users\\ojdav\\visual studio files\\Snake Game\\Snake Game\\Resorses\\HighScores.txt";


        public void SaveTopThreeInJason(HighScoreCard[] TopThreeScores)
        {
            string SerialisedScores = Newtonsoft.Json.JsonConvert.SerializeObject(TopThreeScores, Newtonsoft.Json.Formatting.Indented);
            using (StreamWriter SaveScores = new StreamWriter(HighScorePath))
            { SaveScores.Write(SerialisedScores); }
        }
        public HighScoreCard[] GetSavedScores()
        {
           

            HighScoreCard[] TopThreeScores = new HighScoreCard[3];

            string ReceivedData;

            using (StreamReader SaveScores = new StreamReader(HighScorePath))
            { ReceivedData = SaveScores.ReadToEnd(); }

            TopThreeScores = Newtonsoft.Json.JsonConvert.DeserializeObject<HighScoreCard[]>(ReceivedData);

            return TopThreeScores;
        }
        public void ResetHighScore()
        {
            HighScoreCard BlankCard = new HighScoreCard(" ", 0, 0, false);
            List<HighScoreCard> Reset = new List<HighScoreCard>() { BlankCard, BlankCard, BlankCard };

            //JsonSerializerSettings SerializerSettings = new JsonSerializerSettings();
            //SerializerSettings.TypeNameHandling = TypeNameHandling.All;

            string SerializedReset = Newtonsoft.Json.JsonConvert.SerializeObject(Reset, Newtonsoft.Json.Formatting.Indented);
            using (StreamWriter SaveScores = new StreamWriter(HighScorePath)) { SaveScores.Write(SerializedReset); }
        }
        
        private void JsonTestScript()
        {
            int TestScore = 10;
            GameSetting TestSettings = new GameSetting(200, 4, true);

            var SerializedObject = Newtonsoft.Json.JsonConvert.SerializeObject(TestSettings, Newtonsoft.Json.Formatting.Indented);

            string HighScorePath = $"C:\\Users\\ojdav\\visual studio files\\Snake Game\\Snake Game\\Resorses\\HighScores.txt";
            using (StreamWriter SaveScores = new StreamWriter(HighScorePath))
            { SaveScores.WriteLine(SerializedObject); }

            string RecevedData;

            using (StreamReader SaveScores = new StreamReader(HighScorePath))
            { RecevedData = SaveScores.ReadToEnd(); }

            var RecevedTestSettings = Newtonsoft.Json.JsonConvert.DeserializeObject<GameSetting>(RecevedData);


        }
    }
}
