using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.Entities;
using ChinookSystem.DAL;
using ChinookSystem.ViewModels;
using System.ComponentModel;
#endregion

namespace ChinookSystem.BLL
{
    [DataObject]
    public class TrackController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<SongItem> BLL_Query(string artist)
        {
            using (var context = new ChinookSystemContext())
            {
                //within LinqPad we are using Linq to Sql
                //within our application library we are using Linq to Entity
                //  the Entity DbSet<T> is located in the context class
                var results = from x in context.Tracks
                              where x.Album.Artist.Name == artist
                              orderby x.Name
                              select new SongItem
                              {
                                  Song = x.Name,
                                  Title = x.Album.Title,
                                  Year = x.Album.ReleaseYear,
                                  Length = x.Milliseconds,
                                  Price = x.UnitPrice,
                                  Genre = x.Genre.Name
                              };
                return results.ToList();
            }
        }
    }
}
