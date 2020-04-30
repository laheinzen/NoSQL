
# MongoDB

## Preparando o banco

```bash
docker volume create --name=mongodata 
docker run -d -p 27017-27019:27017-27019 --name mongodb -v mongodata:/data/db mongo
sudo docker exec -it mongodb bash
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

>         "_id" : ObjectId("5ea904df836b1cf0412da7a3"),

>         "name" : "Mike",

>         "species" : "Hamster"

>  }

PS: Esqueci de coletar a saída na primeira vez em rodei. Acho que não era esse o objeto que retornava

# 4. Identifique o ID para o Gato Kilha.
> db.pets.find({"name": "Kilha", "species": "Gato"},{_id: 1} )
# { "_id" : ObjectId("5ea9065e836b1cf0412da7a5") }

# 5. Faça uma busca pelo ID e traga o Hamster Mike
> var iDForMikeTheHamster = db.pets.findOne({"name": "Mike", "species": "Hamster"},{_id: 1})
> db.pets.find({_id: iDForMikeTheHamster._id})
# "_id" : ObjectId("5ea904df836b1cf0412da7a3"), "name" : "Mike", "species" : "Hamster" }

# 6. Use o find para trazer todos os Hamsters
> db.pets.find({"species": "Hamster"})
# { "_id" : ObjectId("5ea904df836b1cf0412da7a3"), "name" : "Mike", "species" : "Hamster" }
# { "_id" : ObjectId("5ea9d7880952b97c36253789"), "name" : "Frodo", "species" : "Hamster" }

# 7. Use o find para listar todos os pets com nome Mike

# 8. Liste apenas o documento que é um Cachorro chamado Mike
