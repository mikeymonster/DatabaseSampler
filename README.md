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

[Cosmos DB MVC](https://developer.okta.com/blog/2019/07/11/aspnet-azure-cosmosdb-tutorial)

[Cosmos DB .NET Core](https://jeremylindsayni.wordpress.com/2019/02/25/getting-started-with-azure-cosmos-db-and-net-core-part-1-installing-the-cosmos-emulator/)

[Official tutorial](https://docs.microsoft.com/en-us/azure/cosmos-db/sql-api-dotnet-application)

[Functions V3 Preview Setup](https://dev.to/azure/develop-azure-functions-using-net-core-3-0-gcm)

## Code Downloads

[Cosmos Book Code](https://github.com/PacktPublishing/Guide-to-NoSQL-with-Azure-Cosmos-DB)


## Azure ML

[First ML experiment with R - Azure Machine Learning](https://docs.microsoft.com/en-us/azure/machine-learning/service/tutorial-1st-r-experiment?WT.mc_id=Revolutions-blog-davidsmi)
[Install](https://azure.github.io/azureml-sdk-for-r/articles/installation.html)
remotes::install_github('https://github.com/Azure/azureml-sdk-for-r', INSTALL_opts=c("--no-multiarch"))

Deployment - see https://github.com/Microsoft/AKSDeploymentTutorial
 or updated https://github.com/microsoft/AKSDeploymentTutorialAML


## Azure Search 

look for data from query below,
index it to search by qualification title

--(localdb)\ProjectsV13
--use [Matching.Live]

select		* 
from		Provider p
inner join	providervenue pv
on			pv.ProviderId = p.Id
inner join	ProviderQualification pvq
on			pvq.ProviderVenueId = pv.Id
inner join	Qualification q
on			q.Id = pvq.QualificationId
inner join	QualificationRouteMapping qrm
on			qrm.QualificationId = q.Id
inner join	Route r
on			r.Id = qrm.RouteId


## Developer setup

### Requirements

* [Docker for X](https://docs.docker.com/install/#supported-platforms)
* https://docs.docker.com/docker-for-windows/install/


### Environment Setup

The default development environment uses docker containers to host the following dependencies.

* Redis

On first setup run the following command from _**/setup/containers/**_ to create the docker container images:

`docker-compose build`

To start the container run:

`docker-compose up -d`

To stop the container run:

`docker-compose down`

You can view the state of the running containers using:

`docker ps -a`

