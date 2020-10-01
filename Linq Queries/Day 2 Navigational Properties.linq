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

//Show all albums for U2, order by year, by title

//if this was sql, 
//	inner join Albums and Artist,
//	where name = U2

//demonstrate the use of navigational properties
//	to access data on another table
//child to parent navighational property generates an inner join

//query syntax
from x in Albums
orderby x.ReleaseYear, x.Title
where x.Artist.Name.Equals("U2")
select x

//method syntax
Albums
   .OrderBy (x => x.ReleaseYear)
   .ThenBy (x => x.Title)
   .Where (x => x.Artist.Name.Equals ("U2"))

//List all tracks of the genre Jazz. Order by track name.
from x in Tracks
orderby x.Name
where x.Genre.Name.Equals("jazz")
select x

Tracks
	.OrderBy(x => x.Name)
	.Where(x => x.Genre.Name.Equals("jazz"))

//List all tracks for the artist AC/DC. Order by album title and track name

from x in Tracks
orderby x.Album.Title, x.Name
where x.Album.Artist.Name.Equals("AC/DC")
select x

Tracks
   .OrderBy (x => x.Album.Title)
   .ThenBy (x => x.Name)
   .Where (x => x.Album.Artist.Name.Equals ("AC/DC"))










