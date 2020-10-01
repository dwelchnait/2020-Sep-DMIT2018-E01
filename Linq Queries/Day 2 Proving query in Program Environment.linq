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
	//think of Main as your display project
	string inputValue = "AC/DC";
	var results = BLL_Query(inputValue);
	results.Dump();
}

// Define other methods, classes and namespaces here
public class SongItem
{
	public string Song{get;set;}
	public string Title{get;set;}
	public int Year{get;set;}
	public int Length{get;set;}
	public decimal Price{get;set;}
	public string Genre{get;set;}
}

public List<SongItem> BLL_Query(string artist)
{
	//change the Anonymous datatype to a strongly-type datatype
	//define a class and use it with the new
	var results = from x in Tracks
				where x.Album.Artist.Name == artist
				orderby x.Name
				select new SongItem
				{
					Song = x.Name,
					Title = x.Album.Title,
					Year = x.Album.ReleaseYear,
					Length = x.Milliseconds,
					Price = x.UnitPrice,
					Genre = x.Genre.Name
				};
	return results.ToList();
}



