[<BACK](README.md)

# System Overview
This document provides a high-level view of the various components and services in which the system is built upon.

## Storage 
The initial set of data is provided by San Francisco's food truck open dataset in a CSV file; however, the open dataset offers an API that can be utilized to retrieve data in realtime with some constraints on the number of requests.  For this reason a data store with geospatial functional is required.  The application is heavy on read operations with writes on a scheduled bases; therefore, ACID is not a concern. For this reason the system leverages Azure CosmosDB.  Additionally, the scalability and guaranteed speed will help API to be highly responsive and highly available.   

## API
The API is the core of the system providing developers a public api in which to build their own apps and powering the web application for end users.  The API needs to scale on-demand as load increases and serve requests with sub-second response times. Azure Functions are perfect for hosting the API and therefore will be utilized for the Good Eats Truck finder API.  

In addition to hosting the API in a Function App, API Management is utilized to provide a gateway to the API, provide developers with a portal for documentation and a subscription model.  

## Web application
The web application needs to be modern, fast, responsive, and ultimately feel like a native application. For this reason a SPA application makes the most sense. There are several Frameworks in which to choose; however, will go with ReactJS for its speed and large ecosystem.  In addition to react, we will leverage Redux for managing state.

## Data Loads
As mentioned in the [Storage](##storage) section of this document, the data provided by [DataSF](https://data.sfgov.org/Economy-and-Community/Mobile-Food-Facility-Permit/rqzj-sfat/data) will be stored in CosmosDB.  The dataset needs to be transformed to include the trucks location in GeoJSON format and this needs to be done on scheduled bases.  There are several services provided by Azure that have this capability; however, we will use a separate Azure Function App to manage the load process.




[<BACK](README.md)