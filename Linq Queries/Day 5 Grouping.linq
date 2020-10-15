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

//Grouping

//basically, grouping is the technique of placing a large
//pile of data into smaller piles of data depending on
//a criteria

//navigational properties allow for natural grouping of
//parent to child (Pkey/fkey) collections
//  pinstance.childnavproperty.Count() counts all the
//     child records associated with the parent instance

//problem: What if ther is no navigational property for
//         the grouping of the data collection

//to solve the problem, you can use the group by clause to create
//a set of smaller collections based on the desired criteria

//Grouping is NOT the same as Ordering

//IT is important to remember that once the smaller groups
//are create AND saved, ALL processing of the data piles MUST
//use the smaller groups as the collection reference

//syntax
//   group placeholdername by criteria [into group-reference-name]

//the placeholder instance is one record from the original pile
//of data
//the criteria can be:
//  a) single attribute ...
//  b) muliple attributes   new{...., ...., ....}
//  c) a class classname

//when you group your results are place in a pile of data
//having 2 components
//  a) key component
//  b) the data component

//You can place the groups results into a temporary holding area
//by using into group-reference-name

//Report albums by ReleaseYear showing the year and 
//the number of albums for the year. Order by the
//most albums, the by the year within the count.

//my process to creating group queries

//  a) create and display the grouping
from x in Albums
group x by x.ReleaseYear into gYear
select gYear

//  b) create the reporting row for a group
from x in Albums
group x by x.ReleaseYear into gYear
select new
{
	year = gYear.Key,
	albumcount = gYear.Count()
}

//  c) complete any additional report customization
from x in Albums
group x by x.ReleaseYear into gYear
orderby gYear.Count() descending, gYear.Key
select new
{
	year = gYear.Key,
	albumcount = gYear.Count()
}

//Report albums by ReleaseYear showing the year and 
//the number of albums for the year. Order by the
//most albums, the by the year within the count.
//Report the album title, artist name and number of
//album tracks. Report ONLY albums of the 90s.

//the process of filtering for the 90s can be done
// a) on the original pile of data
// b) on the grouped piles of data
from x in Albums
//where x.ReleaseYear > 1989 && x.ReleaseYear < 2000
group x by x.ReleaseYear into gYear
where gYear.Key > 1989 && gYear.Key <2000
orderby gYear.Count() descending, gYear.Key
select new
{
	year = gYear.Key,
	albumcount = gYear.Count(),
	albumdata = from grow in gYear
				select new
				{
					title = grow.Title,
					artist = grow.Artist.Name,
					trackcount =
						grow.Tracks.Count(trk => trk.AlbumId == grow.AlbumId)
				}
}

//List tracks for albums produced after 2010 by Genre Name.
//Count Trakcs for the Name.
from trk in Tracks
where trk.Album.ReleaseYear > 2010
group trk by trk.Genre.Name into gTemp
orderby gTemp.Count()
select new
{
	genre = gTemp.Key,
	numberof = gTemp.Count()
	}

//same report BUT use the entity as the group criteria

//when you group on an entity, the entire entity instances
//   becomes the content of your key

//to reference a particular attribute of the key entity
//  use normal object referencing (dot operator)
from trk in Tracks
where trk.Album.ReleaseYear > 2010
group trk by trk.Genre into gTemp
orderby gTemp.Count()
select new
{
	genre = gTemp.Key.Name,
	numberof = gTemp.Count()
	}

from trk in Tracks
where trk.Album.ReleaseYear > 2010
group trk by new{trk.GenreId, trk.Genre.Name} into gTemp
orderby gTemp.Count()
select new
{
	genre = gTemp.Key.Name,
	numberof = gTemp.Count()
	}

//Using group techniques, create a list of customers by
//   employee support individual showing the employee name-phone
//   (last, first (phone)); the number of customers
//   for this employee; and a customer list for the employee.
//   In the customer list show the state, city and customer name
//   (last, first). Order the customer list by state then city.









