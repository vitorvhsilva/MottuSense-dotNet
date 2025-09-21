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
| `GET`  | `/patios/{id}`         | Lista todas motos de um pátio específico  | `id`: ID do pátio (Path), Query: pagina (número da página), tamanho (quantidade por página)            | 200 OK, 400 Bad Request             |
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

## Domínios da Aplicação

### 1. Moto
Representa uma motocicleta cadastrada no sistema.  

- **Propriedades:** placa, chassi, status, modelo, IoT, localização, pátio.  
- **Navegação:** associações com eventos (**EventosMoto**), localização (**LocalizacaoMoto**) e pátio (**Patio**).  
- **Motivo:** Central para o negócio, pois todas as operações giram em torno das motos.  

### 2. EventoMoto
Registra eventos específicos relacionados a uma moto (ex: entrada, saída, manutenção).  

- **Propriedades:** id do evento, id da moto, visualização, data/hora.  
- **Navegação:** referência à moto e ao tipo de evento.  
- **Motivo:** Permite rastrear o histórico e status das motos, essencial para controle e auditoria.  

### 3. Evento
Define os tipos de eventos possíveis (ex: entrada, saída, manutenção).  

- **Propriedades:** descrição, cor, id.  
- **Navegação:** lista de eventos de moto associados.  
- **Motivo:** Abstrai o conceito de evento, facilitando a reutilização e categorização.  

### 4. Patio
Representa o local físico onde as motos ficam armazenadas.  

- **Propriedades:** id, filial, estrutura criada.  
- **Navegação:** lista de motos associadas.  
- **Motivo:** Essencial para organizar e localizar motos fisicamente.  

### 5. EstruturaPatio
Modela detalhes estruturais do pátio (coordenadas, tamanho, rotação).  

- **Navegação:** referência ao pátio.  
- **Motivo:** Permite detalhar a disposição física dos pátios, útil para visualização e gestão espacial.  

### 6. LocalizacaoMoto
Guarda a localização geográfica da moto.  

- **Propriedades:** latitude, longitude.  
- **Navegação:** referência à moto.  
- **Motivo:** Suporte a funcionalidades de rastreamento e monitoramento.


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

## Testes
Os testes da aplicação podem ser feitos utilizando a Collection do Postman presente no repositório:

https://github.com/vitorvhsilva/MottuSense-dotNet/blob/main/mottusense-net.postman_collection.json

## Dependências
- Entity Framework
- Oracle
- Swagger
- AutoMapper
