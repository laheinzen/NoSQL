# Redis

## Ativar o ambiente

Inicialmente havia tentando usando o redis via docker.

Foi abortado e usado o Windows mesmo.

Para ativar, basta seguir orientações do material, ou seja: chocolatey.

## Código em C Sharp

**Atenção**: o código em questão dá um FlushAll em todo o banco Redis cada vez que é executado.

A linguagem utilizada foi o C#, usando o .Net core e pegando a biblioteca mais popular que encontrei.

### Dúvidas

Sobre o 'score'.

No PDF da atividade PDF para armazenar como um Hash. E depois menciona que devemos usar um Sorted Set.

Isso é redundante, não?

Inicialmente eu havia entendido que ali no Hash teria que ter a informação do Score. Estava guardandos valores como

```redis
127.0.0.1:6379> hgetall user:01
1) "name"
2) "user01"
3) "bcartela"
4) "cartela:01"
5) "bscore"
6) "score:0"
```

Fazendo assim, dá erro quando tentar usar o `HINCRBY`. Porque não tem um valor inteiro ali. Então pasei a armazenar como

```redis
127.0.0.1:6379> hgetall user:14
1) "name"
2) "user14"
3) "bcartela"
4) "cartela:14"
5) "bscore"
6) "1"
```

Mas daí vendo o PDF, pede para guardar como `user:01 -> name: “user01”, bcartela: “cartela:01”, bscore: “score:01”`. Como se fosse uma chave para outra estrutura.

O que acabei fazendo foi deixar redundate. Mantenho o score como inteiro no hash e também mantenho o score com Sorted Set. Assim pratica os dois. Mas o cálculo será com base no Score do Sorted Set.

### Figuras

Início do Bingo:

![Início](https://github.com/laheinzen/NoSQL/blob/master/redis/redisngo01.png "Boa sorte")

Fim do Bingo:

![Final](https://github.com/laheinzen/NoSQL/blob/master/redis/redisngo02.png "BINGO!")
