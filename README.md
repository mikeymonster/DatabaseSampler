# Database Samples #

A site with samples for multiple databases

The following needs to be in the configuration table:

```
  "SqlConnectionString": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Matching;Integrated Security=True;MultipleActiveResultSets=True;",
  "AzureSearchConfiguration":
  {
    "SearchServiceName": "Put your search service name here",
    "SearchServiceAdminApiKey": "Put your primary or secondary API key here",
    "SearchServiceQueryApiKey": "Put your query API key here"
  },
  "PostgreSQL": {
		"ConnectionString": "server=localhost;port=5432;userid=postgres;database=students;",
		"DbPassword": "YourPasswordHere"
  },
  "CosmosConnectionString": "TODO",

```

Make sure there is a PostgresSQL database matching the connection string (e.g. *students*)

Set up entities:
```
Add-Migration InitialStudentEntities -Context DatabaseSampler.Application.Data.StudentDbContext 
Update-Database -Context DatabaseSampler.Application.Data.StudentDbContext
```

To save postcode results in the database, create a table:
```

--DROP TABLE [dbo].[PostcodeLookup]
GO
--TODO: Consider Postcode as Primary Key    
CREATE TABLE [dbo].[PostcodeLookup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Postcode] [varchar](10) NOT NULL,
	[DistrictCode] [varchar](10) NOT NULL,
	[Latitude] [decimal](9, 6) NULL,
	[Longitude] [decimal](9, 6) NULL,
	[Location] [geography] NULL,
	[IsTerminated] [bit] NOT NULL,
	[TerminatedYear] [smallint] NULL,
	[TerminatedMonth] [smallint] NULL,
	[Created] [datetime2](7) NOT NULL DEFAULT (getutcdate())
 CONSTRAINT [PK_PostcodeLookup] PRIMARY KEY CLUSTERED ([Id] ASC),
 INDEX [IX_PostcodeLookup_Postcode] NONCLUSTERED ([Postcode])
)
GO
CREATE SPATIAL INDEX [SPATIAL_PostcodeLookup_Location] 
   ON [dbo].[PostcodeLookup](Location);
GO
```


## Articles

[Cosmos DB Emolator](https://developer.okta.com/blog/2019/07/11/aspnet-azure-cosmosdb-tutorial)


