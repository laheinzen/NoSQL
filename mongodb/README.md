
# MongoDB

## Preparando o banco

```bash
docker volume create --name=mongodata
docker pull mongodb
docker run -d -p 27017-27019:27017-27019 --name mongodb -v mongodata:/data/db mongo
docker exec -it mongodb bash
mongo
```

## Exercício 1 - Aquecendo com os pets

Insira os seguintes registros no MongoDB e em seguida responda as questões abaixo:

```bash
use petshop
db.pets.insert({name: "Mike", species: "Hamster"})
db.pets.insert({name: "Dolly", species: "Peixe"})
db.pets.insert({name: "Kilha", species: "Gato"})
db.pets.insert({name: "Mike", species: "Cachorro"})
db.pets.insert({name: "Sally", species: "Cachorro"})
db.pets.insert({name: "Chuck", species: "Gato"})
```

### 1. Adicione outro Peixe e um Hamster com nome Frodo

`db.pets.insert({name: "Frodo", species: "Peixe"})`
> WriteResult({ "nInserted" : 1 })

`db.pets.insert({name: "Frodo", species: "Hamster"})`
> WriteResult({ "nInserted" : 1 })

### 2. Faça uma contagem dos pets na coleção

`db.pets.count()`
> 8

### 3. Retorne apenas um elemento o método prático possível

`db.pets.findOne()`

> {
>
>         "_id" : ObjectId("5eaa45efe82b4e5aeb093981"),
> 
>         "name" : "Mike",
> 
>         "species" : "Hamster"
>
> }

### 4. Identifique o ID para o Gato Kilha.

`db.pets.find({"name": "Kilha", "species": "Gato"},{_id: 1} )`
> { "_id" : ObjectId("5eaa45efe82b4e5aeb093983") }

### 5. Faça uma busca pelo ID e traga o Hamster Mike

`var iDForMikeTheHamster = db.pets.findOne({"name": "Mike", "species": "Hamster"},{_id: 1})`

`db.pets.find({_id: iDForMikeTheHamster._id})`

> { "_id" : ObjectId("5eaa45efe82b4e5aeb093981"), "name" : "Mike", "species" : "Hamster" }

### 6. Use o find para trazer todos os Hamsters

`db.pets.find({"species": "Hamster"})`
> { "_id" : ObjectId("5eaa45efe82b4e5aeb093981"), "name" : "Mike", "species" : "Hamster" }
> 
> { "_id" : ObjectId("5eaa46d90c71cb400eb2cca2"), "name" : "Frodo", "species" : "Hamster" }

### 7. Use o find para listar todos os pets com nome Mike

`db.pets.find({"name": "Mike"})`
> { "_id" : ObjectId("5eaa45efe82b4e5aeb093981"), "name" : "Mike", "species" : "Hamster" }
>
> { "_id" : ObjectId("5eaa45efe82b4e5aeb093984"), "name" : "Mike", "species" : "Cachorro" }

### 8. Liste apenas o documento que é um Cachorro chamado Mike

`db.pets.find({"name": "Mike", "species": "Cachorro"})`
> { "_id" : ObjectId("5eaa45efe82b4e5aeb093984"), "name" : "Mike", "species" : "Cachorro" }

## Exercício 2 - Mama mia!

### 1. Liste/Conte todas as pessoas que tem exatamente 99 anos. Você pode usar um count para indicar a quantidade

`db.italians.count({"age":99})`
> 0

### 2. Identifique quantas pessoas são elegíveis atendimento prioritário (pessoas com mais de 65 anos)

`db.italians.count({"age": {$gt:65} })`
> 1755

### 3. Identifique todos os jovens (pessoas entre 12 a 18 anos)

`db.italians.count({"age": {$gte:12,$lte:18} })`
> 869

### 4. Identifique quantas pessoas tem gatos, quantas tem cachorro e quantas não tem nenhum dos dois

`db.italians.count({"cat": {$exists: true} })`
>6037

`db.italians.count({"dog": {$exists: true} })`
>4047

`db.italians.count({"cat": {$exists: false} , "dog": {$exists: false} })`
> 2347

### 5. Liste/Conte todas as pessoas acima de 60 anos que tenham gato

`db.italians.count({ "age": {$gt:60}, "cat": {$exists:true} })`
> 1420

### 6. Liste/Conte todos os jovens com cachorro

`db.italians.count({"age": {$gte:12,$lte:18}, "dog": {$exists:true} })`
> 348

### 7. Utilizando o $where, liste todas as pessoas que tem gato e cachorro

Primeiro trazemos apenas cinco, para não ser muita coisa

