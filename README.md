# Currency Exchange Services Project

This repository contains two services designed to manage and retrieve currency exchange rates. These services interact with external data sources and a local database to provide up-to-date currency exchange information.

## Services

### CurrencyExchanges Service

#### Overview

The `CurrencyExchanges` service is tasked with fetching currency exchange rates from the XE website and storing these rates in a SQLite database. The service updates the database every minute to ensure the exchange rates are current.

#### Why SQLite?

SQLite is used due to its efficient handling of frequent read and write operations, which is crucial given the service's need to update exchange rates every minute.

### ReadCurrency Service

#### Overview

The `ReadCurrency` service reads the latest currency exchange rates from the database at 1.5-second intervals, ensuring that the most current rates are always available.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.


