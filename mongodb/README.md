
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