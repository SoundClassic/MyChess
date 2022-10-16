using System.Web.Http;
using System.Web.Http.Description;
using ChessAPI.Models;

namespace ChessAPI.Controllers
{
    public class GamesController : ApiController
    {
        //GET: chess/Games
        public Game GetGames()
        {
            return Logic.GetCurrentGame();
        }

        //GET: chess/Games/5
        [ResponseType(typeof(Game))]
        public IHttpActionResult GetGame(int id)
        {
            Game game = Logic.GetGame(id);
            if(game == null)
            {
                return NotFound();
            }

            return Ok(game);
        }

        //GET: chess/Games/5/e2e4
        [ResponseType(typeof(Game))]
        public IHttpActionResult GetMove(int id, string move)
        {
            Game game = Logic.MakeMove(id, move);
            if (game == null)
            {
                return NotFound();
            }

            return Ok(game);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
