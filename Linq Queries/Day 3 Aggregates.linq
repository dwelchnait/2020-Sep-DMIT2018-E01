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

//Aggregrates
//.Count(), .Sum(), .Min(), .Max(), .Average()

//.Sum(), .Min(), .Max(), .Average() require a delegate expression

//query syntax
//  (from ....
//    ...
//    select x.field ).Max()

//method syntax
//   collection.Max(x => x.collectionfield)
//collectionfield could as be a calculation
//   Bill.Sum(x => x.quantity * x.Price)

//IMPORTANT!! aggregrates work ONLY on a collection of data
//            NOT on a single row


//A collection CAN have 0, 1 or more rows
//the delegate of .Sum(), .Max(), .Min(), .Average() CANNOT be null
//.Count() does not need a delegate, it counts occurances

//bad aggregrate
//aggregrate against a single row
from x in Tracks
select new
{
	name = x.Name,
	AveLength = x.Average(x => x.Milliseconds)
}

//good method syntax
//collection is the table Tracks
Tracks.Average(x => x.Milliseconds)

(from x in Tracks
  select x.Milliseconds).Average()

//Can you use a string during certain aggregrates
//yes on Max and Min
Tracks.Max(x => x.Name)

//list all albums showing the title, artist name and various aggregate
//values for albums containing tracks. For each album show the number
// of tracks, the longest track length, the shortest track length, the
//total price of the album tracks, and the average track length

from x in Albums
where x.Tracks.Count() > 1
select new
{
	title = x.Title,
	artist = x.Artist.Name,
	//queryTrackcount = (from y in x.Tracks
	//					select y).Count(),
	queryTrackcount = (from y in Tracks
						where x.AlbumId == y.AlbumId
						select y).Count(),
	avg = x.Tracks.Average(y => y.Milliseconds),
	max = x.Tracks.Max(y => y.Milliseconds),
	min = x.Tracks.Min(y => y.Milliseconds),
	price = x.Tracks.Sum(y => y.UnitPrice),
	badprice = x.Tracks.Count() * .099 //assumes all tracks are the same price
						
}











