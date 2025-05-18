# MottuSense (Moto Service)

## Arquitetura do Projeto
<img src="https://github.com/vitorvhsilva/MottuSense-dotNet/blob/main/assets/arquitetura_mottusense.png">
Essa é a solução completa que vamos entregar pra Mottu.

- Nosso Front End se comunica com as nossas API`s de Java e .NET
- A API de Java é focada em guardar informações do usuário e autenticar ele.
- A API de .NET é responsável por guardar as informações dos pátios, motos e notificações do aplicativo. 


## Descrição do Projeto
Esta API fornece endpoints para gerenciamento de motos, seus eventos e localizações em pátios específicos. A solução é dividida em dois controllers principais:
- MotoController: Gerencia o CRUD completo das motos e suas localizações
- EventoMotoController: Gerencia os eventos relacionados às motos

## Rotas da API

### MotoController

| Método | Endpoint               | Descrição                                  | Parâmetros                           | Status Codes                        |
|--------|------------------------|-------------------------------------------|--------------------------------------|-------------------------------------|
| `GET`  | `/patios/{id}`         | Lista todas motos de um pátio específico  | `id`: ID do pátio (Path)            | 200 OK, 400 Bad Request             |
| `GET`  | `/{id}`                | Obtém detalhes completos de uma moto      | `id`: ID da moto (Path)             | 200 OK, 404 Not Found               |
| `POST` | `/`                    | Cadastra uma nova moto no sistema         | JSON da moto (Body)                 | 201 Created, 400 Bad Request        |
| `PUT`  | `/`                    | Atualiza informações de uma moto existente| JSON atualizado (Body)              | 200 OK, 404 Not Found               |
| `DELETE`| `/{id}`               | Remove permanentemente uma moto           | `id`: ID da moto (Path)             | 204 No Content, 404 Not Found       |

### EventoMotoController 

| Método | Endpoint               | Descrição                                  | Parâmetros                           | Status Codes                        |
|--------|------------------------|-------------------------------------------|--------------------------------------|-------------------------------------|
| `POST` | `/`                    | Registra um novo evento no sistema        | JSON do evento (Body)               | 201 Created, 400 Bad Request        |
| `GET`  | `/{IdEventoMoto}`      | Obtém detalhes de um evento específico    | `IdEventoMoto`: ID do evento (Path) | 200 OK, 404 Not Found               |
| `GET`  | `/motos/{IdMoto}`      | Lista todos eventos associados a uma moto | `IdMoto`: ID da moto (Path)         | 200 OK, 404 Not Found               |
| `GET`  | `/patios/{IdPatio}`    | Lista eventos ocorridos em um pátio       | `IdPatio`: ID do pátio (Path)       | 200 OK, 404 Not Found               |
| `PATCH`| `/visualizar`          | Marca múltiplos eventos como visualizados | IDs dos eventos (Body)              | 200 OK, 400 Bad Request             |

## Instalação

1. Clonar repositório:
```bash
git clone https://github.com/vitorvhsilva/MottuSense-dotNet
cd MottuSense-dotNet
```

2. Configurar conexão (appsettings.json):
```bash
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "Oracle": "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=)(PORT=))) (CONNECT_DATA=(SERVER=DEDICATED)(SID=ORCL)));User Id=;Password=;"
  }
}
```
3. Instalar dependências:
```bash
dotnet restore
dotnet ef database update
```
4. Executar:
```bash
dotnet run
```

## Dependências
- Entity Framework
- Oracle
- Swagger
- AutoMapper
