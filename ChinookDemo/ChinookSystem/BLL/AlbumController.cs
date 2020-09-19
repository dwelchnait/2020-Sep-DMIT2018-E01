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
    public class AlbumController
    {
        public List<AlbumArtist> Album_FindByArtist(int artistid)
        {
            using(var context = new ChinookSystemContext())
            {
                var results = from k in context.Albums
                              where k.ArtistId == artistid
                              select new AlbumArtist
                              {
                                  Title = k.Title,
                                  ReleaseYear = k.ReleaseYear,
                                  ReleaseLabel = k.ReleaseLabel,
                                  AlbumId = k.AlbumId,
                                  ArtistId = k.ArtistId
                              };
                return results.ToList();
            }
        }
    }
}
