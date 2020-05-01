
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

> { "_id" : ObjectId("5eab602a9fb59243e5f97e37"), "firstname" : "Emanuele", "surname" : "De Rosa", "username" : "user116", "age" : 49, "email" : "Emanuele.De Rosa@yahoo.com", "bloodType" : "AB-", "id_num" : "863568413866", "registerDate" : ISODate("2015-01-13T04:13:01.215Z"), "ticketNumber" : 7861, "jobs" : [ "Fabricação Mecânica" ], "favFruits" : [ "Uva", "Banana", "Mamão" ], "movies" : [ { "title" : "Cidade de Deus (2002)", "rating" : 0.31 }, { "title" : "O Senhor dos Anéis: O Retorno do Rei (2003)", "rating" : 0.2 }, { "title" : "Vingadores: Ultimato (2019)", "rating" : 1.06 }, { "title" : "Intocáveis (2011)", "rating" : 4.26 }, { "title" : "Clube da Luta (1999)", "rating" : 2.46 } ], "cat" : { "name" : "Giacomo", "age" : 4 }, "dog" : { "name" : "Alberto", "age" : 4 } }

> { "_id" : ObjectId("5eab602a9fb59243e5f97e38"), "firstname" : "Maurizio", "surname" : "Marchetti", "username" : "user117", "age" : 33, "email" : "Maurizio.Marchetti@live.com", "bloodType" : "B+", "id_num" : "566630828226", "registerDate" : ISODate("2013-07-29T23:21:44.574Z"), "ticketNumber" : 2142, "jobs" : [ "Gestão Desportiva e de Lazer", "Engenharia Metalúrgica" ], "favFruits" : [ "Uva", "Tangerina", "Goiaba" ], "movies" : [ { "title" : "Parasita (2019)", "rating" : 3.33 }, { "title" : "Um Sonho de Liberdade (1994)", "rating" : 0.89 } ], "cat" : { "name" : "Cristina", "age" : 6 }, "dog" : { "name" : "Antonio", "age" : 5 } }

> { "_id" : ObjectId("5eab602a9fb59243e5f97e3a"), "firstname" : "Ilaria", "surname" : "Milani", "username" : "user119", "age" : 50, "email" : "Ilaria.Milani@gmail.com", "bloodType" : "O+", "id_num" : "538660582716", "registerDate" : ISODate("2011-02-23T11:45:31.707Z"), "ticketNumber" : 2078, "jobs" : [ "Teologia", "Engenharia Mecânica" ], "favFruits" : [ "Banana", "Kiwi", "Pêssego" ], "movies" : [ { "title" : "Os Bons Companheiros (1990)", "rating" : 0.11 }, { "title" : "A Vida é Bela (1997)", "rating" : 4.08 }, { "title" : "O Resgate do Soldado Ryan (1998)", "rating" : 1.2 }, { "title" : "12 Homens e uma Sentença (1957)", "rating" : 1.92 } ], "father" : { "firstname" : "Matteo", "surname" : "Milani", "age" : 88 }, "cat" : { "name" : "Patrizia", "age" : 3 }, "dog" : { "name" : "Sonia", "age" : 14 } }

> { "_id" : ObjectId("5eab602a9fb59243e5f97e3d"), "firstname" : "Mattia", "surname" : "Conti", "username" : "user122", "age" : 45, "email" : "Mattia.Conti@outlook.com", "bloodType" : "AB-", "id_num" : "138306201627", "registerDate" : ISODate("2008-04-04T22:42:55.168Z"), "ticketNumber" : 9040, "jobs" : [ "Eletrotécnica Industrial", "Publicidade e Propaganda" ], "favFruits" : [ "Banana" ], "movies" : [ { "title" : "Gladiador (2000)", "rating" : 4.88 }, { "title" : "O Senhor dos Anéis: O Retorno do Rei (2003)", "rating" : 1.56 }, { "title" : "1917 (2019)", "rating" : 4.54 }, { "title" : "Batman: O Cavaleiro das Trevas (2008)", "rating" : 1.3 } ], "father" : { "firstname" : "Paolo", "surname" : "Conti", "age" : 72 }, "cat" : { "name" : "Mattia", "age" : 6 }, "dog" : { "name" : "Sara", "age" : 3 } }

Agora contamos quantos retornam

`db.italians.count({ $where: "this.cat && this.dog" })`
> 2431

### 8. Liste todas as pessoas mais novas que seus respectivos gatos

Primeiro trazemos apenas cinco, para não ser muita coisa

`db.italians.find({ $where: "this.cat != null && this.age < this.cat.age" }).limit(5)`
> { "_id" : ObjectId("5eab602a9fb59243e5f97e46"), "firstname" : "Valeira", "surname" : "Palmieri", "username" : "user131", "age" : 8, "email" : "Valeira.Palmieri@gmail.com", "bloodType" : "O+", "id_num" : "473813786015", "registerDate" : ISODate("2009-11-15T05:55:31.349Z"), "ticketNumber" : 6904, "jobs" : [ "Alimentos", "Mecatrônica Industrial" ], "favFruits" : [ "Laranja", "Banana", "Banana" ], "movies" : [ { "title" : "Vingadores: Ultimato (2019)", "rating" : 2.2 } ], "cat" : { "name" : "Domenico", "age" : 11 } }
>
> { "_id" : ObjectId("5eab602a9fb59243e5f97e4b"), "firstname" : "Valentina", "surname" : "Ferraro", "username" : "user136", "age" : 12, "email" : "Valentina.Ferraro@live.com", "bloodType" : "AB+", "id_num" : "755153755013", "registerDate" : ISODate("2017-04-11T05:18:22.356Z"), "ticketNumber" : 2225, "jobs" : [ "Gestão Pública", "Banco de Dados" ], "favFruits" : [ "Melancia", "Kiwi" ], "movies" : [ { "title" : "A Felicidade Não se Compra (1946)", "rating" : 1.33 }, { "title" : "1917 (2019)", "rating" : 2.17 }, { "title" : "O Poderoso Chefão (1972)", "rating" : 1.11 }, { "title" : "Intocáveis (2011)", "rating" : 3.1 }, { "title" : "Seven: Os Sete Crimes Capitais (1995)", "rating" : 2.21 } ], "father" : { "firstname" : "Daniela", "surname" : "Ferraro", "age" : 31 }, "cat" : { "name" : "Salvatore", "age" : 15 } }
>
> { "_id" : ObjectId("5eab602a9fb59243e5f97e53"), "firstname" : "Lorenzo", "surname" : "Bellini", "username" : "user144", "age" : 12, "email" : "Lorenzo.Bellini@uol.com.br", "bloodType" : "B-", "id_num" : "885536201608", "registerDate" : ISODate("2020-02-09T18:53:52.264Z"), "ticketNumber" : 8311, "jobs" : [ "Processos Metalúrgicos" ], "favFruits" : [ "Melancia", "Kiwi", "Uva" ], "movies" : [ { "title" : "Guerra nas Estrelas (1977)", "rating" : 0.81 }, { "title" : "O Poderoso Chefão (1972)", "rating" : 1.02 }, { "title" : "O Silêncio dos Inocentes (1991)", "rating" : 1.31 }, { "title" : "Star Wars, Episódio V: O Império Contra-Ataca (1980)", "rating" : 3.32 }, { "title" : "A Origem (2010)", "rating" : 2.8 } ], "mother" : { "firstname" : "Luca", "surname" : "Bellini", "age" : 35 }, "cat" : { "name" : "Alessio", "age" : 16 } }
>
> { "_id" : ObjectId("5eab602a9fb59243e5f97e5e"), "firstname" : "Lorenzo", "surname" : "Russo", "username" : "user155", "age" : 14, "email" : "Lorenzo.Russo@yahoo.com", "bloodType" : "A+", "id_num" : "240871241676", "registerDate" : ISODate("2011-06-12T18:28:34.784Z"), "ticketNumber" : 2273, "jobs" : [ "Hotelaria", "Engenharia Nuclear" ], "favFruits" : [ "Laranja", "Pêssego" ], "movies" : [ { "title" : "Três Homens em Conflito (1966)", "rating" : 4.33 }, { "title" : "Parasita (2019)", "rating" : 0.93 }, { "title" : "Guerra nas Estrelas (1977)", "rating" : 1.62 }, { "title" : "Cidade de Deus (2002)", "rating" : 4.39 }, { "title" : "Interestelar (2014)", "rating" : 3.28 } ], "cat" : { "name" : "Emanuele", "age" : 15 } }
>
> { "_id" : ObjectId("5eab602a9fb59243e5f97e6b"), "firstname" : "Antonio", "surname" : "Neri", "username" : "user168", "age" : 3, "email" : "Antonio.Neri@gmail.com", "bloodType" : "B-", "id_num" : "171588556214", "registerDate" : ISODate("2019-01-02T12:28:53.707Z"), "ticketNumber" : 1764, "jobs" : [ "Oftálmica" ], "favFruits" : [ "Tangerina", "Pêssego" ], "movies" : [ { "title" : "Um Sonho de Liberdade (1994)", "rating" : 1.35 }, { "title" : "Três Homens em Conflito (1966)", "rating" : 0.07 } ], "cat" : { "name" : "Salvatore", "age" : 4 }, "dog" : { "name" : "Sergio", "age" : 16 } }

Agora contamos quantos retornam

`db.italians.count({ $where: "this.cat != null && this.age < this.cat.age" })`
> 675

### 9. Liste as pessoas que tem o mesmo nome que seu bichano (gatou ou cachorro)

Primeiro limitamos a cinco documentos

`db.italians.find({ $where: "(this.cat != null && this.firstname == this.cat.name) || (this.dog != null && this.firstname == this.dog.name)" }).limit(5)`
> { "_id" : ObjectId("5eab602a9fb59243e5f97e3d"), "firstname" : "Mattia", "surname" : "Conti", "username" : "user122", "age" : 45, "email" : "Mattia.Conti@outlook.com", "bloodType" : "AB-", "id_num" : "138306201627", "registerDate" : ISODate("2008-04-04T22:42:55.168Z"), "ticketNumber" : 9040, "jobs" : [ "Eletrotécnica Industrial", "Publicidade e Propaganda" ], "favFruits" : [ "Banana" ], "movies" : [ { "title" : "Gladiador (2000)", "rating" : 4.88 }, { "title" : "O Senhor dos Anéis: O Retorno do Rei (2003)", "rating" : 1.56 }, { "title" : "1917 (2019)", "rating" : 4.54 }, { "title" : "Batman: O Cavaleiro das Trevas (2008)", "rating" : 1.3 } ], "father" : { "firstname" : "Paolo", "surname" : "Conti", "age" : 72 }, "cat" : { "name" : "Mattia", "age" : 6 }, "dog" : { "name" : "Sara", "age" : 3 } }
>
> { "_id" : ObjectId("5eab602a9fb59243e5f97e80"), "firstname" : "Paolo", "surname" : "Sartori", "username" : "user189", "age" : 54, "email" : "Paolo.Sartori@uol.com.br", "bloodType" : "AB-", "id_num" : "043468786457", "registerDate" : ISODate("2014-06-16T21:15:41.861Z"), "ticketNumber" : 691, "jobs" : [ "Produção Têxtil", "Ciências do Consumo" ], "favFruits" : [ "Uva" ], "movies" : [ { "title" : "Guerra nas Estrelas (1977)", "rating" : 4.07 }, { "title" : "Intocáveis (2011)", "rating" : 0.29 }, { "title" : "A Viagem de Chihiro (2001)", "rating" : 0.71 }, { "title" : "O Poderoso Chefão (1972)", "rating" : 2.24 }, { "title" : "Matrix (1999)", "rating" : 1.69 } ], "cat" : { "name" : "Paolo", "age" : 15 } }
>
> { "_id" : ObjectId("5eab602a9fb59243e5f97ec8"), "firstname" : "Cristina", "surname" : "Russo", "username" : "user261", "age" : 3, "email" : "Cristina.Russo@hotmail.com", "bloodType" : "O-", "id_num" : "078827216037", "registerDate" : ISODate("2019-03-15T17:02:40.420Z"), "ticketNumber" : 8168, "jobs" : [ "Manutenção Industrial (T/L)" ], "favFruits" : [ "Maçã", "Melancia", "Maçã" ], "movies" : [ { "title" : "O Senhor dos Anéis: O Retorno do Rei (2003)", "rating" : 2.03 }, { "title" : "Harakiri (1962)", "rating" : 1.04 }, { "title" : "Os Sete Samurais (1954)", "rating" : 1.71 }, { "title" : "12 Homens e uma Sentença (1957)", "rating" : 3.95 }, { "title" : "O Silêncio dos Inocentes (1991)", "rating" : 2.22 } ], "father" : { "firstname" : "Alessandro", "surname" : "Russo", "age" : 34 }, "cat" : { "name" : "Teresa", "age" : 7 }, "dog" : { "name" : "Cristina", "age" : 0 } }
>
> { "_id" : ObjectId("5eab602a9fb59243e5f97ff5"), "firstname" : "Cristina", "surname" : "Caruso", "username" : "user562", "age" : 32, "email" : "Cristina.Caruso@hotmail.com", "bloodType" : "O-", "id_num" : "153057755511", "registerDate" : ISODate("2010-10-05T22:34:30.372Z"), "ticketNumber" : 5149, "jobs" : [ "Ciência da Computação", "Engenharia de Petróleo" ], "favFruits" : [ "Goiaba" ], "movies" : [ { "title" : "Star Wars, Episódio V: O Império Contra-Ataca (1980)", "rating" : 4.03 }, { "title" : "Cidade de Deus (2002)", "rating" : 2.5 }, { "title" : "Pulp Fiction: Tempo de Violência (1994)", "rating" : 0.25 }, { "title" : "Um Estranho no Ninho (1975)", "rating" : 3.01 }, { "title" : "A Vida é Bela (1997)", "rating" : 2.44 } ], "father" : { "firstname" : "Pietro", "surname" : "Caruso", "age" : 68 }, "cat" : { "name" : "Cristina", "age" : 10 }, "dog" : { "name" : "Gianluca", "age" : 13 } }
>
> { "_id" : ObjectId("5eab602a9fb59243e5f98028"), "firstname" : "Giorgio", "surname" : "Caruso", "username" : "user613", "age" : 62, "email" : "Giorgio.Caruso@uol.com.br", "bloodType" : "O+", "id_num" : "664840121000", "registerDate" : ISODate("2016-03-18T05:33:11.112Z"), "ticketNumber" : 5110, "jobs" : [ "Artes Visuais", "Sistemas Elétricos" ], "favFruits" : [ "Mamão", "Kiwi", "Goiaba" ], "movies" : [ { "title" : "Três Homens em Conflito (1966)", "rating" : 3.01 }, { "title" : "Matrix (1999)", "rating" : 1.55 } ], "cat" : { "name" : "Giorgio", "age" : 14 }, "dog" : { "name" : "Claudio", "age" : 1 } }

Agora contamos quantos são

`db.italians.count({ $where: "(this.cat != null && this.firstname == this.cat.name) || (this.dog != null && this.firstname == this.dog.name)" })`
> 115

### 10. Projete apenas o nome e sobrenome das pessoas com tipo de sangue de fator RH negativo

Limitando a cinco documentos

`db.italians.find({"bloodType":{$regex:/\-$/}},{"firstname":1,"surname":1,"bloodType":1, _id: 0}).limit(5)`
> { "firstname" : "Mario", "surname" : "De Santis", "bloodType" : "B-" }
>
> { "firstname" : "Marta", "surname" : "Montanari", "bloodType" : "O-" }
>
> { "firstname" : "Federico", "surname" : "Marchetti", "bloodType" : "O-" }
>
> { "firstname" : "Elisa", "surname" : "De Santis", "bloodType" : "A-" }
>
> { "firstname" : "Anna", "surname" : "Silvestri", "bloodType" : "B-" }

