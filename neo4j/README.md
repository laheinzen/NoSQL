
# Neo4j

## Exercício 1 - Retrieving Nodes

### Exercise 1.1: Retrieve all nodes from the database

`MATCH (n) RETURN n`

### Exercise 1.2: Examine the data model for the graph

`call db.schema.visualization()`

Hum... Aparentemente é a mesma reposta que lá adiante. Então imagino que seja apenas para visualizar o grafo e tentar entender. Já havia observado como é intressante a visualização.

### Exercise 1.3: Retrieve all Person nodes

`MATCH (p:Person) RETURN p`

### Exercise 1.4: Retrieve all Movie nodes

`MATCH (m:Movie) RETURN m`

## Exercício 2 – Filtering queries using property values

### Exercise 2.1: Retrieve all movies that were released in a specific year

`MATCH (m:Movie {released: 2000}) RETURN m`

### Exercise 2.2: View the retrieved results as a table

```javascript
{
  "title": "Jerry Maguire",
  "tagline": "The rest of his life begins now.",
  "released": 2000
}

{
  "title": "The Replacements",
  "tagline": "Pain heals, Chicks dig scars... Glory lasts forever",
  "released": 2000
}

{
  "title": "Cast Away",
  "tagline": "At the edge of the world, his journey begins.",
  "released": 2000
}
```

### Exercise 2.3: Query the database for all property keys

`call db.propertyKeys()`

### Exercise 2.4: Retrieve all Movies released in a specific year, returning their titles

`MATCH (m:Movie {released: 2000}) RETURN m.title`

### Exercise 2.5: Display title, released, and tagline values for every Movie node in the graph

`MATCH (m:Movie) RETURN m.title, m.released, m.tagline`

### Exercise 2.6: Display more user-friendly headers in the table

```javascript
MATCH (m:Movie) RETURN m.title AS `Movie Title`, m.released AS `Release Year`, m.tagline As Tagline
```

## Exercício 3 - Filtering queries using relationships

### 3.1: Display the schema of the database

`call db.schema.visualization()`

### Exercise 3.2: Retrieve all people who wrote the movie Speed Racer

`MATCH (n:Person)-[:WROTE]-(:Movie{title:"Speed Racer"}) RETURN n`

### Exercise 3.3: Retrieve all movies that are connected to the person Tom Hanks

`MATCH (m:Movie)--(p:Person {name:"Tom Hanks"}) RETURN m`

### Exercise 3.4: Retrieve information about the relationships Tom Hanks had with the set of movies retrieved earlier

`MATCH (m:Movie)-[relationship]-(p:Person {name:"Tom Hanks"}) RETURN m,type(relationship)`

### Exercise 3.5: Retrieve information about the roles that Tom Hanks acted in

`MATCH (m:Movie)-[relationship:ACTED_IN]-(p:Person {name:"Tom Hanks"}) RETURN m,relationship.roles`

## Exercício 4 – Filtering queries using WHERE clause

### Exercise 4.1: Retrieve all movies that Tom Cruise acted in

`MATCH (m:Movie)-[relationship:ACTED_IN]-(p:Person) WHERE p.name = "Tom Cruise" RETURN m`

### Exercise 4.2: Retrieve all people that were born in the 70’s

`MATCH (p:Person) WHERE (p.born >= 1970 AND p.born < 1980) RETURN p`

### Exercise 4.3: Retrieve the actors who acted in the movie The Matrix who were born after 1960

`MATCH (m:Movie)-[relationship:ACTED_IN]-(p:Person) WHERE (m.title = "The Matrix" AND p.born > 1960) RETURN p`

### Exercise 4.4: Retrieve all movies by testing the node label and a property

Todos os filmes lançados em 2000

`MATCH (m) WHERE m:Movie AND m.released = 2000 RETURN m`

### Exercise 4.5: Retrieve all people that wrote movies by testing the relationship between two nodes

`MATCH (m:Movie)-[relationship]-(p:Person) WHERE type(relationship) = "WROTE" return p`

### Exercise 4.6: Retrieve all people in the graph that do not have a property

Pessoas sem data de nascimento cadastrada

`MATCH (p:Person) WHERE NOT exists(p.born) RETURN p`

### Exercise 4.7: Retrieve all people related to movies where the relationship has a property

Todos os papéis em filmes
`MATCH (p:Person)-[relationship]->(m:Movie) WHERE exists(relationship.roles) RETURN p, relationship, m`

### Exercise 4.8: Retrieve all actors whose name begins with James

`MATCH (a:Person)-[:ACTED_IN]->(m:Movie) WHERE a.name STARTS WITH 'James' RETURN a`

### Exercise 4.9: Retrieve all REVIEW relationships from the graph with filtered results

Todas as críticas que contém a palavra 'cool'

`MATCH (c:Person)-[r:REVIEWED]->(m:Movie) WHERE toLower(r.summary) CONTAINS 'cool' RETURN r`

### Exercise 4.10: Retrieve all people who have produced a movie, but have not directed a movie

`MATCH (p:Person)-[:PRODUCED]->(m:Movie) WHERE NOT ((p)-[:DIRECTED]->(:Movie)) RETURN p, m`

### Exercise 4.11: Retrieve the movies and their actors where one of the actors also directed the movie

`MATCH (p1:Person)-[:ACTED_IN]->(m:Movie)<-[:ACTED_IN]-(p2:Person) WHERE exists( (p2)-[:DIRECTED]->(m) ) RETURN  p1, p2, m`

### Exercise 4.12: Retrieve all movies that were released in a set of years

`MATCH (m:Movie) WHERE m.released in [2002, 2004, 2006] RETURN m`

