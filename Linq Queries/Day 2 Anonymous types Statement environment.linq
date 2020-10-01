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

//Anonymous Types

//This allows creating a query that will display
//	data not on the from table source AND/OR a
//	subset of data from you table source

//List all tracks by AC/DC; ordered by track name.
//Display the track name, the album title, the album release year
//the track length, price and genre

//general go to rule is child to parent is easy using navigational properties
//
//		Album			Genre
//             Track

//the default datatype for the query is either <IQueryable> or <IEnumerable>
//new creates another instance (record, row, ...)	of the collection 
//within the new coding block you will enter the data that you wish to returned
var results = from x in Tracks
				where x.Album.Artist.Name == "AC/DC"
				orderby x.Name
				select new
				{
					Song = x.Name,
					Title = x.Album.Title,
					Year = x.Album.ReleaseYear,
					Length = x.Milliseconds,
					Price = x.UnitPrice,
					Genre = x.Genre.Name
				};

//in the Statement environment you are expected to use C# statements
//results of a query needs to be placed in a variable.
//you will display the results of your query which exists in a
//     local variable using the Linq extension method .Dump()
//on does NOT need to highlight the query to execute if
//		there are multiple queries within the phyiscal file
//queries will execute from top to bottom in the file

results.Dump();

//List all customers in alphabetic order (last name, first name) who
//have yahoo email. Show only the customer full name (last, first) and
//their city and email.
var test = from x in Customers
				orderby x.LastName, x.FirstName
				where x.Email.Contains("yahoo")
				select new
				{
					fullname = x.LastName + ", " + x.FirstName,
					city = x.City,
					email = x.Email
				};
test.Dump();




