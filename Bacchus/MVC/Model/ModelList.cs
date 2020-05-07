using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bacchus.MVC.Model;

namespace Bacchus.MVC.Model
{
    /// <summary>
    /// The class of <c>ModelList</c>. 
    /// Contains the necessary lists of class which can be used when access to the DB.
    /// </summary>
    public class ModelList
    {
        
        public List<Articles> Articles { get; set; }
        public List<Familles> Familles { get; set; }
        public List<Marques> Marques { get; set; }
        public List<SousFamilles> SousFamilles { get; set; }

        public ModelList()
        {
            Articles = new List<Articles>();
            Familles = new List<Familles>();
            Marques = new List<Marques>();
            SousFamilles = new List<SousFamilles>();
        }

    }
}
