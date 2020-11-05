using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additonal Namespaces
using ChinookSystem.BLL;
using ChinookSystem.ViewModels;
//using WebApp.Security;
#endregion

namespace WebApp.SamplePages
{
    public partial class ManagePlaylist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TracksSelectionList.DataSource = null;
        }

        #region Error Handling
        protected void SelectCheckForException(object sender,
                                       ObjectDataSourceStatusEventArgs e)
        {
            MessageUserControl.HandleDataBoundException(e);
        }
        protected void InsertCheckForException(object sender,
                                              ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception == null)
            {
                MessageUserControl.ShowInfo("Success", "Album has been added.");
            }
            else
            {
                MessageUserControl.HandleDataBoundException(e);
            }
        }
        protected void UpdateCheckForException(object sender,
                                               ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception == null)
            {
                MessageUserControl.ShowInfo("Success", "Album has been updated.");
            }
            else
            {
                MessageUserControl.HandleDataBoundException(e);
            }
        }
        protected void DeleteCheckForException(object sender,
                                                ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception == null)
            {
                MessageUserControl.ShowInfo("Success", "Album has been removed.");
            }
            else
            {
                MessageUserControl.HandleDataBoundException(e);
            }
        }

        #endregion


        protected void ArtistFetch_Click(object sender, EventArgs e)
        {               
            TracksBy.Text = "Artist";
            SearchArg.Text = ArtistName.Text;
            if (string.IsNullOrEmpty(ArtistName.Text))
            {
                MessageUserControl.ShowInfo("Search entry error",
                    "Enter an artist name or partial artist name. Press the artist Fetch button");
                SearchArg.Text = "dgcudfk";
            }

            //to force the ListView to rebind (to execute again)
            //Note there is NO DataSource assignment as that is
            //     accomplished using the ODS and a DataSourceID  parameter
            //      on the ListView control
            TracksSelectionList.DataBind();
          }

        protected void MediaTypeFetch_Click(object sender, EventArgs e)
        {

            TracksBy.Text = "MediaType";
            SearchArg.Text = MediaTypeDDL.SelectedValue;
            TracksSelectionList.DataBind();

        }

        protected void GenreFetch_Click(object sender, EventArgs e)
        {
            TracksBy.Text = "Genre";
            SearchArg.Text = GenreDDL.SelectedValue;
            TracksSelectionList.DataBind();
        }

        protected void AlbumFetch_Click(object sender, EventArgs e)
        {
            TracksBy.Text = "Album";
            SearchArg.Text = AlbumTitle.Text;
            if (string.IsNullOrEmpty(AlbumTitle.Text))
            {
                MessageUserControl.ShowInfo("Search entry error",
                    "Enter an album title or partial album title. Press the album Fetch button");
                SearchArg.Text = "dgcudfk";
            }

            //to force the ListView to rebind (to execute again)
            //Note there is NO DataSource assignment as that is
            //     accomplished using the ODS and a DataSourceID  parameter
            //      on the ListView control
            TracksSelectionList.DataBind();
        }

        protected void PlayListFetch_Click(object sender, EventArgs e)
        {
            //security is yet to be inmplemented
            //the username will come from the system via the currently logged user
            //temporarily we will hard code the username
            string username = "HansenB";

            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Playlist name entry error",
                    "Missing playlist name for search. Enter playlist the press button");
            }
            else
            {
                //how do we do error handling using MessageUserControl if the
                //  code executing is NOT part of ODS
                // you could use Try/Catch (BUT we won't)
                // if you examine the source code for MessageUserControl, you will
                //  find embedded within the code the Try/Catch
                //the syntax:
                //  MessageUserControl.TryRun( () => { coding block});
                //  MessageUserControl.TryRun( () => { coding block},"success title",
                //                                           "success message");

                MessageUserControl.TryRun(() => {
                    //standard lookup
                    //connect to BLL controller
                    PlaylistTracksController sysmgr = new PlaylistTracksController();
                    //issue BLL call
                    List<UserPlaylistTrack> info = sysmgr.List_TracksForPlaylist
                        (PlaylistName.Text, username);
                    //assign data to control
                    PlayList.DataSource = info;
                    //bind data to control
                    PlayList.DataBind();
                },"PlayList","View the current requested playlist");

                
            }
        }

        protected void MoveDown_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Track Movement",
                    "You must have a play list name.");
            }
            else
            {
                if (PlayList.Rows.Count == 0)
                {
                    MessageUserControl.ShowInfo("Track Movement",
                        "You must have a play list showing.");
                }
                else
                {
                    //the user can select a song to move
                    CheckBox songSelected = null; //reference point to a control
                    int rowsSelected = 0; //count number of songs selected
                    int trackid = 0; // trackid of the song to move
                    int tracknumber = 0; //tracknumber of the song to move

                    //traverse the song list
                    //only 1 song is allowed to be selected
                    for (int index = 0; index < PlayList.Rows.Count; index++)
                    {
                        //point to a checkbox on the gridview row
                        songSelected = PlayList.Rows[index].FindControl("Selected") as CheckBox;
                        //Selected??
                        if (songSelected.Checked)
                        {
                            rowsSelected++;
                            trackid = int.Parse((PlayList.Rows[index].FindControl("TrackID") as Label).Text);
                            tracknumber = int.Parse((PlayList.Rows[index].FindControl("TrackNumber") as Label).Text);
                        }
                    }

                    //did you select a single row??
                    if (rowsSelected != 1)
                    {
                        MessageUserControl.ShowInfo("Track Movement",
                            "You must select a single song to row");
                    }
                    else
                    {
                        //is this the bottom of the list of song
                        if (tracknumber == PlayList.Rows.Count)
                        {
                            MessageUserControl.ShowInfo("Track Movement",
                            "Song is at the bottom of the list already. No move is necessary");
                        }
                        else
                        {
                            //move the track
                            MoveTrack(trackid, tracknumber, "down");
                        }
                    }
                }
            }

        }

        protected void MoveUp_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Track Movement",
                    "You must have a play list name.");
            }
            else
            {
                if (PlayList.Rows.Count == 0)
                {
                    MessageUserControl.ShowInfo("Track Movement",
                        "You must have a play list showing.");
                }
                else
                {
                    //the user can select a song to move
                    CheckBox songSelected = null; //reference point to a control
                    int rowsSelected = 0; //count number of songs selected
                    int trackid = 0; // trackid of the song to move
                    int tracknumber = 0; //tracknumber of the song to move

                    //traverse the song list
                    //only 1 song is allowed to be selected
                    for (int index = 0; index < PlayList.Rows.Count; index++)
                    {
                        //point to a checkbox on the gridview row
                        songSelected = PlayList.Rows[index].FindControl("Selected") as CheckBox;
                        //Selected??
                        if (songSelected.Checked)
                        {
                            rowsSelected++;
                            trackid = int.Parse((PlayList.Rows[index].FindControl("TrackID") as Label).Text);
                            tracknumber = int.Parse((PlayList.Rows[index].FindControl("TrackNumber") as Label).Text);
                        }
                    }

                    //did you select a single row??
                    if (rowsSelected != 1)
                    {
                        MessageUserControl.ShowInfo("Track Movement",
                            "You must select a single song to row");
                    }
                    else
                    {
                        //is this the top of the list of song
                        if (tracknumber == 1)
                        {
                            MessageUserControl.ShowInfo("Track Movement",
                            "Song is at the top of the list already. No move is necessary");
                        }
                        else
                        {
                            //move the track
                            MoveTrack(trackid, tracknumber, "up");
                        }
                    }
                }
            }
 
        }

        protected void MoveTrack(int trackid, int tracknumber, string direction)
        {
            string username = "HansenB";
            //call BLL to move track
            MessageUserControl.TryRun(() => {
                PlaylistTracksController sysmgr = new PlaylistTracksController();
                sysmgr.MoveTrack(username, PlaylistName.Text, trackid, tracknumber, direction);
                //refresh the playlist
                List<UserPlaylistTrack> info = sysmgr.List_TracksForPlaylist
                   (PlaylistName.Text, username);
                PlayList.DataSource = info;
                PlayList.DataBind();
            }, "Move track on Playlist", "Track has been moved on the playlist");
        }


        protected void DeleteTrack_Click(object sender, EventArgs e)
        {
            //code to go here
 
        }

        protected void TracksSelectionList_ItemCommand(object sender, 
            ListViewCommandEventArgs e)
        {
            string username = "HansenB";
            //validation of incoming data
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Missing data", "Enter the playlist name");
            }
            else
            {
                //Reminder: MessageUserControl will do the error handling
                MessageUserControl.TryRun(() => {
                    //coding block for your logic to be run under the error handling
                    //    control of MessageUserControl

                    //a trx add to the database
                    //connect to controller
                    PlaylistTracksController sysmgr = new PlaylistTracksController();
                    //issue the call to the controller method
                    sysmgr.Add_TrackToPLaylist(PlaylistName.Text, username,
                        int.Parse(e.CommandArgument.ToString()));
                    //refresh the playlist
                    List<UserPlaylistTrack> info = sysmgr.List_TracksForPlaylist
                       (PlaylistName.Text, username);
                    PlayList.DataSource = info;
                    PlayList.DataBind();
                },"Add track to Playlist","Track has been added to the playlist");
            }
            
        }

    }
}