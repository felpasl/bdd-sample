# Exercicio para a Turma de Trainees

## *Exercício de BDD - Consulta de lista de produtos*

## Feature: Consulta de lista de produtos

**Como** um usuário do sistema  
**Eu quero** poder consultar uma lista de produtos  
**Para que** eu possa saber quais produtos estão disponíveis para compra  

## Cenários

### Cenário 1: Consulta de lista de produtos com sucesso

**Dado** que tenho acesso à API de listagem de produtos  
**Quando** eu envio uma requisição GET para o endpoint `/produtos`  
**Então** a API deve retornar uma lista de produtos  
**E** cada produto deve conter um ID, nome, descrição e preço  

### Cenário 2: Consulta de lista de produtos com filtro

**Dado** que tenho acesso à API de listagem de produtos  
**E** existem produtos cadastrados com as palavras "camiseta" e "azul" na descrição  
**Quando** eu envio uma requisição GET para o endpoint `/produtos` com os parâmetros "filtro=camiseta&filtro=azul"  
**Então** a API deve retornar uma lista de produtos que contenham as palavras "camiseta" e "azul" na descrição  
**E** cada produto deve conter um ID, nome, descrição e preço  

### Cenário 3: Consulta de lista de produtos com paginação

**Dado** que tenho acesso à API de listagem de produtos  
**E** existem 20 produtos cadastrados  
**Quando** eu envio uma requisição GET para o endpoint `/produtos` com os parâmetros "page=2&size=5"  
**Então** a API deve retornar uma lista de 5 produtos, começando a partir do 6º produto  
**E** cada produto deve conter um ID, nome, descrição e preço  

### Cenário 4: Consulta de lista de produtos com ordenação

**Dado** que tenho acesso à API de listagem de produtos  
**E** existem produtos cadastrados com os preços 100, 50 e 200  
**Quando** eu envio uma requisição GET para o endpoint `/produtos` com o parâmetro "sort=preco"  
**Então** a API deve retornar uma lista de produtos ordenados pelo preço em ordem crescente  
**E** cada produto deve conter um ID, nome, descrição e preço