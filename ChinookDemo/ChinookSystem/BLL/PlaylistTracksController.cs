using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.ViewModels;
using ChinookSystem.DAL;
using System.ComponentModel;
using ChinookSystem.Entities;
using DMIT2018Common.UserControls;
#endregion

namespace ChinookSystem.BLL
{
    public class PlaylistTracksController
    {
        public List<UserPlaylistTrack> List_TracksForPlaylist(
            string playlistname, string username)
        {
            using (var context = new ChinookSystemContext())
            {
                var results = from x in context.PlaylistTracks
                              orderby x.TrackNumber
                              where x.Playlist.Name.Equals(playlistname) &&
                                     x.Playlist.UserName.Equals(username)
                              select new UserPlaylistTrack
                              {
                                  TrackID = x.TrackId,
                                  TrackName = x.Track.Name,
                                  TrackNumber = x.TrackNumber,
                                  Milliseconds = x.Track.Milliseconds,
                                  UnitPrice = x.Track.UnitPrice
                              };
                return results.ToList();
            }
        }//eom
        public void Add_TrackToPLaylist(string playlistname, string username, int trackid)
        {
            using (var context = new ChinookSystemContext())
            {
                //the code within this using will be done as a Transaction which means
                //      there will be ONLY ONE .SaveChanges() within this code
                //if the .SaveChanges is NOT executed successful, all work
                //      within this method willbe rollback automatically!!!!

                //trx
                //query: Playlist, see if the list name exists
                //if not
                //  create an instance of Playlist
                //  load instance with data
                //  stage (.Add) the instance for adding
                //  set the tracknumber to 1
                //if yes
                //  check to see if the track already exists on playlist (query)
                //  if found
                //      no: determine the current max tracknumber (query), increment++
                //      yes: throw an error (stop processing the trx) BUSINESS RULE violation
                //create an instance of the PlaylistTrack
                //  load instance with data
                //  stage (.Add) the instance for adding
                //COMMIT the work via entityframework (ADO.NET) to the Database (.SaveChanges)

                int tracknumber = 0;
                PlaylistTrack newTrack = null;
                List<string> errors = new List<string>(); // this is to be used by BusinessRuleException cast
                //.FirstOrDefault() returns the first instance matching the criteria or null
                Playlist exists = (from x in context.Playlists
                                  where x.Name.Equals(playlistname)
                                     && x.UserName.Equals(username)
                                  select x).FirstOrDefault();
                if (exists == null)
                {
                    //if not
                    //exists = new Playlist();
                    //exists.Name = playlistname;
                    //exists.UserName = username;

                    exists = new Playlist()
                    {
                        Name = playlistname,
                        UserName = username
                    };
                    context.Playlists.Add(exists);
                    tracknumber = 1;

                }
                else
                {
                    //exists has the record instance of the playlist
                    //??does the track already exist on this playlist
                    newTrack = (from x in context.PlaylistTracks
                                where x.Playlist.Name.Equals(playlistname)
                                   && x.Playlist.UserName.Equals(username)
                                   && x.TrackId == trackid
                                select x).FirstOrDefault();
                    if (newTrack == null)
                    {
                        //track not on playlist AND THEREFORE can be added
                        tracknumber = (from x in context.PlaylistTracks
                                       where x.Playlist.Name.Equals(playlistname)
                                         && x.Playlist.UserName.Equals(username)
                                       select x.TrackNumber).Max();
                        tracknumber++;
                    }
                    else
                    {
                        //track already on playlist AND THEREFORE CANNOT be added
                        //a duplicate track violates the BUSINESS RULE for playlist

                        //throw an exception
                        //throw new Exception("Track already on the playlist. Duplicates not allowed");

                        //use the BusinessRuleException class to throw all errors at once
                        //this class takes in a List<string> representing all errors to be handled
                        //in this example it is overkill to use BRE.
                        //what you have to imagine is there maybe be several of these errors
                        errors.Add("Track already on the playlist. Duplicates not allowed");
                    }
                }

                //handle the creation of the PlaylistTrack record

                //all validation has been passed?
                if (errors.Count > 0)
                {
                    //no, at least one error was found
                    throw new BusinessRuleException("Adding a Track", errors);
                }
                else
                {
                    context.SaveChanges();
                }
            } //the close of the using ensures that the sql connection closes properly
        }//eom
        public void MoveTrack(string username, string playlistname, int trackid, int tracknumber, string direction)
        {
            using (var context = new ChinookSystemContext())
            {
                //trx
                //check to see if the playlist exsits
                //no: error exception
                //yes:
                //      check to see if song exists
                //      no: error exception
                //      yes:
                //          up
                //          check to see if the song is at the top
                //              yes: error exception
                //              no:
                //                  move record 4; above record 3
                //                  find record above (tracknumber -1)
                //                  change above tracknumber (increment)
                //                  move track tracknumber (decrement)
                //          down
                //          check to see if the song is at the bottom
                //              yes: error exception
                //              no:
                //                  move record 4; below record 5
                //                  find record below (tracknumber +1)
                //                  change below tracknumber (decrement)
                //                  move track tracknumber (increment)
                //can I stage/commit
                //no: throw the exception (BRE)
                //yes:
                //    stage updates
                //    commit

                PlaylistTrack moveTrack = null;
                PlaylistTrack otherTrack = null; 
                List<string> errors = new List<string>(); // this is to be used by BusinessRuleException cast
                //.FirstOrDefault() returns the first instance matching the criteria or null
                Playlist exists = (from x in context.Playlists
                                   where x.Name.Equals(playlistname)
                                      && x.UserName.Equals(username)
                                   select x).FirstOrDefault();
                if (exists == null)
                {
                    errors.Add("Play list does not exist");
                }
                else
                {
                    moveTrack = (from x in context.PlaylistTracks
                                 where x.Playlist.Name.Equals(playlistname)
                                    && x.Playlist.UserName.Equals(username)
                                    && x.TrackId == trackid
                                 select x).FirstOrDefault();
                    if (moveTrack == null)
                    {
                        errors.Add("Play list track does not exist");
                    }
                    else
                    {
                        if (direction.Equals("up"))
                        {
                            //up
                            //this means the tracknumber on the selected track
                            //      will decrease (track 4 -> 3)

                            //prep for move, check if the track is at the top
                            if (moveTrack.TrackNumber == 1)
                            {
                                errors.Add("Play list track already at the top.");
                            }
                            else
                            {
                                //manipulate the actal records
                                otherTrack = (from x in context.PlaylistTracks
                                              where x.Playlist.Name.Equals(playlistname)
                                                 && x.Playlist.UserName.Equals(username)
                                                 && x.TrackNumber == (moveTrack.TrackNumber - 1)
                                              select x).FirstOrDefault();
                                if(otherTrack == null)
                                {
                                    errors.Add("Missng required othe song track record.");
                                }
                                else
                                {
                                    moveTrack.TrackNumber -= 1;
                                    otherTrack.TrackNumber += 1;
                                }
                            }
                        }
                        else
                        {
                            //down
                            //this means the tracknumber on the selected track
                            //      will increase (track 4 -> 5)

                            //prep for move, check if the track is at the bottom
                            int songcount = (from x in context.PlaylistTracks
                                             where x.Playlist.Name.Equals(playlistname)
                                                && x.Playlist.UserName.Equals(username)
                                             select x).Count();
                            if (moveTrack.TrackNumber == songcount)
                            {
                                errors.Add("Play list track already at the bottom.");
                            }
                            else
                            {
                                //manipulate the actal records
                                otherTrack = (from x in context.PlaylistTracks
                                              where x.Playlist.Name.Equals(playlistname)
                                                 && x.Playlist.UserName.Equals(username)
                                                 && x.TrackNumber == (moveTrack.TrackNumber + 1)
                                              select x).FirstOrDefault();
                                if (otherTrack == null)
                                {
                                    errors.Add("Missng required othe song track record.");
                                }
                                else
                                {
                                    moveTrack.TrackNumber += 1;
                                    otherTrack.TrackNumber -= 1;
                                }
                            }
                        }
                    }
                }
                //check to see if an error has occured, if so throw
                if (errors.Count > 0)
                {
                    throw new BusinessRuleException("Move Track", errors);
                }
                else
                {
                    //stage
                    //these are updates
                    //a) you can stage an update to alter the entire entity (CRUD)
                    //b) you can stage an update to an entity referencing JUST the property
                    //      to be modified
                    //in this example do stage (b)
                    context.Entry(moveTrack).Property("TrackNumber").IsModified = true;
                    context.Entry(otherTrack).Property(nameof(PlaylistTrack.TrackNumber)).IsModified = true;
                    //commit
                    context.SaveChanges();
                }
            }
        }//eom


        public void DeleteTracks(string username, string playlistname, List<int> trackstodelete)
        {
            using (var context = new ChinookSystemContext())
            {
               //code to go here


            }
        }//eom
    }
}
