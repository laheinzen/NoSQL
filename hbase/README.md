
# HBase

## 1. Crie a tabela com 2 famílias de colunas

### 1.a personal-data

### 1.b professional-data

`create 'italians', 'personal-data', 'professional-data'`
> Created table italians
>
> Took 1.2598 seconds
>
> => Hbase::Table - italians

## 2. Importe o arquivo via linha de comando

`hbase shell /tmp/italians.txt`

Agora execute as seguintes operações:

### 3.1. Adicione mais 2 italianos mantendo adicionando informações como data de nascimento nas informações pessoais e um atributo de anos de experiência nas informações profissionais

```bash
put 'italians', '11', 'personal-data:name', 'Roberto Baggio'
put 'italians', '11', 'personal-data:birthday', '1967-02-18'
put 'italians', '11', 'personal-data:city', 'Verona'
put 'italians', '11', 'professional-data:role', 'Jogador de Futebol'
put 'italians', '11', 'professional-data:salary', '180000'
put 'italians', '11', 'professional-data:experience', '18'
put 'italians', '12', 'personal-data:name', 'Giancarlo Fisichella'
put 'italians', '12', 'personal-data:birthday', '1973-01-14'
put 'italians', '11', 'personal-data:city', 'Milan'
put 'italians', '11', 'professional-data:role', 'Piloto'
put 'italians', '11', 'professional-data:salary', '90000'
put 'italians', '12', 'professional-data:experience', '14'
```

### 3.2 Adicione o controle de 5 versões na tabela de dados pessoais

`alter 'italians', NAME => 'personal-data', VERSIONS => 5`
> Updating all regions with the new schema...
>
> 1/1 regions updated.


### 3.3 Faça 5 alterações em um dos italianos

```bash
put 'italians', '12', 'personal-data:city', 'Austin'
put 'italians', '12', 'personal-data:city', 'Rio de Janeiro'
put 'italians', '12', 'personal-data:city', 'Montreal'
put 'italians', '12', 'personal-data:city', 'Abu Dhabi'
put 'italians', '12', 'personal-data:city', 'Monaco'
```

### 3.4 Com o operador get, verifique como o HBase armazenou o histórico

```bash
get 'italians', '12', { COLUMN=>'personal-data:city', VERSIONS=>5 }
COLUMN                         CELL
 personal-data:city            timestamp=1588383661504, value=Monaco
 personal-data:city            timestamp=1588383660020, value=Abu Dhabi
 personal-data:city            timestamp=1588383659998, value=Montreal
 personal-data:city            timestamp=1588383659980, value=Rio de Janeiro
 personal-data:city            timestamp=1588383659954, value=Austin
```

### 3.5 Utilize o scan para mostrar apenas o nome e profissão dos italianos

```bash
scan 'italians', { COLUMNS => ['personal-data:name','professional-data:role'] }
ROW                            COLUMN+CELL
 1                             column=personal-data:name, timestamp=1588382225166, value=Paolo Sorrentino
 1                             column=professional-data:role, timestamp=1588382225218, value=Gestao Comercial
 10                            column=personal-data:name, timestamp=1588382225406, value=Giovanna Caputo
 10                            column=professional-data:role, timestamp=1588382225417, value=Comunicacao Institucional
 11                            column=personal-data:name, timestamp=1588383308021, value=Roberto Baggio
 11                            column=professional-data:role, timestamp=1588383308094, value=Jogador de Futebol
 12                            column=personal-data:name, timestamp=1588383308191, value=Giancarlo Fisichella
 2                             column=personal-data:name, timestamp=1588382225228, value=Domenico Barbieri
 2                             column=professional-data:role, timestamp=1588382225239, value=Psicopedagogia
 3                             column=personal-data:name, timestamp=1588382225250, value=Maria Parisi
 3                             column=professional-data:role, timestamp=1588382225263, value=Optometria
 4                             column=personal-data:name, timestamp=1588382225276, value=Silvia Gallo
 4                             column=professional-data:role, timestamp=1588382225286, value=Engenharia Industrial Madeireira
 5                             column=personal-data:name, timestamp=1588382225299, value=Rosa Donati
 5                             column=professional-data:role, timestamp=1588382225313, value=Mecatronica Industrial
 6                             column=personal-data:name, timestamp=1588382225324, value=Simone Lombardo
 6                             column=professional-data:role, timestamp=1588382225334, value=Biotecnologia e Bioquimica
 7                             column=personal-data:name, timestamp=1588382225344, value=Barbara Ferretti
 7                             column=professional-data:role, timestamp=1588382225354, value=Libras
 8                             column=personal-data:name, timestamp=1588382225364, value=Simone Ferrara
 8                             column=professional-data:role, timestamp=1588382225374, value=Engenharia de Minas
 9                             column=personal-data:name, timestamp=1588382225385, value=Vincenzo Giordano
 9                             column=professional-data:role, timestamp=1588382225395, value=Marketing
12 row(s)
```

### 3.6 Apague os italianos com row id ímpar

```bash
deleteall 'italians', '1'
deleteall 'italians', '3'
deleteall 'italians', '5'
deleteall 'italians', '7'
deleteall 'italians', '9'
deleteall 'italians', '11'
```

### 3.7 Crie um contador de idade 55 para o italiano de row id 5

```bash
incr 'italians', '12', 'personal-data:age', 55
COUNTER VALUE = 55
```

### 3.8 Incremente a idade do italiano em 1

```bash
incr 'italians', '5', 'personal-data:age', 1
COUNTER VALUE = 56
```
