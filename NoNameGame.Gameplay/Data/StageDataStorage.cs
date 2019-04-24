using Newtonsoft.Json;
using System.IO;

namespace NoNameGame.Gameplay.Data
{
    public class StageDataStorage
    {
        private readonly string _stagesPath = "Content\\Stages";

        public StageData Load(int id)
        {
            var path = Path.Combine(_stagesPath, $"stage_{id}.json");
            var json = File.ReadAllText(path);

            return JsonConvert.DeserializeObject<StageData>(json);
        }
    }
}
