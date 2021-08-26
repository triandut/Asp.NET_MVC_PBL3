using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    
    public class FeedbackDAO
    {
        FreeCyDB db = new FreeCyDB();
        public List<Feedback> ListAllFeedback()
        {
            return db.Feedbacks.Where(p => p.Status == true).ToList();
        }
    }
}
