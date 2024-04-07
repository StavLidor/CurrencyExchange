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

### Prerequisites

- [Visual Studio](https://visualstudio.microsoft.com/downloads/)

### Installation

Clone the repository to your local machine:


git clone [https://github.com/StavLidor/Currency.git ](https://github.com/StavLidor/CurrencyExchange.git)

## Using Chrome WebDriver

This project uses Selenium WebDriver with ChromeDriver to automate browser actions for fetching data from websites. Chrome WebDriver is essential for running tests that require browser interaction.

### Running the Services

Once you have the project set up in Visual Studio, you can run the services as follows:

1. **Open the Solution**

   First, open the solution file (`.sln`) in Visual Studio. This file contains both the `CurrencyExchanges` and `ReadCurrency` projects.

2. **Set the Startup Project**

   Decide which service you want to run:

   - For the `CurrencyExchanges` service, which fetches and updates currency exchange rates:
     - Navigate to the Solution Explorer in Visual Studio.
     - Right-click on the `CurrencyExchanges` project.
     - Choose `Set as Startup Project`.

   - For the `ReadCurrency` service, which reads currency exchange rates from the database:
     - Navigate to the Solution Explorer in Visual Studio.
     - Right-click on the `ReadCurrency` project.
     - Choose `Set as Startup Project`.

3. **Run the Project**

   After setting the desired startup project, start the service:

   - Press `F5` to run the project with debugging enabled, or use `Ctrl+F5` to run without debugging.
   - This will compile and execute the selected service, either updating currency rates in the database or reading them, based on your selection.




