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
    public class AlbumController
    {
        #region Queries
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
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<AlbumItem> Album_List()
        {
            using (var context = new ChinookSystemContext())
            {
                var results = from k in context.Albums
                              select new AlbumItem
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
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public AlbumItem Album_FindByID(int albumid)
        {
            using (var context = new ChinookSystemContext())
            {
                var results = (from k in context.Albums
                              where k.AlbumId == albumid
                              select new AlbumItem
                              {
                                  Title = k.Title,
                                  ReleaseYear = k.ReleaseYear,
                                  ReleaseLabel = k.ReleaseLabel,
                                  AlbumId = k.AlbumId,
                                  ArtistId = k.ArtistId
                              }).FirstOrDefault();
                return results;
            }
        }
        #endregion
        #region CRUD: Add, Update and Delete

        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public void Album_Add(AlbumItem item)
        {
            using (var context = new ChinookSystemContext())
            {
                //move the incoming viewmodel instance data into an instance
                //   of the internal entity
                Album newItem = new Album
                {
                    //pkey is identity, not needed
                    Title = item.Title,
                    ArtistId = item.ArtistId,
                    ReleaseYear = item.ReleaseYear,
                    ReleaseLabel = item.ReleaseLabel
                };
                context.Albums.Add(newItem);
                context.SaveChanges();
            }
        }
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void Album_Update(AlbumItem item)
        {
            using (var context = new ChinookSystemContext())
            {
                //move the incoming viewmodel instance data into an instance
                //   of the internal entity
                Album newItem = new Album
                {
                    //pkey is needed for update to find the instance
                    //   on the database
                    AlbumId = item.AlbumId,
                    Title = item.Title,
                    ArtistId = item.ArtistId,
                    ReleaseYear = item.ReleaseYear,
                    ReleaseLabel = item.ReleaseLabel
                };
                context.Entry(newItem).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        //create an overloaded method for the Album_Delete where will recieve an
        //  instance of the table record. The overloaded method will call the
        //  delete method that requires ONLY the pkey.
        [DataObjectMethod(DataObjectMethodType.Delete,false)]
        public void Album_Delete(AlbumItem item)
        {
            Album_Delete(item.AlbumId);
        }

        //this delete cannot be called from the CRUD ODS. The CRUD ODS control passes
        //  in an instance of the record
        public void Album_Delete(int albumid)
        {
            using(var context = new ChinookSystemContext())
            {
                var exists = context.Albums.Find(albumid);
                context.Albums.Remove(exists);
                context.SaveChanges();
            }
        }
        #endregion
    }
}
