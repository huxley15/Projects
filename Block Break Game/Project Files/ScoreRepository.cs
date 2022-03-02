using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameProject2
{
    internal class ScoreRepository
    {
        BlockBreakGameScoreDBEntities entities; //declare entities at the global level
        //BlockBreakGameScoreDBEntities entities = new BlockBreakGameScoreDBEntities(); 
        public ScoreRepository()
        {
            entities = new BlockBreakGameScoreDBEntities();
        }
        public void AddScore(PlayerScore obj)
        {
            entities.PlayerScores.Add(obj); //update list
            entities.SaveChanges(); //update database
        }
        
        public void DeleteScore(PlayerScore obj)
        {
            entities.PlayerScores.Remove(obj);
            entities.SaveChanges();
        }
        public PlayerScore FindScore(int id)
        {
            var score = entities.PlayerScores.First(n => n.ID == id);
            if (score != null)
            {
                return score;
            }
            else
                return null;
        }
        public int GetMaxID()
        {
            //find the highest ID in DB list for score submission purposes
            return entities.PlayerScores.Max(n => n.ID);
        }
        public ICollection<PlayerScore> ReadScores()
        {
            //return DB list ordered by score with higher scores on top
            var scoreList = (from s in entities.PlayerScores
                             orderby s.Score descending
                             select s).ToList();
            return scoreList;
        }
        public void UpdateScore(string name, PlayerScore scorechanges)
        {
            //searches database for name entered, primary key was set to player ID for Find method was not able to be used
            PlayerScore scoretoupdate = (from s in entities.PlayerScores
                                         where s.Name == name
                                         select s).FirstOrDefault();
            //only score is being changed when updating the record
            scoretoupdate.Score = scorechanges.Score;
            //save updated score to DB
            entities.SaveChanges();
        }
        public PlayerScore FindScore(string name) //overloaded method to search score lists for player name
        {
            try //had to implement a try catch structure because if the name doesn't exist it assigns var to null which is not allowed
            {
                //search db to see if entered name exists already
                var score = entities.PlayerScores.First(n => n.Name == name);
                if (score != null)
                {
                    return score;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //public int HighScore()
        //{
        //    return entities.PlayerScores.Max(n=>n.Score);
        //}
        public PlayerScore HighPlayer()
        {
            //sort items in db by score from high to low and select the top result
            var top = entities.PlayerScores.Select(x=>new { id=x.ID, name=x.Name, score=x.Score }).OrderByDescending(m=>m.score).FirstOrDefault();
            //pull just the name and score from the result for display purposes
            PlayerScore topplayer = new PlayerScore();
            topplayer.Name = top.name;
            topplayer.Score = top.score;
            return topplayer;
        }
        public int PlayerRanking(string playername)
        {
            //sort the players by score high to low to set up count for score ranking
            var top = entities.PlayerScores.Select(x => new { id = x.ID, name = x.Name, score = x.Score }).OrderByDescending(m => m.score).ToList();
            int ranking = 0;
            for (int i = 0; i < top.Count; i++)
            {
                //start going through the index, starting from the top, and count how many places it takes to get to current player's name
                if (top[i].name == playername)
                {
                    ranking = i+1; //index rankings start at 0 so add 1 to make it match normal ranking standards
                    break; //break out of the loop once the match is found
                }
            }
            
            return ranking;
        }
    }
}
