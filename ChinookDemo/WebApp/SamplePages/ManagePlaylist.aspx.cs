﻿using System;
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
            //code to go here
 
        }

        protected void MoveUp_Click(object sender, EventArgs e)
        {
            //code to go here
 
        }

        protected void MoveTrack(int trackid, int tracknumber, string direction)
        {
            //call BLL to move track
 
        }


        protected void DeleteTrack_Click(object sender, EventArgs e)
        {
            //code to go here
 
        }

        protected void TracksSelectionList_ItemCommand(object sender, 
            ListViewCommandEventArgs e)
        {
            //code to go here
            
        }

    }
}