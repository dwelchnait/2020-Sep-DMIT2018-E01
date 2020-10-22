<Query Kind="Statements">
  <Connection>
    <ID>ecafa140-05d4-414f-af79-0d35352e5dfd</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>.</Server>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>Chinook</Database>
  </Connection>
</Query>

//Create a list of customer countries
//.Distinct() remove duplicates

var distinctResults = (from x in Customers
						orderby x.Country
						select x.Country).Distinct();
//distinctResults.Dump();

//Customers
//   .OrderBy (x => x.Country)
//   .Select (x => x.Country)
//   .Distinct ()

//boolean filters .Any() and .All()
//.Any() method iterates through the entire collection
//  if any of the items matchthe specified condition, true is returned
//boolean filters return NO data, just true or false
//an instance of the collection that receive a true on the condition
// is selected for processing

//show Genres that have tracks which are not on any playlist
var anyResults = from x in Genres
				 where x.Tracks.Any(trk => trk.PlaylistTracks.Count() == 0)
				 orderby x.Name
				 select new
				 {
				 	genre = x.Name,
					tracksingenre = x.Tracks.Count(),
					boringtracks = from y in x.Tracks
									where y.PlaylistTracks.Count() == 0
									select y.Name
				 };
//anyResults.Dump();

//sometimes you have lists that need to be compared
//Usually you are looking for items that are the same (in both collections) OR
//	  		you are looking for items that are different
//in either case: you are comparing one collection to a second collection

//obtain a distinct list all all playlist tracks for Roberto Almeida (user AlmeidaR)
var almeida = (from x  in PlaylistTracks
				where x.Playlist.UserName.Contains("Almeida")
				select new
				{
					song = x.Track.Name,
					genre = x.Track.Genre.Name,
					id = x.TrackId
					}).Distinct().OrderBy(x => x.song);
//almeida.Dump(); //110

//obtain a distinct list all all playlist tracks for Michelle Brooks (user BrooksM)
var brooks = (from x  in PlaylistTracks
				where x.Playlist.UserName.Contains("Brooks")
				select new
				{
					song = x.Track.Name,
					genre = x.Track.Genre.Name,
					id = x.TrackId
					}).Distinct().OrderBy(x => x.song);
//brooks.Dump();  //88

//starts some comparisons

//List of the tracks that both Roberto and Michelle like
//processing
//   think of the collections as A and B
//   think of the process for each A, check to see if it is any of B

//A will be almeida
//B will be brooks

var likes = almeida
			.Where(a => brooks.Any(b => b.id == a.id))
			.OrderBy(a => a.song)
			.Select(a => a);
//likes.Dump();

//almeida.Intersect(brooks).Dump();

//brooks.Intersect(almeida).Dump();

//differences

//almeida.Except(brooks).Dump();
//
//brooks.Except(almeida).Dump();

var almeidadiff = almeida
			.Where(a => !brooks.Any(b => b.id == a.id))
			.OrderBy(a => a.song)
			.Select(a => a);
//almeidadiff.Dump();

var brooksdiff = brooks
			.Where(a => !almeida.Any(b => b.id == a.id))
			.OrderBy(a => a.song)
			.Select(a => a);
//brooksdiff.Dump();

//All() method iterates through the entire collection to see
//		all items that match the condition
//returns true or false
//an instance of the collection that receives a true on the condition
//   is selected for processing

//show Genres that have a track appearing on all playlists
Genres.Count().Dump();

var popularGenres = from x in Genres
					where x.Tracks.All(trk => trk.PlaylistTracks.Count() > 0)
					orderby x.Name
					select new
					{
						genre = x.Name,
						genretracks = x.Tracks.Count(),
						popTracks = from y in x.Tracks
									where y.PlaylistTracks.Count() > 0
									select new
									{
										song = y.Name
										}
					};
popularGenres.Dump();






