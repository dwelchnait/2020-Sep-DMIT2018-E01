
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#region Additional Namespaces
using ChinookSystem.Entities;
using ChinookSystem.DAL;
using ChinookSystem.ViewModels;
#endregion

namespace ChinookSystem.BLL
{
    public class ArtistController
    {
        public List<SelectionList> Artists_List()
        {
            //due to the fact that the entities wil be internal
            //  you will NOT be able to use the entity definitions (classes)
            //  as the return datatypes

            //instead, we will create views within the ViewModel classes that will
            //  contain the data definitions for your return datatypes

            //to fill these view model classes, we will use Linq queries
            //Linq quereis return their data as IEnumerable or IQueryable datasets
            //you can use var when declaring your query recieving variables
            //the recieving variables can then be converted to a List<T>
            //this Linq query below uses the query syntax method

            using (var context = new ChinookSystemContext())
            {
                //Linq to Entity queries
                //where is the access to the application library system entities
                //they are in your context class ChinookSystemContext -> context
                //the entities are accessed by your DbSet<T> -> Artists
                //x represents any occurance (instance) in the DbSet<T>
                var results = from x in context.Artists
                              select new SelectionList
                              {
                                  ValueId = x.ArtistId,
                                  DisplayText = x.Name
                              };
                //sort the dataset by a specified property
                return results.OrderBy(x => x.DisplayText).ToList();

                //return context.Artists.ToList();  // in CPSC1517 entities were public
            }
        }
    }
}
