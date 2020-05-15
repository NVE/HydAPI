# Powershell example

Below are some examples for using Powershell to fetch data from the Hydrological API (HydAPI).

Powershell is installed on most Windows machines and is a powerfull scripting language. It natively parses json, which makes it a good fit for querying HydAPI.

## Setup

Note: You may need to change the execution policy for Powershell-scripts:

```powershell
PS> Set-ExecutionPolicy -RemoteSigned
```

This will allow scripts that are not signed to be run locally. For more information on execution policy, please see:

[Set-ExecutionPolicy documentation](https://docs.microsoft.com/en-us/powershell/module/microsoft.powershell.security/set-executionpolicy?view=powershell-6)

## Getting API key

If you haven't generated an API-key yet, you can do that on this page:

https://hydapi.nve.no/users

In the examples below, substitute the text INSERT_KEY_HERE with the API-key you obtained.

## Getting metadata (Parameters and Series)

The Get-Metadata.ps1 is a script for querying the HydAPI for metadata. Currently, you can query for parameters and series.

Get list of all parameters:

```powershell
PS> .\Get-Metadata.ps1 -ApiKey "INSERT_KEY_HERE" -Parameters
```

The command will get the whole respons and will show some meta-information about the request. The parameters will be stored in the data attribute. If you want to list out the parameters available in a table (ft is an alias for the Powershell-command Format-Table)

```powershell
PS> $result = .\Get-Metadata.ps1 -ApiKey "INSERT_KEY_HERE" -Parameters
PS> $result.data | ft
```

Get list of all series:

```powershell
PS> $result = .\Get-Metadata.ps1 -ApiKey "INSERT_KEY_HERE" -Series
PS> $result.data | ft
```

You you want to project only some of the attributes for each result:

```powershell
PS> $result = .\Get-Metadata.ps1 -ApiKey "INSERT_KEY_HERE" -Series
PS> $result.data | ft stationId, stationName, resolutionList
```

You can also search for specific station. This will return all series that has a station with _gryta_ in its name:

```powershell
PS> $result = .\Get-Metadata.ps1 -ApiKey "INSERT_KEY_HERE" -Series -StationName "gryta"
```

In addition, the HydAPI supports quering for stations, rating curves and percentiles, but this is not currently implemented in the Powershell-script.

## Getting observations

The Get-Observations.ps1 script is a script for query for observations.

This query will get all observations in 2018 for stage (parameter 1000) in daily values (resolution time 1440 minutes):

```powershell
PS> .\Get-Observations.ps1 -ApiKey "INSERT_KEY_HERE" -StationId 6.10.0 -Parameter 1000 -ResolutionTime 1440 -ReferenceTime "2018-01-01/2018-12-31"
PS> $result.data.observations | ft
```

You can use the Get-Metadata.ps1 to get other StationIds for other stations. The supported ResolutionTimes are now 1440 (daily measurements), 60 (hourly measurements) and 0 (instantaneous).

You can also easily export the data to CSV using the Export-Csv Powershell-command:

```powershell
PS> $result = .\Get-Observations.ps1 -ApiKey "INSERT_KEY_HERE" -StationId 6.10.0 -Parameter 1000 -ResolutionTime 1440 -ReferenceTime "2018-01-01/2018-12-31"
PS> $result.data.observations | Export-Csv -Path output.csv -Delimiter ";" -Encoding UTF8
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