Contando total

`db.italians.count({"bloodType":{$regex:/\-$/}},{"firstname":1,"surname":1,"bloodType":1, _id: 0})`
> 4942

PS: Notei que o 2.11 especifica que não é para colocar o id. Esse aqui,diz que é apenas para lista nome e sobrenome. Assumi que não era pra listar o id. Caso se queira, basta omitir o ', _id: 0'

### 11. Projete apenas os animais dos italianos. Devem ser listados os animais com nome e  idade. Não mostre o identificado do mongo (ObjectId)

Listando cinco documentos

`db.italians.find ( { $or: [{ "cat": { $exists: true } }, { "dog": { $exists: true } } ] } , { "cat":1, "dog":1, _id: 0}).limit(5)`
> { "cat" : { "name" : "Antonella", "age" : 5 } }
>
> { "dog" : { "name" : "Luca", "age" : 6 } }
>
> { "dog" : { "name" : "Ilaria", "age" : 1 } }
>
> { "cat" : { "name" : "Claudio", "age" : 10 } }
>
> { "cat" : { "name" : "Stefano", "age" : 9 }, "dog" : { "name" : "Mario", "age" : 16 } }

Contando número de documentos

`db.italians.count ( { $or: [{ "cat": { $exists: true } }, { "dog": { $exists: true } } ] } , { "cat":1, "dog":1, _id: 0})`
> 7653

### 12. Quais são as 5 pessoas mais velhas com sobrenome Rossi?

Dessa vez, vamos listar apenas alguns atributos. Mais velhos primeiro. Em caso de empate, ordem alfabética

`db.italians.find({"surname":"Rossi"}, {_id:0,"firstname":1,"age":1}).sort({"age":-1, "firstname":1}).limit(5)`
> { "firstname" : "Enzo ", "age" : 79 }
>
> { "firstname" : "Giacomo", "age" : 79 }
>
> { "firstname" : "Riccardo", "age" : 79 }
>
> { "firstname" : "Angelo", "age" : 78 }
>
> { "firstname" : "Michela", "age" : 78 }

### 13. Crie um italiano que tenha um leão como animal de estimação. Associe um nome e idade ao bichano

`db.italians.insert({"firstname":"Donatello","surname":"Splinterson","age":15,"username":"dona_the_ninja_turtle","email":"dona@cowabunga.com", "registeredDate":new Date(),"jobs":["Tartaruga Ninja"],"favFood":["Pizza"],"movies":[{title:"O Rei Leão","rating":10},{"title":"Karate Kid","rating":9},{"title":"O grande dragão branco","rating":7}],"lion":{"name":"Simba","age":7}})`
> WriteResult({ "nInserted" : 1 })

### 14. Infelizmente o Leão comeu o italiano. Remova essa pessoa usando o Id

```javascript
var iDForDonatelloTheNinjaTurtle = db.italians.findOne({"firstname":"Donatello", "surname":"Splinterson"}, {_id:1})
db.italians.deleteOne({_id: iDForDonatelloTheNinjaTurtle._id})
```

> { "acknowledged" : true, "deletedCount" : 1 }

### 15. Passou um ano. Atualize a idade de todos os italianos e dos bichanos em 1

```javascript
var allItalians = db.italians.find()
allItalians.forEach(
    function(italian)
    {
        db.italians.update(
            {_id:italian._id},
            {$inc:{age:1}}
        );
        //Se tem bichando, envelhece
        if(italian.cat){
            db.italians.update(
                {_id:italian._id},
                {$inc:{"cat.age":1}}
            )
        }
        //"bichano" parece ser apenas para gato. Mas cães também envelhecem
        if(italian.dog){
            db.italians.update(
                {_id:italian._id},
                {$inc:{"dog.age":1}}
            )
        }
        //E se ele tem um leão?
        if(italian.lion){
            db.italians.update(
                {_id:italian._id},
                {$inc:{"lion.age":1}}
            )
        }
        //Os pais envelhecem?
        if(italian.father){
            db.italians.update(
                {_id:italian._id},
                {$inc:{"father.age":1}}
            )
        }
        //Mães também certo? Como presente para dias mães que está chegando, podíamos deixar quieto né?
        if(italian.mother){
            db.italians.update(
                {_id:italian._id},
                {$inc:{"mother.age":1}}
            )
        }
    }
)
```
>

Como usei o cursor, não trouxe retorno. Mas se eu buscar com por um dos italianos retornados anteriormente, noto que todas as idades foram atualizadas.

Alternativamente, podemos usar o updateMany

`db.italians.updateMany({}, {$inc:{"age":1}})`
> { "acknowledged" : true, "matchedCount" : 10000, "modifiedCount" : 10000 }

`db.italians.updateMany({"cat":{$exists:true}}, {$inc:{'cat.age':1}})`
> { "acknowledged" : true, "matchedCount" : 6037, "modifiedCount" : 6037 }

E por aí vai.

### 16. O Corona Vírus chegou na Itália e misteriosamente atingiu pessoas somente com gatos e de 66 anos. Remova esses italianos

`db.italians.deleteMany({"age":{$eq:66},"cat":{$exists:true}})`
> { "acknowledged" : true, "deletedCount" : 75 }

### 17. Utilizando o framework agregate, liste apenas as pessoas com nomes iguais a sua respectiva mãe e que tenha gato ou cachorro

Aqui parti do que estava no PDF. Mas é estranho mostar 'isEqual: 0'. O $cmp retorna 0 se as expresões são equivalentes. Então mudei para cmpResult.

```javascript
db.italians.aggregate([
    {'$match': { mother: { $exists: true} }},
    {'$match': { $or: [ { "cat": { $exists: true } }, { "dog": { $exists: true } } ] } },
    {'$project': {
        "firstname": true,
        "mother": true,
        "cat": true,
        "dog": true,
        "cmpResult": { "$cmp": ["$firstname","$mother.firstname"]}
        }
    },
    {'$match': {"cmpResult": 0}}
])
```

> { "_id" : ObjectId("5eab602c9fb59243e5f9856b"), "firstname" : "Lucia", "mother" : { "firstname" : "Lucia", "surname" : "Bellini", "age" : 59 }, "dog" : { "name" : "Gianluca", "age" : 12 }, "cmpResult" : 0 }
>
>{ "_id" : ObjectId("5eab60309fb59243e5f9900a"), "firstname" : "Davide", "mother" : { "firstname" : "Davide", "surname" : "Montanari", "age" : 93 }, "cat" : { "name" : "Giusy", "age" : 17 }, "dog" : { "name" : "Alessio", "age" : 11 }, "cmpResult" : 0 }
>
>{ "_id" : ObjectId("5eab60349fb59243e5f9916c"), "firstname" : "Elisa", "mother" : { "firstname" : "Elisa", "surname" : "Mazza", "age" : 96 }, "dog" : { "name" : "Massimo", "age" : 5 }, "cmpResult" : 0 }
>
>{ "_id" : ObjectId("5eab60369fb59243e5f99519"), "firstname" : "Andrea", "mother" : { "firstname" : "Andrea", "surname" : "Grassi", "age" : 82 }, "cat" : { "name" : "Antonella", "age" : 11 }, "cmpResult" : 0 }
>
>{ "_id" : ObjectId("5eab60369fb59243e5f9952e"), "firstname" : "Lucia", "mother" : { "firstname" : "Lucia", "surname" : "Colombo", "age" : 102 }, "cat" : { "name" : "Sara", "age" : 19 }, "cmpResult" : 0 }
>
>{ "_id" : ObjectId("5eab603a9fb59243e5f9993f"), "firstname" : "Emanuela", "mother" : { "firstname" : "Emanuela", "surname" : "Costa", "age" : 36 }, "cat" : { "name" : "Giorgio", "age" : 6 }, "cmpResult" : 0 }
>
>{ "_id" : ObjectId("5eab603b9fb59243e5f99a7a"), "firstname" : "Angela", "mother" : { "firstname" : "Angela", "surname" : "Bernardi", "age" : 77 }, "cat" : { "name" : "Lucia", "age" : 13 }, "dog" : { "name" : "Nicola", "age" : 18 }, "cmpResult" : 0 }
>
>{ "_id" : ObjectId("5eab603f9fb59243e5f99c51"), "firstname" : "Giacomo", "mother" : { "firstname" : "Giacomo", "surname" : "Montanari", "age" : 69 }, "cat" : { "name" : "Giorgio", "age" : 6 }, "dog" : { "name" : "Cinzia", "age" : 11 }, "cmpResult" : 0 }
>
>{ "_id" : ObjectId("5eab603f9fb59243e5f99e70"), "firstname" : "Daniela", "mother" : { "firstname" : "Daniela", "surname" : "Greco", "age" : 55 }, "cat" : { "name" : "Federica", "age" : 14 }, "dog" : { "name" : "Antonio", "age" : 5 }, "cmpResult" : 0 }
>
>{ "_id" : ObjectId("5eab60449fb59243e5f9a318"), "firstname" : "Emanuela", "mother" : { "firstname" : "Emanuela", "surname" : "Rinaldi", "age" : 74 }, "dog" : { "name" : "Fabrizio", "age" : 4 }, "cmpResult" : 0 }
>
>{ "_id" : ObjectId("5eab60449fb59243e5f9a36b"), "firstname" : "Sara", "mother" : { "firstname" : "Sara", "surname" : "Bianchi", "age" : 51 }, "cat" : { "name" : "Sonia", "age" : 12 }, "cmpResult" : 0 }

### 18. Utilizando aggregate framework, faça uma lista de nomes única de nomes. Faça isso usando apenas o primeiro nome

`db.italians.aggregate( [ { $group : { _id : "$firstname" } } ] )`
>
>{ "_id" : "Marta" }
>
>{ "_id" : "Elisa" }
>
>{ "_id" : "Martina" }
>
>{ "_id" : "Silvia" }
>
>{ "_id" : "Stefania" }
>
>{ "_id" : "Cristina" }
>
>{ "_id" : "Giulia" }
>
>{ "_id" : "Paolo" }
>
>{ "_id" : "Angela" }
>
>{ "_id" : "Massimo" }
>
>{ "_id" : "Sabrina" }
>
>{ "_id" : "Elena" }
>
>{ "_id" : "Simone" }
>
>{ "_id" : "Anna" }
>
>{ "_id" : "Domenico" }
>
>{ "_id" : "Giusy" }
>
>{ "_id" : "Veronica" }
>
>{ "_id" : "Valeira" }
>
>{ "_id" : "Filipo" }
>
>{ "_id" : "Fabrizio" }
>
>Type "it" for more

### 19. Agora faça a mesma lista do item acima, considerando nome completo

`db.italians.aggregate( [ { $group : { _id : {"firstName": "$firstname", "surname": "$surname" } } } ] )`
>
>{ "_id" : { "firstName" : "Pietro", "surname" : "Longo" } }
>
>{ "_id" : { "firstName" : "Sara", "surname" : "Conte" } }
>
>{ "_id" : { "firstName" : "Andrea", "surname" : "Colombo" } }
>
>{ "_id" : { "firstName" : "Martina", "surname" : "Pagano" } }
>
>{ "_id" : { "firstName" : "Matteo", "surname" : "Marini" } }
>
>{ "_id" : { "firstName" : "Valentina", "surname" : "Esposito" } }
>
>{ "_id" : { "firstName" : "Laura", "surname" : "Amato" } }
>
>{ "_id" : { "firstName" : "Stefania", "surname" : "Basile" } }
>
>{ "_id" : { "firstName" : "Pasquale", "surname" : "Sorrentino" } }
{>
> "_id" : { "firstName" : "Federica", "surname" : "Rossi" } }
>
>{ "_id" : { "firstName" : "Marta", "surname" : "Ferraro" } }
>
>{ "_id" : { "firstName" : "Davide", "surname" : "Testa" } }
>
>{ "_id" : { "firstName" : "Carlo", "surname" : "Marino" } }
>
>{ "_id" : { "firstName" : "Cinzia", "surname" : "Martini" } }
>
>{ "_id" : { "firstName" : "Giacomo", "surname" : "Palumbo" } }
>
>{ "_id" : { "firstName" : "Rita", "surname" : "Grasso" } }
>
>{ "_id" : { "firstName" : "Nicola", "surname" : "Piras" } }
>
>{ "_id" : { "firstName" : "Veronica", "surname" : "De Rosa" } }
>
>{ "_id" : { "firstName" : "Vincenzo", "surname" : "Parisi" } }
>
>{ "_id" : { "firstName" : "Antonio", "surname" : "Vitali" } }
>
>Type "it" for more

### 20. Procure pessoas que gosta de Banana ou Maçã, tenham cachorro ou gato, mais de 20 e menos de 60 anos

Limitando a cinco documentos

```javascript
db.italians.find({
    $and: [
        { "favFruits": {"$in" : ["Banana","Maçã"] } },
        { $or: [ { "dog": { $exists: true } }, { "cat": { $exists: true } } ] } ,
        { "age": { $gt: 20 } }, { "age": { $lt: 60 } }
    ]},
    { "firstName": 1, "surname": true, "age": true, "cat": true, "dog":true, "favFruits": true}
    ).limit(5)
```

>
>{ "_id" : ObjectId("5eab60299fb59243e5f97e2e"), "surname" : "De Santis", "age" : 37, "favFruits" : [ "Pêssego", "Banana", "Maçã" ], "cat" : { "name" : "Claudio", "age" : 14 } }
>
>{ "_id" : ObjectId("5eab60299fb59243e5f97e2f"), "surname" : "Silvestri", "age" : 53, "favFruits" : [ "Mamão", "Banana" ], "cat" : { "name" : "Stefano", "age" : 13 }, "dog" : { "name" : "Mario", "age" : 19 } }
>
>{ "_id" : ObjectId("5eab602a9fb59243e5f97e34"), "surname" : "Carbone", "age" : 32, "favFruits" : [ "Banana", "Pêssego", "Maçã" ], "cat" : { "name" : "Elena", "age" : 5 } }
>
>{ "_id" : ObjectId("5eab602a9fb59243e5f97e37"), "surname" : "De Rosa", "age" : 53, "favFruits" : [ "Uva", "Banana", "Mamão" ], "cat" : { "name" : "Giacomo", "age" : 8 }, "dog" : { "name" : "Alberto", "age" : 7 } }
>
>{ "_id" : ObjectId("5eab602a9fb59243e5f97e3a"), "surname" : "Milani", "age" : 54, "favFruits" : [ "Banana", "Kiwi", "Pêssego" ], "cat" : { "name" : "Patrizia", "age" : 7 }, "dog" : { "name" : "Sonia", "age" : 17 } }

Mas quantos são?

```javascript
db.italians.count({
    $and: [
        { "favFruits": {"$in" : ["Banana","Maçã"] } },
        { $or: [ { "dog": { $exists: true } }, { "cat": { $exists: true } } ] } ,
        { "age": { $gt: 20 } }, { "age": { $lt: 60 } }
    ]},
    { "firstName": 1, "surname": true, "age": true, "cat": true, "dog":true, "favFruits": true}
    )
```

> 1340

## Exercício 3 - Stockbrokers

### 1. Liste as ações com profit acima de 0.5 (limite a 10 o resultado)

```db.stocks.find({"Profit Margin": {"$gte": 0.5}}).sort({"Profit Margin": 1}).limit(10)```

