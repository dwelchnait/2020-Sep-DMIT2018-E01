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

//comments are entered as C# comments

//hotkeys for comments
//Ctrl + K,C make comment
//Ctrl + K,U uncomment

//there are two styles of coding linq queries
//Query Syntax (very sql-ish)
//Method syntax (very C#-ish)

//in the Expression environment you can code multiple queries
//  BUT you MUST highlight the query to execute  (F5)

//in the Statement environment you can code multiple queries
//  as C# statements and run the entire phyiscal file without
//  highlighting the query

//int the Program environment you can code multiple queries
//   AND class definitions or programs methods which are tested
//   in a Main() program

//Simple selection with sort
//Query Syntax of a query
//from clause is 1st and select clause is last
from AnyRowInCollection in Albums
orderby AnyRowInCollection.Title ascending
select AnyRowInCollection

//Method Syntax
Albums
   .OrderBy (AnyRowInCollection => AnyRowInCollection.Title)


from x in Albums
orderby x.ReleaseYear descending, x.Title ascending
select x

Albums
   .OrderByDescending (x => x.ReleaseYear)
   .ThenBy (x => x.Title)

//Filtering Data
//where clause
//List artists with a Q in their name
Artists
	.Where (x => x.Name.Contains("Q"))

from x in Artists
where x.Name.Contains("Q")
select x

//Show all Albums released in the 90's
Albums
	.Where (x => x.ReleaseYear > 1989 && x.ReleaseYear < 2000)

from x in Albums
where x.ReleaseYear > 1989 && x.ReleaseYear < 2000
select x

//list all customers in alphabetic order by last name
//	who live in the USA. The customer must have an yahoo email

from x in Customers

where x.Country == "USA" &&
		x.Email.Contains("yahoo")
orderby x.LastName
select x

Customers
   .Where (x => ((x.Country == "USA") && x.Email.Contains ("yahoo")))
   .OrderBy (x => x.LastName)








