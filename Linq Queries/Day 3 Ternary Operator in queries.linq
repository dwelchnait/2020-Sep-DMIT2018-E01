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

//Ternary Operator with linq queries

//conditional statement using if
//if (condition(s))
//{
//	true path
//}
//else
//{
//	false path
//}

//conditions
// arg1 operator arg2
// instance.method(value)

//ternary operator
// iif (immediate if)
//condition(s) ? true value : false value


//nested ternary operator

//condition(s) ?
//true			(condition(s) ? true value : false value)
//false      :	(condition(s) ? true value : 
//								(condition(s) ? true value : 
//								(condition(s) ? true value : false value)))

//List all albums by release label. Any Album with no lable should
// be indicated as Unknown. List Title and label.

var ternaryResult = from x in Albums
				    orderby x.ReleaseLabel
					select new
					{
						title = x.Title,
						//label = x.ReleaseLabel!=null ? x.ReleaseLabel : "unknown"
						label = x.ReleaseLabel == null ? "unknown" : x.ReleaseLabel  
					};
//ternaryResult.Dump();

//List all Albums showing their Title, ArtistName, and a decades indicator
// (oldies, 70's, 80's, 90's, modern). Order by artist.

var nestedResults = from x in Albums
					orderby x.Artist.Name
					select new
					{
						title = x.Title,
						artistname = x.Artist.Name,
//						year = x.ReleaseYear,
						decade = x.ReleaseYear < 1970 ? "Oldies" :
									x.ReleaseYear >= 1970 && x.ReleaseYear < 1980 ? "70's" :
									x.ReleaseYear >= 1980 && x.ReleaseYear < 1990 ? "80's" :
									x.ReleaseYear >= 1990 && x.ReleaseYear < 2000 ? "90's" : "Modern"
					};
//nestedResults.Dump();

//list all tracks indicating whether they are longer, shorter or equal
//to the average of all track lengths. Show track name and length.

//this query will find the average track length
//query 1: Find the average
//pre processing
var resultavg = Tracks.Average(x => x.Milliseconds);
resultavg.Dump();
//using the results of query 1 in query 2
var variableResults = from x in Tracks
						select new
						{
							Name = x.Name,
							Length = x.Milliseconds,
							indicator = x. Milliseconds < resultavg ? "Shorter" :
										 x.Milliseconds == resultavg ? "Average" : "Longer"
						};
variableResults.Dump();