`db.italians.find({ $where: "this.cat && this.dog" }).limit(5)`
> { "_id" : ObjectId("5eab60299fb59243e5f97e2f"), "firstname" : "Anna", "surname" : "Silvestri", "username" : "user108", "age" : 49, "email" : "Anna.Silvestri@outlook.com", "bloodType" : "B-", "id_num" : "733473363650", "registerDate" : ISODate("2019-11-09T11:53:45.799Z"), "ticketNumber" : 4464, "jobs" : [ "Relações Internacionais", "Engenharia de Biossistemas" ], "favFruits" : [ "Mamão", "Banana" ], "movies" : [ { "title" : "Três Homens em Conflito (1966)", "rating" : 1.4 }, { "title" : "Parasita (2019)", "rating" : 2.09 }, { "title" : "Um Estranho no Ninho (1975)", "rating" : 3.49 } ], "cat" : { "name" : "Stefano", "age" : 9 }, "dog" : { "name" : "Mario", "age" : 16 } }
{ "_id" : ObjectId("5eab602a9fb59243e5f97e37"), "firstname" : "Emanuele", "surname" : "De Rosa", "username" : "user116", "age" : 49, "email" : "Emanuele.De Rosa@yahoo.com", "bloodType" : "AB-", "id_num" : "863568413866", "registerDate" : ISODate("2015-01-13T04:13:01.215Z"), "ticketNumber" : 7861, "jobs" : [ "Fabricação Mecânica" ], "favFruits" : [ "Uva", "Banana", "Mamão" ], "movies" : [ { "title" : "Cidade de Deus (2002)", "rating" : 0.31 }, { "title" : "O Senhor dos Anéis: O Retorno do Rei (2003)", "rating" : 0.2 }, { "title" : "Vingadores: Ultimato (2019)", "rating" : 1.06 }, { "title" : "Intocáveis (2011)", "rating" : 4.26 }, { "title" : "Clube da Luta (1999)", "rating" : 2.46 } ], "cat" : { "name" : "Giacomo", "age" : 4 }, "dog" : { "name" : "Alberto", "age" : 4 } }
{ "_id" : ObjectId("5eab602a9fb59243e5f97e38"), "firstname" : "Maurizio", "surname" : "Marchetti", "username" : "user117", "age" : 33, "email" : "Maurizio.Marchetti@live.com", "bloodType" : "B+", "id_num" : "566630828226", "registerDate" : ISODate("2013-07-29T23:21:44.574Z"), "ticketNumber" : 2142, "jobs" : [ "Gestão Desportiva e de Lazer", "Engenharia Metalúrgica" ], "favFruits" : [ "Uva", "Tangerina", "Goiaba" ], "movies" : [ { "title" : "Parasita (2019)", "rating" : 3.33 }, { "title" : "Um Sonho de Liberdade (1994)", "rating" : 0.89 } ], "cat" : { "name" : "Cristina", "age" : 6 }, "dog" : { "name" : "Antonio", "age" : 5 } }
{ "_id" : ObjectId("5eab602a9fb59243e5f97e3a"), "firstname" : "Ilaria", "surname" : "Milani", "username" : "user119", "age" : 50, "email" : "Ilaria.Milani@gmail.com", "bloodType" : "O+", "id_num" : "538660582716", "registerDate" : ISODate("2011-02-23T11:45:31.707Z"), "ticketNumber" : 2078, "jobs" : [ "Teologia", "Engenharia Mecânica" ], "favFruits" : [ "Banana", "Kiwi", "Pêssego" ], "movies" : [ { "title" : "Os Bons Companheiros (1990)", "rating" : 0.11 }, { "title" : "A Vida é Bela (1997)", "rating" : 4.08 }, { "title" : "O Resgate do Soldado Ryan (1998)", "rating" : 1.2 }, { "title" : "12 Homens e uma Sentença (1957)", "rating" : 1.92 } ], "father" : { "firstname" : "Matteo", "surname" : "Milani", "age" : 88 }, "cat" : { "name" : "Patrizia", "age" : 3 }, "dog" : { "name" : "Sonia", "age" : 14 } }
{ "_id" : ObjectId("5eab602a9fb59243e5f97e3d"), "firstname" : "Mattia", "surname" : "Conti", "username" : "user122", "age" : 45, "email" : "Mattia.Conti@outlook.com", "bloodType" : "AB-", "id_num" : "138306201627", "registerDate" : ISODate("2008-04-04T22:42:55.168Z"), "ticketNumber" : 9040, "jobs" : [ "Eletrotécnica Industrial", "Publicidade e Propaganda" ], "favFruits" : [ "Banana" ], "movies" : [ { "title" : "Gladiador (2000)", "rating" : 4.88 }, { "title" : "O Senhor dos Anéis: O Retorno do Rei (2003)", "rating" : 1.56 }, { "title" : "1917 (2019)", "rating" : 4.54 }, { "title" : "Batman: O Cavaleiro das Trevas (2008)", "rating" : 1.3 } ], "father" : { "firstname" : "Paolo", "surname" : "Conti", "age" : 72 }, "cat" : { "name" : "Mattia", "age" : 6 }, "dog" : { "name" : "Sara", "age" : 3 } }

Agora contamos quantos retornam
`db.italians.find({ $where: "this.cat && this.dog" }).count()`
> 2431

### 8. Liste todas as pessoas mais novas que seus respectivos gatos

### 9. Liste as pessoas que tem o mesmo nome que seu bichano (gatou ou cachorro)

### 10. Projete apenas o nome e sobrenome das pessoas com tipo de sangue de fator RH negativo

### 11. Projete apenas os animais dos italianos. Devem ser listados os animais com nome e  idade. Não mostre o identificado do mongo (ObjectId)

### 12. Quais são as 5 pessoas mais velhas com sobrenome Rossi?

### 13. Crie um italiano que tenha um leão como animal de estimação. Associe um nome e idade ao bichano

### 14. Infelizmente o Leão comeu o italiano. Remova essa pessoa usando o Id

### 15. Passou um ano. Atualize a idade de todos os italianos e dos bichanos em 1

### 16. O Corona Vírus chegou na Itália e misteriosamente atingiu pessoas somente com gatos e de 66 anos. Remova esses italianos

### 17. Utilizando o framework agregate, liste apenas as pessoas com nomes iguais a sua respectiva mãe e que tenha gato ou cachorro

### 18. Utilizando aggregate framework, faça uma lista de nomes única de nomes. Faça isso usando apenas o primeiro nome

### 19. Agora faça a mesma lista do item acima, considerando nome completo 

### 20. Procure pessoas que gosta de Banana ou Maçã, tenham cachorro ou gato, mais de 20 e menos de 60 anos