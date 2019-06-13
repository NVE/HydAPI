# Powershell Example

Below are some examples for using Powershell to fetch data from the Hydrological API (HydAPI).

Powershell is installed on most Windows machines and is a powerfull scripting language. It natively parses json, which makes it a good fit for querying HydAPI.

## Setup
Note: You may need to change the execution policy for Powershell-scripts:

```powershell
PS> Set-ExecutionPolicy -RemoteSigned
```

This will allow scripts that are not signed locally to be run. For more information on execution policy, please see:

[Set-ExecutionPolicy documentation](https://docs.microsoft.com/en-us/powershell/module/microsoft.powershell.security/set-executionpolicy?view=powershell-6)


## Getting API key 
If you haven't generated an API-key yet, you can do that on this page:

https://hydapi.nve.no/users

In the examples below, substitute the text INSERT_KEY_HERE with the API-key you obtained. 

## Getting metadata (Parameters and Series)

The Get-Metadata.ps1 is a script for querying the HydAPI for metadata. Currently, you can query for parameters and series. Parameters are different 

Get list of all parameters:

```powershell
PS> .\Get-Metadata.ps1 -ApiKey "INSERT_KEY_HERE" -Parameters
```

The command will get the whole respons and will show some meta-informasjon, and the parameters will be stored in the data attribute. If you want to list out the parameters available in a table (ft is an alias for the Powershell-command Format-Table)

```powershell
PS> $result = .\Get-Metadata.ps1 -ApiKey "INSERT_KEY_HERE" -Parameters
PS> $result.data | ft
```



Get list of all series:

```powershell
PS> $result = .\Get-Metadata.ps1 -ApiKey "INSERT_KEY_HERE" -Series
PS>
```



Get series that contains the substring "gryta" in its name

```powershell
PS> .\Get-Metadata.ps1 -ApiKey "INSERT_KEY_HERE" -Series -StationName "gryta"
```

Format the 

In addition, the HydAPI supports quering for stations, rating curves and percentiles, but this is not currently implemented in the Powershell-script.

## Getting observations

The Get-Observations.ps1 script is a script for query for observations. It can be run

This query will get all observations in 2018 for parameter 1000 (waterleve/stage) in daily values (1440 minutes):

```powershell
PS> .\Get-Observations.ps1 -ApiKey "INSERT_KEY_HERE" -StationId 6.10.0 -Parameter 1440 -ResolutionTime 1440 -From 2018-01-01 -To 2018-12-31
```

You can use the Get-Metadata.ps1 

Getting observations and listing out all observations on table format:

```powershell
PS> $result = .\Get-Observations.ps1 -ApiKey "INSERT_KEY_HERE" -StationId 6.10.0 -Parameter 1440 -ResolutionTime 1440 -From 2018-01-01 -To 2018-12-31
PS> $result.data | ft

<trucated>
```
Getting observations and exporting data to csv format: 

```powershell
PS> $result = .\Get-Observations.ps1 -ApiKey "INSERT_KEY_HERE" -StationId 6.10.0 -Parameter 1440 -ResolutionTime 1440 -From 2018-01-01 -To 2018-12-31
PS> $result.data | Export-Csv -Path output.csv -Delimiter ";" -Encoding UTF8
```

UTF8 is used for handling norwegian characters, and ; is used as delimiter for easy import in Excel.

## Insert API key into scripts
If you get tired of writing the ApiKey for every call, you can modify the scripts and insert the api key directly into the script.

Edit Get-Metadata.ps1 and/or Get-Observations.ps1:

Substitute:
```powershell
[Parameter(Mandatory=$true, HelpMessage="To get an API-Key, go too https://hydapi.nve.no/users")][string] $ApiKey,
```

with:
```powershell
[Parameter(Mandatory=$false, HelpMessage="To get an API-Key, go too https://hydapi.nve.no/users")][string] $ApiKey="INSERT_KEY_HERE",
```
