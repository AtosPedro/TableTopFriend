# TableTopFriend

## Como rodar o projeto

baixe o docker desktop

entre no aqruivo appsettings.development.json que está no caminho TableTopFriend/TableTopFriend.Api/appsettings.Development.json
os seguintes parâmetros

{{IPV4}} = IPV4 DO SEU COMPUTADOR

```json
"CachingSettings": {
    "ConnectionString": "{{IPV4}}:6379"
  },

"DBConfiguration":{
    "ConnectionString":"Server={{IPV4}},1433;Database=tabletopfriend_db;User Id=sa;Password=\\@root123;Encrypt=false"
 }

```
entre na pasta raiz do projeto e execute este comando no terminal

```bash
docker-compose up -d
```


este comando irá subir a api, banco de dados MSSSQL e também o Redis
