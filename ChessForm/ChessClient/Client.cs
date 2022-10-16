using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace ChessForm.ChessClient
{
    public class Client
    {
        public string URL { get; private set; }
        protected int currentGameId;

        public Client(string host)
        {
            URL = host;
        }

        public GameInfo GetCurrentGame(int id)
        {
            currentGameId = id;
            GameInfo game = new GameInfo(ParseJson(CallServer()));
            return game;
        }

        public GameInfo SendMove(string move)
        {
            return new GameInfo(ParseJson(CallServer(move)));
        }

        /// <summary>
        /// Связь с сервером
        /// </summary>
        /// <param name="param">Параметр</param>
        /// <returns></returns>
        public string CallServer(string param = "")
        {
            WebRequest request = WebRequest.Create(URL + currentGameId + "/" + param);
            WebResponse response = request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
                return reader.ReadToEnd();
        }

        /// <summary>
        /// Парсинг Json
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private NameValueCollection ParseJson(string json)
        {
            json = json.Replace("\\", "");
            NameValueCollection list = new NameValueCollection();
            string pattern = @"""(\w+)\"":""?([^,""]*)""?";
            foreach (Match match in Regex.Matches(json, pattern))
            {
                if (match.Groups.Count == 3)
                    list.Add(match.Groups[1].Value, match.Groups[2].Value);
            }
            return list;
        }
    }
}