### Exercise 4.13: Retrieve the movies that have an actor’s role that is the name of the movie

`MATCH (a:Person)-[r:ACTED_IN]->(m:Movie) WHERE m.title in r.roles RETURN m`

## Exercício 5 – Controlling query processing

### Exercise 5.1: Retrieve data using multiple MATCH patterns

Filmes onde Tom Hanks atuou, com nome do diretor e outros atores
`MATCH (a:Person)-[:ACTED_IN]->(m:Movie)<-[:DIRECTED]-(d:Person), (aa:Person)-[:ACTED_IN]->(m) WHERE a.name = 'Tom Hanks' RETURN m, d, aa`

### Exercise 5.2: Retrieve particular nodes that have a relationship

`MATCH (p1:Person)-[:FOLLOWS]-(p2:Person) WHERE p1.name = 'James Thompson' RETURN p1, p2`

### Exercise 5.3: Modify the query to retrieve nodes that are exactly three hops away

`MATCH (p1:Person)-[:FOLLOWS*3]-(p2:Person) WHERE p1.name = 'James Thompson' RETURN p1, p2`

### Exercise 5.4: Modify the query to retrieve nodes that are one and two hops away

`MATCH (p1:Person)-[:FOLLOWS*1..2]-(p2:Person) WHERE p1.name = 'James Thompson' RETURN p1, p2`

### Exercise 5.5: Modify the query to retrieve particular nodes that are connected no matter how many hops are required

`MATCH (p1:Person)-[:FOLLOWS*]-(p2:Person) WHERE p1.name = 'James Thompson' RETURN p1, p2`

### Exercise 5.6: Specify optional data to be retrieved during the query

Filmes onde 'Tom' atuou e opcionalmente 'Tom' dirigiu
`MATCH (p:Person) WHERE p.name STARTS WITH 'Tom' OPTIONAL MATCH (p)-[:DIRECTED]->(m:Movie) RETURN p, m`

### Exercise 5.7: Retrieve nodes by collecting a list

Atores e seus filmes
`MATCH (a:Person)-[:ACTED_IN]->(m:Movie) RETURN a , collect(m)`

### 5.8: Retrieve all movies that Tom Cruise has acted in and the co-actors that acted in the same movie by collecting a list

Esse aqui tive não estava no PDF, mas não foi difícil de achar
`MATCH (t:Person)-[:ACTED_IN]->(m:Movie)<-[:ACTED_IN]-(a:Person) WHERE t.name ='Tom Cruise' RETURN m, collect(a)`

### Exercise 5.9: Retrieve nodes as lists and return data associated with the corresponding lists

### Exercise 5.10: Retrieve nodes and their relationships as lists

### Exercise 5.11: Retrieve the actors who have acted in exactly five movies

### Exercise 5.12: Retrieve the movies that have at least 2 directors with other optional data

## Exercício 6 – Controlling results returned

### Exercise 6.1: Execute a query that returns duplicate records

### Exercise 6.2: Modify the query to eliminate duplication

### Exercise 6.3: Modify the query to eliminate more duplication

### Exercise 6.4: Sort results returned

### Exercise 6.5: Retrieve the top 5 ratings and their associated movies

### Exercise 6.6: Retrieve all actors that have not appeared in more than 3 movies

## Exercício 7 – Working with cypher data

### Exercise 7.1: Collect and use lists

### Exercise 7.2: Collect a list

### Exercise 7.3: Unwind a list

### Exercise 7.4: Perform a calculation with the date type

## Exercício 8 – Creating nodes

### Exercise 8.1: Create a Movie node

### Exercise 8.2: Retrieve the newly-created node

### Exercise 8.3: Create a Person node

### Exercise 8.4: Retrieve the newly-created node

### Exercise 8.5: Add a label to a node

### Exercise 8.6: Retrieve the node using the new label

### Exercise 8.7: Add the Female label to selected nodes

### Exercise 8.8: Retrieve all Female nodes

### Exercise 8.9: Remove the Female label from the nodes that have this label

### Exercise 8.10: View the current schema of the graph

### Exercise 8.11: Add properties to a movie

### Exercise 8.12: Retrieve an OlderMovie node to confirm the label and properties

### Exercise 8.13: Add properties to the person, Robin Wright

### Exercise 8.14: Retrieve an updated Person node

### Exercise 8.15: Remove a property from a Movie node

### Exercise 8.16: Retrieve the node to confirm that the property has been removed

### Exercise 8.17: Remove a property from a Person node

### Exercise 8.18: Retrieve the node to confirm that the property has been removed

## Exercício 9 – Creating relationships

### Exercise 9.1: Create ACTED_IN relationships

### Exercise 9.2: Create DIRECTED relationships

### Exercise 9.3: Create a HELPED relationship

### Exercise 9.4: Query nodes and new relationships

### Exercise 9.5: Add properties to relationships

### Exercise 9.6: Add a property to the HELPED relationship

### Exercise 9.7: View the current list of property keys in the graph

### Exercise 9.8: View the current schema of the graph

### Exercise 9.9: Retrieve the names and roles for actors

### Exercise 9.10: Retrieve information about any specific relationships

### Exercise 9.11: Modify a property of a relationship

### Exercise 9.12: Remove a property from a relationship

### Exercise 9.13: Confirm that your modifications were made to the graph

## Exercício 10 – Deleting nodes and relationships

### Exercise 10.1: Delete a relationship

### Exercise 10.2: Confirm that the relationship has been deleted

### Exercise 10.3: Retrieve a movie and all of its relationships

### Exercise 10.4: Try deleting a node without detaching its relationships

### Exercise 10.5: Delete a Movie node, along with its relationships

### Exercise 10.6: Confirm that the Movie node has been deleted
