
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

### 19. Agora faça a mesma lista do item acima, considerando nome completo

### 20. Procure pessoas que gosta de Banana ou Maçã, tenham cachorro ou gato, mais de 20 e menos de 60 anos
