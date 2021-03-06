# C# Example

This is an example of a C# program that is an example of how to use the Hydrological API (HydAPI). 

It is a .NET Core console application that can be run on several platforms (Windows, Linux and iOS).

## Setup

You need the Dotnet Core SDK installed in order to run the example.

Dotnet Core SDK: https://dotnet.microsoft.com/download/dotnet-core/2.2

## Get API-key

If you haven't generated an API-key yet, you can do that on this page:

https://hydapi.nve.no/users

In the examples below, substitute the text INSERT_KEY_HERE with the API-key you obtained. 

# Getting parameters from the API

This example will fetch available parameters from the API and list them. It can also be extended to fetching other types of data, using the same principles as described in the example.

To build the example, write:
```
dotnet build
```
To run the example, write
```
dotnet run <INSERT_KEY_HERE>
```
