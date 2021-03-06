# Investimento Cliente Custódia - Guia de início rápido

## O QUE É
Serviço responsável por consolidar todas as custódias dos clientes, na qual realizará integração com outros serviços.

## A QUEM SE DESTINA / OBJETIVO
Este projeto se trata de um case técnico abordado pela empresta XPTO. 
O objetivo deste documento é facilitar a compreenção do escopo da solução e de como funciona.

## INICIANDO...
- `git clone https://github.com/lucasolvalves/investimento-cliente-custodia.git`

## PRÉ-REQUISITOS
- `dotnet --version`
Você deverá ver a indicação da versão do dotnet instalado.
Observe que para executar o projeto é necessario possuir a 5.0.

## REQUEST

`GET /api/v1/clientes/{accountId}/investimentos_consolidados`

**Parameters**

|          Name | Required |  Type   | Description                                                                                                                                                           |
| -------------:|:--------:|:-------:| --------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
|     `product` | required | string  | The product for which to perform the action. <br/><br/> Supported values: `publish` or `analyze`.

    curl -X GET "https://investimentoclientecustodia.azurewebsites.net/api/v1/clientes/123456/investimentos_consolidados" -H  "accept: application/json"

## DESENHO DA ARQUITETURA
![](https://raw.githubusercontent.com/lucasolvalves/investimento-cliente-custodia/main/design_investimento_cliente_cust%C3%B3dia.png)

## DEPENDÊNCIAS

* [Investimento tesouro direto](https://github.com/lucasolvalves/investimento-tesourodireto)<br>
* [Investimento renda fixa](https://github.com/lucasolvalves/investimento-rendafixa)<br>
* [Investimento fundo](https://github.com/lucasolvalves/investimento-fundo)

## SOBRE O AUTOR/ORGANIZADOR
Lucas de Oliveira Alves<br>
lucas.olvalves@gmail.com
