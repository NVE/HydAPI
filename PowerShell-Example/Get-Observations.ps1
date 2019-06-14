param(
    [Parameter(Mandatory=$true, HelpMessage="To get an API-Key, go to https://hydapi.nve.no/users")][string] $ApiKey,
    [Parameter(Mandatory=$true)][string] $StationId,
    [Parameter(Mandatory=$true)][string] $Parameter,
    [Parameter(Mandatory=$true)][string] $ResolutionTime,
    [Parameter(Mandatory=$false)][string] $From,
    [Parameter(Mandatory=$false)][string] $To 
)

$baseUrl = "https://hydapi.nve.no/api/v0.9/Observations"

function CallMethod ([string] $uri, [string] $apiKey)
{
    $request = @{
        Method  = "Get"
        Uri     = $uri
        Headers = @{
            Accept        = "application/json"
	        "X-API-Key" = $apiKey
        }
    }

    return Invoke-RestMethod @request
}

[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12

$uri = "$($BaseUrl)?StationId=$($StationId)&Parameter=$($Parameter)&ResolutionTime=$($ResolutionTime)"

if ($From)
{
    $uri = "$($uri)&From=$($From)"
}

if ($To)
{
    $uri = "$($uri)&To=$($To)"
}

CallMethod $uri $ApiKey