param(    
    [Parameter(Mandatory = $true, HelpMessage = "To get an API-Key, go to https://hydapi.nve.no/users")][string] $ApiKey,
    [Parameter(Mandatory = $false, ParameterSetName = "Parameters")][switch] $Parameters = $false,
    [Parameter(Mandatory = $false, ParameterSetName = "Series")][switch] $Series = $false,
    [Parameter(Mandatory = $false, ParameterSetName = "Percentiles")][switch] $Percentiles = $false,
    [Parameter(Mandatory = $false, ParameterSetName = "Percentiles")][string] $StationId,
    [Parameter(Mandatory = $false, ParameterSetName = "Percentiles")][string] $Parameter,
    [Parameter(Mandatory = $false)] [string] $StationName
)


$baseUrl = "https://hydapi.nve.no/api/v1"

$parametersUrl = "$($baseUrl)/Parameters"
$seriesUrl = "$($baseUrl)/Series"
$percentilesUrl = "$($baseUrl)/Percentiles"

function CallMethod ([string] $uri, [string] $apiKey) {
    $request = @{
        Method  = "Get"
        Uri     = $uri
        Headers = @{
            Accept      = "application/json"
            "X-API-Key" = $apiKey
        }
    }

    return Invoke-RestMethod @request
}

if ($Percentiles) {
    if ($Parameter -and $StationId) {
        $url = "$($percentilesUrl)/$($StationId)/$($Parameter)"
        CallMethod $url $ApiKey
    }
    else {
        $url = $percentilesUrl
        CallMethod $url $ApiKey
    }
    
    exit    
}

if ($Parameters) {
    CallMethod $parametersUrl $ApiKey
    exit
} 

if ($Series) {
    if ($StationName) {
        $seriesUrl = "$($seriesUrl)?StationName=$($StationName)"
    }

    CallMethod $seriesUrl $ApiKey    
    exit
}