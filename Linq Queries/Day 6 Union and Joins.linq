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

//Union
//will combine 2 or more queries into one result
//each query needs to have the same number of columns
//each query should have the same associated data within a column
//each query column needs to be the same datatype between queries

//syntax
//  (query1).union(query2).union(queryN).OrderBy(first sort).ThenBy(nth sort)
//sorting is done using the column name form the union

//Generate a report covering all albums showing their title
//  their track count, the album price, and average track length.
//Order by the number of tracks on the album, the by album title.

//Albums.Count().Dump();

var unionresults = (from x in Albums
				where x.Tracks.Count() > 0
			   select new
			   {
			   	title = x.Title,
				trackcount = x.Tracks.Count(),
				albumprice = x.Tracks.Sum(y => y.UnitPrice),
				averagelength = x.Tracks.Average(y => y.Milliseconds/1000.0)
				}).Union(from x in Albums
							where x.Tracks.Count() == 0
						   select new
						   {
						   	title = x.Title,
							trackcount = 0,
							albumprice = 0.00m,
							averagelength = 0.0
							}).OrderBy(y => y.trackcount).ThenBy(y => y.title);
unionresults.Dump();

//Joins

//http://dotnetlearners.com/linq

//AVOID joins if there is an acceptable navigational property available
//joins can be used where navigational property DO NOT EXIST
//joins can be used between associated entities
//   scenario fkey <==> pkey

//left side of the join, as the support data
//right side of the join, as the processing record collection

//unfortunately, in Chinook entities (relationships) are all navigational properties
//****Assume*** there is NO navigational property between artist and album

//syntax
//   leftside-entity join rightside-entity 
//			on leftside-entity.field == rightside-entity.field

//leftside (supportside) => artist
//rightside (processcollection) => album


var joinResults = from supportside in Artists
				  join processcollection in Albums
				    on supportside.ArtistId equals processcollection.ArtistId
				select new
				{
					title = processcollection.Title,
					year = processcollection.ReleaseYear,
					label = processcollection.ReleaseLabel == null ? "Unknown"
								: processcollection.ReleaseLabel,
					artist = supportside.Name,
					trackcount = processcollection.Tracks.Count()
				};
joinResults.Dump();
				
				
				
				
				
				
				
				
				