<Query Kind="Expression">
  <Connection>
    <ID>ecafa140-05d4-414f-af79-0d35352e5dfd</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>.</Server>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>Chinook</Database>
  </Connection>
</Query>

//Nested Queries
//simply put, they are query within a query

//List all sales support employees showing their fullname (lastname, firstname),
//their title and the number of customers each support. Order by fullname.
//In addition, show a list of the customers for each employee. Show the 
//customer fullname, city and state


from x in Employees
where x.Title.Contains("Support")
orderby x.LastName, x.FirstName
select new {
		name = x.LastName + ", " + x.FirstName,
		title = x.Title,
		//clientcount = x.SupportRepCustomers.Count()
		//clientcount = (from y in x.SupportRepCustomers
		//			  select y).Count()
		clientcount = (from y in Customers
						where y.SupportRepId == x.EmployeeId
					  select y).Count(),
		//this is a subquest (nested query)
		clientlist = from y in x.SupportRepCustomers
					 orderby y.LastName + ", " + y.FirstName
					 select new
					 {
					 	name = y.LastName + ", " + y.FirstName,
						city = y.City,
						state = y.State
					 }
}


//create a list of albums showing their title and artist.
//Show albums with 20 or more tracks only.
//Show the songs on the album (name and length).

from x in Albums
where x.Tracks.Count() >= 20
orderby x.Title
select new
{
	title = x.Title,
	artist = x.Artist.Name,
	albumsize = x.Tracks.Count(),
	songs = from y in x.Tracks
			select new
			{
				name = y.Name,
				length = y.Milliseconds
			}
}

//Create a playlist report that shows the playlist name, the number
//of songs on the playlist, the user name belonging to the playlist
//and the songs on the playlist with their Genre.
//Show only playlists with 20 or more songs. Order by the playlist name.

from x in Playlists
where x.PlaylistTracks.Count() >= 15
orderby x.Name
select new
{
	name = x.Name,
	totalsongs = x.PlaylistTracks.Count(),
	username = x.UserName,
	songs = from y in x.PlaylistTracks
			select new
			{
				name = y.Track.Name,
				genre = y.Track.Genre.Name
			}
}











