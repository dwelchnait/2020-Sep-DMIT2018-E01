<Query Kind="Program">
  <Connection>
    <ID>ecafa140-05d4-414f-af79-0d35352e5dfd</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>.</Server>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>Chinook</Database>
  </Connection>
</Query>

void Main()
{
	//Create a playlist report that shows the playlist name, the number
//of songs on the playlist, the user name belonging to the playlist
//and the songs on the playlist with their Genre.
//Show only playlists with 20 or more songs. Order by the playlist name.
var playlistsize = 15;
var results = from x in Playlists
				where x.PlaylistTracks.Count() >= playlistsize
				orderby x.Name
				select new PlayListItem
				{
					Name = x.Name,
					TrackCount = x.PlaylistTracks.Count(),
					UserName = x.UserName,
					Songs = from y in x.PlaylistTracks
							select new PlayListSong
							{
								Song = y.Track.Name,
								GenreName = y.Track.Genre.Name
							}
				};
results.Dump();
}

// You can define other methods, fields, classes and namespaces here
// define ViewModel classes
public class PlayListSong
{
	public string Song{get;set;}
	public string GenreName{get;set;}
}

public class PlayListItem
{
	public string Name{get;set;}
	public int TrackCount{get;set;}
	public string UserName{get;set;}
	public IEnumerable<PlayListSong> Songs{get;set;}
}