>
> { "_id" : ObjectId("52853801bb1177ca391c1af0"), "Ticker" : "BPO", "Profit Margin" : 0.503, "Institutional Ownership" : 0.958, "EPS growth past 5 years" : 0.354, "Total Debt/Equity" : 1.15, "Current Ratio" : 1, "Return on Assets" : 0.043, "Sector" : "Financial", "P/S" : 4.04, "Change from Open" : 0.001, "Performance (YTD)" : 0.1519, "Performance (Week)" : -0.0052, "Quick Ratio" : 1, "P/B" : 0.9, "EPS growth quarter over quarter" : -0.415, "Payout Ratio" : 0.235, "Performance (Quarter)" : 0.1825, "Forward P/E" : 18.74, "P/E" : 8.65, "200-Day Simple Moving Average" : 0.1124, "Shares Outstanding" : 505, "Earnings Date" : ISODate("2011-02-11T13:30:00Z"), "52-Week High" : -0.022, "P/Cash" : 22.13, "Change" : 0.0021, "Analyst Recom" : 3.1, "Volatility (Week)" : 0.0127, "Country" : "USA", "Return on Equity" : 0.115, "50-Day Low" : 0.1976, "Price" : 19.15, "50-Day High" : -0.022, "Return on Investment" : 0.015, "Shares Float" : 504.86, "Dividend Yield" : 0.0293, "EPS growth next 5 years" : 0.0735, "Industry" : "Property Management", "Beta" : 1.64, "Sales growth quarter over quarter" : 0.01, "Operating Margin" : 0.552, "EPS (ttm)" : 2.21, "PEG" : 1.18, "Float Short" : 0.0062, "52-Week Low" : 0.2728, "Average True Range" : 0.23, "EPS growth next year" : -0.105, "Sales growth past 5 years" : -0.043, "Company" : "Brookfield Properties Corporation", "Gap" : 0.001, "Relative Volume" : 0.17, "Volatility (Month)" : 0.0112, "Market Cap" : 9650.55, "Volume" : 249482, "Gross Margin" : 0.621, "Short Ratio" : 1.9, "Performance (Half Year)" : 0.0269, "Relative Strength Index (14)" : 62.08, "Insider Ownership" : 0.4972, "20-Day Simple Moving Average" : 0.012, "Performance (Month)" : 0.0154, "Institutional Transactions" : -0.004, "Performance (Year)" : 0.2482, "LT Debt/Equity" : 1.15, "Average Volume" : 1650.73, "EPS growth this year" : -0.212, "50-Day Simple Moving Average" : 0.0538 }
>
> { "_id" : ObjectId("52853803bb1177ca391c1f9e"), "Ticker" : "EQM", "Profit Margin" : 0.506, "Institutional Ownership" : 0.494, "EPS growth past 5 years" : 0.016, "Total Debt/Equity" : 0, "Current Ratio" : 2, "Return on Assets" : 0.121, "Sector" : "Basic Materials", "P/S" : 13.44, "Change from Open" : 0.0294, "Performance (YTD)" : 0.774, "Performance (Week)" : 0.007, "Quick Ratio" : 2, "Insider Transactions" : 0.0621, "P/B" : 3.22, "EPS growth quarter over quarter" : 0.5, "Payout Ratio" : 0.523, "Performance (Quarter)" : 0.1765, "Forward P/E" : 21.14, "P/E" : 34, "200-Day Simple Moving Average" : 0.2305, "Shares Outstanding" : 44.53, "Earnings Date" : ISODate("2013-10-24T12:30:00Z"), "52-Week High" : 0.0055, "P/Cash" : 76.67, "Change" : 0.0294, "Analyst Recom" : 1.7, "Volatility (Week)" : 0.0331, "Country" : "USA", "Return on Equity" : 0.153, "50-Day Low" : 0.181, "Price" : 54.95, "50-Day High" : 0.0055, "Return on Investment" : 0.087, "Shares Float" : 26.81, "Dividend Yield" : 0.0322, "EPS growth next 5 years" : 0.374, "Industry" : "Major Integrated Oil & Gas", "Sales growth quarter over quarter" : 0.33, "Operating Margin" : 0.604, "EPS (ttm)" : 1.57, "PEG" : 0.91, "Float Short" : 0.019, "52-Week Low" : 1.0532, "Average True Range" : 1.66, "EPS growth next year" : 0.0907, "Sales growth past 5 years" : 0.148, "Company" : "EQT Midstream Partners, LP", "Gap" : 0, "Relative Volume" : 0.39, "Volatility (Month)" : 0.0336, "Market Cap" : 2376.8, "Volume" : 55022, "Short Ratio" : 3.28, "Performance (Half Year)" : 0.1257, "Relative Strength Index (14)" : 67.51, "Insider Ownership" : 0.1833, "20-Day Simple Moving Average" : 0.0584, "Performance (Month)" : 0.1015, "Institutional Transactions" : 0.0552, "Performance (Year)" : 0.8769, "LT Debt/Equity" : 0, "Average Volume" : 155.01, "EPS growth this year" : -0.521, "50-Day Simple Moving Average" : 0.0963 }
>
> { "_id" : ObjectId("5285380bbb1177ca391c2d17"), "Ticker" : "SFL", "Profit Margin" : 0.506, "Institutional Ownership" : 0.309, "EPS growth past 5 years" : -0.007, "Total Debt/Equity" : 1.45, "Current Ratio" : 0.6, "Return on Assets" : 0.048, "Sector" : "Services", "P/S" : 5.14, "Change from Open" : 0.0065, "Performance (YTD)" : 0.0657, "Performance (Week)" : 0.006, "Quick Ratio" : 0.6, "P/B" : 1.2, "EPS growth quarter over quarter" : -0.623, "Payout Ratio" : 0.912, "Performance (Quarter)" : 0.0779, "Forward P/E" : 11.65, "P/E" : 9.87, "200-Day Simple Moving Average" : 0.0723, "Shares Outstanding" : 86.14, "Earnings Date" : ISODate("2013-11-18T05:00:00Z"), "52-Week High" : -0.0097, "P/Cash" : 13.5, "Change" : 0.0047, "Analyst Recom" : 2.1, "Volatility (Week)" : 0.0157, "Country" : "Bermuda", "Return on Equity" : 0.137, "50-Day Low" : 0.1383, "Price" : 16.96, "50-Day High" : 0.0006, "Return on Investment" : 0.072, "Shares Float" : 58.7, "Dividend Yield" : 0.0924, "EPS growth next 5 years" : -0.018, "Industry" : "Shipping", "Beta" : 1.29, "Sales growth quarter over quarter" : -0.211, "Operating Margin" : 0.428, "EPS (ttm)" : 1.71, "Float Short" : 0.0647, "52-Week Low" : 0.3298, "Average True Range" : 0.27, "EPS growth next year" : 0.2156, "Sales growth past 5 years" : -0.043, "Company" : "Ship Finance International Limited", "Gap" : -0.0018, "Relative Volume" : 0.42, "Volatility (Month)" : 0.0151, "Market Cap" : 1453.98, "Volume" : 235866, "Gross Margin" : 0.656, "Short Ratio" : 6.14, "Performance (Half Year)" : 0.0305, "Relative Strength Index (14)" : 69.43, "Insider Ownership" : 0.4006, "20-Day Simple Moving Average" : 0.0262, "Performance (Month)" : 0.089, "Institutional Transactions" : -0.0169, "Performance (Year)" : 0.2569, "LT Debt/Equity" : 1.22, "Average Volume" : 618.23, "EPS growth this year" : 0.37, "50-Day Simple Moving Average" : 0.0694 }
>
> { "_id" : ObjectId("52853805bb1177ca391c238b"), "Ticker" : "IBN", "Profit Margin" : 0.507, "Institutional Ownership" : 0.278, "EPS growth past 5 years" : 0.231, "Total Debt/Equity" : 2.29, "Return on Assets" : 0.03, "Sector" : "Financial", "P/S" : 3.96, "Change from Open" : -0.0102, "Performance (YTD)" : -0.2197, "Performance (Week)" : -0.0622, "P/B" : 2.13, "EPS growth quarter over quarter" : 0.933, "Payout Ratio" : 0.103, "Performance (Quarter)" : 0.0645, "Forward P/E" : 10.5, "P/E" : 19.71, "200-Day Simple Moving Average" : -0.1092, "Shares Outstanding" : 568.99, "Earnings Date" : ISODate("2011-01-24T05:00:00Z"), "52-Week High" : -0.289, "P/Cash" : 0.51, "Change" : 0.0119, "Analyst Recom" : 1, "Volatility (Week)" : 0.0237, "Country" : "India", "Return on Equity" : 0.303, "50-Day Low" : 0.1886, "Price" : 33.9, "50-Day High" : -0.1091, "Return on Investment" : 0.048, "Shares Float" : 577.01, "Dividend Yield" : 0.02, "EPS growth next 5 years" : 0.22, "Industry" : "Money Center Banks", "Beta" : 2.13, "Sales growth quarter over quarter" : 0.202, "Operating Margin" : 0.279, "EPS (ttm)" : 1.7, "PEG" : 0.9, "Float Short" : 0.0063, "52-Week Low" : 0.3593, "Average True Range" : 0.97, "EPS growth next year" : 0.1728, "Sales growth past 5 years" : 0.057, "Company" : "ICICI Bank Ltd.", "Gap" : 0.0224, "Relative Volume" : 0.51, "Volatility (Month)" : 0.0215, "Market Cap" : 19061.3, "Volume" : 1210132, "Short Ratio" : 1.39, "Performance (Half Year)" : -0.2912, "Relative Strength Index (14)" : 44.04, "20-Day Simple Moving Average" : -0.0414, "Performance (Month)" : 0.0207, "Institutional Transactions" : -0.0365, "Performance (Year)" : -0.124, "LT Debt/Equity" : 2.29, "Average Volume" : 2615.94, "EPS growth this year" : 0.425, "50-Day Simple Moving Average" : 0.0193 }
>
> { "_id" : ObjectId("5285380dbb1177ca391c2f68"), "Ticker" : "TPRE", "Profit Margin" : 0.507, "Institutional Ownership" : 0.562, "EPS growth past 5 years" : 0, "Total Debt/Equity" : 0, "Return on Assets" : 0.178, "Sector" : "Financial", "P/S" : 3.06, "Change from Open" : -0.018, "Performance (YTD)" : 0.1746, "Performance (Week)" : -0.026, "Insider Transactions" : 0.2342, "P/B" : 1.25, "EPS growth quarter over quarter" : 1.75, "Payout Ratio" : 0, "Performance (Quarter)" : 0.1746, "Forward P/E" : 8.02, "P/E" : 6.7, "200-Day Simple Moving Average" : 0.041, "Shares Outstanding" : 79.05, "Earnings Date" : ISODate("2013-11-12T13:30:00Z"), "52-Week High" : -0.0901, "P/Cash" : 37.2, "Change" : -0.0218, "Analyst Recom" : 2.1, "Volatility (Week)" : 0.0482, "Country" : "Bermuda", "Return on Equity" : 0.288, "50-Day Low" : 0.165, "Price" : 15.01, "50-Day High" : -0.0901, "Return on Investment" : 0.116, "Shares Float" : 102.07, "EPS growth next 5 years" : 0.15, "Industry" : "Property & Casualty Insurance", "Operating Margin" : 0.513, "EPS (ttm)" : 2.29, "PEG" : 0.45, "Float Short" : 0.0054, "52-Week Low" : 0.2249, "Average True Range" : 0.51, "EPS growth next year" : 0.0247, "Company" : "Third Point Reinsurance Ltd.", "Gap" : -0.0039, "Relative Volume" : 0.31, "Volatility (Month)" : 0.0299, "Market Cap" : 1212.69, "Volume" : 170761, "Short Ratio" : 0.92, "Relative Strength Index (14)" : 44.51, "Insider Ownership" : 0.009, "20-Day Simple Moving Average" : -0.0403, "Performance (Month)" : -0.0007, "LT Debt/Equity" : 0, "Average Volume" : 603.79, "50-Day Simple Moving Average" : 0.0164 }
>
> { "_id" : ObjectId("52853805bb1177ca391c22f2"), "Ticker" : "HLSS", "Profit Margin" : 0.513, "Institutional Ownership" : 0.854, "EPS growth past 5 years" : 0, "Total Debt/Equity" : 4.52, "Current Ratio" : 122.1, "Return on Assets" : 0.021, "Sector" : "Financial", "P/S" : 8.29, "Change from Open" : 0.0013, "Performance (YTD)" : 0.3024, "Performance (Week)" : -0.0111, "Quick Ratio" : 122.1, "Insider Transactions" : 0.1181, "P/B" : 1.34, "EPS growth quarter over quarter" : 0.324, "Payout Ratio" : 0.903, "Performance (Quarter)" : -0.0077, "Forward P/E" : 11.29, "P/E" : 12.52, "200-Day Simple Moving Average" : 0.0296, "Shares Outstanding" : 71.02, "Earnings Date" : ISODate("2013-10-17T12:30:00Z"), "52-Week High" : -0.0724, "P/Cash" : 7.12, "Change" : 0.0047, "Analyst Recom" : 1.7, "Volatility (Week)" : 0.0228, "Country" : "Cayman Islands", "Return on Equity" : 0.096, "50-Day Low" : 0.1069, "Price" : 23.28, "50-Day High" : -0.0279, "Return on Investment" : 0.014, "Shares Float" : 70, "Dividend Yield" : 0.0777, "EPS growth next 5 years" : 0.05, "Industry" : "Mortgage Investment", "Sales growth quarter over quarter" : 4.095, "Operating Margin" : 0.95, "EPS (ttm)" : 1.85, "PEG" : 2.5, "Float Short" : 0.0219, "52-Week Low" : 0.4431, "Average True Range" : 0.47, "EPS growth next year" : 0.0715, "Company" : "Home Loan Servicing Solutions, Ltd.", "Gap" : 0.0035, "Relative Volume" : 0.4, "Volatility (Month)" : 0.0196, "Market Cap" : 1645.46, "Volume" : 198033, "Short Ratio" : 2.83, "Performance (Half Year)" : -0.0064, "Relative Strength Index (14)" : 53.39, "Insider Ownership" : 0.002, "20-Day Simple Moving Average" : 0.0007, "Performance (Month)" : 0.0275, "P/Free Cash Flow" : 2.43, "Institutional Transactions" : 0.0067, "Performance (Year)" : 0.3324, "LT Debt/Equity" : 4.52, "Average Volume" : 542.18, "EPS growth this year" : 0.231, "50-Day Simple Moving Average" : 0.0252 }
>
> { "_id" : ObjectId("52853806bb1177ca391c2552"), "Ticker" : "KFN", "Profit Margin" : 0.518, "Institutional Ownership" : 0.526, "EPS growth past 5 years" : -0.047, "Total Debt/Equity" : 2.32, "Return on Assets" : 0.033, "Sector" : "Financial", "P/S" : 3.63, "Change from Open" : 0.0052, "Performance (YTD)" : -0.0113, "Performance (Week)" : -0.0113, "Insider Transactions" : 0.1791, "P/B" : 0.79, "EPS growth quarter over quarter" : -0.738, "Payout Ratio" : 0.618, "Performance (Quarter)" : -0.0504, "Forward P/E" : 8.37, "P/E" : 6.91, "200-Day Simple Moving Average" : -0.0526, "Shares Outstanding" : 204.13, "Earnings Date" : ISODate("2013-10-23T20:30:00Z"), "52-Week High" : -0.1194, "P/Cash" : 8.83, "Change" : 0.0062, "Analyst Recom" : 1.8, "Volatility (Week)" : 0.0182, "Country" : "USA", "Return on Equity" : 0.121, "50-Day Low" : 0.0277, "Price" : 9.66, "50-Day High" : -0.0996, "Return on Investment" : 0.042, "Shares Float" : 202.79, "Dividend Yield" : 0.0917, "EPS growth next 5 years" : 0.1, "Industry" : "Mortgage Investment", "Beta" : 2.09, "Sales growth quarter over quarter" : -0.102, "Operating Margin" : 0.313, "EPS (ttm)" : 1.39, "PEG" : 0.69, "Float Short" : 0.0061, "52-Week Low" : 0.1303, "Average True Range" : 0.16, "EPS growth next year" : -0.1311, "Sales growth past 5 years" : -0.086, "Company" : "KKR Financial Holdings LLC", "Gap" : 0.001, "Relative Volume" : 1.23, "Volatility (Month)" : 0.0146, "Market Cap" : 1959.69, "Volume" : 999030, "Gross Margin" : 0.631, "Short Ratio" : 1.4, "Performance (Half Year)" : -0.0661, "Relative Strength Index (14)" : 40.86, "Insider Ownership" : 0.001, "20-Day Simple Moving Average" : -0.0239, "Performance (Month)" : -0.0607, "Institutional Transactions" : -0.0077, "Performance (Year)" : 0.0549, "LT Debt/Equity" : 0, "Average Volume" : 887.93, "EPS growth this year" : 0.069, "50-Day Simple Moving Average" : -0.0364 }
>
> { "_id" : ObjectId("52853802bb1177ca391c1c51"), "Ticker" : "CIT", "Profit Margin" : 0.519, "Institutional Ownership" : 0.963, "EPS growth past 5 years" : -0.224, "Total Debt/Equity" : 2.42, "Return on Assets" : 0.018, "Sector" : "Financial", "P/S" : 6.29, "Change from Open" : 0.0088, "Performance (YTD)" : 0.2704, "Performance (Week)" : 0.0285, "Insider Transactions" : -0.0833, "P/B" : 1.11, "EPS growth quarter over quarter" : 1.859, "Payout Ratio" : 0, "Performance (Quarter)" : -0.0047, "Forward P/E" : 12.12, "P/E" : 12.21, "200-Day Simple Moving Average" : 0.0699, "Shares Outstanding" : 200.81, "Earnings Date" : ISODate("2013-10-22T12:30:00Z"), "52-Week High" : -0.0382, "P/Cash" : 1.65, "Change" : 0.0094, "Analyst Recom" : 1.9, "Volatility (Week)" : 0.0183, "Country" : "USA", "Return on Equity" : 0.095, "50-Day Low" : 0.0592, "Price" : 49.55, "50-Day High" : -0.0341, "Return on Investment" : -0.05, "Shares Float" : 199.61, "Dividend Yield" : 0.0081, "EPS growth next 5 years" : 0.1, "Industry" : "Credit Services", "Beta" : 1.31, "Sales growth quarter over quarter" : 0.336, "EPS (ttm)" : 4.02, "PEG" : 1.22, "Float Short" : 0.0147, "52-Week Low" : 0.3756, "Average True Range" : 0.79, "EPS growth next year" : 0.109, "Sales growth past 5 years" : -0.18, "Company" : "CIT Group Inc.", "Gap" : 0.0006, "Relative Volume" : 0.55, "Volatility (Month)" : 0.0163, "Market Cap" : 9857.81, "Volume" : 580248, "Short Ratio" : 2.53, "Performance (Half Year)" : 0.1182, "Relative Strength Index (14)" : 56.38, "Insider Ownership" : 0.004, "20-Day Simple Moving Average" : 0.0146, "Performance (Month)" : -0.0184, "Institutional Transactions" : 0.0233, "Performance (Year)" : 0.3307, "LT Debt/Equity" : 2.42, "Average Volume" : 1157.92, "EPS growth this year" : 2.113, "50-Day Simple Moving Average" : 0.0105 }
>
> { "_id" : ObjectId("5285380bbb1177ca391c2cb0"), "Ticker" : "SB", "Profit Margin" : 0.522, "Institutional Ownership" : 0.15, "EPS growth past 5 years" : -0.199, "Total Debt/Equity" : 1.06, "Current Ratio" : 2.8, "Return on Assets" : 0.088, "Sector" : "Services", "P/S" : 3.25, "Change from Open" : 0.0052, "Performance (YTD)" : 1.3221, "Performance (Week)" : -0.078, "Quick Ratio" : 2.8, "P/B" : 1.17, "EPS growth quarter over quarter" : 0.143, "Payout Ratio" : 0.248, "Performance (Quarter)" : 0.3422, "Forward P/E" : 9.52, "P/E" : 6.26, "200-Day Simple Moving Average" : 0.3936, "Shares Outstanding" : 76.68, "Earnings Date" : ISODate("2013-11-04T21:30:00Z"), "52-Week High" : -0.1489, "P/Cash" : 8.82, "Change" : 0.0119, "Analyst Recom" : 1.8, "Volatility (Week)" : 0.0424, "Country" : "Greece", "Return on Equity" : 0.212, "50-Day Low" : 0.2335, "Price" : 7.66, "50-Day High" : -0.1489, "Return on Investment" : 0.108, "Shares Float" : 30.22, "Dividend Yield" : 0.0264, "EPS growth next 5 years" : 0.05, "Industry" : "Shipping", "Beta" : 1.66, "Sales growth quarter over quarter" : -0.119, "Operating Margin" : 0.584, "EPS (ttm)" : 1.21, "PEG" : 1.25, "Float Short" : 0.0182, "52-Week Low" : 1.5266, "Average True Range" : 0.39, "EPS growth next year" : 0.1072, "Sales growth past 5 years" : 0.021, "Company" : "Safe Bulkers, Inc.", "Gap" : 0.0066, "Relative Volume" : 1.09, "Volatility (Month)" : 0.044, "Market Cap" : 580.46, "Volume" : 464401, "Gross Margin" : 0.735, "Short Ratio" : 1.18, "Performance (Half Year)" : 0.4699, "Relative Strength Index (14)" : 50.64, "Insider Ownership" : 0.6057, "20-Day Simple Moving Average" : -0.0075, "Performance (Month)" : 0.0876, "Institutional Transactions" : 0.0588, "Performance (Year)" : 0.7523, "LT Debt/Equity" : 1.03, "Average Volume" : 468.74, "EPS growth this year" : -0.016, "50-Day Simple Moving Average" : 0.0533 }
{ "_id" : ObjectId("52853807bb1177ca391c2690"), "Ticker" : "MCGC", "Profit Margin" : 0.525, "Institutional Ownership" : 0.481, "EPS growth past 5 years" : -0.444, "Total Debt/Equity" : 0.56, "Return on Assets" : 0.044, "Sector" : "Financial", "P/S" : 6.34, "Change from Open" : 0.0142, "Performance (YTD)" : 0.1034, "Performance (Week)" : -0.0065, "Insider Transactions" : 0.0117, "P/B" : 0.89, "EPS growth quarter over quarter" : 2.333, "Payout Ratio" : 1.368, "Performance (Quarter)" : -0.069, "Forward P/E" : 9.41, "P/E" : 12.08, "200-Day Simple Moving Average" : -0.0175, "Shares Outstanding" : 71.22, "Earnings Date" : ISODate("2013-10-30T12:30:00Z"), "52-Week High" : -0.1198, "P/Cash" : 3.03, "Change" : 0.012, "An> db.stocks.find({"Profit Margin": {$gt: 0.5}}).sort({"Profit Margin": 1}).limit(10)
>
> { "_id" : ObjectId("52853801bb1177ca391c1af0"), "Ticker" : "BPO", "Profit Margin" : 0.503, "Institutional Ownership" : 0.958, "EPS growth past 5 years" : 0.354, "Total Debt/Equity" : 1.15, "Current Ratio" : 1, "Return on Assets" : 0.043, "Sector" : "Financial", "P/S" : 4.04, "Change from Open" : 0.001, "Performance (YTD)" : 0.1519, "Performance (Week)" : -0.0052, "Quick Ratio" : 1, "P/B" : 0.9, "EPS growth quarter over quarter" : -0.415, "Payout Ratio" : 0.235, "Performance (Quarter)" : 0.1825, "Forward P/E" : 18.74, "P/E" : 8.65, "200-Day Simple Moving Average" : 0.1124, "Shares Outstanding" : 505, "Earnings Date" : ISODate("2011-02-11T13:30:00Z"), "52-Week High" : -0.022, "P/Cash" : 22.13, "Change" : 0.0021, "Analyst Recom" : 3.1, "Volatility (Week)" : 0.0127, "Country" : "USA", "Return on Equity" : 0.115, "50-Day Low" : 0.1976, "Price" : 19.15, "50-Day High" : -0.022, "Return on Investment" : 0.015, "Shares Float" : 504.86, "Dividend Yield" : 0.0293, "EPS growth next 5 years" : 0.0735, "Industry" : "Property Management", "Beta" : 1.64, "Sales growth quarter over quarter" : 0.01, "Operating Margin" : 0.552, "EPS (ttm)" : 2.21, "PEG" : 1.18, "Float Short" : 0.0062, "52-Week Low" : 0.2728, "Average True Range" : 0.23, "EPS growth next year" : -0.105, "Sales growth past 5 years" : -0.043, "Company" : "Brookfield Properties Corporation", "Gap" : 0.001, "Relative Volume" : 0.17, "Volatility (Month)" : 0.0112, "Market Cap" : 9650.55, "Volume" : 249482, "Gross Margin" : 0.621, "Short Ratio" : 1.9, "Performance (Half Year)" : 0.0269, "Relative Strength Index (14)" : 62.08, "Insider Ownership" : 0.4972, "20-Day Simple Moving Average" : 0.012, "Performance (Month)" : 0.0154, "Institutional Transactions" : -0.004, "Performance (Year)" : 0.2482, "LT Debt/Equity" : 1.15, "Average Volume" : 1650.73, "EPS growth this year" : -0.212, "50-Day Simple Moving Average" : 0.0538 }
>
> { "_id" : ObjectId("52853803bb1177ca391c1f9e"), "Ticker" : "EQM", "Profit Margin" : 0.506, "Institutional Ownership" : 0.494, "EPS growth past 5 years" : 0.016, "Total Debt/Equity" : 0, "Current Ratio" : 2, "Return on Assets" : 0.121, "Sector" : "Basic Materials", "P/S" : 13.44, "Change from Open" : 0.0294, "Performance (YTD)" : 0.774, "Performance (Week)" : 0.007, "Quick Ratio" : 2, "Insider Transactions" : 0.0621, "P/B" : 3.22, "EPS growth quarter over quarter" : 0.5, "Payout Ratio" : 0.523, "Performance (Quarter)" : 0.1765, "Forward P/E" : 21.14, "P/E" : 34, "200-Day Simple Moving Average" : 0.2305, "Shares Outstanding" : 44.53, "Earnings Date" : ISODate("2013-10-24T12:30:00Z"), "52-Week High" : 0.0055, "P/Cash" : 76.67, "Change" : 0.0294, "Analyst Recom" : 1.7, "Volatility (Week)" : 0.0331, "Country" : "USA", "Return on Equity" : 0.153, "50-Day Low" : 0.181, "Price" : 54.95, "50-Day High" : 0.0055, "Return on Investment" : 0.087, "Shares Float" : 26.81, "Dividend Yield" : 0.0322, "EPS growth next 5 years" : 0.374, "Industry" : "Major Integrated Oil & Gas", "Sales growth quarter over quarter" : 0.33, "Operating Margin" : 0.604, "EPS (ttm)" : 1.57, "PEG" : 0.91, "Float Short" : 0.019, "52-Week Low" : 1.0532, "Average True Range" : 1.66, "EPS growth next year" : 0.0907, "Sales growth past 5 years" : 0.148, "Company" : "EQT Midstream Partners, LP", "Gap" : 0, "Relative Volume" : 0.39, "Volatility (Month)" : 0.0336, "Market Cap" : 2376.8, "Volume" : 55022, "Short Ratio" : 3.28, "Performance (Half Year)" : 0.1257, "Relative Strength Index (14)" : 67.51, "Insider Ownership" : 0.1833, "20-Day Simple Moving Average" : 0.0584, "Performance (Month)" : 0.1015, "Institutional Transactions" : 0.0552, "Performance (Year)" : 0.8769, "LT Debt/Equity" : 0, "Average Volume" : 155.01, "EPS growth this year" : -0.521, "50-Day Simple Moving Average" : 0.0963 }
>
> { "_id" : ObjectId("5285380bbb1177ca391c2d17"), "Ticker" : "SFL", "Profit Margin" : 0.506, "Institutional Ownership" : 0.309, "EPS growth past 5 years" : -0.007, "Total Debt/Equity" : 1.45, "Current Ratio" : 0.6, "Return on Assets" : 0.048, "Sector" : "Services", "P/S" : 5.14, "Change from Open" : 0.0065, "Performance (YTD)" : 0.0657, "Performance (Week)" : 0.006, "Quick Ratio" : 0.6, "P/B" : 1.2, "EPS growth quarter over quarter" : -0.623, "Payout Ratio" : 0.912, "Performance (Quarter)" : 0.0779, "Forward P/E" : 11.65, "P/E" : 9.87, "200-Day Simple Moving Average" : 0.0723, "Shares Outstanding" : 86.14, "Earnings Date" : ISODate("2013-11-18T05:00:00Z"), "52-Week High" : -0.0097, "P/Cash" : 13.5, "Change" : 0.0047, "Analyst Recom" : 2.1, "Volatility (Week)" : 0.0157, "Country" : "Bermuda", "Return on Equity" : 0.137, "50-Day Low" : 0.1383, "Price" : 16.96, "50-Day High" : 0.0006, "Return on Investment" : 0.072, "Shares Float" : 58.7, "Dividend Yield" : 0.0924, "EPS growth next 5 years" : -0.018, "Industry" : "Shipping", "Beta" : 1.29, "Sales growth quarter over quarter" : -0.211, "Operating Margin" : 0.428, "EPS (ttm)" : 1.71, "Float Short" : 0.0647, "52-Week Low" : 0.3298, "Average True Range" : 0.27, "EPS growth next year" : 0.2156, "Sales growth past 5 years" : -0.043, "Company" : "Ship Finance International Limited", "Gap" : -0.0018, "Relative Volume" : 0.42, "Volatility (Month)" : 0.0151, "Market Cap" : 1453.98, "Volume" : 235866, "Gross Margin" : 0.656, "Short Ratio" : 6.14, "Performance (Half Year)" : 0.0305, "Relative Strength Index (14)" : 69.43, "Insider Ownership" : 0.4006, "20-Day Simple Moving Average" : 0.0262, "Performance (Month)" : 0.089, "Institutional Transactions" : -0.0169, "Performance (Year)" : 0.2569, "LT Debt/Equity" : 1.22, "Average Volume" : 618.23, "EPS growth this year" : 0.37, "50-Day Simple Moving Average" : 0.0694 }
>
> { "_id" : ObjectId("52853805bb1177ca391c238b"), "Ticker" : "IBN", "Profit Margin" : 0.507, "Institutional Ownership" : 0.278, "EPS growth past 5 years" : 0.231, "Total Debt/Equity" : 2.29, "Return on Assets" : 0.03, "Sector" : "Financial", "P/S" : 3.96, "Change from Open" : -0.0102, "Performance (YTD)" : -0.2197, "Performance (Week)" : -0.0622, "P/B" : 2.13, "EPS growth quarter over quarter" : 0.933, "Payout Ratio" : 0.103, "Performance (Quarter)" : 0.0645, "Forward P/E" : 10.5, "P/E" : 19.71, "200-Day Simple Moving Average" : -0.1092, "Shares Outstanding" : 568.99, "Earnings Date" : ISODate("2011-01-24T05:00:00Z"), "52-Week High" : -0.289, "P/Cash" : 0.51, "Change" : 0.0119, "Analyst Recom" : 1, "Volatility (Week)" : 0.0237, "Country" : "India", "Return on Equity" : 0.303, "50-Day Low" : 0.1886, "Price" : 33.9, "50-Day High" : -0.1091, "Return on Investment" : 0.048, "Shares Float" : 577.01, "Dividend Yield" : 0.02, "EPS growth next 5 years" : 0.22, "Industry" : "Money Center Banks", "Beta" : 2.13, "Sales growth quarter over quarter" : 0.202, "Operating Margin" : 0.279, "EPS (ttm)" : 1.7, "PEG" : 0.9, "Float Short" : 0.0063, "52-Week Low" : 0.3593, "Average True Range" : 0.97, "EPS growth next year" : 0.1728, "Sales growth past 5 years" : 0.057, "Company" : "ICICI Bank Ltd.", "Gap" : 0.0224, "Relative Volume" : 0.51, "Volatility (Month)" : 0.0215, "Market Cap" : 19061.3, "Volume" : 1210132, "Short Ratio" : 1.39, "Performance (Half Year)" : -0.2912, "Relative Strength Index (14)" : 44.04, "20-Day Simple Moving Average" : -0.0414, "Performance (Month)" : 0.0207, "Institutional Transactions" : -0.0365, "Performance (Year)" : -0.124, "LT Debt/Equity" : 2.29, "Average Volume" : 2615.94, "EPS growth this year" : 0.425, "50-Day Simple Moving Average" : 0.0193 }
>
> { "_id" : ObjectId("5285380dbb1177ca391c2f68"), "Ticker" : "TPRE", "Profit Margin" : 0.507, "Institutional Ownership" : 0.562, "EPS growth past 5 years" : 0, "Total Debt/Equity" : 0, "Return on Assets" : 0.178, "Sector" : "Financial", "P/S" : 3.06, "Change from Open" : -0.018, "Performance (YTD)" : 0.1746, "Performance (Week)" : -0.026, "Insider Transactions" : 0.2342, "P/B" : 1.25, "EPS growth quarter over quarter" : 1.75, "Payout Ratio" : 0, "Performance (Quarter)" : 0.1746, "Forward P/E" : 8.02, "P/E" : 6.7, "200-Day Simple Moving Average" : 0.041, "Shares Outstanding" : 79.05, "Earnings Date" : ISODate("2013-11-12T13:30:00Z"), "52-Week High" : -0.0901, "P/Cash" : 37.2, "Change" : -0.0218, "Analyst Recom" : 2.1, "Volatility (Week)" : 0.0482, "Country" : "Bermuda", "Return on Equity" : 0.288, "50-Day Low" : 0.165, "Price" : 15.01, "50-Day High" : -0.0901, "Return on Investment" : 0.116, "Shares Float" : 102.07, "EPS growth next 5 years" : 0.15, "Industry" : "Property & Casualty Insurance", "Operating Margin" : 0.513, "EPS (ttm)" : 2.29, "PEG" : 0.45, "Float Short" : 0.0054, "52-Week Low" : 0.2249, "Average True Range" : 0.51, "EPS growth next year" : 0.0247, "Company" : "Third Point Reinsurance Ltd.", "Gap" : -0.0039, "Relative Volume" : 0.31, "Volatility (Month)" : 0.0299, "Market Cap" : 1212.69, "Volume" : 170761, "Short Ratio" : 0.92, "Relative Strength Index (14)" : 44.51, "Insider Ownership" : 0.009, "20-Day Simple Moving Average" : -0.0403, "Performance (Month)" : -0.0007, "LT Debt/Equity" : 0, "Average Volume" : 603.79, "50-Day Simple Moving Average" : 0.0164 }
>
> { "_id" : ObjectId("52853805bb1177ca391c22f2"), "Ticker" : "HLSS", "Profit Margin" : 0.513, "Institutional Ownership" : 0.854, "EPS growth past 5 years" : 0, "Total Debt/Equity" : 4.52, "Current Ratio" : 122.1, "Return on Assets" : 0.021, "Sector" : "Financial", "P/S" : 8.29, "Change from Open" : 0.0013, "Performance (YTD)" : 0.3024, "Performance (Week)" : -0.0111, "Quick Ratio" : 122.1, "Insider Transactions" : 0.1181, "P/B" : 1.34, "EPS growth quarter over quarter" : 0.324, "Payout Ratio" : 0.903, "Performance (Quarter)" : -0.0077, "Forward P/E" : 11.29, "P/E" : 12.52, "200-Day Simple Moving Average" : 0.0296, "Shares Outstanding" : 71.02, "Earnings Date" : ISODate("2013-10-17T12:30:00Z"), "52-Week High" : -0.0724, "P/Cash" : 7.12, "Change" : 0.0047, "Analyst Recom" : 1.7, "Volatility (Week)" : 0.0228, "Country" : "Cayman Islands", "Return on Equity" : 0.096, "50-Day Low" : 0.1069, "Price" : 23.28, "50-Day High" : -0.0279, "Return on Investment" : 0.014, "Shares Float" : 70, "Dividend Yield" : 0.0777, "EPS growth next 5 years" : 0.05, "Industry" : "Mortgage Investment", "Sales growth quarter over quarter" : 4.095, "Operating Margin" : 0.95, "EPS (ttm)" : 1.85, "PEG" : 2.5, "Float Short" : 0.0219, "52-Week Low" : 0.4431, "Average True Range" : 0.47, "EPS growth next year" : 0.0715, "Company" : "Home Loan Servicing Solutions, Ltd.", "Gap" : 0.0035, "Relative Volume" : 0.4, "Volatility (Month)" : 0.0196, "Market Cap" : 1645.46, "Volume" : 198033, "Short Ratio" : 2.83, "Performance (Half Year)" : -0.0064, "Relative Strength Index (14)" : 53.39, "Insider Ownership" : 0.002, "20-Day Simple Moving Average" : 0.0007, "Performance (Month)" : 0.0275, "P/Free Cash Flow" : 2.43, "Institutional Transactions" : 0.0067, "Performance (Year)" : 0.3324, "LT Debt/Equity" : 4.52, "Average Volume" : 542.18, "EPS growth this year" : 0.231, "50-Day Simple Moving Average" : 0.0252 }
{ "_id" : ObjectId("52853806bb1177ca391c2552"), "Ticker" : "KFN", "Profit Margin" : 0.518, "Institutional Ownership" : 0.526, "EPS growth past 5 years" : -0.047, "Total Debt/Equity" : 2.32, "Return on Assets" : 0.033, "Sector" : "Financial", "P/S" : 3.63, "Change from Open" : 0.0052, "Performance (YTD)" : -0.0113, "Performance (Week)" : -0.0113, "Insider Transactions" : 0.1791, "P/B" : 0.79, "EPS growth quarter over quarter" : -0.738, "Payout Ratio" : 0.618, "Performance (Quarter)" : -0.0504, "Forward P/E" : 8.37, "P/E" : 6.91, "200-Day Simple Moving Average" : -0.0526, "Shares Outstanding" : 204.13, "Earnings Date" : ISODate("2013-10-23T20:30:00Z"), "52-Week High" : -0.1194, "P/Cash" : 8.83, "Change" : 0.0062, "Analyst Recom" : 1.8, "Volatility (Week)" : 0.0182, "Country" : "USA", "Return on Equity" : 0.121, "50-Day Low" : 0.0277, "Price" : 9.66, "50-Day High" : -0.0996, "Return on Investment" : 0.042, "Shares Float" : 202.79, "Dividend Yield" : 0.0917, "EPS growth next 5 years" : 0.1, "Industry" : "Mortgage Investment", "Beta" : 2.09, "Sales growth quarter over quarter" : -0.102, "Operating Margin" : 0.313, "EPS (ttm)" : 1.39, "PEG" : 0.69, "Float Short" : 0.0061, "52-Week Low" : 0.1303, "Average True Range" : 0.16, "EPS growth next year" : -0.1311, "Sales growth past 5 years" : -0.086, "Company" : "KKR Financial Holdings LLC", "Gap" : 0.001, "Relative Volume" : 1.23, "Volatility (Month)" : 0.0146, "Market Cap" : 1959.69, "Volume" : 999030, "Gross Margin" : 0.631, "Short Ratio" : 1.4, "Performance (Half Year)" : -0.0661, "Relative Strength Index (14)" : 40.86, "Insider Ownership" : 0.001, "20-Day Simple Moving Average" : -0.0239, "Performance (Month)" : -0.0607, "Institutional Transactions" : -0.0077, "Performance (Year)" : 0.0549, "LT Debt/Equity" : 0, "Average Volume" : 887.93, "EPS growth this year" : 0.069, "50-Day Simple Moving Average" : -0.0364 }
>
> { "_id" : ObjectId("52853802bb1177ca391c1c51"), "Ticker" : "CIT", "Profit Margin" : 0.519, "Institutional Ownership" : 0.963, "EPS growth past 5 years" : -0.224, "Total Debt/Equity" : 2.42, "Return on Assets" : 0.018, "Sector" : "Financial", "P/S" : 6.29, "Change from Open" : 0.0088, "Performance (YTD)" : 0.2704, "Performance (Week)" : 0.0285, "Insider Transactions" : -0.0833, "P/B" : 1.11, "EPS growth quarter over quarter" : 1.859, "Payout Ratio" : 0, "Performance (Quarter)" : -0.0047, "Forward P/E" : 12.12, "P/E" : 12.21, "200-Day Simple Moving Average" : 0.0699, "Shares Outstanding" : 200.81, "Earnings Date" : ISODate("2013-10-22T12:30:00Z"), "52-Week High" : -0.0382, "P/Cash" : 1.65, "Change" : 0.0094, "Analyst Recom" : 1.9, "Volatility (Week)" : 0.0183, "Country" : "USA", "Return on Equity" : 0.095, "50-Day Low" : 0.0592, "Price" : 49.55, "50-Day High" : -0.0341, "Return on Investment" : -0.05, "Shares Float" : 199.61, "Dividend Yield" : 0.0081, "EPS growth next 5 years" : 0.1, "Industry" : "Credit Services", "Beta" : 1.31, "Sales growth quarter over quarter" : 0.336, "EPS (ttm)" : 4.02, "PEG" : 1.22, "Float Short" : 0.0147, "52-Week Low" : 0.3756, "Average True Range" : 0.79, "EPS growth next year" : 0.109, "Sales growth past 5 years" : -0.18, "Company" : "CIT Group Inc.", "Gap" : 0.0006, "Relative Volume" : 0.55, "Volatility (Month)" : 0.0163, "Market Cap" : 9857.81, "Volume" : 580248, "Short Ratio" : 2.53, "Performance (Half Year)" : 0.1182, "Relative Strength Index (14)" : 56.38, "Insider Ownership" : 0.004, "20-Day Simple Moving Average" : 0.0146, "Performance (Month)" : -0.0184, "Institutional Transactions" : 0.0233, "Performance (Year)" : 0.3307, "LT Debt/Equity" : 2.42, "Average Volume" : 1157.92, "EPS growth this year" : 2.113, "50-Day Simple Moving Average" : 0.0105 }
>
> { "_id" : ObjectId("5285380bbb1177ca391c2cb0"), "Ticker" : "SB", "Profit Margin" : 0.522, "Institutional Ownership" : 0.15, "EPS growth past 5 years" : -0.199, "Total Debt/Equity" : 1.06, "Current Ratio" : 2.8, "Return on Assets" : 0.088, "Sector" : "Services", "P/S" : 3.25, "Change from Open" : 0.0052, "Performance (YTD)" : 1.3221, "Performance (Week)" : -0.078, "Quick Ratio" : 2.8, "P/B" : 1.17, "EPS growth quarter over quarter" : 0.143, "Payout Ratio" : 0.248, "Performance (Quarter)" : 0.3422, "Forward P/E" : 9.52, "P/E" : 6.26, "200-Day Simple Moving Average" : 0.3936, "Shares Outstanding" : 76.68, "Earnings Date" : ISODate("2013-11-04T21:30:00Z"), "52-Week High" : -0.1489, "P/Cash" : 8.82, "Change" : 0.0119, "Analyst Recom" : 1.8, "Volatility (Week)" : 0.0424, "Country" : "Greece", "Return on Equity" : 0.212, "50-Day Low" : 0.2335, "Price" : 7.66, "50-Day High" : -0.1489, "Return on Investment" : 0.108, "Shares Float" : 30.22, "Dividend Yield" : 0.0264, "EPS growth next 5 years" : 0.05, "Industry" : "Shipping", "Beta" : 1.66, "Sales growth quarter over quarter" : -0.119, "Operating Margin" : 0.584, "EPS (ttm)" : 1.21, "PEG" : 1.25, "Float Short" : 0.0182, "52-Week Low" : 1.5266, "Average True Range" : 0.39, "EPS growth next year" : 0.1072, "Sales growth past 5 years" : 0.021, "Company" : "Safe Bulkers, Inc.", "Gap" : 0.0066, "Relative Volume" : 1.09, "Volatility (Month)" : 0.044, "Market Cap" : 580.46, "Volume" : 464401, "Gross Margin" : 0.735, "Short Ratio" : 1.18, "Performance (Half Year)" : 0.4699, "Relative Strength Index (14)" : 50.64, "Insider Ownership" : 0.6057, "20-Day Simple Moving Average" : -0.0075, "Performance (Month)" : 0.0876, "Institutional Transactions" : 0.0588, "Performance (Year)" : 0.7523, "LT Debt/Equity" : 1.03, "Average Volume" : 468.74, "EPS growth this year" : -0.016, "50-Day Simple Moving Average" : 0.0533 }
>
> { "_id" : ObjectId("52853807bb1177ca391c2690"), "Ticker" : "MCGC", "Profit Margin" : 0.525, "Institutional Ownership" : 0.481, "EPS growth past 5 years" : -0.444, "Total Debt/Equity" : 0.56, "Return on Assets" : 0.044, "Sector" : "Financial", "P/S" : 6.34, "Change from Open" : 0.0142, "Performance (YTD)" : 0.1034, "Performance (Week)" : -0.0065, "Insider Transactions" : 0.0117, "P/B" : 0.89, "EPS growth quarter over quarter" : 2.333, "Payout Ratio" : 1.368, "Performance (Quarter)" : -0.069, "Forward P/E" : 9.41, "P/E" : 12.08, "200-Day Simple Moving Average" : -0.0175, "Shares Outstanding" : 71.22, "Earnings Date" : ISODate("2013-10-30T12:30:00Z"), "52-Week High" : -0.1198, "P/Cash" : 3.03, "Change" : 0.012, "Analyst Recom" : 3, "Volatility (Week)" : 0.0228, "Country" : "USA", "Return on Equity" : 0.073, "50-Day Low" : 0.0317, "Price" : 4.64, "50-Day High" : -0.11, "Return on Investment" : 0.03, "Shares Float" : 68.4, "Dividend Yield" : 0.1089, "EPS growth next 5 years" : 0.1, "Industry" : "Asset Management", "Beta" : 1.77, "Sales growth quarter over quarter" : -0.279, "Operating Margin" : 0.484, "EPS (ttm)" : 0.38, "PEG" : 1.21, "Float Short" : 0.014, "52-Week Low" : 0.2805, "Average True Range" : 0.12, "EPS growth next year" : 0.1091, "Sales growth past 5 years" : -0.201, "Company" : "MCG Capital Corporation", "Gap" : -0.0022, "Relative Volume" : 0.82, "Volatility (Month)" : 0.0267, "Market Cap" : 326.89, "Volume" : 201297, "Gross Margin" : 0.806, "Short Ratio" : 3.58, "Performance (Half Year)" : -0.0594, "Relative Strength Index (14)" : 41.9, "Insider Ownership" : 0.034, "20-Day Simple Moving Average" : -0.0408, "Performance (Month)" : -0.0765, "Institutional Transactions" : 0.0012, "Performance (Year)" : 0.2016, "LT Debt/Equity" : 0.49, "Average Volume" : 268.13, "EPS growth this year" : 1.057, "50-Day Simple Moving Average" : -0.0421 }

### 2. Liste as ações com perdas (limite a 10 novamente)

```db.stocks.find({"Profit Margin": {$lt: 0}}).sort({"Profit Margin": -1}).limit(10)```
>
> { "_id" : ObjectId("52853801bb1177ca391c18c1"), "Ticker" : "ALCS", "Profit Margin" : -0.001, "Institutional Ownership" : 0.572, "EPS growth past 5 years" : 0.18, "Total Debt/Equity" : 0.97, "Current Ratio" : 4, "Return on Assets" : -0.002, "Sector" : "Services", "P/S" : 0.07, "Change from Open" : -0.0241, "Performance (YTD)" : 0.1529, "Performance (Week)" : 0.0056, "Quick Ratio" : 0.5, "Insider Transactions" : -0.5017, "P/B" : 0.35, "EPS growth quarter over quarter" : -0.655, "Performance (Quarter)" : -0.232, "200-Day Simple Moving Average" : -0.0138, "Shares Outstanding" : 3.26, "Earnings Date" : ISODate("2013-12-02T05:00:00Z"), "52-Week High" : -0.3102, "P/Cash" : 12.64, "Change" : -0.0313, "Volatility (Week)" : 0.0275, "Country" : "USA", "Return on Equity" : -0.005, "50-Day Low" : 0.0365, "Price" : 10.52, "50-Day High" : -0.2977, "Return on Investment" : 0.029, "Shares Float" : 2.67, "EPS growth next 5 years" : 0.15, "Industry" : "Discount, Variety Stores", "Beta" : 0.64, "Sales growth quarter over quarter" : 0.036, "Operating Margin" : 0.006, "EPS (ttm)" : -0.04, "Float Short" : 0.0007, "52-Week Low" : 0.5796, "Average True Range" : 0.35, "Sales growth past 5 years" : 0.005, "Company" : "ALCO Stores, Inc.", "Gap" : -0.0074, "Relative Volume" : 0.17, "Volatility (Month)" : 0.0215, "Market Cap" : 35.38, "Volume" : 1720, "Gross Margin" : 0.3, "Short Ratio" : 0.17, "Performance (Half Year)" : 0.1753, "Relative Strength Index (14)" : 25.7, "Insider Ownership" : 0.002, "20-Day Simple Moving Average" : -0.1462, "Performance (Month)" : -0.2221, "Institutional Transactions" : 0.0153, "Performance (Year)" : 0.3575, "LT Debt/Equity" : 0.96, "Average Volume" : 11.13, "EPS growth this year" : -0.02, "50-Day Simple Moving Average" : -0.2139 }
>
> { "_id" : ObjectId("52853801bb1177ca391c191e"), "Ticker" : "ANGO", "Profit Margin" : -0.001, "Institutional Ownership" : 0.947, "EPS growth past 5 years" : -0.154, "Total Debt/Equity" : 0.27, "Current Ratio" : 2, "Return on Assets" : 0, "Sector" : "Healthcare", "P/S" : 1.57, "Change from Open" : 0.0123, "Performance (YTD)" : 0.3985, "Performance (Week)" : 0.0385, "Quick Ratio" : 1.2, "Insider Transactions" : 0.2335, "P/B" : 1.02, "EPS growth quarter over quarter" : 0.5, "Performance (Quarter)" : 0.3518, "Forward P/E" : 37.49, "200-Day Simple Moving Average" : 0.3092, "Shares Outstanding" : 34.95, "Earnings Date" : ISODate("2013-10-10T20:30:00Z"), "52-Week High" : -0.0358, "P/Cash" : 22.38, "Change" : 0.0163, "Analyst Recom" : 2.8, "Volatility (Week)" : 0.0273, "Country" : "USA", "Return on Equity" : -0.001, "50-Day Low" : 0.437, "Price" : 15.62, "50-Day High" : -0.0358, "Return on Investment" : 0.011, "Shares Float" : 34.75, "EPS growth next 5 years" : 0.15, "Industry" : "Medical Instruments & Supplies", "Beta" : 0.64, "Sales growth quarter over quarter" : 0.002, "Operating Margin" : 0.062, "EPS (ttm)" : -0.01, "Float Short" : 0.0123, "52-Week Low" : 0.6408, "Average True Range" : 0.42, "EPS growth next year" : 0.213, "Sales growth past 5 years" : 0.155, "Company" : "AngioDynamics Inc.", "Gap" : 0.0039, "Relative Volume" : 0.79, "Volatility (Month)" : 0.0258, "Market Cap" : 537.18, "Volume" : 114009, "Gross Margin" : 0.503, "Short Ratio" : 2.69, "Performance (Half Year)" : 0.4391, "Relative Strength Index (14)" : 62.15, "Insider Ownership" : 0.013, "20-Day Simple Moving Average" : 0.0145, "Performance (Month)" : 0.0629, "P/Free Cash Flow" : 21.57, "Institutional Transactions" : 0.0026, "Performance (Year)" : 0.4879, "LT Debt/Equity" : 0.24, "Average Volume" : 159.15, "EPS growth this year" : 0.9, "50-Day Simple Moving Average" : 0.1267 }
>
> { "_id" : ObjectId("52853801bb1177ca391c1a08"), "Ticker" : "BAGR", "Profit Margin" : -0.001, "Institutional Ownership" : 0.238, "EPS growth past 5 years" : 0.246, "Total Debt/Equity" : 1.21, "Current Ratio" : 1.9, "Return on Assets" : -0.002, "Sector" : "Services", "P/S" : 1.64, "Change from Open" : 0.0208, "Performance (YTD)" : 0.615, "Performance (Week)" : -0.0015, "Quick Ratio" : 1.9, "Insider Transactions" : 0.0053, "P/B" : 4.68, "EPS growth quarter over quarter" : 0, "Performance (Quarter)" : 0.0538, "Forward P/E" : 61.52, "200-Day Simple Moving Average" : -0.0473, "Shares Outstanding" : 24.68, "Earnings Date" : ISODate("2013-05-15T20:30:00Z"), "52-Week High" : -0.3147, "P/Cash" : 7.09, "Change" : -0.0898, "Analyst Recom" : 1.5, "Volatility (Week)" : 0.0426, "Country" : "USA", "Return on Equity" : -0.01, "50-Day Low" : 0.073, "Price" : 5.88, "50-Day High" : -0.3041, "Return on Investment" : 0.032, "Shares Float" : 10.48, "EPS growth next 5 years" : 0.25, "Industry" : "Restaurants", "Beta" : -0.99, "Sales growth quarter over quarter" : 0.617, "Operating Margin" : 0.015, "EPS (ttm)" : -0.01, "Float Short" : 0.034, "52-Week Low" : 0.68, "Average True Range" : 0.31, "EPS growth next year" : 1.625, "Sales growth past 5 years" : 0.868, "Company" : "Diversified Restaurant Holdings, Inc.", "Gap" : -0.1084, "Relative Volume" : 1.9, "Volatility (Month)" : 0.044, "Market Cap" : 159.43, "Volume" : 82165, "Gross Margin" : 0.692, "Short Ratio" : 7.5, "Performance (Half Year)" : -0.0771, "Relative Strength Index (14)" : 34.36, "Insider Ownership" : 0.483, "20-Day Simple Moving Average" : -0.1228, "Performance (Month)" : -0.1729, "Institutional Transactions" : 0.0143, "Performance (Year)" : 0.3458, "LT Debt/Equity" : 1, "Average Volume" : 47.44, "EPS growth this year" : -0.9, "50-Day Simple Moving Average" : -0.1138 }
>
> { "_id" : ObjectId("52853802bb1177ca391c1bcf"), "Ticker" : "CDE", "Profit Margin" : -0.001, "Institutional Ownership" : 0.729, "EPS growth past 5 years" : -0.118, "Total Debt/Equity" : 0.13, "Current Ratio" : 3.4, "Return on Assets" : 0, "Sector" : "Basic Materials", "P/S" : 1.37, "Change from Open" : 0.0256, "Performance (YTD)" : -0.5467, "Performance (Week)" : -0.0785, "Quick Ratio" : 2.2, "Insider Transactions" : 0.8719, "P/B" : 0.47, "EPS growth quarter over quarter" : -2.346, "Payout Ratio" : 0, "Performance (Quarter)" : -0.3147, "P/E" : 371.67, "200-Day Simple Moving Average" : -0.223, "Shares Outstanding" : 99.83, "Earnings Date" : ISODate("2013-11-06T21:30:00Z"), "52-Week High" : -0.5446, "P/Cash" : 4.46, "Change" : 0.0404, "Analyst Recom" : 3.3, "Volatility (Week)" : 0.0434, "Country" : "USA", "Return on Equity" : 0, "50-Day Low" : 0.0964, "Price" : 11.6, "50-Day High" : -0.2183, "Return on Investment" : 0.037, "Shares Float" : 100.18, "Industry" : "Silver", "Beta" : 1.49, "Sales growth quarter over quarter" : -0.196, "Operating Margin" : 0.075, "EPS (ttm)" : 0.03, "Float Short" : 0.0457, "52-Week Low" : 0.0964, "Average True Range" : 0.49, "EPS growth next year" : 0.109, "Sales growth past 5 years" : 0.436, "Company" : "Coeur d'Alene Mines Corporation", "Gap" : 0.0143, "Relative Volume" : 0.63, "Volatility (Month)" : 0.0367, "Market Cap" : 1113.14, "Volume" : 1178777, "Gross Margin" : 0.167, "Short Ratio" : 2.24, "Performance (Half Year)" : -0.1771, "Relative Strength Index (14)" : 42.62, "Insider Ownership" : 0.002, "20-Day Simple Moving Average" : -0.0397, "Performance (Month)" : -0.0591, "P/Free Cash Flow" : 8.85, "Institutional Transactions" : -0.0019, "Performance (Year)" : -0.5343, "LT Debt/Equity" : 0.13, "Average Volume" : 2042.2, "EPS growth this year" : -0.481, "50-Day Simple Moving Average" : -0.0641 }
>
> { "_id" : ObjectId("52853803bb1177ca391c1de4"), "Ticker" : "DDE", "Profit Margin" : -0.001, "Institutional Ownership" : 0.348, "EPS growth past 5 years" : -0.283, "Total Debt/Equity" : 0.46, "Current Ratio" : 0.5, "Return on Assets" : -0.001, "Sector" : "Services", "P/S" : 0.24, "Change from Open" : 0, "Performance (YTD)" : -0.3318, "Performance (Week)" : 0.0068, "Quick Ratio" : 0.4, "P/B" : 0.41, "EPS growth quarter over quarter" : -0.75, "Performance (Quarter)" : 0.0809, "Forward P/E" : 8.65, "200-Day Simple Moving Average" : -0.1523, "Shares Outstanding" : 31.85, "Earnings Date" : ISODate("2013-10-24T12:30:00Z"), "52-Week High" : -0.4691, "P/Cash" : 3.25, "Change" : -0.0068, "Analyst Recom" : 3, "Volatility (Week)" : 0.0333, "Country" : "USA", "Return on Equity" : -0.001, "50-Day Low" : 0.0815, "Price" : 1.46, "50-Day High" : -0.2193, "Return on Investment" : 0.038, "Shares Float" : 16.42, "Dividend Yield" : 0.0544, "Industry" : "Gaming Activities", "Beta" : 1.18, "Sales growth quarter over quarter" : -0.087, "Operating Margin" : 0.01, "EPS (ttm)" : 0, "Float Short" : 0.0024, "52-Week Low" : 0.1967, "Average True Range" : 0.06, "EPS growth next year" : 4.6667, "Sales growth past 5 years" : -0.014, "Company" : "Dover Downs Gaming & Entertainment Inc.", "Gap" : -0.0068, "Relative Volume" : 0.17, "Volatility (Month)" : 0.0505, "Market Cap" : 46.82, "Volume" : 11967, "Gross Margin" : 0.174, "Short Ratio" : 0.51, "Performance (Half Year)" : -0.1302, "Relative Strength Index (14)" : 44.41, "Insider Ownership" : 0.0746, "20-Day Simple Moving Average" : -0.0653, "Performance (Month)" : 0.05, "P/Free Cash Flow" : 6.16, "Institutional Transactions" : -0.2073, "Performance (Year)" : -0.2576, "LT Debt/Equity" : 0, "Average Volume" : 77.9, "EPS growth this year" : -0.118, "50-Day Simple Moving Average" : -0.0071 }
>
> { "_id" : ObjectId("52853803bb1177ca391c1e3a"), "Ticker" : "DLLR", "Profit Margin" : -0.001, "EPS growth past 5 years" : -0.151, "Total Debt/Equity" : 2.42, "Current Ratio" : 2.9, "Return on Assets" : 0, "Sector" : "Financial", "P/S" : 0.45, "Change from Open" : 0.0429, "Performance (YTD)" : -0.3359, "Performance (Week)" : 0.0668, "Quick Ratio" : 2.9, "Insider Transactions" : 0.1594, "P/B" : 1.16, "EPS growth quarter over quarter" : 2.8, "Performance (Quarter)" : -0.2293, "Forward P/E" : 7.02, "200-Day Simple Moving Average" : -0.1192, "Shares Outstanding" : 40.8, "Earnings Date" : ISODate("2013-10-30T20:30:00Z"), "52-Week High" : -0.355, "P/Cash" : 2.56, "Change" : 0.0472, "Analyst Recom" : 2.2, "Volatility (Week)" : 0.0306, "Country" : "USA", "Return on Equity" : -0.002, "50-Day Low" : 0.1817, "Price" : 12.88, "50-Day High" : -0.0257, "Return on Investment" : 0.163, "Shares Float" : 38.4, "EPS growth next 5 years" : 0.17, "Industry" : "Credit Services", "Beta" : 1.46, "Sales growth quarter over quarter" : 0.009, "Operating Margin" : 0.276, "EPS (ttm)" : -0.04, "Float Short" : 0.0641, "52-Week Low" : 0.1915, "Average True Range" : 0.38, "EPS growth next year" : 0.7741, "Sales growth past 5 years" : 0.144, "Company" : "DFC Global Corp.", "Gap" : 0.0041, "Relative Volume" : 0.5, "Volatility (Month)" : 0.0325, "Market Cap" : 501.84, "Volume" : 268706, "Short Ratio" : 4.2, "Performance (Half Year)" : -0.1924, "Relative Strength Index (14)" : 62.18, "Insider Ownership" : 0.023, "20-Day Simple Moving Average" : 0.0553, "Performance (Month)" : 0.0224, "P/Free Cash Flow" : 2.26, "Institutional Transactions" : -0.0621, "Performance (Year)" : -0.1997, "LT Debt/Equity" : 2.26, "Average Volume" : 585.6, "EPS growth this year" : -1.017, "50-Day Simple Moving Average" : 0.0886 }
>
> { "_id" : ObjectId("52853804bb1177ca391c2055"), "Ticker" : "FBC", "Profit Margin" : -0.001, "Institutional Ownership" : 0.889, "EPS growth past 5 years" : 0.15, "Total Debt/Equity" : 0.36, "Return on Assets" : 0, "Sector" : "Financial", "P/S" : 2.72, "Change from Open" : 0.0055, "Performance (YTD)" : -0.0644, "Performance (Week)" : 0.1344, "Insider Transactions" : 0.3017, "P/B" : 1.01, "EPS growth quarter over quarter" : -0.831, "Performance (Quarter)" : 0.1495, "Forward P/E" : 8.03, "200-Day Simple Moving Average" : 0.2482, "Shares Outstanding" : 56.1, "Earnings Date" : ISODate("2013-10-22T20:30:00Z"), "52-Week High" : -0.105, "P/Cash" : 0.36, "Change" : 0.005, "Analyst Recom" : 1.5, "Volatility (Week)" : 0.0485, "Country" : "USA", "Return on Equity" : -0.001, "50-Day Low" : 0.3304, "Price" : 18.24, "50-Day High" : -0.0178, "Return on Investment" : 0.032, "Shares Float" : 55.52, "EPS growth next 5 years" : 0.02, "Industry" : "Savings & Loans", "Beta" : 2.09, "Sales growth quarter over quarter" : -0.342, "Operating Margin" : 0.046, "EPS (ttm)" : -0.03, "Float Short" : 0.0159, "52-Week Low" : 0.4841, "Average True Range" : 0.59, "EPS growth next year" : 0.235, "Sales growth past 5 years" : -0.119, "Company" : "Flagstar Bancorp Inc.", "Gap" : -0.0006, "Relative Volume" : 0.44, "Volatility (Month)" : 0.0351, "Market Cap" : 1018.14, "Volume" : 107544, "Short Ratio" : 3.32, "Performance (Half Year)" : 0.2809, "Relative Strength Index (14)" : 73.67, "Insider Ownership" : 0.006, "20-Day Simple Moving Average" : 0.1039, "Performance (Month)" : 0.2044, "Institutional Transactions" : 0.008, "Performance (Year)" : 0.0881, "LT Debt/Equity" : 0.36, "Average Volume" : 265.99, "EPS growth this year" : 1.242, "50-Day Simple Moving Average" : 0.1914 }
>
> { "_id" : ObjectId("52853806bb1177ca391c247c"), "Ticker" : "ISSI", "Profit Margin" : -0.001, "Institutional Ownership" : 0.843, "EPS growth past 5 years" : -0.176, "Total Debt/Equity" : 0.02, "Current Ratio" : 4.4, "Return on Assets" : -0.001, "Sector" : "Technology", "P/S" : 1.08, "Change from Open" : 0.0009, "Performance (YTD)" : 0.2822, "Performance (Week)" : 0.051, "Quick Ratio" : 3.4, "Insider Transactions" : -0.0035, "P/B" : 1.18, "EPS growth quarter over quarter" : 1.182, "Performance (Quarter)" : 0.1139, "Forward P/E" : 10.05, "200-Day Simple Moving Average" : 0.1053, "Shares Outstanding" : 28.29, "Earnings Date" : ISODate("2013-10-29T12:30:00Z"), "52-Week High" : -0.0458, "P/Cash" : 2.36, "Change" : -0.0078, "Analyst Recom" : 1.8, "Volatility (Week)" : 0.0251, "Country" : "USA", "Return on Equity" : -0.002, "50-Day Low" : 0.0884, "Price" : 11.45, "50-Day High" : -0.0458, "Return on Investment" : -0.003, "Shares Float" : 28.68, "EPS growth next 5 years" : 0.1, "Industry" : "Semiconductor- Memory Chips", "Beta" : 2.18, "Sales growth quarter over quarter" : 0.201, "Operating Margin" : 0.083, "EPS (ttm)" : -0.05, "Float Short" : 0.0224, "52-Week Low" : 0.3946, "Average True Range" : 0.32, "EPS growth next year" : 0.2265, "Sales growth past 5 years" : 0.016, "Company" : "Integrated Silicon Solution Inc.", "Gap" : -0.0087, "Relative Volume" : 0.54, "Volatility (Month)" : 0.0287, "Market Cap" : 326.5, "Volume" : 50070, "Gross Margin" : 0.311, "Short Ratio" : 6.31, "Performance (Half Year)" : 0.0928, "Relative Strength Index (14)" : 54.4, "Insider Ownership" : 0.01, "20-Day Simple Moving Average" : 0.0081, "Performance (Month)" : 0.0044, "P/Free Cash Flow" : 18.55, "Institutional Transactions" : 0.0073, "Performance (Year)" : 0.3481, "LT Debt/Equity" : 0.02, "Average Volume" : 101.81, "EPS growth this year" : -1.051, "50-Day Simple Moving Average" : 0.0164 }
>
> { "_id" : ObjectId("52853808bb1177ca391c27e7"), "Ticker" : "NAFC", "Profit Margin" : -0.001, "Institutional Ownership" : 0.865, "EPS growth past 5 years" : -0.365, "Total Debt/Equity" : 1.42, "Current Ratio" : 2.1, "Return on Assets" : -0.003, "Sector" : "Services", "P/S" : 0.07, "Change from Open" : 0.0021, "Performance (YTD)" : 0.366, "Performance (Week)" : 0.0068, "Quick Ratio" : 0.9, "Insider Transactions" : -0.1024, "P/B" : 1.2, "EPS growth quarter over quarter" : 1.104, "Performance (Quarter)" : 0.154, "Forward P/E" : 13.45, "200-Day Simple Moving Average" : 0.221, "Shares Outstanding" : 12.99, "Earnings Date" : ISODate("2013-11-12T13:30:00Z"), "52-Week High" : -0.0366, "P/Cash" : 305.1, "Change" : -0.0018, "Analyst Recom" : 3, "Volatility (Week)" : 0.0226, "Country" : "USA", "Return on Equity" : -0.011, "50-Day Low" : 0.1647, "Price" : 28.13, "50-Day High" : -0.0366, "Return on Investment" : -0.14, "Shares Float" : 12.25, "Dividend Yield" : 0.0256, "EPS growth next 5 years" : 0.15, "Industry" : "Food Wholesale", "Beta" : 0.78, "Sales growth quarter over quarter" : 0.091, "Operating Margin" : 0.015, "EPS (ttm)" : -0.27, "Float Short" : 0.0162, "52-Week Low" : 0.5626, "Average True Range" : 0.63, "EPS growth next year" : 0.032, "Sales growth past 5 years" : 0.015, "Company" : "Nash Finch Co.", "Gap" : -0.0039, "Relative Volume" : 0.55, "Volatility (Month)" : 0.0229, "Market Cap" : 366.11, "Volume" : 18447, "Gross Margin" : 0.083, "Short Ratio" : 5.36, "Performance (Half Year)" : 0.3529, "Relative Strength Index (14)" : 57.62, "Insider Ownership" : 0.0054, "20-Day Simple Moving Average" : 0.0071, "Performance (Month)" : 0.0395, "Institutional Transactions" : 0.0415, "Performance (Year)" : 0.5604, "LT Debt/Equity" : 1.41, "Average Volume" : 37.08, "EPS growth this year" : -3.642, "50-Day Simple Moving Average" : 0.0506 }
>
> { "_id" : ObjectId("52853808bb1177ca391c28d7"), "Ticker" : "NUVA", "Profit Margin" : -0.001, "Institutional Ownership" : 0.976, "EPS growth past 5 years" : 0.173, "Total Debt/Equity" : 0.6, "Current Ratio" : 4.5, "Return on Assets" : -0.001, "Sector" : "Healthcare", "P/S" : 2.26, "Change from Open" : -0.0045, "Performance (YTD)" : 1.1662, "Performance (Week)" : 0.0571, "Quick Ratio" : 3.2, "Insider Transactions" : -0.2204, "P/B" : 2.62, "EPS growth quarter over quarter" : 2.2, "Performance (Quarter)" : 0.4197, "Forward P/E" : 28.03, "200-Day Simple Moving Average" : 0.4469, "Shares Outstanding" : 44.57, "Earnings Date" : ISODate("2013-10-29T20:30:00Z"), "52-Week High" : -0.0068, "P/Cash" : 7.11, "Change" : -0.0047, "Analyst Recom" : 2.4, "Volatility (Week)" : 0.0322, "Country" : "USA", "Return on Equity" : -0.001, "50-Day Low" : 0.4385, "Price" : 33.33, "50-Day High" : -0.0068, "Return on Investment" : 0.03, "Shares Float" : 44.13, "EPS growth next 5 years" : 0.1247, "Industry" : "Medical Appliances & Equipment", "Beta" : 1.16, "Sales growth quarter over quarter" : 0.14, "Operating Margin" : 0.048, "EPS (ttm)" : -0.03, "Float Short" : 0.0528, "52-Week Low" : 1.5541, "Average True Range" : 1.09, "EPS growth next year" : 0.0585, "Sales growth past 5 years" : 0.321, "Company" : "NuVasive, Inc.", "Gap" : -0.0003, "Relative Volume" : 0.52, "Volatility (Month)" : 0.0346, "Market Cap" : 1492.72, "Volume" : 194293, "Gross Margin" : 0.737, "Short Ratio" : 5.63, "Performance (Half Year)" : 0.4605, "Relative Strength Index (14)" : 74.92, "Insider Ownership" : 0.005, "20-Day Simple Moving Average" : 0.1467, "Performance (Month)" : 0.3248, "P/Free Cash Flow" : 26.1, "Institutional Transactions" : 0.0206, "Performance (Year)" : 1.5124, "LT Debt/Equity" : 0.6, "Average Volume" : 413.48, "EPS growth this year" : 1.04, "50-Day Simple Moving Average" : 0.2656 }

### 3. Liste as 10 ações mais rentáveis

```db.stocks.find().sort({"Profit Margin": -1}).limit(10)```
> { "_id" : ObjectId("52853801bb1177ca391c1af3"), "Ticker" : "BPT", "Profit Margin" : 0.994, "Institutional Ownership" : 0.098, "EPS growth past 5 years" : 0.025, "Total Debt/Equity" : 0, "Sector" : "Basic Materials", "P/S" : 8.81, "Change from Open" : 0.0125, "Performance (YTD)" : 0.2758, "Performance (Week)" : -0.018, "P/B" : 2620, "EPS growth quarter over quarter" : -0.087, "Payout Ratio" : 1.001, "Performance (Quarter)" : -0.0556, "P/E" : 8.87, "200-Day Simple Moving Average" : -0.0305, "Shares Outstanding" : 21.4, "Earnings Date" : ISODate("2013-11-11T05:00:00Z"), "52-Week High" : -0.159, "P/Cash" : 1682.04, "Change" : 0.0064, "Volatility (Week)" : 0.0151, "Country" : "USA", "50-Day Low" : 0.0136, "Price" : 79.1, "50-Day High" : -0.0973, "Shares Float" : 21.4, "Dividend Yield" : 0.1103, "Industry" : "Oil & Gas Refining & Marketing", "Beta" : 0.77, "Sales growth quarter over quarter" : -0.086, "Operating Margin" : 0.994, "EPS (ttm)" : 8.86, "Float Short" : 0.0173, "52-Week Low" : 0.3422, "Average True Range" : 1.37, "Sales growth past 5 years" : 0.024, "Company" : "BP Prudhoe Bay Royalty Trust", "Gap" : -0.0061, "Relative Volume" : 0.93, "Volatility (Month)" : 0.016, "Market Cap" : 1682.04, "Volume" : 71575, "Short Ratio" : 4.41, "Performance (Half Year)" : 0.0022, "Relative Strength Index (14)" : 38.01, "20-Day Simple Moving Average" : -0.0318, "Performance (Month)" : -0.079, "Institutional Transactions" : -0.0057, "Performance (Year)" : 0.1837, "LT Debt/Equity" : 0, "Average Volume" : 84.15, "EPS growth this year" : -0.012, "50-Day Simple Moving Average" : -0.0496 }
>
> { "_id" : ObjectId("52853802bb1177ca391c1b69"), "Ticker" : "CACB", "Profit Margin" : 0.994, "Institutional Ownership" : 0.547, "EPS growth past 5 years" : -0.584, "Total Debt/Equity" : 0, "Return on Assets" : 0.039, "Sector" : "Financial", "P/S" : 4.66, "Change from Open" : -0.0137, "Performance (YTD)" : -0.1869, "Performance (Week)" : 0.0079, "Insider Transactions" : 1.0755, "P/B" : 1.28, "EPS growth quarter over quarter" : 25.422, "Payout Ratio" : 0, "Performance (Quarter)" : -0.1314, "Forward P/E" : 42.42, "P/E" : 4.71, "200-Day Simple Moving Average" : -0.1709, "Shares Outstanding" : 47.17, "Earnings Date" : ISODate("2013-11-13T21:30:00Z"), "52-Week High" : -0.2994, "P/Cash" : 2.26, "Change" : -0.0118, "Analyst Recom" : 3, "Volatility (Week)" : 0.0353, "Country" : "USA", "Return on Equity" : 0.336, "50-Day Low" : 0.006, "Price" : 5.03, "50-Day High" : -0.2066, "Return on Investment" : 0.346, "Shares Float" : 40.67, "EPS growth next 5 years" : 0.05, "Industry" : "Regional - Pacific Banks", "Beta" : 2.34, "Sales growth quarter over quarter" : -0.101, "Operating Margin" : 0.027, "EPS (ttm)" : 1.08, "PEG" : 0.94, "Float Short" : 0.0088, "52-Week Low" : 0.0817, "Average True Range" : 0.19, "EPS growth next year" : -0.8904, "Sales growth past 5 years" : -0.203, "Company" : "Cascade Bancorp", "Gap" : 0.002, "Relative Volume" : 1.35, "Volatility (Month)" : 0.0399, "Market Cap" : 240.11, "Volume" : 21432, "Short Ratio" : 20.55, "Performance (Half Year)" : -0.1239, "Relative Strength Index (14)" : 29.61, "Insider Ownership" : 0.009, "20-Day Simple Moving Average" : -0.0729, "Performance (Month)" : -0.1039, "Institutional Transactions" : 0.0004, "Performance (Year)" : 0.0241, "LT Debt/Equity" : 0, "Average Volume" : 17.39, "EPS growth this year" : 1.12, "50-Day Simple Moving Average" : -0.116 }
>
> { "_id" : ObjectId("5285380bbb1177ca391c2c3c"), "Ticker" : "ROYT", "Profit Margin" : 0.99, "Institutional Ownership" : 0.696, "EPS growth past 5 years" : 0, "Total Debt/Equity" : 0, "Return on Assets" : 0.255, "Sector" : "Basic Materials", "P/S" : 7.62, "Change from Open" : 0, "Performance (YTD)" : -0.1408, "Performance (Week)" : -0.0447, "Insider Transactions" : -0.5437, "P/B" : 2.03, "EPS growth quarter over quarter" : 1.75, "Payout Ratio" : 0.338, "Performance (Quarter)" : -0.2202, "Forward P/E" : 7.92, "P/E" : 7.68, "200-Day Simple Moving Average" : -0.1864, "Shares Outstanding" : 38.58, "52-Week High" : -0.243, "Change" : 0.0037, "Analyst Recom" : 2.4, "Volatility (Week)" : 0.0174, "Country" : "USA", "Return on Equity" : 0.255, "50-Day Low" : 0.0088, "Price" : 13.72, "50-Day High" : -0.243, "Return on Investment" : 0.15, "Shares Float" : 38.58, "Dividend Yield" : 0.1295, "EPS growth next 5 years" : 0.126, "Industry" : "Independent Oil & Gas", "Sales growth quarter over quarter" : 1.6, "Operating Margin" : 0.99, "EPS (ttm)" : 1.78, "PEG" : 0.61, "Float Short" : 0.0042, "52-Week Low" : 0.0088, "Average True Range" : 0.3, "EPS growth next year" : -0.0655, "Company" : "Pacific Coast Oil Trust", "Gap" : 0.0037, "Relative Volume" : 0.75, "Volatility (Month)" : 0.0201, "Market Cap" : 527.43, "Volume" : 262050, "Short Ratio" : 0.42, "Performance (Half Year)" : -0.1978, "Relative Strength Index (14)" : 20.73, "Insider Ownership" : 0.5205, "20-Day Simple Moving Average" : -0.0644, "Performance (Month)" : -0.1237, "Institutional Transactions" : 0.0154, "Performance (Year)" : -0.1141, "LT Debt/Equity" : 0, "Average Volume" : 388.63, "EPS growth this year" : 0.745, "50-Day Simple Moving Average" : -0.1265 }
>
> { "_id" : ObjectId("52853808bb1177ca391c281b"), "Ticker" : "NDRO", "Profit Margin" : 0.986, "Institutional Ownership" : 0.532, "EPS growth past 5 years" : 0, "Total Debt/Equity" : 0, "Return on Assets" : 0.078, "Sector" : "Basic Materials", "P/S" : 8.11, "Change from Open" : 0, "Performance (YTD)" : -0.2111, "Performance (Week)" : -0.0369, "Insider Transactions" : -0.3613, "P/B" : 0.67, "EPS growth quarter over quarter" : -0.378, "Payout Ratio" : 0.313, "Performance (Quarter)" : -0.1716, "Forward P/E" : 7.53, "P/E" : 8.23, "200-Day Simple Moving Average" : -0.1708, "Shares Outstanding" : 33, "Earnings Date" : ISODate("2013-11-11T05:00:00Z"), "52-Week High" : -0.2732, "Change" : -0.0073, "Analyst Recom" : 2.3, "Volatility (Week)" : 0.0224, "Country" : "USA", "Return on Equity" : 0.078, "50-Day Low" : 0.0437, "Price" : 12.17, "50-Day High" : -0.2028, "Return on Investment" : 0.091, "Shares Float" : 33, "Dividend Yield" : 0.1476, "EPS growth next 5 years" : -0.061, "Industry" : "Independent Oil & Gas", "Sales growth quarter over quarter" : -0.388, "Operating Margin" : 0.986, "EPS (ttm)" : 1.49, "Float Short" : 0.0011, "52-Week Low" : 0.0437, "Average True Range" : 0.26, "EPS growth next year" : 0.1097, "Company" : "Enduro Royalty Trust", "Gap" : -0.0073, "Relative Volume" : 1.43, "Volatility (Month)" : 0.0205, "Market Cap" : 404.58, "Volume" : 406061, "Short Ratio" : 0.12, "Performance (Half Year)" : -0.2106, "Relative Strength Index (14)" : 33.3, "Insider Ownership" : 0.6, "20-Day Simple Moving Average" : -0.0471, "Performance (Month)" : 0.0381, "Institutional Transactions" : 0.1111, "Performance (Year)" : -0.1987, "LT Debt/Equity" : 0, "Average Volume" : 311.39, "EPS growth this year" : 4.677, "50-Day Simple Moving Average" : -0.0824 }
>
> { "_id" : ObjectId("5285380fbb1177ca391c318e"), "Ticker" : "WHZ", "Profit Margin" : 0.982, "Institutional Ownership" : 0.199, "EPS growth past 5 years" : 0, "Total Debt/Equity" : 0, "Return on Assets" : 0.321, "Sector" : "Basic Materials", "P/S" : 4.79, "Change from Open" : -0.0042, "Performance (YTD)" : 0.0782, "Performance (Week)" : 0.0369, "P/B" : 1.67, "EPS growth quarter over quarter" : -0.337, "Payout Ratio" : 1.003, "Performance (Quarter)" : 0.0955, "P/E" : 4.88, "200-Day Simple Moving Average" : 0.0993, "Shares Outstanding" : 18.4, "52-Week High" : -0.0668, "P/Cash" : 1319.28, "Change" : -0.0042, "Analyst Recom" : 3, "Volatility (Week)" : 0.0183, "Country" : "USA", "Return on Equity" : 0.321, "50-Day Low" : 0.12, "Price" : 14.28, "50-Day High" : -0.0138, "Return on Investment" : 0.28, "Shares Float" : 18.4, "Dividend Yield" : 0.2064, "Industry" : "Independent Oil & Gas", "Sales growth quarter over quarter" : -0.343, "Operating Margin" : 0.982, "EPS (ttm)" : 2.94, "Float Short" : 0.004, "52-Week Low" : 0.2081, "Average True Range" : 0.26, "Company" : "Whiting USA Trust II", "Gap" : 0, "Relative Volume" : 2.18, "Volatility (Month)" : 0.0194, "Market Cap" : 263.86, "Volume" : 244298, "Short Ratio" : 0.6, "Performance (Half Year)" : 0.1282, "Relative Strength Index (14)" : 68.85, "20-Day Simple Moving Average" : 0.0301, "Performance (Month)" : 0.0864, "Institutional Transactions" : 0.0834, "Performance (Year)" : -0.0311, "LT Debt/Equity" : 0, "Average Volume" : 123.73, "50-Day Simple Moving Average" : 0.0734 }
>
> { "_id" : ObjectId("52853808bb1177ca391c27bd"), "Ticker" : "MVO", "Profit Margin" : 0.976, "Institutional Ownership" : 0.048, "EPS growth past 5 years" : 0.044, "Total Debt/Equity" : 0, "Return on Assets" : 1.258, "Sector" : "Basic Materials", "P/S" : 8.25, "Change from Open" : 0.0176, "Performance (YTD)" : 0.2883, "Performance (Week)" : -0.0007, "P/B" : 11.04, "EPS growth quarter over quarter" : -0.147, "Payout Ratio" : 1, "Performance (Quarter)" : 0.0678, "P/E" : 8.43, "200-Day Simple Moving Average" : 0.0422, "Shares Outstanding" : 11.5, "Earnings Date" : ISODate("2013-11-11T05:00:00Z"), "52-Week High" : -0.0998, "Change" : 0.0131, "Analyst Recom" : 4, "Volatility (Week)" : 0.0108, "Country" : "USA", "Return on Equity" : 1.258, "50-Day Low" : 0.0765, "Price" : 27.75, "50-Day High" : -0.0611, "Return on Investment" : 1.355, "Shares Float" : 8.63, "Dividend Yield" : 0.1431, "EPS growth next 5 years" : 0.07, "Industry" : "Oil & Gas Drilling & Exploration", "Beta" : 0.45, "Sales growth quarter over quarter" : -0.143, "Operating Margin" : 0.979, "EPS (ttm)" : 3.25, "PEG" : 1.2, "Float Short" : 0.0123, "52-Week Low" : 0.3994, "Average True Range" : 0.54, "Sales growth past 5 years" : 0.043, "Company" : "MV Oil Trust", "Gap" : -0.0044, "Relative Volume" : 0.36, "Volatility (Month)" : 0.0202, "Market Cap" : 314.98, "Volume" : 14403, "Short Ratio" : 2.44, "Performance (Half Year)" : 0.0367, "Relative Strength Index (14)" : 55.11, "Insider Ownership" : 0.25, "20-Day Simple Moving Average" : 0.0181, "Performance (Month)" : 0.0092, "Institutional Transactions" : -0.0054, "Performance (Year)" : 0.2008, "LT Debt/Equity" : 0, "Average Volume" : 43.54, "EPS growth this year" : 0.029, "50-Day Simple Moving Average" : 0.0058 }
>
> { "_id" : ObjectId("52853801bb1177ca391c1895"), "Ticker" : "AGNC", "Profit Margin" : 0.972, "Institutional Ownership" : 0.481, "EPS growth past 5 years" : -0.0107, "Total Debt/Equity" : 8.56, "Return on Assets" : 0.022, "Sector" : "Financial", "P/S" : 3.77, "Change from Open" : 0.0102, "Performance (YTD)" : -0.1652, "Performance (Week)" : -0.017, "Insider Transactions" : 0.4931, "P/B" : 0.86, "EPS growth quarter over quarter" : -8.2, "Payout Ratio" : 0.79, "Performance (Quarter)" : -0.0083, "Forward P/E" : 7.64, "P/E" : 3.68, "200-Day Simple Moving Average" : -0.1282, "Shares Outstanding" : 390.6, "Earnings Date" : ISODate("2013-10-28T20:30:00Z"), "52-Week High" : -0.2938, "P/Cash" : 3.93, "Change" : 0.0131, "Analyst Recom" : 2.6, "Volatility (Week)" : 0.0268, "Country" : "USA", "Return on Equity" : 0.205, "50-Day Low" : 0.0695, "Price" : 21.71, "50-Day High" : -0.1066, "Return on Investment" : 0.015, "Shares Float" : 383.97, "Dividend Yield" : 0.1493, "EPS growth next 5 years" : 0.035, "Industry" : "REIT - Residential", "Beta" : 0.51, "Sales growth quarter over quarter" : 0.073, "Operating Margin" : 0.67, "EPS (ttm)" : 5.82, "PEG" : 1.05, "Float Short" : 0.0311, "52-Week Low" : 0.1117, "Average True Range" : 0.52, "EPS growth next year" : -0.3603, "Company" : "American Capital Agency Corp.", "Gap" : 0.0028, "Relative Volume" : 0.71, "Volatility (Month)" : 0.02, "Market Cap" : 8370.56, "Volume" : 4576064, "Gross Margin" : 0.746, "Short Ratio" : 1.69, "Performance (Half Year)" : -0.2136, "Relative Strength Index (14)" : 43.53, "Insider Ownership" : 0.003, "20-Day Simple Moving Average" : -0.0318, "Performance (Month)" : -0.042, "Institutional Transactions" : 0.0077, "Performance (Year)" : -0.1503, "LT Debt/Equity" : 0, "Average Volume" : 7072.83, "EPS growth this year" : -0.169, "50-Day Simple Moving Average" : -0.0376 }
>
> { "_id" : ObjectId("5285380ebb1177ca391c3101"), "Ticker" : "VOC", "Profit Margin" : 0.971, "Institutional Ownership" : 0.161, "EPS growth past 5 years" : 0, "Total Debt/Equity" : 0, "Return on Assets" : 0.253, "Sector" : "Basic Materials", "P/S" : 9.03, "Change from Open" : -0.0129, "Performance (YTD)" : 0.4186, "Performance (Week)" : 0.0103, "P/B" : 2.44, "EPS growth quarter over quarter" : -0.304, "Payout Ratio" : 1, "Performance (Quarter)" : 0.1116, "P/E" : 9.3, "200-Day Simple Moving Average" : 0.2104, "Shares Outstanding" : 17, "52-Week High" : -0.0417, "P/Cash" : 948.6, "Change" : 0.0024, "Analyst Recom" : 3, "Volatility (Week)" : 0.0289, "Country" : "USA", "Return on Equity" : 0.253, "50-Day Low" : 0.1106, "Price" : 16.78, "50-Day High" : -0.0417, "Return on Investment" : 0.304, "Shares Float" : 12.75, "Dividend Yield" : 0.1266, "Industry" : "Independent Oil & Gas", "Sales growth quarter over quarter" : -0.286, "Operating Margin" : 0.971, "EPS (ttm)" : 1.8, "Float Short" : 0.006, "52-Week Low" : 0.529, "Average True Range" : 0.47, "Company" : "VOC Energy Trust", "Gap" : 0.0155, "Relative Volume" : 0.47, "Volatility (Month)" : 0.0292, "Market Cap" : 284.58, "Volume" : 32718, "Short Ratio" : 0.98, "Performance (Half Year)" : 0.2847, "Relative Strength Index (14)" : 54.66, "Insider Ownership" : 0.3505, "20-Day Simple Moving Average" : 0.009, "Performance (Month)" : 0.0582, "Institutional Transactions" : -0.0349, "Performance (Year)" : 0.3892, "LT Debt/Equity" : 0, "Average Volume" : 77.47, "EPS growth this year" : 0.542, "50-Day Simple Moving Average" : 0.0418 }
>
> { "_id" : ObjectId("52853807bb1177ca391c279a"), "Ticker" : "MTR", "Profit Margin" : 0.97, "Institutional Ownership" : 0.024, "EPS growth past 5 years" : -0.217, "Total Debt/Equity" : 0, "Return on Assets" : 0.518, "Sector" : "Financial", "P/S" : 12.1, "Change from Open" : -0.0038, "Performance (YTD)" : 0.1833, "Performance (Week)" : -0.0241, "P/B" : 7.82, "EPS growth quarter over quarter" : -0.255, "Payout Ratio" : 0.997, "Performance (Quarter)" : 0.0156, "P/E" : 12.68, "200-Day Simple Moving Average" : -0.0568, "Shares Outstanding" : 1.86, "Earnings Date" : ISODate("2013-11-11T05:00:00Z"), "52-Week High" : -0.1539, "P/Cash" : 23.5, "Change" : -0.0135, "Volatility (Week)" : 0.018, "Country" : "USA", "Return on Equity" : 0.593, "50-Day Low" : 0.0168, "Price" : 21.14, "50-Day High" : -0.1062, "Return on Investment" : 0.655, "Shares Float" : 1.86, "Dividend Yield" : 0.0845, "Industry" : "Diversified Investments", "Beta" : 0.93, "Sales growth quarter over quarter" : -0.222, "Operating Margin" : 0.939, "EPS (ttm)" : 1.69, "Float Short" : 0.0004, "52-Week Low" : 0.2026, "Average True Range" : 0.53, "Sales growth past 5 years" : -0.208, "Company" : "Mesa Royalty Trust", "Gap" : -0.0098, "Relative Volume" : 1.14, "Volatility (Month)" : 0.0226, "Market Cap" : 39.95, "Volume" : 4150, "Short Ratio" : 0.2, "Performance (Half Year)" : -0.1221, "Relative Strength Index (14)" : 34.54, "Insider Ownership" : 0.0385, "20-Day Simple Moving Average" : -0.0408, "Performance (Month)" : -0.0294, "Institutional Transactions" : -0.4527, "Performance (Year)" : 0.0418, "LT Debt/Equity" : 0, "Average Volume" : 3.97, "EPS growth this year" : -0.348, "50-Day Simple Moving Average" : -0.0539 }
>
> { "_id" : ObjectId("52853809bb1177ca391c2946"), "Ticker" : "OLP", "Profit Margin" : 0.97, "Institutional Ownership" : 0.481, "EPS growth past 5 years" : 0.008, "Total Debt/Equity" : 0.91, "Return on Assets" : 0.072, "Sector" : "Financial", "P/S" : 8.28, "Change from Open" : 0.0072, "Performance (YTD)" : 0.0398, "Performance (Week)" : -0.0156, "Insider Transactions" : 0.0039, "P/B" : 1.2, "EPS growth quarter over quarter" : 1.261, "Payout Ratio" : 0.456, "Performance (Quarter)" : -0.0804, "Forward P/E" : 10.48, "P/E" : 22.12, "200-Day Simple Moving Average" : -0.0742, "Shares Outstanding" : 14.84, "Earnings Date" : ISODate("2013-05-06T04:00:00Z"), "52-Week High" : -0.2453, "P/Cash" : 7.31, "Change" : 0.0077, "Analyst Recom" : 3, "Volatility (Week)" : 0.0166, "Country" : "USA", "Return on Equity" : 0.146, "50-Day Low" : 0.027, "Price" : 20.28, "50-Day High" : -0.1166, "Return on Investment" : 0.051, "Shares Float" : 13.88, "Dividend Yield" : 0.0695, "EPS growth next 5 years" : 0.111, "Industry" : "REIT - Diversified", "Beta" : 2.2, "Sales growth quarter over quarter" : 0.099, "Operating Margin" : 0.537, "EPS (ttm)" : 0.91, "PEG" : 1.99, "Float Short" : 0.0126, "52-Week Low" : 0.2285, "Average True Range" : 0.45, "EPS growth next year" : 0.171, "Sales growth past 5 years" : 0.06, "Company" : "One Liberty Properties Inc.", "Gap" : 0.0005, "Relative Volume" : 0.2, "Volatility (Month)" : 0.023, "Market Cap" : 298.81, "Volume" : 6907, "Gross Margin" : 0.983, "Short Ratio" : 4.64, "Performance (Half Year)" : -0.2219, "Relative Strength Index (14)" : 37.17, "Insider Ownership" : 0.158, "20-Day Simple Moving Average" : -0.0315, "Performance (Month)" : -0.0455, "Institutional Transactions" : 0.0003, "Performance (Year)" : 0.1663, "LT Debt/Equity" : 0.91, "Average Volume" : 37.56, "EPS growth this year" : -0.013, "50-Day Simple Moving Average" : -0.0356 }

### 4. Qual foi o setor mais rentável?

### 5. Ordene as ações pelo profit e usando um cursor, liste as ações.

### 6. Renomeie o campo “Profit Margin” para apenas “profit”.

### 7. Agora liste apenas a empresa e seu respectivo resultado

### 8. Analise as ações. É uma bola de cristal na sua mão... Quais as três ações você investiria?

### 9. Liste as ações agrupadas por setor
