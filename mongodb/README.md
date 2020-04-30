
# MongoDB

## Preparando o banco

```bash
docker volume create --name=mongodata
docker pull mongodb
docker run -d -p 27017-27019:27017-27019 --name mongodb -v mongodata:/data/db mongo
docker exec -it mongodb bash
mongo
```

## Exercício 1- Aquecendo com os pets

Insira os seguintes registros no MongoDB e em seguida responda as questões abaixo:

`use petshop`
> switched to db petshop

`db.pets.insert({name: "Mike", species: "Hamster"})`
> WriteResult({ "nInserted" : 1 })

`db.pets.insert({name: "Dolly", species: "Peixe"})`
> WriteResult({ "nInserted" : 1 })

`db.pets.insert({name: "Kilha", species: "Gato"})`
> WriteResult({ "nInserted" : 1 })

`db.pets.insert({name: "Mike", species: "Cachorro"})`
> WriteResult({ "nInserted" : 1 })

`db.pets.insert({name: "Sally", species: "Cachorro"})`
> WriteResult({ "nInserted" : 1 })

`db.pets.insert({name: "Chuck", species: "Gato"})`
> WriteResult({ "nInserted" : 1 })

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