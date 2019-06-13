param(    
    [Parameter(Mandatory=$true, HelpMessage="To get an API-Key, go too https://hydapi.nve.no/users")][string] $ApiKey,
    [Parameter(Mandatory=$false, ParameterSetName="Parameters")][switch] $Parameters = $false,
    [Parameter(Mandatory=$false, ParameterSetName="Series")][switch] $Series = $false,
    [Parameter(Mandatory=$false)] [string] $StationName
)


$baseUrl = "https://hydapi.nve.no/api/v0.9"

$parametersUrl = "$($baseUrl)/Parameters"
$seriesUrl = "$($baseUrl)/Series"

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


if ($Parameters) 
{
    CallMethod $parametersUrl $ApiKey
    exit
} 

if ($Series)
{
    if ($StationName)
    {
        $seriesUrl = "$($seriesUrl)?StationName=$($StationName)"
    }

    CallMethod $seriesUrl $ApiKey    
    exit
}