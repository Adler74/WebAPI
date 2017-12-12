# WebAPI
Short Url
.NET implementação usando o serviço bit.ly

##Como configurar

Você precisa configurar o arquivo web.config.

```xml
<configuration>
  <connectionStrings>
    <add name="ShortUrl" connectionString="Server=<SQL-SERVER-INSTANCE>;Database=ShortUrl;User Id=<USERNAME>;Password=<PASSWORD>;" providerName="System.Data.SqlClient" />
  </connectionStrings>
</configuration>
```

Você precisa criar um Database e uma tabela no SQLServer que estão nos arquivos:
- DataBase.sql
- Tabela.sql

##Cadastrando uma nova URL
```http
Pode ser usado o Postman (WebApp do Chrome), ou Telerik Fiddler Web
POST http://localhost:(porta)/short/
Parametros:
Host: localhost:(porta)
Accept: application/json
Content-Type: application/json
Content-Length: 0

body( passar um json )
{"url":"http:\\yyy.com"}
```

##Acessando uma URL
```html
GET http://localhost:(porta)/(id)
```
##Exibição
index.html
