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

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<TrackItem> Track_List()
        {
            using (var context = new ChinookSystemContext())
            {
                var results = from x in context.Tracks
                              select new TrackItem
                              {

                                  TrackId = x.TrackId,
                                  Name = x.Name,
                                  AlbumId = x.AlbumId,
                                  MediaTypeId = x.MediaTypeId,
                                  GenreId = x.GenreId,
                                  Composer = x.Composer,
                                  Milliseconds = x.Milliseconds,
                                  Bytes = x.Bytes,
                                  UnitPrice = x.UnitPrice
                              };
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public TrackItem Track_Find(int trackid)
        {
            using (var context = new ChinookSystemContext())
            {
                var results = (from x in context.Tracks
                              where x.TrackId == trackid
                              select new TrackItem
                              {
                                  TrackId = x.TrackId,
                                  Name = x.Name,
                                  AlbumId = x.AlbumId,
                                  MediaTypeId = x.MediaTypeId,
                                  GenreId = x.GenreId,
                                  Composer = x.Composer,
                                  Milliseconds = x.Milliseconds,
                                  Bytes = x.Bytes,
                                  UnitPrice = x.UnitPrice
                              }).FirstOrDefault();
                return results;
            }
        }

        //[DataObjectMethod(DataObjectMethodType.Select, false)]
        //public List<TrackItem> Track_GetByAlbumId(int albumid)
        //{
        //    using (var context = new ChinookSystemContext())
        //    {
        //        var results = from aRowOn in context.Tracks
        //                      where aRowOn.AlbumId.HasValue
        //                      && aRowOn.AlbumId == albumid
        //                      select aRowOn;
        //        return results.ToList();
        //    }
        //}

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<TrackList> List_TracksForPlaylistSelection(string tracksby, string arg)
        {
            using (var context = new ChinookSystemContext())
            {
                //is it possible to code this query in a different fashion but obtain the same results
                //AND if possible, WHICH is correct? (Answer: depends on your desired output)

                //var results = from x in context.Tracks
                //              orderby x.Name
                //              where (x.Album.Artist.Name.Contains(arg) && tracksby.Equals("Artist")) ||
                //                      (x.Album.Title.Contains(arg) && tracksby.Equals("Album"))
                //              select new TrackList
                //              {
                //                  TrackID = x.TrackId,
                //                  Name = x.Name,
                //                  Title = x.Album.Title,
                //                  ArtistName = x.Album.Artist.Name,
                //                  MediaName = x.MediaType.Name,
                //                  GenreName = x.Genre.Name,
                //                  Composer = x.Composer,
                //                  Milliseconds = x.Milliseconds,
                //                  Bytes = x.Bytes,
                //                  UnitPrice = x.UnitPrice
                //              };

                IEnumerable<TrackList> results = null;
                if (tracksby.Equals("Artist"))
                {
                    results = from x in context.Tracks
                                  orderby x.Album.Artist.Name, x.Name
                                  where x.Album.Artist.Name.Contains(arg) 
                                  select new TrackList
                                  {
                                      TrackID = x.TrackId,
                                      Name = x.Name,
                                      Title = x.Album.Title,
                                      ArtistName = x.Album.Artist.Name,
                                      MediaName = x.MediaType.Name,
                                      GenreName = x.Genre.Name,
                                      Composer = x.Composer,
                                      Milliseconds = x.Milliseconds,
                                      Bytes = x.Bytes,
                                      UnitPrice = x.UnitPrice
                                  };
                }
                else
                {
                    results = from x in context.Tracks
                                  orderby x.Album.Title, x.Name
                                  where x.Album.Title.Contains(arg)
                                  select new TrackList
                                  {
                                      TrackID = x.TrackId,
                                      Name = x.Name,
                                      Title = x.Album.Title,
                                      ArtistName = x.Album.Artist.Name,
                                      MediaName = x.MediaType.Name,
                                      GenreName = x.Genre.Name,
                                      Composer = x.Composer,
                                      Milliseconds = x.Milliseconds,
                                      Bytes = x.Bytes,
                                      UnitPrice = x.UnitPrice
                                  };
                }


                return results.ToList();
            }
        }//eom
    }
}
