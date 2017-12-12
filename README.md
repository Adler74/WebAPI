# WebAPI
Short Url
.NET implementação usando o serviço bit.ly

##Como configurar

Você precisa configurar o arquivo web.config.

<configuration>
  <connectionStrings>
    <add name="ShortUrl" connectionString="Server=<SQL-SERVER-INSTANCE>;Database=ShortUrl;User Id=<USERNAME>;Password=<PASSWORD>;" providerName="System.Data.SqlClient" />
  </connectionStrings>
</configuration>

Você precisa criar um Database e uma tabela que estão nos arquivos:
- DataBase.sql
- BancodeDados.sql

